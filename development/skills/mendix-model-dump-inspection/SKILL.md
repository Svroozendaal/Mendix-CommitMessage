---
name: mendix-model-dump-inspection
description: Retrieve detailed Mendix model changes from `mx dump-mpr` JSON artifacts, including microflow action usage/details and domain model entity attribute additions. Use when analysing `working-dump.json` vs `head-dump.json`, troubleshooting model diff output, or extending model change extraction logic in this repository.
---

# MENDIX MODEL DUMP INSPECTION

## USE THIS WORKFLOW

1. Locate dump artifacts from export payloads:
- Read `modelDumpArtifact.workingDumpPath`.
- Read `modelDumpArtifact.headDumpPath`.
- If missing, trigger export from the extension first so dumps are persisted in `mendix-data/dumps/`.

2. Parse both dump files as JSON:
- Handle UTF-8 BOM if present.
- Expect root shape with `units` and nested Mendix model objects.

3. Build two snapshots (`working`, `head`) using object IDs:
- Index every object by `$ID`.
- Track parent links via `$ContainerID`.
- Register trackable resources when either rule matches:
  - `$ContainerProperty` is `documents` or `projectDocuments`.
  - `$Type` is one of `DomainModels$Entity`, `DomainModels$Association`, `DomainModels$Enumeration`.

4. Compute resource changes:
- `Added`: resource in `working` only.
- `Deleted`: resource in `head` only.
- `Modified`: same resource ID exists in both and JSON differs.

5. Add nested ownership changes:
- For changed non-resource objects, resolve owning resource by climbing parent chain.
- Increment nested added/modified/deleted counters on owner resource.

6. Add resource-specific detail extractors:
- Microflows (`Microflows$Microflow`):
  - Traverse `Microflows$ActionActivity`.
  - Read action object from `action`.
  - Count action types by `$Type` short name (for example `RetrieveAction`, `ChangeObjectAction`).
  - Build action descriptors from action fields:
    - `RetrieveAction`: `retrieveSource.$Type`, `startVariableName`, `association`, `entity`, `outputVariableName`.
    - `ChangeObjectAction`: `changeVariableName` plus changed member names from `items[*].attribute|association`.
    - `CommitAction`: `commitVariableName`.
    - `CreateObjectAction`: `entity`, `outputVariableName`, and changed members from `items`.
- Domain entities (`DomainModels$Entity`):
  - Inspect `attributes` array for `DomainModels$Attribute`.
  - Compare attribute keys between `working` and `head`.
  - Output attribute names added in the working model.

7. Emit final model changes:
- Include `changeType`, `elementType`, `elementName`, and merged detail text.
- Sort by `elementType`, `elementName`, then `changeType`.

## FIELD MAP FOR ACTION DETAILS

- `Microflows$RetrieveAction`
  - `outputVariableName`
  - `retrieveSource.$Type`
  - `retrieveSource.startVariableName`
  - `retrieveSource.association`
  - `retrieveSource.entity`

- `Microflows$ChangeObjectAction`
  - `changeVariableName`
  - `items[*].attribute`
  - `items[*].association`
  - `items[*].value` (expression text if needed)

- `Microflows$CommitAction`
  - `commitVariableName`

- `Microflows$CreateObjectAction`
  - `entity`
  - `outputVariableName`
  - `items[*].attribute|association`

## REPOSITORY ENTRY POINT

- Use `studio-pro-extension-csharp/MendixModelDiffService.cs`:
  - `CompareDumps(...)` is the main entry point.
  - `BuildMicroflowActionDetails(...)` and `BuildDomainEntityAttributeDetails(...)` implement the specialised detail extraction.

## OUTPUT EXAMPLE STYLE

- `SmartExpenses.NEW_MICROFLOW_test (Added) - actions used (3): ChangeObjectAction x1, CommitAction x1, RetrieveAction x1; action details: RetrieveAction: retrieve CurrentSession over association User_Session from Account; ChangeObjectAction: change CurrentSession (SessionId); CommitAction: commit CurrentSession`

