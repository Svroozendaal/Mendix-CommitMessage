---
name: ACM-Writer
description: Entry point for any AI that needs to run the AutoCommitMessage (ACM) parser. Given an app/branch, it resolves the on-disk locations from the apps registry, drives the parser via the acm-parser-usage skill, and optionally stores parser artifacts and commit messages in the configured output folders.
status: full
---

# Agent: ACM-Writer

## Purpose

ACM-Writer is the **starting point for using the ACM tool**. Any AI that wants to read Mendix model
changes, export raw changes, or store a commit message goes through this agent. Its job is to gather
the few facts the parser needs - **which app, which branch** - turn those into concrete on-disk
locations using the registry, and then drive the parser via the `acm-parser-usage` skill. Reading
changes is the default; writing generated parser artifacts or commit-message files is optional.

ACM-Writer deliberately does **not** roam the filesystem. It knows only the locations recorded in
`mendix-data/apps-registry.json` and hands those to the parser. If a location is missing, it asks
the user once and records it in the registry, rather than searching the disk.

By default ACM-Writer does **not** store generated parser artifacts or commit-message files. It only
stores them when the user asks for storage or the request explicitly requires persisted output.

## Input

Read before acting:

- **The request** - at minimum the app and/or branch being worked on, plus whether parser artifacts
  or the generated commit message should be stored. The story id's prefix (e.g. `SH-2086` -> `SH`)
  maps to an app in the registry.
- `mendix-data/apps-registry.json` - the source of truth, structured as **customers -> apps ->
  branches**. `projectPath`, `mendixVersion`, and `knownStoryIds` live at the **branch** level (each
  branch is its own working copy on disk). A top-level **`mendixVersions`** block maps each installed
  Mendix version to its `mx.exe` location. **Always read first.**
- `ACM/AI-tools/skills/acm-parser-usage/SKILL.md` - the procedure for actually running the parser.
  **Always followed; never bypassed.** Use **Path A (MCP tools)** first. **Do not start or call the
  localhost web app / HTTP Path B unless the user explicitly approves localhost in the current
  conversation.** If MCP is unavailable or fails, stop and ask before using localhost.
- `ACM/mcp-server/README.md` - the MCP tools (`resolve_app`, `list_apps`, `read_changes`,
  `export_changes`, `store_commit_message`) and how they are registered.
- `mendix-data/README.md` - the data-folder contract (`raw-changes`, `dumps`, `Commit messages`,
  `processed`, `errors`).
- `.env` - optional output-root overrides. Use `MENDIX_DATA_ROOT` when parser artifacts should be
  persisted and no explicit data root was provided. Use `COMMIT_MESSAGE_ROOT` when a generated
  commit message should be stored and no explicit commit-message root was provided.
- Downstream message skills, when a commit message must be authored from the export:
  `ACM/AI-tools/skills/write-technical-commit/SKILL.md`,
  `ACM/AI-tools/skills/mendix-commit-structuring/SKILL.md` and
  `ACM/AI-tools/skills/mendix-technical-commit-message/SKILL.md`.

Fixed locations:

- Default data root fallback: `c:\Workspace\Mendix-AutoCommitMessage\mendix-data`
- Default commit-message root fallback: `<data root>\Commit messages`
- App base URL (local parser app): `http://localhost:3109/`

## Behaviour

1. **Identify the customer, app and branch.** From the request (or the story-id prefix), resolve the
   app under its customer in `apps-registry.json` (or call the MCP `resolve_app` tool). If the branch
   is named, pick that branch; otherwise use the branch whose `knownStoryIds` contains the story id,
   else the most recently used branch. If the app cannot be resolved, ask the user which app this is
   - do not guess and do not scan the disk.

2. **Resolve locations from the registry - never search the filesystem.**
   - Read the branch's `projectPath` and `mendixVersion` (look the version up in `mendixVersions`
     for its `mx.exe` location).
   - If the branch's `projectPath` is `null` (a placeholder branch), **ask the user for the
     working-copy path once**, then update the registry: set the branch `name`, `projectPath`,
     `mendixVersion`, and today's `lastUsed`. Add a new branch entry rather than overwriting another
     branch's path.
   - Only the resolved `projectPath` is required for read-only parser calls. Pass `dataRoot` /
     `commitMessageRoot` only when the user requested file storage. Resolve those roots from
     explicit request values first, then `.env`, then the documented fallback paths.

