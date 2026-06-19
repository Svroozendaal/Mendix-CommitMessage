# ACM Agents

AutoCommitMessage (ACM) is a local Mendix model-change reader. It compares uncommitted `.mpr` changes against Git `HEAD`, uses the local Mendix Studio Pro `mx.exe` dump tooling, and exposes the result through a local MCP server for AI clients.

ACM is meant to run on the developer machine that has the Mendix app working copy, Git repository, and matching Studio Pro version installed.

## Routing

Use `AI-tools/ACM-Init.md` when:

- installing ACM from a copied or zipped `ACM` folder,
- building or registering the MCP server on another computer,
- creating the local `mendix-data` folders and starter registry,
- explaining how to configure Claude Code, VS Code Copilot, Cursor, or another MCP client.

Use `AI-tools/ACM-Writer.md` when:

- writing a commit message for a Mendix branch,
- reading uncommitted Mendix model changes,
- exporting raw parser artifacts,
- storing generated commit-message files.

Use `AI-tools/skills/acm-parser-usage/SKILL.md` as the parser operation reference. It documents the MCP tools, the localhost fallback, data-root behavior, and storage rules.

Use the skills under `AI-tools/skills/` for commit-message wording and Mendix-specific interpretation:

- `write-technical-commit`: primary commit-message format.
- `mendix-technical-commit-message`: technical row formatting from parser output.
- `mendix-commit-structuring`: structured commit data conventions.
- `mendix-model-dump-inspection`: deeper dump inspection when parser output needs investigation.

## MCP Server

The MCP server lives in:

```text
mcp-server
```

Build it with:

```powershell
dotnet build .\mcp-server\AutoCommitMessage.Mcp.csproj -c Release
```

The expected executable is:

```text
mcp-server\bin\Release\net8.0-windows\AutoCommitMessage.Mcp.exe
```

Register it in an MCP-capable AI client with `ACM_DATA_ROOT` pointing at the local `mendix-data` folder.

## Zip Distribution

For another developer, zip the `ACM` folder and send it. On their computer, run the `ACM-Init` instructions after extracting it. The installer flow will build the MCP server, create or connect a local `mendix-data` folder, and produce the MCP client config.

The zip does not include Mendix Studio Pro or the other developer's Mendix app working copies. Those must already exist on their machine.
