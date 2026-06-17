# Claude Code — Project Guide

`AutoCommitMessage` is a Mendix Studio Pro 10 extension that inspects uncommitted
model changes (`.mpr`/`.mprops`), derives semantic diffs from `mx dump-mpr`
output, and exports structured JSON for downstream commit-message tooling.

## Where things live

- `studio-pro-extension-csharp/` — the extension source code.
- `studio-pro-extension-csharp/Docs/` — primary technical documentation
  (ARCHITECTURE, PROCESSING_PIPELINE, EXPORT_CONTRACT, REPOSITORY_WORKFLOWS).
- `standalone/` — standalone/browser version of the app.
- `.app-info/app/PRODUCT_PLAN.md` — product vision, scope, and boundaries.
- `.app-info/features/` — feature registry (what the app does).
- `.app-info/skills/` — Mendix domain knowledge: SDK, Studio Pro 10 constraints,
  model-dump inspection, and commit-message structuring rules. Consult these
  before working on parsing, diffing, or commit-message logic.

## Conventions

- UK English in documentation.
- Build/deploy via the PowerShell scripts in the repo root
  (`deploy-autocommitmessage.ps1`, `start-mendix-app.ps1`).
