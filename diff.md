# Diff overview: mcp-read-changes-timeout

## Iteration 1 - 2026-06-18

### Summary
The MCP `read_changes` and `export_changes` tools now run Mendix model analysis with an internal timeout below the MCP client timeout. `mx.exe dump-mpr` is cancellation-aware and kills the child process when the MCP budget expires, so slow Mendix dumps return a normal parser payload with an analysis-unavailable row instead of letting the MCP call fail at the transport layer.

### Files changed

| File | Change |
|---|---|
| `ACM/mcp-server/AcmTools.cs` | modified - added a 100 second MCP analysis budget for `read_changes` and `export_changes`. |
| `ACM/parserfolder/Processing/Services/AutoCommitMessageChangeService.cs` | modified - threaded cancellation through `ReadChanges` and MPR analysis. |
| `ACM/parserfolder/Processing/Services/MxToolService.cs` | modified - made `dump-mpr` cancellation-aware and ensured cancelled `mx.exe` processes are killed. |

### Dev-library
- **Skills used**: None. The dev-library index currently contains no topic skills.
- **Skills created**: None.
- **Built without a skill**: MCP timeout handling, synchronous process cancellation, and parser cancellation propagation.

### Review verdict - timeout fix

MUST FIX:
- None.

SHOULD FIX:
- None.

NICE TO HAVE:
- Add a focused unit/integration test around MCP timeout behavior with a fake long-running dump process. The current design is testable, but the existing code does not have a seam for replacing `mx.exe` execution cleanly without expanding scope.

Security check: N/A
Verdict: CLEAN

### Verification
- `dotnet build ACM/mcp-server/AutoCommitMessage.Mcp.csproj -c Release` passed with one pre-existing nullable warning in `AutoCommitMessageChangeService.cs`.
- `dotnet test ACM/parserfolder/tests/AutoCommitMessage.Tests.csproj -c Release --no-build --filter MendixV2ChangedModuleDetectorTests` passed: 12/12.
- Full parser test suite was run and still has 32 unrelated existing failures in model-diff/cache tests.

### Open points
- The current Codex MCP transport closed after stopping the stale `AutoCommitMessage.Mcp` process that was locking the rebuilt DLL, so the live MCP tool could not be re-invoked inside this same session. A new MCP client session should pick up the rebuilt binary.
