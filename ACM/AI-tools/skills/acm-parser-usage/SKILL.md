---
name: acm-parser-usage
description: How to drive the AutoCommitMessage (ACM) parser end to end: where the Mendix version lives, where the .mpr working copy lives, and how to optionally persist generated files under MENDIX_DATA_ROOT / COMMIT_MESSAGE_ROOT. Use whenever an AI needs to run the parser to read, export, or store Mendix model changes.
status: full
---

# ACM PARSER USAGE

## Purpose

This skill explains exactly how to run the **AutoCommitMessage (ACM) parser** and where each input
and optional output lives, so any AI can drive it without reading the C# source. The parser inspects
uncommitted Mendix model changes (`.mpr`), derives a semantic diff from `mx dump-mpr` output, and can
optionally write generated parser artifacts to the configured data root.

There are two ways to drive the parser. Both wrap the **same** parser services:

- **MCP server (preferred for AI clients)** - `ACM/mcp-server` exposes the parser as MCP tools over
  local stdio. Claude Code / Copilot / Cursor launch it on demand (no port, no server to manage) and
  auto-discover the tools `resolve_app`, `list_apps`, `read_changes`, `export_changes`,
  `store_commit_message`. This is the low-threshold path; see `ACM/mcp-server/README.md`.
- **Local web app (browser + scripted HTTP)** - `ACM/applicatiefolder` (Kestrel on `localhost`). The
  human UI, and a fallback for scripted callers. Documented in full below.

Either way the location concerns are identical: locating the Mendix version, pointing the parser at
the right `.mpr` working copy, and only writing generated files when persistence is explicitly
requested.

## Input

Three locations must be known before running. Two are usually resolved automatically; the third
(the project path) must always be supplied.

| Concern | What it is | How it is resolved |
|---|---|---|
| **Where the Mendix version lives** | `mx.exe` for the exact Studio Pro version the project needs | Auto-detected from the `.mpr` via `mx show-version`, then matched under the install root. Default root `C:\Program Files\Mendix\<version>\modeler\mx.exe`. Override with `MENDIX_INSTALL_ROOT` env var or the `override` query parameter on `/api/detection`. |
| **Where the `.mpr` lives** | The Mendix app's **git working copy** containing `<App>.mpr` | Supplied as the `projectPath` query parameter. The parser finds the first `*.mpr` at the top level of that folder and diffs the working tree against git `HEAD`. The folder **must** be a git repository. |
| **Where parser artifacts are written** | Optional data root for `raw-changes` and `dumps` | Only used when export persistence is requested. Prefer an explicit `dataRoot`; otherwise read `MENDIX_DATA_ROOT` from `.env`; fallback to `c:\Workspace\Mendix-AutoCommitMessage\mendix-data`. |
| **Where commit messages are written** | Optional commit-message root | Only used when commit-message storage is requested. Prefer an explicit `commitMessageRoot`; otherwise read `COMMIT_MESSAGE_ROOT` from `.env`; fallback to `mendix-data\Commit messages` if no env value exists. |

Required values to collect before calling:

- `projectPath` - absolute path to the Mendix app working copy (the folder holding the `.mpr`).
- `dataRoot` - optional absolute path for persisted parser artifacts. If omitted and persistence is
  requested, use `MENDIX_DATA_ROOT` from `.env`.
- (for storing commit messages) `storyId`, `signature`, and optional `commitMessageRoot`. If
  `commitMessageRoot` is omitted, use `COMMIT_MESSAGE_ROOT` from `.env`.
- (optional) `mendixInstallRoot` - only if Studio Pro is not under `C:\Program Files\Mendix`.

## Output

Default behavior is read-only: `read_changes` / refresh returns the live change payload without
writing generated files. Files are written only when the caller explicitly requests persistence.

When parser artifact persistence is requested, write under `dataRoot` / `MENDIX_DATA_ROOT`:

- `raw-changes/*.json` - the export payload (`schemaVersion: 1.0`): branch, user, and per-file
  `modelChangesByModule` with `displayText`. This is the canonical parser input for the
  commit-message skills.
- `dumps/<timestamp>_<mpr>_<guid>/working-dump.json` + `head-dump.json` - full `mx dump-mpr`
  artifacts for deep inspection (see `mendix-model-dump-inspection`).

When commit-message storage is requested, write under `commitMessageRoot` / `COMMIT_MESSAGE_ROOT`:

- `<commitMessageRoot>/<storyId>_<signature>_<yyyyMMdd>.txt` - stored commit message, prefixed with
  a `#commit:<shortHash>` header line.

## Steps

### Path A - MCP tools (preferred)

