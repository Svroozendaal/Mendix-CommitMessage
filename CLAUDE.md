# Claude Code - Project Starting Point

This repository contains AutoCommitMessage (ACM), local development knowledge for ACM, and runtime data used by the tooling. Use this file as the first orientation point only; detailed application knowledge lives closer to the relevant code.

## Repository Map

- `ACM/` - the actual AutoCommitMessage product. Start with `ACM/inf_acm.md` for the application map.
- `ACM/parserfolder/` - shared parser library. It reads local Git changes, runs Mendix `mx dump-mpr`, compares model dumps, groups changes, formats display text, exports JSON, and stores commit-message files.
- `ACM/applicatiefolder/` - local browser app. It starts a localhost Kestrel server and wraps the parser with a human-facing UI.
- `ACM/mcp-server/` - local stdio MCP server. It exposes parser operations to AI clients such as Claude Code, Copilot, or Cursor.
- `ACM/AI-tools/` - ACM-specific AI guidance and Mendix domain skills used for parser usage, dump inspection, and technical commit-message writing.
- `dev-brain/` - development brain for this repository. It contains agents, skills, conventions, command docs, and app context used to guide development work and knowledge maintenance.
- `mendix-data/` - local runtime data root. It stores app registry data, raw exports, dump artifacts, and generated commit messages.

## Where To Look First

- For the application shape: `ACM/inf_acm.md`.
- For parser internals: `ACM/parserfolder/inf_parserfolder.md`.
- For the browser app: `ACM/applicatiefolder/inf_applicatiefolder.md`.
- For MCP usage: `ACM/mcp-server/inf_mcp-server.md`.
- For AI/domain skills: `ACM/AI-tools/inf_ai-tools.md`.
- For development-agent behaviour and conventions: `dev-brain/README.md`, `dev-brain/CONVENTIONS.md`, and `dev-brain/manifest.md`.

## Common Commands

- Run the app from source: `./open-browser-app.ps1`.
- Build app and parser: `dotnet build ACM/applicatiefolder/AutoCommitMessage.App.csproj`.
- Run parser tests: `dotnet test ACM/parserfolder/tests`.

## Conventions

- Keep root documentation concise; put detailed knowledge in `inf_*.md` files near the code it describes.
- Do not edit generated `bin/`, `obj/`, or debug output folders.
- Use UK English in documentation.
