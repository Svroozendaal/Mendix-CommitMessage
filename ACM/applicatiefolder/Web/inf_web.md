# Web UI and HTTP Adapter

## Purpose

`Web/` contains the browser UI, shared HTTP routing, styling, embedded assets, and Windows folder picker integration.

## Main Files

- `AutoCommitMessageWebServerExtension.cs` is the shared request router. It handles refresh, export, commit-message storage, history, read-message, changed-module listing, detection, image assets, folder browsing, and initial HTML rendering.
- `AutoCommitMessagePanelHtml.cs` renders the full HTML/CSS/JavaScript UI shell.
- `AutoCommitMessageStyleLibrary.cs` centralizes CSS used by the rendered panel.
- `IAcmHttp.cs` defines request/response abstractions plus HttpListener adapters.
- `ShellFolderPicker.cs` uses the Windows shell folder picker for selecting a Mendix project folder.
- `logoACM.png` and `faviconACM.png` are embedded app assets.

## HTTP Actions

Routes are controlled by `action` query values from `ExtensionConstants`:

- `refresh` reads current changes without exporting.
- `export` reads changes and optionally persists raw-changes JSON and dump artifacts.
- `store-commit-message` stores a commit message from the request body.
- `list-commit-messages` and `read-commit-message` provide commit-message history.
- `list-change-modules` detects changed modules for MPR v2 pre-filtering.
- `browse-folder` opens the local folder picker.
- `/api/detection` detects the correct Mendix Studio Pro installation.

## Maintenance Notes

- Keep response payloads JSON and no-cache headers consistent for API actions.
- Do not duplicate parser logic in UI code; call parser services.
- Query constants must stay aligned with `ExtensionConstants`.
