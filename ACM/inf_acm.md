# ACM Knowledgebase

## Purpose

`ACM/` contains the AutoCommitMessage product. It reads uncommitted Mendix model changes from a local Git working copy, turns `mx dump-mpr` output into semantic model-change rows, and exposes those rows to humans and AI tooling.

## Main Parts

- `parserfolder/` is the shared parser library. It owns Git status reading, `mx.exe` discovery, `.mpr` dump generation, dump comparison, module grouping, export writing, commit-message storage, and tests.
- `applicatiefolder/` is the local browser app. It starts a Kestrel server on localhost, renders the HTML UI, and routes UI actions to parser services.
- `mcp-server/` is the local stdio MCP adapter for AI clients. It exposes the same parser behaviour as MCP tools.
- `AI-tools/` contains ACM-specific AI instructions and Mendix domain skills used to interpret parser output and write technical commit messages.

## Runtime Flow

1. A user or AI client supplies a Mendix project path.
2. `AutoCommitMessageChangeService.ReadChanges` inspects `.mpr` and `.mprops` Git changes with LibGit2Sharp.
3. For changed `.mpr` files, the parser runs `mx.exe dump-mpr` for the working copy and HEAD snapshot.
4. `MendixModelDiffService.CompareDumps` produces deterministic `MendixModelChange` rows.
5. `MendixModelChangeStructurer.GroupByModule` groups rows by module and category.
6. `MendixModelChangeDisplayTextFormatter` converts rows into short commit-message-ready `displayText` lines.
7. The app or MCP server can export raw changes, persist dump artifacts, or store commit-message text in `mendix-data`.

## Important Data Locations

- `mendix-data/raw-changes/` stores exported JSON payloads.
- `mendix-data/dumps/` stores optional dump artifacts and HEAD dump cache files.
- `mendix-data/Commit messages/` stores generated commit-message text files.
- `mendix-data/apps-registry.json` maps story prefixes and app names to local Mendix working-copy paths for MCP clients.

## Maintenance Notes

- Build the app with `dotnet build ACM/applicatiefolder/AutoCommitMessage.App.csproj`.
- Run parser tests with `dotnet test ACM/parserfolder/tests`.
- Do not edit generated `bin/`, `obj/`, or debug output folders.
- Keep parser behaviour shared: web and MCP adapters should call parser services instead of duplicating parsing logic.
