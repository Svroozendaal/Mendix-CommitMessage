# Model Diff

## Purpose

`ModelDiff/` turns Mendix dump JSON into semantic model changes and groups those changes for export/UI/commit-message use.

## Files

- `MendixModelDiffService.cs` compares two `mx dump-mpr` JSON files and emits sorted `MendixModelChange` rows.
- `MendixModelChangeStructurer.cs` groups rows by module and category, and folds association summaries into their parent entity rows where possible.
- `MendixMprFormatDetector.cs` detects whether a project uses MPR v2 by checking for `mprcontents`.
- `MendixV2ChangedModuleDetector.cs` detects changed modules from Git status paths under `mprcontents` so the UI can pre-filter expensive analysis.

## Diff Semantics

`MendixModelDiffService` builds resource snapshots by `$ID`, classifies added/deleted/modified resources, ignores layout-only noise where appropriate, and adds resource-specific details for important Mendix element types.

Supported/recognised areas include entities, non-persistent entities, associations, enumerations, pages/snippets/layout-like resources, microflows, nanoflows, constants, scheduled events, consumed REST services, published REST/OData services, Java actions, mappings, workflows, and generic resources.

## Grouping Semantics

`MendixModelChangeStructurer` derives the module from the prefix before the first dot in `ElementName`. It groups known element types into domain model, microflows, pages, nanoflows, and resources. Association changes are promoted into domain-model entity details when the parent entity can be parsed.

## Maintenance Notes

- Add a focused test for every new diff rule or element type.
- Keep noisy structural/layout changes out of semantic output unless they affect commit-message value.
- If a new detail format is introduced here, check whether the formatter needs a matching compact display rule.
