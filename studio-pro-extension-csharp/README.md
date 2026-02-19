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

Set shared local defaults in a repo-root `.env` file (copy from `.env.example`):

```dotenv
MENDIX_APP_PATH=C:\MendixWorkers\Smart Expenses app-main
MENDIX_DATA_ROOT=C:\Workspace\Mendix-AutoCommitMessage\mendix-data
```

The deploy script uses `MENDIX_APP_PATH` and `MENDIX_DATA_ROOT` automatically when parameters are not provided.

Or specify a custom app path directly:

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

The extension writes export files to `<DataRootPath>\exports` and keeps the parser contract folders available:

- `<DataRootPath>\exports`
- `<DataRootPath>\processed`
- `<DataRootPath>\errors`
- `<DataRootPath>\structured`
- `<DataRootPath>\dumps`

## Start the Mendix app quickly

Use the root launcher script:

```powershell
.\start-mendix-app.ps1
```

It reads `MENDIX_APP_PATH` from `.env`, locates `studiopro.exe`, and starts Studio Pro with extension development enabled using `--enable-extension-development`.  
You can still override the path:

```powershell
.\start-mendix-app.ps1 -AppPath "C:\Workspaces\Mendix\YourApp"
```

Optional: pin a specific Studio Pro executable:

```powershell
.\start-mendix-app.ps1 -StudioProPath "C:\Program Files\Mendix\10.x.x.x\modeler\studiopro.exe"
```

## Notes

- No localhost web server or `npm` workflow is required.
- Pane URL contains a cache-buster token per open to avoid stale UI rendering.
- Export mode persists dump artifacts in `mendix-data\dumps` for deeper offline inspection.
