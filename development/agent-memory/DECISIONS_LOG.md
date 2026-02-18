# DECISIONS_LOG

## TEMPLATE

```markdown
## DECISION - [id] - [timestamp]
CONTEXT: [...]
DECISION: [...]
RATIONALE: [...]
ALTERNATIVES_REJECTED: [...]
```

## LIVE_LOG

## DECISION - 001 - 2026-02-18
CONTEXT: Phase 6 requires raw export files that must be consumed by the existing Phase 7 parser contract.
DECISION: Export payload now uses parser-compatible fields (`timestamp`, `projectName`, `branchName`, `userName`, `userEmail`, `changes[*].filePath/status/isStaged/diffText/modelChanges`) with added optional `schemaVersion`.
RATIONALE: Keeps Phase 7 ingestion working without parser schema breakage while allowing schema versioning.
ALTERNATIVES_REJECTED: Introducing a brand-new schema with parser migration in this phase.

## DECISION - 002 - 2026-02-18
CONTEXT: Data root path must be inside this repository and configurable when building the extension.
DECISION: Use a repo-local `mendix-data` folder contract and pass `MendixDataRoot` via `deploy-autocommitmessage.ps1` into extension build metadata.
RATIONALE: Ensures deterministic local paths and aligns extension export location with parser watcher defaults.
ALTERNATIVES_REJECTED: Keeping hardcoded `C:\MendixGitData` paths.
