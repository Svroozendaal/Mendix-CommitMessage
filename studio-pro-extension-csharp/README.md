# AutoCommitMessage Extension (Studio Pro 10)

Mendix Studio Pro extension that shows uncommitted Git changes for Mendix project files (`.mpr`, `.mprops`) in a dockable pane.

## Build

```powershell
dotnet build .\studio-pro-extension-csharp\AutoCommitMessage.csproj -c Debug
```

Output:

- `studio-pro-extension-csharp\bin\Debug\net8.0-windows\AutoCommitMessage.dll`
- `studio-pro-extension-csharp\bin\Debug\net8.0-windows\manifest.json`

## Deploy to a Mendix app

Use the root deploy script:

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

Set a custom data root for Phase 6 exports:

```powershell
.\deploy-autocommitmessage.ps1 -DataRootPath "C:\Path\To\Mendix-AutoCommitMessage\mendix-data"
```

This deploys to:

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

- No localhost UI dependency.
- No `npm run dev` is required.
- Model analysis now detects `Added`, `Modified`, and `Deleted` resources beyond only pages/microflows, including nanoflows and domain model resources (`Entity`, `Association`, `Enumeration`) with nested-change attribution.
- Microflow model details now include action usage summaries (for example, `CreateObjectAction x1, ChangeObjectAction x2`).
- Domain model entity details now include attribute names added in the change set.
- Export operations persist full HEAD/working model dumps for changed `.mpr` files under `mendix-data\dumps`.