If the `autocommitmessage` MCP server is registered (see `ACM/mcp-server/README.md`), drive the
parser directly through its tools - no app to start:

1. `resolve_app("<app name or story id>")` -> the app entry incl. `projectPath` (from the registry).
2. `read_changes(projectPath)` -> confirm branch + that uncommitted `.mpr` changes exist. This is
   the default read path and should not write generated files.
3. To persist generated parser files, call `export_changes(projectPath, dataRoot)` where `dataRoot`
   is explicit or resolved from `MENDIX_DATA_ROOT`. If persistence is not requested, do not call
   export just to write files; use the `read_changes` payload.
4. Author the message (see the commit-message skills), then call
   `store_commit_message(projectPath, storyId, signature, message, commitMessageRoot)` only when
   commit-message file storage is requested. Resolve `commitMessageRoot` from `COMMIT_MESSAGE_ROOT`
   if the caller did not provide one.

For MCP tools, pass roots explicitly when the tool supports it. If omitted, the server may use its
own environment defaults; callers that need deterministic storage should resolve `.env` themselves
and pass the path. Path B below is the equivalent HTTP route for the browser app or scripted callers.

### Path B - local web app (HTTP)

Only use the local web app when the caller has explicitly allowed localhost fallback.

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

### 4. Export the raw changes + dumps (optional)

```
GET /?action=export
      &projectPath=<projectPath>
      &dataRootBasePath=<dataRoot or MENDIX_DATA_ROOT>
      &persistRawChanges=true
      &persistDumps=true
```

Only call export when generated parser files should be stored. `dataRootBasePath` must be explicit:
use the caller-provided data root, otherwise use `MENDIX_DATA_ROOT` from `.env`. Do not omit
`dataRootBasePath`; without it the output may go into the Mendix app working folder.

Response includes `outputPath` (the raw-changes file), `exportFolder`, `dumpsFolder`, and
`changeCount`. Export fails with 400 if the project is not a git repo or has no uncommitted changes.

### 5. Store the commit message (optional)

```
POST /?action=store-commit-message
       &projectPath=<projectPath>
       &commitMessagesBasePath=<commitMessageRoot or COMMIT_MESSAGE_ROOT>
       &storyId=<storyId>
       &signature=<signature>
Body: {"message":"<full commit message text>"}
```

Only call store when the generated commit message should be written to disk. `commitMessagesBasePath`
must be explicit: use the caller-provided commit-message root, otherwise use `COMMIT_MESSAGE_ROOT`
from `.env`. If no env value exists, fall back to `mendix-data\Commit messages`.
The short commit hash is read from the project's git `HEAD` automatically and written as the
`#commit:` header. Existing files for the same story/date/hash are overwritten; a different hash
gets a `_2`, `_3`, ... suffix.

### 6. (Optional) List / read stored messages

```
GET /?action=list-commit-messages&projectPath=<projectPath>&commitMessagesBasePath=<commitMessageRoot>
GET /?action=read-commit-message&filePath=<file>&commitMessagesBasePath=<commitMessageRoot>
```

## Notes

- **Default to no storage.** Reading changes should not write generated parser files. Persist
  `raw-changes`, `dumps`, or commit-message files only when the caller explicitly requests it.
- **Always pass base-path parameters when storing.** `dataRootBasePath` (export) and
  `commitMessagesBasePath` (store) keep generated files in the intended folders. Resolve them from
  explicit input first, then `.env` (`MENDIX_DATA_ROOT` / `COMMIT_MESSAGE_ROOT`), then documented
  fallback paths.
- **The project path must be a real git working copy of the Mendix app**, not this repo. The parser
  diffs the working tree against `HEAD`, so an uncommitted `.mpr` change must exist for export to
  produce anything.
- **Version mismatch** is the most common failure: if the exact Studio Pro version is not installed,
  detection falls back to a `major.minor` match and warns; if nothing matches, `mx dump-mpr` fails
  and the change shows `Model analysis unavailable`. Verify with step 2 and install the version or
  set `MENDIX_INSTALL_ROOT` if needed.
- HEAD dumps are cached under `<dataRoot>\dumps\head-cache` (keyed by commit SHA) to avoid redundant
  `mx.exe` runs; this is automatic and safe to leave on (`headDumpCacheEnabled=true`) when export
  persistence is requested.
- Downstream of this skill: use `modelChangesByModule` / `displayText` from `read_changes` or an
  explicitly persisted export to produce the commit-message body, then store it only when requested.
- The agent that orchestrates all of this - gathering the app/branch, resolving paths from the
  registry, and calling these steps in order - is `ACM/AI-tools/ACM-Writer.md`.
