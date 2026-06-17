# Mendix Data Folder Contract

This folder is the shared data root for export and downstream commit-message processing.

Subfolders:

- `raw-changes`: raw change exports written by the Studio Pro extension.
- `processed`: raw files moved after successful downstream processing.
- `errors`: raw files moved when downstream processing fails.
- `dumps`: full `mx dump-mpr` working/HEAD artifacts persisted during export for deep model inspection.
- `Commit messages`: stored commit messages (`<storyId>_<signature>_<date>.txt`, `#commit:` header).

Files (not subfolders):

- `apps-registry.json`: registry of Mendix apps/branches and their on-disk working-copy locations,
  maintained by `ACM/AI-tools/ACM-Writer.md` so the parser can be driven without re-supplying paths.

Override at runtime with environment variable `MENDIX_GIT_DATA_ROOT`.

## Schema mapping

- `raw-changes/*.json`: raw extension payload (`schemaVersion: 1.0`).

## Dump inspection quick path

1. Open a raw/processed export file and read `changes[*].modelDumpArtifact`.
2. Load both `workingDumpPath` and `headDumpPath`.
3. Compare object IDs and resource ownership to reconstruct added/modified/deleted model resources.
4. Use extracted detail strings in `changes[*].modelChanges[*].details` as parser input for action/domain summaries.
