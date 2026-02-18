# PHASE_6_DATA_COLLECTION
## Goal

Collect and export structured change data for reliable downstream parsing.

## Entry Criteria

1. Data-collection scope, acceptance criteria, and non-goals are clear.
2. Outputs from `PHASE_5.5_MODEL_DIFF_ANALYSIS` are available when Mendix model files are in scope.
3. Clarifying workflow questions have been asked as needed.

## Required First Action

1. Read `development/AGENTS.md`.
2. Read `development/AI_WORKFLOW.md`.
3. Ask workflow questions first.
4. Confirm scope and non-goals.
5. Ask which skills should be used and suggest relevant defaults.
6. Load relevant skills, including `development/skills/mendix-studio-pro-10/SKILL.md`, `development/skills/mendix-sdk/SKILL.md`, `development/skills/testing/SKILL.md`, and `development/skills/documentation/SKILL.md`.

## Tasks

1. Pause at `WAIT_FOR_APPROVAL` before implementation changes, unless `AUTO_APPROVE` is explicit.
2. Confirm data source boundaries (repository scope, change types, inclusion and exclusion rules).
3. Define and version the export schema (required fields, optional fields, enums, and timestamps).
4. Define folder and file contract for parser ingestion (naming, atomic writes, and error routing).
5. Implement export behaviour and user feedback in small verifiable steps.
6. Validate success and failure paths, including empty change sets, malformed data, binary-model handling, and permission issues.
7. Record decisions and implementation progress in `development/agent-memory/DECISIONS_LOG.md` and `development/agent-memory/PROGRESS.md`.
8. Document parser consumer expectations, known limitations, and hand-off notes for phase 7.

## Exit Criteria

1. Export schema and folder contract are documented and parser-ready.
2. Validation outcomes cover both success and failure paths.
3. Parser consumer expectations and known limitations are clear.
4. Memory log updates and hand-off notes are complete.
