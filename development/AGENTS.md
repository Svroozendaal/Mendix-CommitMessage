# AGENTS
## AI Collaboration Framework for This Repository

This file is the primary source of truth for all AI assistants working in this repository.

All prompts, commands, skills, and agent roles must follow this file.

## Purpose

Use a multi-agent workflow to plan, implement, test, review, document, and maintain this project with consistent quality and traceability.

## First Step Rule

Before any prompt is executed, do the following in order:

1. Read `development/AGENTS.md`.
2. Ask clarifying workflow questions first, using as many questions as needed.
3. Confirm assumptions before changing files.

## Approval Checkpoint

After planning and before implementation changes, use:

`WAIT_FOR_APPROVAL`

Exception:

If the user or prompt explicitly states `AUTO_APPROVE`, continue without pausing.

## Core Principles

1. Use UK English in all documentation.
2. Keep tone friendly, clear, and technical.
3. Use neutral wording such as "AI assistant" or "assistant", not tool-brand names.
4. Keep project-specific constraints in skills, not in this file.
5. Keep this file generic enough for any assistant.
6. Keep commands as guidance, not strict templates.

## Directory Contract

- `development/AGENTS.md`: Primary governance.
- `development/AI_WORKFLOW.md`: End-to-end operating flow.
- `development/agents/`: Agent role definitions.
- `development/prompts/`: Prompt playbooks (including phase prompts).
- `development/commands/`: Reusable command-level guidance.
- `development/skills/`: Domain and task skills.
- `development/agent-memory/`: Shared memory templates and live logs.

## Agent Roster

| Agent | File | Responsibility |
|---|---|---|
| Architect | `development/agents/ARCHITECT.md` | Design, scope, contracts, decisions |
| Implementer | `development/agents/IMPLEMENTER.md` | Delivery of production changes |
| Tester | `development/agents/TESTER.md` | Validation, edge cases, regressions |
| Reviewer | `development/agents/REVIEWER.md` | Quality gate and approval |
| Memory | `development/agents/MEMORY.md` | Session continuity and hand-offs |
| Prompt Refiner | `development/agents/PROMPT_REFINER.md` | Prompt quality and consistency |
| Debugger | `development/agents/DEBUGGER.md` | Root-cause analysis and fixes |
| Documenter | `development/agents/DOCUMENTER.md` | Documentation quality and upkeep |

## Skills Overview

Skills contain specific knowledge or procedures.

General skills:

- `development/skills/documentation/SKILL.md`
- `development/skills/testing/SKILL.md`
- `development/skills/mendix-sdk/SKILL.md`
- `development/skills/mcp-server/SKILL.md`
- `development/skills/mendix-model-dump-inspection/SKILL.md`
- `development/skills/mendix-commit-structuring/SKILL.md`

Project-specific constraints skill:

- `development/skills/mendix-studio-pro-10/SKILL.md`

## Prompt Model

This file does not define phase content.

Prompts are defined in `development/prompts/`.
Each prompt must begin by reading this file and asking workflow questions before execution.
Each prompt must include:

1. Entry criteria.
2. Exit criteria.
3. A skill suggestion step that asks which skills should be used and proposes relevant defaults.

## Memory Model

Memory files are both:

1. Templates for a clean start.
2. Live logs during active work.

Canonical files:

- `development/agent-memory/SESSION_STATE.md`
- `development/agent-memory/DECISIONS_LOG.md`
- `development/agent-memory/PROGRESS.md`
- `development/agent-memory/REVIEW_NOTES.md`
- `development/agent-memory/PROMPT_CHANGES.md`
- `development/agent-memory/INCIDENTS.md`

## Naming Standards

1. Use uppercase file names for agent, prompt, command, and memory markdown files.
2. Keep naming predictable and explicit.
3. Avoid duplicate variants of the same instruction set.

## Agent Operating Defaults

1. Implementer may create new files when needed.
2. Tester should focus on automated testing by default.
3. Reviewer blocks only on unresolved `MUST FIX` items.
4. Prompt Refiner runs only when explicitly requested.
5. Do not compact memory logs automatically; ask first if maintenance is needed.

## Required Handoff Block

Every agent hand-off should append this structure to `development/agent-memory/SESSION_STATE.md`:

```markdown
## HANDOFF - [Agent] - [timestamp]
STATUS: COMPLETE | BLOCKED | NEEDS_INPUT
NEXT_AGENT: [Agent or none]
SUMMARY: [1-3 sentences]
BLOCKERS: [none or details]
```
