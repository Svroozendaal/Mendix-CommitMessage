using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using AutoCommitMessage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoCommitMessage.Standalone;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        LoadDotEnv();
        ApplyPathArgument(args);

        var preferredPort = ResolvePort(args);
        var port = ResolveUsablePort(preferredPort);
        if (port != preferredPort)
        {
            Console.WriteLine($"Port {preferredPort} is unavailable on this machine; using {port} instead.");
        }

        var url = $"http://localhost:{port}";
        var openBrowser = !HasFlag(args, "--no-browser");

        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseUrls(url);
        // Keep the console quiet — this is an end-user app, not a server log.
        builder.Logging.ClearProviders();
        builder.Logging.AddSimpleConsole(options => options.SingleLine = true);
        builder.Logging.SetMinimumLevel(LogLevel.Warning);

        var app = builder.Build();

        // Single terminal handler — delegate every request to the shared router.
        app.Run(async context =>
        {
            await AutoCommitMessageWebServerExtension.HandleRequestCoreAsync(
                new KestrelRequestAdapter(context.Request),
                new KestrelResponseAdapter(context.Response),
                context.RequestAborted);
        });

        try
        {
            await app.StartAsync();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to start the web server on {url}: {ex.Message}");
            Console.Error.WriteLine($"The port may be in use. Try a different one with --port <number>.");
            Environment.Exit(1);
            return;
        }

        Console.WriteLine($"AutoCommitMessage running at {url}");
        Console.WriteLine("Press Ctrl+C to stop.");

        if (openBrowser)
        {
            TryOpenBrowser(url);
        }

        await app.WaitForShutdownAsync();
        Console.WriteLine("Server stopped.");
    }

    private static void TryOpenBrowser(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch
        {
            Console.WriteLine($"Open {url} in your browser to get started.");
        }
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
    /// Returns the preferred port if it can actually be bound on loopback, otherwise an
    /// OS-assigned free port. Some Windows machines reserve/exclude port ranges (Hyper-V, WSL,
    /// Docker), so binding the default port can fail with a socket-access error — in that case we
    /// transparently fall back rather than crashing.
    /// </summary>
    private static int ResolveUsablePort(int preferredPort)
    {
        if (CanBind(preferredPort))
        {
            return preferredPort;
        }

        // Ask the OS for any free loopback port.
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var freePort = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return freePort;
    }

    private static bool CanBind(int port)
    {
        TcpListener? listener = null;
        try
        {
            listener = new TcpListener(IPAddress.Loopback, port);
            listener.Start();
            return true;
        }
        catch (SocketException)
        {
            return false;
        }
        finally
        {
            listener?.Stop();
        }
    }

    private static bool HasFlag(string[] args, string flag) =>
        args.Any(arg => string.Equals(arg, flag, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Reads an optional <c>--path &lt;dir&gt;</c> argument and exposes it as MENDIX_APP_PATH, the
    /// variable the shared handler falls back to when no projectPath query parameter is supplied.
    /// </summary>
    private static void ApplyPathArgument(string[] args)
    {
        for (var i = 0; i < args.Length - 1; i++)
        {
            if (string.Equals(args[i], "--path", StringComparison.OrdinalIgnoreCase) &&
                !string.IsNullOrWhiteSpace(args[i + 1]))
            {
                Environment.SetEnvironmentVariable("MENDIX_APP_PATH", args[i + 1]);
                return;
            }
        }
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

/// <summary>Adapts an ASP.NET Core <see cref="HttpRequest"/> to the shared handler abstraction.</summary>
internal sealed class KestrelRequestAdapter : IAcmHttpRequest
{
    private readonly HttpRequest _request;

    public KestrelRequestAdapter(HttpRequest request)
    {
        _request = request;
        // Reconstruct an absolute Uri so the shared query-string parsing works unchanged.
        Url = new Uri($"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}{request.QueryString}");
    }

    public Uri? Url { get; }

    public Encoding ContentEncoding => Encoding.UTF8;

    public Stream InputStream => _request.Body;
}

/// <summary>Adapts an ASP.NET Core <see cref="HttpResponse"/> to the shared handler abstraction.</summary>
internal sealed class KestrelResponseAdapter : IAcmHttpResponse
{
    private readonly HttpResponse _response;

    public KestrelResponseAdapter(HttpResponse response) => _response = response;

    public int StatusCode
    {
        get => _response.StatusCode;
        set => _response.StatusCode = value;
    }

    public string ContentType
    {
        get => _response.ContentType ?? string.Empty;
        set => _response.ContentType = value;
    }

    public long ContentLength64
    {
        get => _response.ContentLength ?? 0;
        set => _response.ContentLength = value;
    }

    public void SetHeader(string name, string value) => _response.Headers[name] = value;

    public Stream OutputStream => _response.Body;
}
