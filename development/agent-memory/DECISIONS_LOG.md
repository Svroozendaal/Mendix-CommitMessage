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

## DECISION - 003 - 2026-02-18
CONTEXT: Model diff output missed modified resources because only direct comparisons of `Entity`, `Page`, and `Microflow` objects were evaluated.
DECISION: Replace the narrow comparer with object-graph snapshot analysis that maps nested object changes back to owning resources.
RATIONALE: Mendix edits are often represented on nested objects, so ownership-based attribution is required to correctly surface modified microflows/pages/entities and support nanoflows and other document resources.
ALTERNATIVES_REJECTED: Expanding only the static type allow-list without nested ownership attribution.

## DECISION - 004 - 2026-02-18
CONTEXT: Model-change output lacked actionable details for microflow internals and domain model attribute additions.
DECISION: Enrich resource-level details with microflow action usage summaries and explicit domain-entity added attribute names.
RATIONALE: Commit assistance needs concrete low-level change semantics, not only resource-level labels.
ALTERNATIVES_REJECTED: Keeping only generic nested-change counters and file-level summaries.

## DECISION - 005 - 2026-02-18
CONTEXT: Agents need inspectable full model snapshots for advanced analysis and auditing.
DECISION: Persist working/head `mx dump-mpr` JSON artifacts under `mendix-data/dumps` during export actions and include artifact paths in export payload.
RATIONALE: Provides deterministic, inspectable source data for deeper tooling without changing the parser ingestion contract.
ALTERNATIVES_REJECTED: Persisting dumps on every pane refresh (excessive storage churn), or not persisting dumps at all.

## DECISION - 006 - 2026-02-18
CONTEXT: Users observed model-analysis failures after commit where temp `mprcontents` paths were missing during HEAD dump reconstruction.
DECISION: Build HEAD analysis workspace by copying `mprcontents` from the HEAD Git tree when available, with filesystem fallback and environment-error short-circuit handling.
RATIONALE: HEAD `.mpr` files can reference unit files that do not align with the current working `mprcontents`, so HEAD-consistent contents are required for reliable `mx dump-mpr`.
ALTERNATIVES_REJECTED: Reusing only working `mprcontents` for HEAD snapshot dumps.

## DECISION - 007 - 2026-02-18
CONTEXT: Pane reopen sometimes showed a stale UI state.
DECISION: Add a cache-busting query token to the dockable pane webview URL and remove legacy alternative pane registration path.
RATIONALE: Ensures deterministic loading of the current web UI implementation each open and avoids fallback to legacy pane plumbing.
ALTERNATIVES_REJECTED: Relying only on HTTP no-cache headers.

## DECISION - 008 - 2026-02-18
CONTEXT: Manual Refresh feedback was unclear and users reported stale model-change views after updates.
DECISION: Implement an explicit refresh web action that re-runs `GitChangesService.ReadChanges(...)` and update the panel to fetch it with visible reload status text.
RATIONALE: Makes refresh behaviour deterministic and communicates when model analysis is actively reloading.
ALTERNATIVES_REJECTED: Keeping browser-only `window.location.reload()` without explicit refresh state messaging.

## DECISION - 009 - 2026-02-18
CONTEXT: Microflow model details only provided action counts, missing meaningful context such as association retrieval and changed members.
DECISION: Extend microflow detail extraction to include action descriptors for Retrieve, Change Object, Commit, and Create Object actions, and add a reusable skill document for dump inspection.
RATIONALE: Commit reviews require actionable semantics (what was retrieved/changed/committed), not only aggregate counts.
ALTERNATIVES_REJECTED: Preserving count-only microflow detail output.
