# MCP Server

## Purpose

`mcp-server/` builds `AutoCommitMessage.Mcp`, a local stdio MCP server for AI clients. It exposes ACM parser operations without requiring the browser app.

## Runtime Model

The server is launched by an MCP client as a child process and communicates over stdio. It does not listen on a network port. This is necessary because parsing depends on local Git working copies, local `mendix-data`, and local Mendix Studio Pro `mx.exe`.

## Tools

- `read_changes(projectPath)` reads uncommitted `.mpr` model changes and returns flattened rows with `displayText`.
- `export_changes(projectPath, dataRoot?)` persists raw-changes JSON and dump artifacts, then returns the output path and flattened rows.
- `store_commit_message(projectPath, storyId, signature, message, dataRoot?)` stores a commit-message text file with the current Git short hash.
- `list_apps(dataRoot?)` reads `mendix-data/apps-registry.json`.
- `resolve_app(appOrStoryId, dataRoot?)` resolves an app or story prefix to a registered branch/project path and Mendix version metadata.

## Data Root Resolution

Per-call `dataRoot` wins. Otherwise the server checks `ACM_DATA_ROOT`, then `MENDIX_GIT_DATA_ROOT`, then parser defaults.

## Maintenance Notes

- MCP tools should remain thin adapters over parser services.
- Keep tool descriptions accurate because MCP clients use them for tool selection.
- If parser contracts change, update MCP flattening in `AcmTools.Flatten`.
