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

Or specify a custom app path:

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

## Notes

- No localhost UI dependency.
- No `npm run dev` is required.
- Model analysis now detects `Added`, `Modified`, and `Deleted` resources beyond only pages/microflows, including nanoflows and domain model resources (`Entity`, `Association`, `Enumeration`) with nested-change attribution.
