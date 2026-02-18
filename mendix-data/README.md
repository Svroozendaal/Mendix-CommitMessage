# Mendix Data Folder Contract

This folder is the shared Phase 6 and Phase 7 data root.

Subfolders:

- `exports`: raw change exports written by the Studio Pro extension.
- `processed`: raw files successfully consumed by the parser watcher.
- `errors`: raw files that failed parser processing.
- `structured`: structured parser outputs derived from raw exports.
- `dumps`: full `mx dump-mpr` working/HEAD artifacts persisted during export for deep model inspection.

Override at runtime with environment variable `MENDIX_GIT_DATA_ROOT`.

## Running the parser

Do not run `MendixCommitParser` as a bare term in PowerShell.

Use one of these commands from the repository root:

```powershell
dotnet run --project .\MendixCommitParser\MendixCommitParser.csproj
```

or after building:

```powershell
.\MendixCommitParser\bin\Debug\net8.0\MendixCommitParser.exe
```

## Structured Output Notes

Structured commit files include:

- `modelChanges`: flattened model-level changes from `.mpr` analysis.
- `modelDumpArtifacts`: persisted dump artifact paths when available in the raw export.
