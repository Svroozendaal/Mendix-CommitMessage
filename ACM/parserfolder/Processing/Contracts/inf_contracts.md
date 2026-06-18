# Parser Contracts

## Purpose

`Contracts/` defines the records passed between parser services, the web UI, exports, tests, and MCP responses.

## Records

- `AutoCommitMessagePayload` is the top-level result of reading a project. It contains Git status, branch name, filtered changes, and an optional error string.
- `AutoCommitMessageFileChange` represents one changed `.mpr` or `.mprops` file, including status, staged flag, diff text, optional model changes, grouped model changes, and optional dump artifact paths.
- `MendixModelChange` is one semantic model change with `ChangeType`, `ElementType`, `ElementName`, optional `Details`, and computed `DisplayText`.
- `MendixModuleChangeGroup` groups model changes per Mendix module into `DomainModel`, `Microflows`, `Pages`, `Nanoflows`, and `Resources`.
- `ModelDumpArtifact` records persisted working and HEAD dump paths for debugging or export traceability.

## Contract Rules

- `ChangeType` values are normally `Added`, `Modified`, or `Deleted`.
- `ElementName` usually includes the module prefix, such as `ModuleName.ElementName`.
- `DisplayText` is derived, not stored; changing formatter behaviour changes all consumers.
- Export JSON uses these records as source data but serializes a dedicated export shape in `AutoCommitMessageExportService`.

## Maintenance Notes

- Treat these records as cross-surface contracts.
- Prefer additive changes when possible.
- If a contract changes, check app rendering, MCP flattening, export JSON, and tests.
