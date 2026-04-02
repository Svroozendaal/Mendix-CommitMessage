# AutoCommitMessage Architecture

## Purpose and scope

`AutoCommitMessage` is a Mendix Studio Pro 10 extension that inspects local uncommitted Mendix changes, derives semantic model deltas, and stores deterministic artefacts for downstream automation.

Current implementation scope:

- Uncommitted Git analysis for `*.mpr` and `*.mprops`.
- Semantic diffing for changed `*.mpr` files via `mx dump-mpr`.
- In-pane model-change review grouped by module and category.
- Raw-change JSON export (`schemaVersion: 1.0`).
- Commit-message text persistence and history endpoints.

Out of scope:

- Full Git client operations (history, push/pull, merge/rebase).
- Remote API calls or hosted processing.
- Direct Git commit execution from extension runtime.

## Runtime component map

| Layer | Component | File | Responsibility |
|---|---|---|---|
| Studio Pro UI | Dockable pane extension | `UI/DockablePane/AutoCommitMessageDockablePaneExtension.cs` | Registers pane and builds route URL |
| Studio Pro UI | Pane view model | `UI/DockablePane/AutoCommitMessageDockablePaneViewModel.cs` | Binds pane title and initial web address |
| Studio Pro UI | Menu extension | `UI/Menu/AutoCommitMessageMenuExtension.cs` | Exposes open/close menu commands |
| Web route | Route handler | `UI/Web/AutoCommitMessageWebServerExtension.cs` | Handles HTML, refresh, export, module listing, and commit-message endpoints |
| Web route | Embedded UI | `UI/Web/AutoCommitMessagePanelHtml.cs` | Renders model changes, settings, and commit-message compose/history UI |
| Processing | Change analysis | `Processing/Services/AutoCommitMessageChangeService.cs` | Reads Git status/diff and `.mpr` model changes |
| Processing | Raw-change export | `Processing/Services/AutoCommitMessageExportService.cs` | Writes deterministic raw-change JSON |
| Processing | Commit-message storage | `Processing/Services/AutoCommitMessageCommitMessageStoreService.cs` | Persists supplied commit-message text to disk |
| Processing | Commit-message history | `Processing/Services/AutoCommitMessageHistoryService.cs` | Lists and reads stored commit messages with path traversal guards |
| Processing | `mx.exe` execution | `Processing/Services/MxToolService.cs` | Finds compatible `mx.exe` and runs `dump-mpr` |
| Processing | Diff engine | `Processing/ModelDiff/MendixModelDiffService.cs` | Computes semantic resource-level model changes |
| Processing | Grouping | `Processing/ModelDiff/MendixModelChangeStructurer.cs` | Groups model changes by module/category |
| Processing | Display formatter | `Processing/Formatting/MendixModelChangeDisplayTextFormatter.cs` | Produces deterministic `displayText` strings |
| Processing | Installation detection | `Processing/Services/MendixInstallationDetectorService.cs` | Auto-detects correct `mx.exe` for a given `.mpr` file |
| Processing | Configuration service | `Processing/Services/ExtensionConfigurationService.cs` | Stores detection result and install-root override state |

## Web actions and endpoints

Route prefix: `autocommitmessage/`

| Action query value | Method | Behaviour |
|---|---|---|
| _(none)_ | `GET` | Returns embedded HTML shell |
| `refresh` | `GET` | Returns latest `AutoCommitMessagePayload` |
| `export` | `POST` | Stores selected outputs (raw changes and/or dumps) |
| `store-commit-message` | `POST` | Stores commit message with deterministic filename and git hash header |
| `list-commit-messages` | `GET` | Lists stored commit messages from history folder |
| `read-commit-message` | `GET` | Reads content of specific commit message file |
| `list-change-modules` | `GET` | Lists changed modules for MPR v2 prefiltering |
| `/api/detection` | `GET` | Runs Mendix installation detection with optional `override=<path>` parameter |

Important query keys include:

- `projectPath`
- `dataRootBasePath`
- `commitMessagesBasePath`
- `persistDumps`
- `persistRawChanges`
- `headDumpCacheEnabled`
- `moduleFilter` (comma-separated)

## Request lifecycles

### Pane load

1. Pane extension builds URL with cache-buster `_v` and optional `projectPath`.
2. Web route returns HTML UI shell.
3. UI waits for explicit `Refresh` to load analysis payload.

### Refresh

1. UI calls `?action=refresh`.
2. `AutoCommitMessageChangeService.ReadChanges(...)` runs status/diff/model analysis.
3. JSON payload returns branch + filtered file changes + grouped model deltas.

### Export

1. UI calls `POST ?action=export` with persistence toggles.
2. Route validates output selection and repository state.
3. If `persistRawChanges=true`, raw-change export JSON is written.
4. If `persistDumps=true`, per-file `working/head` dump artefacts are persisted during analysis.
5. Response returns success, output path, and folder information.

### Commit-message storage

1. Client submits `POST ?action=store-commit-message` with JSON body `{ "message": "..." }`.
2. Store service sanitises file token and writes UTF-8 text atomically.
3. Response returns output file and folder path.

## Data root resolution and storage

`ExtensionDataPaths.ResolveDataRoot(projectPath, dataRootBasePath)` resolves in this order:

1. Explicit `dataRootBasePath` query (normalised to `<base>/mendix-data` if needed).
2. Build-configured root from environment variable `MENDIX_GIT_DATA_ROOT`.
3. Build-configured root from assembly metadata key `MendixDataRoot`.
4. Fallback `<projectPath>/mendix-data`.

Primary runtime folders:

- `raw-changes` (raw change exports)
- `processed` (reserved downstream)
- `errors` (reserved downstream)
- `dumps` (persisted dump artefacts)

Commit-message storage folder:

- `<commitMessagesBasePath or resolved base>/Commit messages`

## Error handling strategy

- Non-Git projects return `IsGitRepo=false` with non-crashing payloads.
- `.mpr` model-analysis failures degrade to synthetic `Model Analysis unavailable` rows instead of failing all changes.
- Known `mx.exe` environment issues (`mprcontents`, workspace mismatch) are treated as recoverable for individual files.
- Export/store routes return structured JSON error responses with status `400` or `500`.
- File writes use temporary files plus atomic move where applicable.

## Performance notes

- Status and diff scans are filtered to Mendix-relevant extensions.
- `.mpr` diffing requires two dumps per changed model file.
- `mx.exe` execution is timeout-guarded.

## Related documents

- `PROCESSING_PIPELINE.md`
- `EXPORT_CONTRACT.md`
- `REPOSITORY_WORKFLOWS.md`
- `OPEN_QUESTIONS.md`
