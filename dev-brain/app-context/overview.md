# ACM — Application Context (overview)

> Grounding for the `development-agent` and `parser-upgrader`. Read this first, then drill
> into `PRODUCT_PLAN.md`, `features/`, the canonical technical docs in
> `.app-info/docs/`, and the Mendix domain skills in `ACM/AI-tools/skills/`.

## What ACM is

`AutoCommitMessage` (ACM) inspects **uncommitted Mendix model changes**
(`.mpr` / `.mprops`), derives semantic diffs from `mx dump-mpr` output, and
presents/exports structured change data for commit-message tooling.

Mendix `.mpr` files are not readable in standard Git text diffs. ACM closes that
gap: it detects relevant changes, dumps working vs `HEAD` model state, compares
them semantically, groups the result by module/category, renders a deterministic
`displayText` per change, and exports a stable JSON payload for downstream
parsing and commit-message generation.

## Current shape (authoritative)

ACM now runs as a **local browser app on `localhost`** (ASP.NET Core / Kestrel),
not as an in-IDE Studio Pro dockable pane. The parser is a standalone library
that the app — or Claude Code — can call directly. Where the product/feature docs
below mention a Studio Pro extension or `studio-pro-extension-csharp/...` paths,
treat those as the **historical extension incarnation**; the live code lives under
`ACM/` (see the path map below). CLAUDE.md and `.app-info/docs/` are the source of
truth for current state.

## Where the code lives

| Area | Path |
|---|---|
| Parser (no UI/platform deps; namespace `AutoCommitMessage`) | `ACM/parserfolder/` (`AutoCommitMessage.Parser.csproj`) |
| Parser tests | `ACM/parserfolder/tests/` |
| Model diff + structuring | `ACM/parserfolder/Processing/ModelDiff/` |
| Display-text formatting | `ACM/parserfolder/Processing/Formatting/MendixModelChangeDisplayTextFormatter.cs` |
| Services (change, export, mx tool, …) | `ACM/parserfolder/Processing/Services/` |
| Contracts (payload, `MendixModelChange`) | `ACM/parserfolder/Processing/Contracts/` |
| Localhost web-app shell | `ACM/applicatiefolder/` (`AutoCommitMessage.App.csproj`) |
| Web handler + HTML/assets | `ACM/applicatiefolder/Web/` (`AutoCommitMessagePanelHtml.cs`, `AutoCommitMessageWebServerExtension.cs`) |
| Mendix domain skills (SDK, Studio Pro 10, dump inspection, commit rules) | `ACM/AI-tools/skills/` |
| Canonical technical docs | `.app-info/docs/` (ARCHITECTURE, PROCESSING_PIPELINE, EXPORT_CONTRACT, …) |
| Runtime data root (exports, dumps, commit messages, raw changes) | `mendix-data/` |

## Build & run

- Run the app: `./open-browser-app.ps1` (builds `ACM/applicatiefolder`, opens `http://localhost:3109`).
- Build: `dotnet build ACM/applicatiefolder/AutoCommitMessage.App.csproj`.
- Tests: `dotnet test ACM/parserfolder/tests`.

## The two quality surfaces (why `parser-upgrader` exists)

ACM has two coupled outputs that must stay aligned:

1. **Extraction quality** — the `details` produced by `MendixModelDiffService`
   (does the parser pull the right semantic fields out of the dumps?).
2. **Presentation quality** — the `displayText` produced by
   `MendixModelChangeDisplayTextFormatter` and the final commit-message lines
   (is the change rendered compactly and correctly?).

`parser-upgrader` is the standalone agent dedicated to improving the parser by
closing gaps between these two surfaces through an additive, rule-governed growth
loop. See `agents/parser-upgrader.md`.

## Conventions

- UK English in product/technical documentation.
- Deterministic, reproducible export behaviour is a primary quality goal.
