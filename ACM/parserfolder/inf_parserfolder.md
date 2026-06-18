# Parser Library

## Purpose

`parserfolder/` builds `AutoCommitMessage.Parser`, the shared core used by both the browser app and MCP server. It has no UI dependency and is the canonical source for ACM parsing, export, and storage behaviour.

## Project Shape

- `AutoCommitMessage.Parser.csproj` targets `net8.0-windows`, references `LibGit2Sharp`, and exposes internals to the app, MCP server, and test assembly.
- `Processing/` contains production parser code.
- `tests/` contains xUnit coverage for model diff rules, formatting rules, Mendix installation detection, MPR format detection, changed-module detection, and HEAD dump caching.

## Core Responsibilities

- Inspect Git status for `.mpr` and `.mprops` files.
- Reconstruct the HEAD version of changed `.mpr` files from Git blobs.
- Run `mx.exe dump-mpr` for working and HEAD snapshots.
- Compare dump JSON into `MendixModelChange` records.
- Group changes by module and category.
- Persist raw change exports, dump artifacts, and commit-message files.

## Key Entry Points

- `AutoCommitMessageChangeService.ReadChanges(...)` is the main read path.
- `AutoCommitMessageExportService.ExportChanges(...)` writes raw export JSON.
- `AutoCommitMessageCommitMessageStoreService.StoreCommitMessage(...)` writes commit-message text files.
- `MendixModelDiffService.CompareDumps(...)` compares two dump JSON files directly.

## Maintenance Notes

- Add parser rules with tests first or in the same change. The formatter and diff service are deterministic and should stay deterministic.
- Prefer adding narrowly-scoped helper methods inside `MendixModelDiffService` or `MendixModelChangeDisplayTextFormatter` over broad rewrites.
- Keep public contract records in `Processing/Contracts/` stable because exports, UI rendering, and MCP responses all depend on them.
