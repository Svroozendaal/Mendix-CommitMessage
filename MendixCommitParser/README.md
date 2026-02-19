# MendixCommitParser

Console watcher that consumes extension exports from `mendix-data/exports` and writes structured commit-analysis JSON to `mendix-data/structured`.

## Purpose

Transform raw export payloads (`schemaVersion: 1.0`) into commit-generation-friendly structured outputs (`schemaVersion: 2.0`) with:

- normalised file summaries
- model-change aggregations
- microflow/domain highlights
- pre-computed commit-message context

## Runtime flow

1. Resolve data root from `MENDIX_GIT_DATA_ROOT`, repository root fallback, or local app data fallback.
2. Ensure `exports`, `processed`, `errors`, `structured` folders exist.
3. On start, queue existing `exports/*.json` files (old backlog safety).
4. Watch for new JSON files.
5. Parse and enrich each file via `CommitParserService.ProcessFile(...)`.
6. Save structured JSON to `structured/<commitId>.json`.
7. Move raw file to:
   - `processed` on success
   - `errors` on parse/runtime failure

## Structured schema 2.0

Main top-level fields:

- `schemaVersion`
- `commitId`
- `sourceFileName`
- `timestamp`, `projectName`, `branchName`, `userName`, `userEmail`
- `entities`, `affectedFiles`, `metrics`
- `files` (normalised file-level metadata and tags)
- `modelChanges` (flattened model rows from export)
- `modelSummary` (aggregates by element/change/file + microflow/domain summaries)
- `modelDumpArtifacts` (persisted dump locations from export)
- `commitMessageContext` (`suggestedType`, `suggestedScopes`, `suggestedSubject`, `highlights`, `risks`)

## Key implementation files

- `MendixCommitParser/Services/CommitParserService.cs`
- `MendixCommitParser/Services/FileWatcherService.cs`
- `MendixCommitParser/Services/EntityExtractorService.cs`
- `MendixCommitParser/Storage/JsonStorage.cs`
- `MendixCommitParser/Models/RawCommitData.cs`
- `MendixCommitParser/Models/StructuredCommitData.cs`

## Run

From repository root:

```powershell
dotnet run --project .\MendixCommitParser\MendixCommitParser.csproj
```

Or run compiled executable:

```powershell
.\MendixCommitParser\bin\Debug\net8.0\MendixCommitParser.exe
```

## Notes

- Parsing of microflow action details depends on detail text format emitted by `studio-pro-extension-csharp/MendixModelDiffService.cs`.
- `.mpr` binary diff risks are surfaced in `commitMessageContext.risks`.
- Duplicate raw filenames are deconflicted when moved to `processed`.
