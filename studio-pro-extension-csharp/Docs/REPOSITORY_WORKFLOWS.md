# Repository Workflows

## Purpose

Describes current development and runtime workflows for the Studio Pro extension.

## Prerequisites

- Windows environment (Studio Pro + `mx.exe`).
- .NET SDK supporting `net8.0-windows`.
- Mendix Studio Pro 10 installation.
- Local Mendix app repository containing at least one `.mpr`.

## Key paths

- Extension project: `studio-pro-extension-csharp/AutoCommitMessage.csproj`
- Extension manifest: `studio-pro-extension-csharp/manifest.json`
- Deploy script: `deploy-autocommitmessage.ps1`
- Studio Pro launcher: `start-mendix-app.ps1`
- Default data root: `<project-or-configured-base>/mendix-data`

## `.env` keys used by scripts

- `MENDIX_APP_PATH`
- `MENDIX_DATA_ROOT`
- `MENDIX_STUDIOPRO_EXE`

## Build and deploy workflow

Command:

```powershell
.\deploy-autocommitmessage.ps1
```

What it does:

1. Resolves app/data paths from parameters or `.env`.
2. Builds extension with `MendixDataRoot` MSBuild property.
3. Copies `AutoCommitMessage.dll` and `manifest.json` to app `extensions/AutoCommitMessage`.
4. Ensures data folders exist.

Useful variants:

```powershell
.\deploy-autocommitmessage.ps1 -Configuration Release
.\deploy-autocommitmessage.ps1 -AppPath "C:\Workspaces\Mendix\YourApp"
.\deploy-autocommitmessage.ps1 -DataRootPath "D:\MendixData\AutoCommit"
.\deploy-autocommitmessage.ps1 -SkipBuild
```

## Run workflow (Studio Pro)

Command:

```powershell
.\start-mendix-app.ps1
```

What it does:

1. Resolves app path and Studio Pro executable.
2. Locates first `.mpr` in app root.
3. Launches Studio Pro with `--enable-extension-development`.

## In-pane runtime workflow

1. Open pane (`Open AutoCommitMessage`).
2. Press `Refresh` to load repository + model analysis.
3. Inspect grouped model changes and optional diff drawer.
4. Optional `Export` to persist selected outputs:
   - raw changes
   - dumps
5. Optional commit-message flows:
   - create/copy from model changes
   - store to disk
   - browse history

## Data-output workflow

Current runtime output folders under `mendix-data`:

- `raw-changes`
- `processed` (reserved)
- `errors` (reserved)
- `dumps`

Commit-message storage folder:

- `<base>/Commit messages`

## Release hygiene

- Keep extension naming aligned with `AutoCommitMessage*`.
- Do not commit generated `bin/` and `obj/` artefacts.
- Rebuild when `MendixDataRoot` or contract-affecting behaviour changes.
- Coordinate schema changes with downstream parser owners.
