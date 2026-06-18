# Parser Services

## Purpose

`Services/` contains orchestration code around Git, Mendix Studio Pro tooling, data export, cache, and commit-message storage.

## Important Services

- `AutoCommitMessageChangeService` is the main parser entry point. It reads Git status, builds file-change payloads, dumps changed `.mpr` files, compares dumps, groups changes, and optionally persists dump artifacts.
- `AutoCommitMessageExportService` writes raw-changes JSON under `mendix-data/raw-changes/` with project, branch, Git user, file changes, grouped model changes, display text, and optional dump artifact paths.
- `AutoCommitMessageCommitMessageStoreService` stores commit messages under `Commit messages` with `#commit:<shortHash>` metadata and collision handling by current commit hash.
- `AutoCommitMessageHistoryService` lists and reads stored commit messages, strips the commit header, and guards against path traversal.
- `AutoCommitMessageHeadDumpCacheService` caches HEAD dump JSON per commit SHA to reduce repeated `mx.exe dump-mpr` calls.
- `MxToolService` locates compatible `mx.exe` installations and runs `mx dump-mpr`.
- `MendixInstallationDetectorService` determines the required Studio Pro version for a project and resolves the matching `mx.exe`.
- `ExtensionConfigurationService` holds the current detection result and optional install-root override in process memory.
- `AutoCommitMessageFolderMigrationService` handles legacy data-folder migration.

## Main External Dependencies

- LibGit2Sharp for repository discovery, status, diffs, HEAD tree reads, and commit hashes.
- Mendix Studio Pro `mx.exe` for `dump-mpr` and `show-version`.
- Local filesystem paths under `mendix-data`.

## Maintenance Notes

- Keep `ReadChanges` non-destructive unless explicit persistence options are enabled.
- `mx.exe` failures can be environment-specific; preserve tolerant handling for known transient dump issues.
- Cache writes and pruning are best-effort and should not block parsing.
