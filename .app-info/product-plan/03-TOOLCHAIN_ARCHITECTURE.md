# Toolchain Architecture

## Objective

Define the script-level architecture for generating an AI-useful KB with current file contract preserved.

## Runtime Pipeline

1. `run-dump-parser.ps1` (entrypoint):
   - dumps `.mpr`
   - runs overview parser
   - scaffolds KB structure
   - runs KB composer
   - runs scaffold validation
   - runs quality gate
   - runs semantic benchmark.
2. `run-kb-scaffold.ps1` (existing):
   - creates expected folder/file skeleton and manifest copy.
3. `run-kb-compose.ps1` (new):
   - composes all KB markdown from overview export JSON.
4. `run-kb-quality-gate.ps1` (expanded):
   - structural checks
   - semantic threshold checks.
5. `run-kb-semantic-benchmark.ps1` (new):
   - canonical QA benchmark with score and critical-failure policy.

## Data Flow

1. Input:
   - `mendix-data/app-overview/<run>/manifest.json`
   - `general/*.json`
   - `modules/*/{domain-model,flows,pages,resources}.json`
2. Output:
   - `mendix-data/knowledge-base/<app>/...` markdown files
   - benchmark/validation terminal report.

## Generation Boundaries

1. Parser is source-of-truth for model extraction (`schemaVersion: 2.0`).
2. Composer owns markdown synthesis and tiered narratives.
3. Quality gate owns pass/fail enforcement.
4. Benchmark owns QA-style confidence scoring.

## Deterministic Composition Rules

1. Sort modules alphabetically.
2. Use stable sort on names for entities/pages/flows.
3. Build rankings using deterministic score formulas and tie-breakers.
4. Keep link paths relative and resolvable.

## Script Contracts

### `run-kb-compose.ps1`

Parameters:

1. `-RunFolder` (required)
2. `-AppName` (required)
3. `-OutputRoot` (optional, default `mendix-data/knowledge-base`)
4. `-SkipScaffold` (optional)

Responsibilities:

1. Validate source contract.
2. Build app, module, route docs.
3. Apply custom-depth and tier rules.
4. Minimise avoidable unknowns.

### `run-kb-quality-gate.ps1` (expanded)

Responsibilities:

1. Preserve existing structural checks.
2. Add semantic checks for custom modules.
3. Emit explicit metric values and thresholds.

### `run-kb-semantic-benchmark.ps1`

Responsibilities:

1. Execute canonical QA scenarios.
2. Score each scenario.
3. Enforce minimum score and critical-failure policy.

## Backward Compatibility

1. No file path changes for generated KB.
2. Existing consumers can continue using current pointer structure.
3. New scripts are additive and integrated through `run-dump-parser.ps1`.

## Operational Logging

Each run should print:

1. Run folder
2. App name
3. Module count
4. Structural validation status
5. Quality gate status with semantic metrics
6. Benchmark score and verdict.
