# Processing Pipeline

## Objective

The processing layer converts local repository state into deterministic, machine-readable artefacts for:

- In-pane model-change review.
- Raw-change export consumed by parser/commit-message automation.
- Optional commit-message text storage and retrieval.

## Pipeline families

## 1) Change analysis pipeline (`ReadChanges`)

Entry point: `AutoCommitMessageChangeService.ReadChanges(projectPath, persistModelDumps, dataRootBasePath, headDumpCacheEnabled, selectedModules)`

1. Discover Git repository from `projectPath`.
2. Retrieve filtered status for `*.mpr` and `*.mprops`.
3. Retrieve filtered patch data.
4. Build file-change records (`filePath`, `status`, `isStaged`, `diffText`).
5. For each changed `.mpr`:
   - (v2 only) check `selectedModules`; skip analysis if not selected.
   - dump working model (`mx dump-mpr`).
   - reconstruct and dump committed (`HEAD`) snapshot.
   - (if enabled) use HEAD dump cache before invoking `mx.exe`.
   - diff dumps semantically via `MendixModelDiffService`.
   - group by module/category via `MendixModelChangeStructurer`.
   - optionally persist dump artefacts (`persistModelDumps=true`).
6. (if enabled) prune stale cache entries.
7. Return `AutoCommitMessagePayload`.

Fallback behaviour:

- Per-file model-analysis failures become synthetic `Model Analysis` changes.
- Known dump environment mismatches return empty model changes for the affected file instead of failing full payload.
- Cache read/write failures are non-critical and do not block refresh.

## 2) Raw-change export pipeline

Entry point: `AutoCommitMessageExportService.ExportChanges(payload, projectPath, dataRootBasePath)`

1. Validate payload (`IsGitRepo`, no payload error, non-empty changes).
2. Resolve metadata (`projectName`, `branchName`, `user.name`, `user.email`, timestamp).
3. Ensure output folders exist (`raw-changes`, `processed`, `errors`, `dumps`).
4. Convert grouped model changes to export records including deterministic `displayText`.
5. Write JSON (`schemaVersion: 1.0`) via temp file plus atomic move.

## 3) Changed module detection pipeline (`ListChangeModules`)

Entry point: HTTP GET `/list-change-modules` (handled by `AutoCommitMessageWebServerExtension.HandleListChangeModulesRequestAsync`)

1. Validate Git repository at `projectPath`.
2. Find first `.mpr` file in project.
3. Detect MPR format via `MendixMprFormatDetector.IsMprV2(mprPath)`.
4. For **v2 projects**: extract changed modules via `MendixV2ChangedModuleDetector.DetectChangedModules(repository, mprContentsPath)`.
5. For **v1 projects**: return empty module list.
6. Return JSON response with format version, module list, and `supportsPreFilter`.

## 4) Commit-message storage pipeline

Entry point: `AutoCommitMessageCommitMessageStoreService.StoreCommitMessage(...)`

1. Validate non-empty commit text.
2. Resolve folder `<base>/Commit messages`.
3. Derive file token from first line (sanitised, length-limited).
4. Write UTF-8 text via temp file plus atomic move.

## Service responsibilities

| Service | Responsibility |
|---|---|
| `AutoCommitMessageChangeService` | Git status/diff collection and `.mpr` model analysis orchestration |
| `AutoCommitMessageHeadDumpCacheService` | HEAD dump caching (lookup, storage, pruning) |
| `MendixMprFormatDetector` | MPR v1 vs v2 format detection |
| `MendixV2ChangedModuleDetector` | Changed module extraction from `mprcontents/` structure |
| `MxToolService` | `mx.exe` discovery, compatibility probing, and dump execution |
| `MendixModelDiffService` | Semantic diffing and resource-specific detail enrichment |
| `MendixModelChangeStructurer` | Module/category grouping and association-to-domain detail promotion |
| `MendixModelChangeDisplayTextFormatter` | Stable `displayText` generation for UI/export |
| `AutoCommitMessageExportService` | Raw-change payload serialisation and persistence |
| `AutoCommitMessageCommitMessageStoreService` | Commit-message text storage |
| `AutoCommitMessageHistoryService` | Commit-message listing and safe file reads |

## Failure modes and handling

| Failure case | Handling |
|---|---|
| Empty project path | Returns payload error |
| Not a Git repository | Returns `IsGitRepo=false` |
| `mx.exe` unavailable/incompatible | Per-file model-analysis fallback entry |
| Dump workspace mismatch | Per-file empty model-change fallback |
| Export with no enabled outputs | HTTP 400 in web route |
| Export raw-changes with zero changes | HTTP 400 in web route |
| Store commit message with empty body | HTTP 400 in web route |
