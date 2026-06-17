using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoCommitMessage.Mcp;

/// <summary>
/// Local stdio MCP server that exposes the AutoCommitMessage parser as tools.
///
/// It is launched on demand by an MCP-capable client (Claude Code, GitHub Copilot,
/// Cursor, …) as a child process and communicates over stdio. There is no network
/// port and nothing to host: the parser needs local access to the .mpr working copy,
/// the local git tree, and the local Studio Pro mx.exe, so the server runs on the
/// same machine as those.
/// </summary>
internal static class Program
{
    private static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        // stdio MCP uses stdout for the JSON-RPC channel; every log line must go to stderr.
        builder.Logging.AddConsole(options => options.LogToStandardErrorThreshold = LogLevel.Trace);
        builder.Logging.SetMinimumLevel(LogLevel.Warning);

        builder.Services
            .AddMcpServer()
            .WithStdioServerTransport()
            .WithToolsFromAssembly();

        await builder.Build().RunAsync();
    }
}
