---
name: acm-parser-usage
description: How to drive the AutoCommitMessage (ACM) parser end to end — where the Mendix version lives, where the .mpr working copy lives, and where the parsed results are written (mendix-data). Use whenever an AI needs to run the parser to read, export, or store Mendix model changes.
status: full
---

# ACM PARSER USAGE

## Purpose

This skill explains exactly how to run the **AutoCommitMessage (ACM) parser** and where each
input and output lives, so any AI can drive it without reading the C# source. The parser inspects
uncommitted Mendix model changes (`.mpr`), derives a semantic diff from `mx dump-mpr` output, and
writes structured results into the shared `mendix-data` folder.

There are two ways to drive the parser. Both wrap the **same** parser services:

- **MCP server (preferred for AI clients)** — `ACM/mcp-server` exposes the parser as MCP tools over
  local stdio. Claude Code / Copilot / Cursor launch it on demand (no port, no server to manage) and
  auto-discover the tools `resolve_app`, `list_apps`, `read_changes`, `export_changes`,
  `store_commit_message`. This is the low-threshold path; see `ACM/mcp-server/README.md`.
- **Local web app (browser + scripted HTTP)** — `ACM/applicatiefolder` (Kestrel on `localhost`). The
  human UI, and a fallback for scripted callers. Documented in full below.

Either way the three location concerns are identical: locating the Mendix version, pointing the
parser at the right `.mpr` working copy, and forcing every result into the `mendix-data` folder.

## Input

Three locations must be known before running. Two are usually resolved automatically; the third
(the project path) must always be supplied.

| Concern | What it is | How it is resolved |
|---|---|---|
| **Where the Mendix version lives** | `mx.exe` for the exact Studio Pro version the project needs | Auto-detected from the `.mpr` via `mx show-version`, then matched under the install root. Default root `C:\Program Files\Mendix\<version>\modeler\mx.exe`. Override with `MENDIX_INSTALL_ROOT` env var or the `override` query parameter on `/api/detection`. |
| **Where the `.mpr` lives** | The Mendix app's **git working copy** containing `<App>.mpr` | Supplied as the `projectPath` query parameter. The parser finds the first `*.mpr` at the top level of that folder and diffs the working tree against git `HEAD`. The folder **must** be a git repository. |
| **Where results are parsed to** | The shared `mendix-data` data root | Forced with `dataRootBasePath` (raw-changes + dumps) and `commitMessagesBasePath` (commit messages). Point both at this repo's `mendix-data` folder. |

Required values to collect before calling:

- `projectPath` — absolute path to the Mendix app working copy (the folder holding the `.mpr`).
- `dataRoot` — absolute path to `c:\Workspace\Mendix-AutoCommitMessage\mendix-data`.
- (for storing) `storyId` and `signature` — e.g. `SH-2086` and `SvR`.
- (optional) `mendixInstallRoot` — only if Studio Pro is not under `C:\Program Files\Mendix`.

## Output

Written under the `mendix-data` folder that `dataRootBasePath` / `commitMessagesBasePath` resolve to:

- `mendix-data/raw-changes/*.json` — the export payload (`schemaVersion: 1.0`): branch, user, and
  per-file `modelChangesByModule` with `displayText`. This is the canonical parser input for the
  commit-message skills. **Always produce this** so the raw changes are available for later reuse.
- `mendix-data/dumps/<timestamp>_<mpr>_<guid>/working-dump.json` + `head-dump.json` — full
  `mx dump-mpr` artifacts for deep inspection (see `mendix-model-dump-inspection`).
- `mendix-data/Commit messages/<storyId>_<signature>_<yyyyMMdd>.txt` — stored commit message,
  prefixed with a `#commit:<shortHash>` header line. (This is the folder the project calls the
  commit-message destination; note the literal name is `Commit messages`, with a space.)

## Steps

### Path A — MCP tools (preferred)

If the `autocommitmessage` MCP server is registered (see `ACM/mcp-server/README.md`), drive the
parser directly through its tools — no app to start:

1. `resolve_app("<app name or story id>")` → the app entry incl. `projectPath` (from the registry).
2. `read_changes(projectPath)` → confirm branch + that uncommitted `.mpr` changes exist.
3. `export_changes(projectPath)` → writes `raw-changes` + `dumps` into `mendix-data`; returns the
   flattened changes with `displayText`.
4. Author the message (see the commit-message skills), then
   `store_commit_message(projectPath, storyId, signature, message)`.

