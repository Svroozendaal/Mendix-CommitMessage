# `.env`-Based Developer Workflow

## Status

- `DONE`

## Goal

Make local development reproducible by centralising common paths in a repository-level `.env` file consumed by helper scripts.

## Current behaviour

Supported `.env` keys:

- `MENDIX_APP_PATH`
- `MENDIX_DATA_ROOT`
- `MENDIX_STUDIOPRO_EXE`

Workflow:

1. Configure values once in `.env`.
2. Run the build/run helper (`./open-browser-app.ps1`) without repeatedly passing paths.
3. Studio Pro / `mx.exe` is discovered automatically or via the configured executable.

## Implementation references

- `.env.example`
- `open-browser-app.ps1`

## Constraints

- Scripts are PowerShell-oriented and assume Windows execution context.
- Missing or invalid paths fail fast with explicit errors.

## Improvement opportunities

1. Add script validation command for environment checks before build/run.
2. Add optional profile support for multiple app environments.
3. Document team conventions for shared vs local-only `.env` values.
