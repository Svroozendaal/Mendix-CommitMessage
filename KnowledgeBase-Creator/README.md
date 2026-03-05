# KnowledgeBase Creator Artifact

This folder is a portable drop-in package for creating a Mendix knowledge base.

It includes:
- `run-dump-parser.ps1` for dump, parser export, scaffold, compose, and validation.
- `run-kb-compose.ps1` for deterministic behaviour-rich markdown composition.
- `run-kb-quality-gate.ps1` for structural + semantic completeness checks.
- `run-kb-semantic-benchmark.ps1` for canonical QA benchmark scoring.
- `Mendix-model-overview-parser/` with source and prebuilt parser binary.
- `.agents/` with only the agent and skills required to create a KB.
- `artifacts/` markdown templates copied into each generated KB.

## Quick Start

1. Edit `.env`.
2. Run `./run-dump-parser.ps1`.
3. Give your AI agent the instruction from `agents.md`.

## Validation Contract

`run-dump-parser.ps1` executes and requires:

1. `./run-kb-scaffold.ps1 -Validate -OutputRoot mendix-data/knowledge-base -AppName <app-name>`
2. `./run-kb-quality-gate.ps1 -OutputRoot mendix-data/knowledge-base -AppName <app-name>`
3. `./run-kb-semantic-benchmark.ps1 -OutputRoot mendix-data/knowledge-base -AppName <app-name>`

## Default Output

By default, output is created in:
- `../mendix-data/dumps/`
- `../mendix-data/app-overview/`
- `../mendix-data/knowledge-base/`

Set `MENDIX_DATA_ROOT` in `.env` to override.
