# info_studio-pro-extension-csharp

> Last updated: 2026-02-18

## Purpose

Studio Pro 10 extension that presents Mendix-relevant Git changes and `.mpr` model details in a dockable WebView pane. It also exports parser-ready JSON plus optional persisted model dumps.

## Key files

| File | Purpose |
|---|---|
| `AutoCommitMessage.csproj` | Build configuration and dependency references |
| `manifest.json` | Mendix extension manifest (`AutoCommitMessage.dll`) |
| `ExtensionConstants.cs` | Pane ID/title, route name, query/action constants |
| `ExtensionDataPaths.cs` | Build-time/runtime data-root resolution (`mendix-data`) |
| `GitChangesDockablePaneExtension.cs` | Pane entrypoint and cache-busted WebView URL generation |
| `GitChangesDockablePaneViewModel.cs` | WebView binding for pane title and URL |
| `GitChangesWebServerExtension.cs` | Internal web routes for default UI, `refresh`, and `export` actions |
| `GitChangesPanelHtml.cs` | In-extension HTML/CSS/JS UI (model changes pane + files/diff pane) |
| `GitChangesService.cs` | Git status, diff loading, `.mpr` dump/compare analysis |
| `MendixModelDiffService.cs` | Dump comparison and detailed model-change extraction |
| `GitChangesExportService.cs` | Raw export file writer (`schemaVersion: 1.0`) |
| `GitChangesPayload.cs` | Payload records (`GitFileChange`, `MendixModelChange`, dump artifact paths) |

## Runtime flow

1. User opens the pane (`PaneId`) from Studio Pro.
2. Pane URL points to `autocommitmessage/` with cache-buster and `projectPath`.
3. Default route calls `GitChangesService.ReadChanges(projectPath)` and renders HTML.
4. UI loads:
   - `Model changes (.mpr)` on the left (primary text-heavy pane)
   - `Changed files` and `Diff` on the right
5. `Refresh` calls `?action=refresh`:
   - server returns fresh JSON from `GitChangesService.ReadChanges(...)`
   - UI status shows reload progress and completion timestamp
6. `Export` calls `?action=export`:
   - server runs `ReadChanges(..., persistModelDumps: true)`
   - writes export JSON to `mendix-data/exports`
   - stores `working-dump.json` and `head-dump.json` under `mendix-data/dumps`

## Model detail behaviour

- `.mpr` plain git diff is treated as binary fallback text.
- Semantic model changes are derived from `mx.exe dump-mpr` snapshots.
- Microflow details include action usage plus second-level descriptors (retrieve metadata, assignment/value details, call targets, commit options).
- Domain entity changes include attribute names added in the change.

## Operational notes

- If the app is not deployed with the latest DLL/manifest, Studio Pro can show outdated UI behaviour.
- `deploy-autocommitmessage.ps1` removes the legacy `WellBased_Copilot_StudioPro10.dll` file if found.
- Some `mx dump-mpr` environment issues are handled gracefully as unavailable analysis rather than pane crashes.
