# AutoCommitMessage Extension (Studio Pro 10)

Studio Pro 10 dockable-pane extension for Mendix Git changes (`.mpr`, `.mprops`) with model-level `.mpr` analysis and export-ready data.

## Current behaviour

1. Filters Git status to Mendix model/config files only (`*.mpr`, `*.mprops`).
2. Renders a WebView UI with:
   - left pane: `Model changes (.mpr)` (primary, larger pane)
   - right pane: `Changed files` table and `Diff`
3. `Refresh` calls the extension refresh route and re-runs Git plus model analysis.
4. `Export` writes raw payload JSON and also persists full model dumps (`working/head`) for changed `.mpr` files.

## Build

```powershell
dotnet build .\studio-pro-extension-csharp\AutoCommitMessage.csproj -c Debug
```

Build output:

- `studio-pro-extension-csharp\bin\Debug\net8.0-windows\AutoCommitMessage.dll`
- `studio-pro-extension-csharp\bin\Debug\net8.0-windows\manifest.json`

## Deploy to Mendix app

Default target app path:

```powershell
.\deploy-autocommitmessage.ps1
```

Custom app path:

```powershell
.\deploy-autocommitmessage.ps1 -AppPath "C:\Workspaces\Mendix\YourApp"
```

Custom shared data root:

```powershell
.\deploy-autocommitmessage.ps1 -DataRootPath "C:\Path\To\Mendix-CommitMessage\mendix-data"
```

Deployment target:

- `<AppPath>\extensions\AutoCommitMessage\AutoCommitMessage.dll`
- `<AppPath>\extensions\AutoCommitMessage\manifest.json`

## Data contract folders

The deploy script ensures these folders exist under `<DataRootPath>`:

- `exports`
- `processed`
- `errors`
- `structured`
- `dumps`

## Model analysis detail level

- Resource-level change detection covers added/modified/deleted model resources.
- Microflow details include action counts and second-level descriptors (for example retrieve source, changed member assignments, commit flags, call targets).
- Domain entity details include added attribute names.
- `.mpr` textual diff remains binary fallback; semantic model diff comes from `mx.exe dump-mpr` comparison.

## Refresh and reload feedback

- Status line default: `Ready. Refresh re-runs Git + model analysis.`
- While refreshing: `Reloading Git + model changes...`
- After completion: `Reloaded Git + model changes at <time>`

## Notes

- No localhost web server or `npm` workflow is required.
- Pane URL contains a cache-buster token per open to avoid stale UI rendering.
- Export mode persists dump artifacts in `mendix-data\dumps` for deeper offline inspection.