`dataRoot` defaults to the server's `ACM_DATA_ROOT`; pass it explicitly to override. Path B below is
the equivalent HTTP route for the browser app or scripted callers.

### Path B — local web app (HTTP)

### 1. Start the local app (once per session)

From the repo root:

```powershell
./open-browser-app.ps1            # builds ACM/applicatiefolder, serves http://localhost:3109
```

To run headless (no browser tab), build and run the app directly:

```powershell
dotnet run --project ACM/applicatiefolder -c Release -- --no-browser
```

Base URL is `http://localhost:3109/`. If 3109 is taken the app prints the fallback port it bound.
All actions are served from the root path as query parameters: `http://localhost:3109/?action=<action>&...`.

### 2. (Optional) Confirm the Mendix version

```
GET /api/detection?projectPath=<projectPath>[&override=<installRoot>]
```

Returns `detectedVersion`, `mxExePath`, `installRoot`. Use this when a project needs a Studio Pro
version that may not be installed, or when the install root is non-default. Detection runs
automatically inside `refresh`/`export` too, so this step is only for verifying up front.

### 3. Read the changes (dry run)

```
GET /?action=refresh&projectPath=<projectPath>
```

Returns the live payload (`isGitRepo`, `branchName`, `changes[]` with `modelChangesByModule`)
**without writing anything**. Use it to confirm the right app/branch is checked out and that there
are uncommitted `.mpr` changes before exporting.

For MPR v2 projects you can scope to changed modules first:
`GET /?action=list-change-modules&projectPath=<projectPath>` then pass
`&moduleFilter=ModuleA,ModuleB` on refresh/export.

### 4. Export the raw changes + dumps (into mendix-data)

```
GET /?action=export
      &projectPath=<projectPath>
      &dataRootBasePath=<dataRoot>
      &persistRawChanges=true
      &persistDumps=true
```

`dataRootBasePath` **must** be the `mendix-data` folder — when its folder name is `mendix-data`
it is used as-is, so `raw-changes/` and `dumps/` land directly inside it. **Without
`dataRootBasePath` the output goes into the Mendix app's own folder, not this repo.** Always set it.

Response includes `outputPath` (the raw-changes file), `exportFolder`, `dumpsFolder`, and
`changeCount`. Export fails with 400 if the project is not a git repo or has no uncommitted changes.

### 5. Store the commit message (into mendix-data/Commit messages)

```
POST /?action=store-commit-message
       &projectPath=<projectPath>
       &commitMessagesBasePath=<dataRoot>
       &storyId=<storyId>
       &signature=<signature>
Body: {"message":"<full commit message text>"}
```

`commitMessagesBasePath` **must** be the `mendix-data` folder — the service appends
`Commit messages` to it, giving `mendix-data/Commit messages/<storyId>_<signature>_<date>.txt`.
The short commit hash is read from the project's git `HEAD` automatically and written as the
`#commit:` header. Existing files for the same story/date/hash are overwritten; a different hash
gets a `_2`, `_3`, … suffix.

### 6. (Optional) List / read stored messages

```
GET /?action=list-commit-messages&projectPath=<projectPath>&commitMessagesBasePath=<dataRoot>
GET /?action=read-commit-message&filePath=<file>&commitMessagesBasePath=<dataRoot>
```

## Notes

- **Always pass the base-path parameters.** `dataRootBasePath` (export) and
  `commitMessagesBasePath` (store) are what keep every artifact inside this repo's `mendix-data`.
  Omitting them silently scatters output into each Mendix app's working folder.
- **The project path must be a real git working copy of the Mendix app**, not this repo. The parser
  diffs the working tree against `HEAD`, so an uncommitted `.mpr` change must exist for export to
  produce anything.
- **Version mismatch** is the most common failure: if the exact Studio Pro version is not installed,
  detection falls back to a `major.minor` match and warns; if nothing matches, `mx dump-mpr` fails
  and the change shows `Model analysis unavailable`. Verify with step 2 and install the version or
  set `MENDIX_INSTALL_ROOT` if needed.
- HEAD dumps are cached under `mendix-data/dumps/head-cache` (keyed by commit SHA) to avoid
  redundant `mx.exe` runs; this is automatic and safe to leave on (`headDumpCacheEnabled=true`).
- Downstream of this skill: feed `raw-changes/*.json` into `mendix-commit-structuring` and
  `mendix-technical-commit-message` to produce the commit-message body, then store it via step 5.
- The agent that orchestrates all of this — gathering the app/branch, resolving paths from the
  registry, and calling these steps in order — is `ACM/AI-tools/ACM-Writer.md`.
