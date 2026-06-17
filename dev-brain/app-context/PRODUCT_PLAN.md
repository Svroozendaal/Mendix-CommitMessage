# PRODUCT_PLAN
## Mendix Studio Pro 10 Change Analysis and Export

> This plan describes the product intent and improvement direction. For the
> current code shape and path map, read `overview.md` first — the live
> implementation is the localhost web app under `ACM/`.

## Product statement

`AutoCommitMessage` helps Mendix developers inspect uncommitted Mendix model
changes and export structured, deterministic change data for downstream
commit-message tooling.

## Current users

- Mendix developers who work in Git-enabled app repositories.
- Maintainers of the downstream parsing and commit-message flow that consumes export JSON.

## Problem addressed

Mendix project changes in `.mpr` are not directly readable in standard Git text diffs. ACM closes this gap by:

1. Detecting relevant uncommitted changes (`.mpr`, `.mprops`).
2. Deriving model-level semantic changes from dump comparisons.
3. Presenting grouped model changes.
4. Exporting stable JSON that other automation can process.

## Implemented capabilities (current state)

1. UI for changed files, diff text, and grouped model changes.
2. Internal web routes for initial load, refresh, and export actions.
3. Filtered Git status and patch analysis via `LibGit2Sharp`.
4. Working vs HEAD `mx dump-mpr` analysis for changed `.mpr` files.
5. Module/category grouping of model changes (`DomainModel`, `Microflows`, `Pages`, `Nanoflows`, `Resources`).
6. Deterministic `displayText` generation for model changes.
7. JSON export with schema version, metadata, grouped changes, and optional dump artefact paths.
8. Scripted build/run workflow with `.env` support.

## Product boundaries (non-goals)

1. No full Git client replacement (commit history, rebase, push/pull, merge resolution).
2. No external hosted API calls in runtime.
3. No direct commit creation in current UI.
4. No broad platform integration outside the defined local export pipeline.

## Quality goals

1. Deterministic and reproducible export behaviour.
2. Graceful handling of common local environment issues (`mx.exe` detection, dump workspace mismatch).
3. Clear traceability between UI output and export payload.
4. Operational clarity through maintainable technical documentation.

## Architecture at product level

High-level flow:

1. The app/UI is opened.
2. ACM reads local repository state.
3. Changed `.mpr` files are dumped and semantically compared.
4. The UI displays grouped changes.
5. Export action writes JSON payload to the configured data root.

Primary technical docs (canonical):

- `.app-info/docs/README.md`
- `.app-info/docs/ARCHITECTURE.md`
- `.app-info/docs/PROCESSING_PIPELINE.md`
- `.app-info/docs/EXPORT_CONTRACT.md`
- `.app-info/docs/REPOSITORY_WORKFLOWS.md`

## Improvement backlog (based on current implementation)

1. Add automated tests for model diffing, grouping, and formatter edge cases.
2. Publish explicit machine-readable export schema for downstream consumers.
3. Introduce configurable retention/cleanup policy for export and dump folders.
4. Add structured diagnostics to improve supportability for environment-specific failures.
5. Improve performance visibility (timing and size telemetry) for large-model refresh/export runs.
6. Define compatibility strategy for future Studio Pro major versions while retaining Studio Pro 10 support.

## Operational workflow summary

1. Developer builds and runs the app (`./open-browser-app.ps1`).
2. Developer opens the UI and validates change analysis.
3. Developer exports payload to `mendix-data/exports`.
4. Downstream parser pipeline consumes and processes exported payloads.

Detailed workflow documentation:

- `.app-info/docs/REPOSITORY_WORKFLOWS.md`

## Risks and constraints

1. `mx.exe` availability and compatibility are runtime prerequisites for model analysis.
2. Large model files can increase refresh/export latency.
3. Dump artefact growth can consume disk space without retention policy.
4. Export schema changes can break downstream tooling if not version-managed.

## Open product questions

Pending product-owner input is tracked in:

- `.app-info/docs/OPEN_QUESTIONS.md`