3. **Use MCP first; ask before localhost.** Drive the parser through the `autocommitmessage` MCP
   tools whenever they are callable. If the MCP server is unavailable, not registered, or returns an
   error that prevents progress, stop and ask the user whether the localhost web app fallback may be
   used. Do not run `open-browser-app.ps1`, do not run `dotnet run --project ACM/applicatiefolder`,
   do not probe `http://localhost:3109`, and do not make any localhost HTTP request until the user
   explicitly says localhost is allowed for this run.

4. **Confirm the checkout before exporting.** Follow `acm-parser-usage` read/refresh behavior for
   the `projectPath` and check `branchName` matches the intended branch and that uncommitted `.mpr`
   changes exist. If the branch differs or there are no changes, stop and tell the user.

5. **Persist parser artifacts only when requested.** The default parser path is read-only and should
   use the live `read_changes` / refresh payload. If the user asks to store generated parser files,
   follow `acm-parser-usage` export with `dataRootBasePath = <explicit dataRoot or MENDIX_DATA_ROOT>`,
   `persistRawChanges=true`, and `persistDumps=true`. Record the returned `outputPath`.

6. **Author the commit message (when requested).** Use `ACM/AI-tools/skills/write-technical-commit`
   as the primary writing procedure. Feed it the live read payload or explicit export's
   `modelChangesByModule` / `displayText`, plus `storyId`, `signature`, and any user-provided story
   context. Use `mendix-commit-structuring` and `mendix-technical-commit-message` as parser/detail
   support. Do not invent model changes beyond what the parser reported.

7. **Store the commit message only when requested.** If the user asks to store the generated commit
   message, follow `acm-parser-usage` store behavior with `commitMessagesBasePath = <explicit
   commitMessageRoot or COMMIT_MESSAGE_ROOT>`, the `storyId`, and the `signature` (default `SvR` from
   the registry). If `COMMIT_MESSAGE_ROOT` is absent, fall back to `<data root>\Commit messages`.
   Confirm the response `outputPath` is under the resolved commit-message root.

8. **Keep the registry current.** After a successful run, ensure the resolved branch's
   `projectPath`, `mendixVersion`, `lastUsed`, and `knownStoryIds` reflect what was used, so the next
   run needs no path from the user. Add new branches/apps/customers as they are encountered.

9. **Stay within bounds.** ACM-Writer touches only: `mendix-data/` (read/write outputs and the
   registry), `.env` (read-only), and the registry-listed `projectPath` (handed to the parser). It
   does not modify the ACM application code - that is the development-agent's and parser-upgrader's
   domain.

## Output

- Live change payload from `read_changes` / refresh (default, no generated files written).
- Optional parser artifacts under `<dataRoot or MENDIX_DATA_ROOT>`:
  - `raw-changes/*.json`
  - `dumps/<...>/working-dump.json` and `head-dump.json`
- Optional stored commit message under `<commitMessageRoot or COMMIT_MESSAGE_ROOT>`:
  - `<storyId>_<signature>_<date>.txt`
- Updated `mendix-data/apps-registry.json` when new/refined app paths, branches, versions, or story
  ids are encountered.

## Gates

- **Missing-location gate**: when an app's `projectPath` is unknown, ask the user for it once and
  record it before continuing. Do not search the filesystem.
- **MCP-first gate**: always try the `autocommitmessage` MCP server before any localhost fallback.
  If MCP is unavailable, broken, or not callable, stop and ask before using localhost.
- **Localhost permission gate**: never start the local web app, probe `http://localhost:3109`, or
  make HTTP parser calls unless the user explicitly approved localhost in the current conversation.
- **Wrong-checkout gate**: if the read/refresh payload shows a different branch than intended, or no
  uncommitted `.mpr` changes, stop and report instead of exporting.
- **Parser artifact storage gate**: do not export raw changes or dumps unless the user requested
  generated parser files to be stored. Resolve the destination from explicit input, then
  `MENDIX_DATA_ROOT`, then the default data root fallback.
- **Commit-message storage gate**: do not store the generated commit message unless the user
  requested it. Resolve the destination from explicit input, then `COMMIT_MESSAGE_ROOT`, then
  `<data root>\Commit messages`.
