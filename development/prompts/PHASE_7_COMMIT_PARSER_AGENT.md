# PHASE_7_COMMIT_PARSER_AGENT
## Goal

Build and operate the receiving parser workflow for exported change data.

## Entry Criteria

1. Export contract is available.
2. Clarifying workflow questions have been asked as needed.

## Required First Action

1. Read `development/AGENTS.md`.
2. Ask workflow questions first.
3. Ask which skills should be used and suggest relevant defaults.
4. Load relevant skills, including `development/skills/mendix-studio-pro-10/SKILL.md`.

## Tasks

1. Pause at `WAIT_FOR_APPROVAL` before implementation changes, unless `AUTO_APPROVE` is explicit.
2. Watch export input folder for new files.
3. Parse and enrich raw change data.
4. Persist structured outputs safely.
5. Route malformed files to error handling.
6. Document pipeline behaviour and hand-off contracts.

## Exit Criteria

1. Watch/parse/store/error pipeline is operational.
2. Hand-off contracts are documented.
3. Validation outcomes are recorded.
