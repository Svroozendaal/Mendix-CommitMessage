# Core Constants and Paths

## Purpose

`Core/` contains shared constants and data-root path resolution used by parser services, the web app, and MCP server.

## Files

- `ExtensionConstants.cs` defines query-string keys, action names, route identifiers, and UI constants used by the shared web request handler.
- `ExtensionDataPaths.cs` resolves the ACM data root and the standard subfolders for raw changes, processed files, errors, dumps, and commit messages.

## Data Root Resolution

`ExtensionDataPaths.ResolveDataRoot` uses this precedence:

1. Explicit `dataRootBasePath` argument.
2. Build-configured data root from assembly metadata.
3. Fallback to `<projectPath>/mendix-data`.

The app also bridges `.env` value `MENDIX_DATA_ROOT` to `MENDIX_GIT_DATA_ROOT` before parser services run. The MCP server separately accepts `ACM_DATA_ROOT` and `MENDIX_GIT_DATA_ROOT`.

## Maintenance Notes

- Query action names in `ExtensionConstants` must stay aligned with `AutoCommitMessageWebServerExtension` and UI JavaScript in `AutoCommitMessagePanelHtml`.
- Be careful with path inputs: some APIs accept a data-root path, while commit-message APIs accept a base path that is combined with `Commit messages`.
