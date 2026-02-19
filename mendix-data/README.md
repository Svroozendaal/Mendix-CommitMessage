# Mendix Data Folder Contract

This folder is the shared Phase 6 and Phase 7 data root.

Subfolders:

- `exports`: raw change exports written by the Studio Pro extension.
- `processed`: raw files successfully consumed by the parser watcher.
- `errors`: raw files that failed parser processing.
- `structured`: structured parser outputs derived from raw exports.
- `dumps`: full `mx dump-mpr` working/HEAD artifacts persisted during export for deep model inspection.

Override at runtime with environment variable `MENDIX_GIT_DATA_ROOT`.

## Schema mapping

- `exports/*.json`: raw extension payload (`schemaVersion: 1.0`).
- `structured/*.json`: enriched parser payload (`schemaVersion: 2.0`).

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

- `sourceFileName`: original raw export filename.
- `files`: normalised file-level summaries (status kind, tags, diff line count, staged state).
- `modelChanges`: flattened model-level changes from `.mpr` analysis.
- `modelSummary`: aggregate breakdowns and extracted highlights:
  - by element type, change type, and file
  - microflow action usage plus examples
  - domain model entities and added attributes
- `modelDumpArtifacts`: persisted dump artifact paths when available in the raw export.
- `commitMessageContext`: pre-computed commit drafting hints (`suggestedType`, scopes, subject, highlights, risks).

## Dump inspection quick path

1. Open a raw/processed export file and read `changes[*].modelDumpArtifact`.
2. Load both `workingDumpPath` and `headDumpPath`.
3. Compare object IDs and resource ownership to reconstruct added/modified/deleted model resources.
4. Use extracted detail strings in `changes[*].modelChanges[*].details` as parser input for action/domain summaries.
