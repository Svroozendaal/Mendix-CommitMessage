# Claude Code — Project Guide

`AutoCommitMessage` inspects uncommitted Mendix model changes (`.mpr`/`.mprops`),
derives semantic diffs from `mx dump-mpr` output, and presents/exports structured
change data for commit-message tooling. It runs as a local browser app on
`localhost`; the parser can also be invoked directly (by the app or by Claude Code).

## Structure

- `ACM/` — the functional product, in three parts:
  - `ACM/parserfolder/` — the parser itself (`AutoCommitMessage.Parser.csproj`,
    namespace `AutoCommitMessage`). No UI or platform dependencies; callable by
    both the app and Claude Code. Tests live in `ACM/parserfolder/tests/`.
  - `ACM/applicatiefolder/` — the localhost web-app shell that wraps the parser
    (`AutoCommitMessage.App.csproj`, ASP.NET Core / Kestrel). `Web/` holds the
    shared HTTP handler and HTML/asset resources.
  - `ACM/AI-tools/` — Mendix domain knowledge (skills): SDK, Studio Pro 10
    constraints, model-dump inspection, commit-message structuring rules.
- `Dev_brain/` — product/project knowledge (product plan, features, agents).
- `.app-info/docs/` — technical documentation (ARCHITECTURE, PROCESSING_PIPELINE,
  EXPORT_CONTRACT, etc.).
- `mendix-data/` — runtime data root (exports, dumps, commit messages).

## Build & run

- Run the app from source: `./open-browser-app.ps1` (builds `ACM/applicatiefolder`
  and opens `http://localhost:3109`).
- Build parser/app: `dotnet build ACM/applicatiefolder/AutoCommitMessage.App.csproj`.
- Tests: `dotnet test ACM/parserfolder/tests`.

## Conventions

- UK English in documentation.
