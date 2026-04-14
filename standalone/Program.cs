using System.Net;

namespace AutoCommitMessage.Standalone;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        LoadDotEnv();
        var port = ResolvePort(args);
        var prefix = $"http://localhost:{port}/";

        using var listener = new HttpListener();
        listener.Prefixes.Add(prefix);

        try
        {
            listener.Start();
        }
        catch (HttpListenerException ex)
        {
            Console.Error.WriteLine($"Failed to start listener on {prefix}: {ex.Message}");
            Console.Error.WriteLine("Try running as administrator, or choose a different port with --port <number>.");
            Environment.Exit(1);
        }

        Console.WriteLine($"AutoCommitMessage running at http://localhost:{port}");
        Console.WriteLine("Press Ctrl+C to stop.");

        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
        };

        while (!cts.Token.IsCancellationRequested)
        {
            HttpListenerContext context;
            try
            {
                context = await listener.GetContextAsync().WaitAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (HttpListenerException)
            {
                break;
            }

            _ = Task.Run(async () =>
            {
                try
                {
                    await AutoCommitMessageWebServerExtension.HandleRequestAsync(
                        context.Request,
                        context.Response,
                        CancellationToken.None);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Request error: {ex.Message}");
                    try { context.Response.StatusCode = 500; } catch { }
                }
                finally
                {
                    try { context.Response.Close(); } catch { }
                }
            });
        }

        listener.Stop();
        Console.WriteLine("Server stopped.");
    }

    private static int ResolvePort(string[] args)
    {
        // Check --port <number> argument
        for (var i = 0; i < args.Length - 1; i++)
        {
            if (string.Equals(args[i], "--port", StringComparison.OrdinalIgnoreCase) &&
                int.TryParse(args[i + 1], out var argPort) && argPort > 0)
            {
                return argPort;
            }
        }

        // Check PORT environment variable
        var envPort = Environment.GetEnvironmentVariable("PORT");
        if (!string.IsNullOrWhiteSpace(envPort) && int.TryParse(envPort, out var port) && port > 0)
        {
            return port;
        }

        return 3109;
    }

    /// <summary>
    /// Reads the .env file in the repo root and sets environment variables from it.
    /// Also bridges MENDIX_DATA_ROOT → MENDIX_GIT_DATA_ROOT (the name the C# code expects).
    /// </summary>
    private static void LoadDotEnv()
    {
        // Walk up from the standalone/ directory to find .env next to the solution
        var dir = Path.GetDirectoryName(AppContext.BaseDirectory);
        string? envFile = null;
        while (dir is not null)
        {
            var candidate = Path.Combine(dir, ".env");
            if (File.Exists(candidate))
            {
                envFile = candidate;
                break;
            }
            dir = Path.GetDirectoryName(dir);
        }

        if (envFile is null)
        {
            return;
        }

        foreach (var line in File.ReadAllLines(envFile))
        {
            var trimmed = line.Trim();
            if (trimmed.Length == 0 || trimmed.StartsWith('#'))
            {
                continue;
            }

            var sep = trimmed.IndexOf('=');
            if (sep <= 0)
            {
                continue;
            }

            var key = trimmed[..sep].Trim();
            var value = trimmed[(sep + 1)..].Trim();

            if (string.IsNullOrEmpty(key))
            {
                continue;
            }

            // Only set if not already overridden by the process environment
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(key)))
            {
                Environment.SetEnvironmentVariable(key, value);
            }
        }

        // Bridge MENDIX_DATA_ROOT → MENDIX_GIT_DATA_ROOT (the name ExtensionDataPaths reads)
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("MENDIX_GIT_DATA_ROOT")))
        {
            var dataRoot = Environment.GetEnvironmentVariable("MENDIX_DATA_ROOT");
            if (!string.IsNullOrEmpty(dataRoot))
            {
                Environment.SetEnvironmentVariable("MENDIX_GIT_DATA_ROOT", dataRoot);
            }
        }
    }

}
