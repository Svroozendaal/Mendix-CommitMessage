# AI Tools

## Purpose

`AI-tools/` contains ACM-specific AI guidance. These files explain how an AI assistant should use parser output, inspect Mendix model dumps, and write technical commit messages.

## Contents

- `ACM-Writer.md` is the high-level agent entry point for using ACM to resolve an app, read/export changes, and store commit messages.
- `skills/` contains app-specific skills for Mendix SDK knowledge, Studio Pro 10 constraints, model-dump inspection, commit structuring, parser usage, and technical commit-message writing.

## Relationship to Runtime Code

These files do not implement the parser. They describe how AI agents should operate around the parser and Mendix domain. Runtime behaviour lives in `parserfolder/`, `applicatiefolder/`, and `mcp-server/`.

## Maintenance Notes

- Keep AI guidance aligned with the parser export contract and MCP tools.
- When parser output shape or display-text rules change, review commit-message and model-dump inspection skills.
- Add new app-specific skills under `skills/<name>/SKILL.md` and update `skills/OVERVIEW.md`.
