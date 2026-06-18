# Parser Tests

## Purpose

`tests/` contains xUnit coverage for parser services, model-diff rules, formatting rules, installation detection, cache behaviour, and MPR format helpers.

## Test Project

`AutoCommitMessage.Tests.csproj` targets the parser library and exercises internals through `InternalsVisibleTo`.

## Coverage Areas

- `MendixModelDiffService*Tests.cs` cover element-specific dump diff rules for constants, consumed/published REST services, scheduled events, Java actions, pages, flows, and other model details.
- `MendixModelChangeDisplayTextFormatterTests.cs` covers final display text wording and noise suppression.
- `MendixInstallationDetectorServiceTests.cs` covers Studio Pro / `mx.exe` detection.
- `MendixMprFormatDetectorTests.cs` covers MPR v1/v2 detection.
- `MendixV2ChangedModuleDetectorTests.cs` covers changed-module extraction for v2 `mprcontents`.
- `AutoCommitMessageHeadDumpCacheServiceTests.cs` covers HEAD dump cache pathing, hits, writes, pruning, and sanitization.

## Maintenance Notes

- Add tests close to the behaviour being changed.
- For new diff rules, assert both raw `Details` and derived `DisplayText` when wording matters.
- Keep temporary test data isolated and disposable.
