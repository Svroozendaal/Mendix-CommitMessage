# skills/
## App-Specific Skills

This folder contains skills that are specific to this application — Mendix domain knowledge used when working on parsing, diffing, and commit-message logic.

## When to Use App-Specific Skills

App-specific skills are used when:
- The task requires knowledge of this application's domain, stack, or conventions.
- The task involves platform-specific procedures (e.g. Mendix SDK operations).
- The task requires applying project-specific rules (e.g. styling guidelines, data contracts).

## Contents

| Skill | Folder | Description |
|---|---|---|
| Mendix SDK | `mendix-sdk/` | Mendix SDK usage and model manipulation |
| Mendix Studio Pro 10 | `mendix-studio-pro-10/` | Studio Pro 10 extension development constraints |
| Mendix Model Dump Inspection | `mendix-model-dump-inspection/` | Inspecting dump outputs, maintaining diff parser contracts, and evolving deterministic diff rules (`Dxxx`) |
| Mendix Commit Structuring | `mendix-commit-structuring/` | Structuring commit data for the parser pipeline |
| Mendix Technical Commit Message | `mendix-technical-commit-message/` | Rule-driven conversion of module-grouped export data into technical commit message lines |
| ACM Parser Usage | `acm-parser-usage/` | How to drive the ACM parser end to end: Mendix version, `.mpr` working copy, and where results land in `mendix-data` |

## Agents

| Agent | File | Description |
|---|---|---|
| ACM-Writer | `../ACM-Writer.md` | Entry point for any AI using the parser: resolves app/branch locations from the apps registry, runs the parser via `acm-parser-usage`, and writes raw changes + commit messages into `mendix-data` |

## Parser surfaces

The parser library (`ACM/parserfolder`) is driven through thin adapters:

| Surface | Path | For |
|---|---|---|
| MCP server | `ACM/mcp-server/` | AI clients (Claude Code, Copilot, Cursor) — local stdio tools, preferred |
| Web app | `ACM/applicatiefolder/` | Human browser UI + scripted HTTP fallback |

## Adding a New App-Specific Skill

Place the new skill in a subfolder here, then update this OVERVIEW.md with a new row in the Contents table.
