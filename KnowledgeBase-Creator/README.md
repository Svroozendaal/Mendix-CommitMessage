# KnowledgeBase Creator Artifact

This folder is a portable drop-in package for creating a Mendix knowledge base.

It includes:
- `run-dump-parser.ps1` for dump, parser export, KB scaffold, and template seeding.
- `Mendix-model-overview-parser/` with source and prebuilt parser binary.
- `.agents/` with only the agent and skills required to create a KB.
- `artifacts/` markdown templates copied into each generated KB.

## Quick Start

1. Edit `.env`.
2. Run `./run-dump-parser.ps1`.
3. Give your AI agent the instruction from `agents.md`.

## Default Output

By default, output is created in:
- `../mendix-data/dumps/`
- `../mendix-data/app-overview/`
- `../mendix-data/knowledge-base/`

Set `MENDIX_DATA_ROOT` in `.env` to override.
