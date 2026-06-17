# Changes Pane and In-App Review

## Status

- `DONE`

## Goal

Give developers immediate visibility into relevant uncommitted Mendix changes.

> Historical note: this feature began as a Studio Pro dockable pane. The live
> implementation is the localhost web app served from `ACM/applicatiefolder/`;
> the UI references below point to that app.

## Current behaviour

1. Serves the ACM UI on `localhost` (`http://localhost:3109`).
2. Exposes actions to load and refresh the view.
3. Renders the web UI via the internal web handler.
4. Shows:
   - branch name
   - changed files table (`*.mpr`, `*.mprops`)
   - file diff section
   - grouped model-change section for `.mpr` files
5. Supports manual `Refresh` to recompute repository and model analysis.

## Implementation references

- `ACM/applicatiefolder/Program.cs`
- `ACM/applicatiefolder/Web/AutoCommitMessageWebServerExtension.cs`
- `ACM/applicatiefolder/Web/AutoCommitMessagePanelHtml.cs`
- `ACM/applicatiefolder/Web/AutoCommitMessageStyleLibrary.cs`

## Constraints

- UI is embedded HTML/JS served by the app runtime.
- Diff text for `.mpr` is binary/unavailable by design; semantic model changes are shown instead.

## Improvement opportunities

1. Add loading/performance hints for long-running refresh operations.
2. Add inline guidance when model analysis is skipped or partially unavailable.
3. Improve accessibility and keyboard navigation in file/model tables.
