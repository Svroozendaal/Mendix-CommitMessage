# AutoCommitMessage MCP server

A local **stdio** MCP server that exposes the AutoCommitMessage parser as tools, so any MCP-capable
AI client (Claude Code, GitHub Copilot in VS Code, Cursor, …) can read, export, and store Mendix
model changes without the browser web app.

It is a thin adapter over the same `AutoCommitMessage.Parser` library the web app uses — the parser
is compiled **into** this one binary; there is no separate service to run.

## Why it runs locally (and needs no hosting)

The parser needs local access to things that only exist on your machine:

- the `.mpr` git working copy on disk,
- the local git tree,
- `mx.exe` from your local Studio Pro installation.

A cloud-hosted server could not see any of those. With **stdio transport** the AI client launches
this server as a child process on demand and talks to it over pipes — **no network port, nothing to
expose, your machine never needs to be "reachable".** Distribution = ship/build the binary on each
developer machine and add one entry to that machine's MCP config.

## Tools

| Tool | Purpose |
|---|---|
| `resolve_app` | Resolve an app from `mendix-data/apps-registry.json` by name or story id → returns its `projectPath`. |
| `list_apps` | List all known apps/branches and their on-disk paths. |
| `read_changes` | Read uncommitted `.mpr` changes (dry run) — branch + per-module changes with `displayText`. |
| `export_changes` | Write raw-changes JSON + `mx dump-mpr` artifacts into `mendix-data`. |
| `store_commit_message` | Store a commit message under `mendix-data/Commit messages`. |

## Build

```powershell
dotnet build ACM/mcp-server/AutoCommitMessage.Mcp.csproj -c Release
# → ACM/mcp-server/bin/Release/net8.0-windows/AutoCommitMessage.Mcp.exe
```

For a machine without the .NET runtime, publish a self-contained single file:

```powershell
dotnet publish ACM/mcp-server/AutoCommitMessage.Mcp.csproj -c Release -r win-x64 `
  --self-contained -p:PublishSingleFile=true -o dist/acm-mcp
```

## Register in an MCP client

Set `ACM_DATA_ROOT` to the `mendix-data` folder so all output (raw-changes, dumps, commit messages)
and the apps registry resolve there. Per-call `dataRoot` arguments override it.

### Claude Code — `.mcp.json` (project) or `claude mcp add`

```json
{
  "mcpServers": {
    "autocommitmessage": {
      "command": "C:\\Workspace\\Mendix-AutoCommitMessage\\ACM\\mcp-server\\bin\\Release\\net8.0-windows\\AutoCommitMessage.Mcp.exe",
      "env": { "ACM_DATA_ROOT": "C:\\Workspace\\Mendix-AutoCommitMessage\\mendix-data" }
    }
  }
}
```

### VS Code (GitHub Copilot) — `.vscode/mcp.json`

```json
{
  "servers": {
    "autocommitmessage": {
      "type": "stdio",
      "command": "C:\\Workspace\\Mendix-AutoCommitMessage\\ACM\\mcp-server\\bin\\Release\\net8.0-windows\\AutoCommitMessage.Mcp.exe",
      "env": { "ACM_DATA_ROOT": "C:\\Workspace\\Mendix-AutoCommitMessage\\mendix-data" }
    }
  }
}
```

After registering, the client auto-discovers the five tools. Typical flow: `resolve_app("SH-2086")`
→ `read_changes(projectPath)` → `export_changes(projectPath)` → author message →
`store_commit_message(...)`.

## Relationship to the other surfaces

- **MCP server (this)** — primary AI-facing surface for Claude Code / Copilot / Cursor.
- **Web app** (`ACM/applicatiefolder`) — browser UI for humans.
- **Parser library** (`ACM/parserfolder`) — the shared core both wrap.
