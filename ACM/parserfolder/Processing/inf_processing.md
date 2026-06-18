# Processing Pipeline

## Purpose

`Processing/` contains the parser implementation. It turns local Git state and Mendix dump JSON into structured ACM payloads.

## Folder Map

- `Contracts/` defines the payload and model-change records shared by all consumers.
- `Core/` defines constants and data-root path resolution.
- `Services/` orchestrates Git, `mx.exe`, export, cache, commit-message storage, history, and installation detection.
- `ModelDiff/` compares Mendix model dumps and groups changes.
- `Formatting/` turns structured changes into compact human-readable display text.

## Main Read Flow

1. `AutoCommitMessageChangeService.ReadChanges` discovers the Git repository and filters status to `.mpr` and `.mprops`.
2. Text diffs are read for `.mprops`; `.mpr` files get binary-diff placeholders plus model analysis.
3. For `.mpr` analysis, the service dumps the working model and a reconstructed HEAD model.
4. `MendixModelDiffService.CompareDumps` compares resources and nested semantic changes.
5. `MendixModelChangeStructurer.GroupByModule` groups rows into domain model, microflows, pages, nanoflows, and resources.
6. `MendixModelChange.DisplayText` delegates to the formatter for commit-message-ready text.

## Error Behaviour

The parser favours useful payloads over hard failures. Repository discovery failures return non-Git payloads, and known transient dump-environment issues return empty model-analysis results for that `.mpr`. Unexpected per-file analysis errors are represented as a `Model Analysis` change row.

## Maintenance Notes

- `MendixModelDiffService` owns raw dump semantics.
- `MendixModelChangeStructurer` owns module/category grouping and association promotion.
- `MendixModelChangeDisplayTextFormatter` owns final wording.
- Keep those boundaries clear when adding new element support.
