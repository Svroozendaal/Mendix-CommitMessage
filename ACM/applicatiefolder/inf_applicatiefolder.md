# Browser Application

## Purpose

`applicatiefolder/` builds `AutoCommitMessage.Standalone`, the localhost browser app for humans. It wraps the parser library with a Kestrel server and serves the UI from `Web/`.

## Runtime

- `Program.cs` loads optional `.env` settings, accepts `--path <dir>`, resolves a port, starts Kestrel, and opens the browser unless `--no-browser` is supplied.
- Default port is `3109`; if unavailable, the app binds a free loopback port.
- `MENDIX_APP_PATH` is used as the default project path when no `projectPath` query parameter is supplied.
- `.env` value `MENDIX_DATA_ROOT` is bridged to `MENDIX_GIT_DATA_ROOT` for parser path resolution.

## Dependencies

- References `AutoCommitMessage.Parser`.
- Uses `Microsoft.AspNetCore.App`.
- Embeds `Web/faviconACM.png` and `Web/logoACM.png` as resources with stable logical names.

## Request Handling

`Program.cs` does not implement product routes directly. It adapts ASP.NET Core request/response objects to `IAcmHttpRequest` and `IAcmHttpResponse`, then delegates every request to `AutoCommitMessageWebServerExtension.HandleRequestCoreAsync`.

## Maintenance Notes

- Keep app startup and route handling separate.
- UI behaviour belongs in `Web/`; parser behaviour belongs in `parserfolder/`.
- Use `./open-browser-app.ps1` for the normal local run path.
