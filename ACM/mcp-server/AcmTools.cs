using System.ComponentModel;
using System.Text.Json;
using LibGit2Sharp;
using ModelContextProtocol.Server;

namespace AutoCommitMessage.Mcp;

/// <summary>
/// MCP tools that wrap the AutoCommitMessage parser services. Each tool is a thin adapter over the
/// same parser library the local web app uses, so behaviour stays identical across both surfaces.
/// </summary>
[McpServerToolType]
internal static class AcmTools
{
    private static readonly JsonSerializerOptions JsonOut = new() { WriteIndented = true };
    private static readonly JsonSerializerOptions JsonIn = new() { PropertyNameCaseInsensitive = true };

    [McpServerTool(Name = "read_changes")]
    [Description("Read uncommitted Mendix model changes (.mpr) for a project working copy without writing anything. " +
                 "Returns the branch and a per-module list of model changes with ready-to-use displayText lines. " +
                 "Use this to confirm the right app/branch is checked out before exporting.")]
    public static string ReadChanges(
        [Description("Absolute path to the Mendix app git working copy that contains the .mpr file.")]
        string projectPath)
    {
        try
        {
            var payload = AutoCommitMessageChangeService.ReadChanges(projectPath);
            return Json(new
            {
                success = true,
                isGitRepo = payload.IsGitRepo,
                branch = payload.BranchName,
                error = payload.Error,
                changes = Flatten(payload),
            });
        }
        catch (Exception ex)
        {
            return Error(ex);
        }
    }

    [McpServerTool(Name = "export_changes")]
    [Description("Export the uncommitted Mendix model changes to the shared mendix-data folder: writes a raw-changes " +
                 "JSON payload (always) and the full mx dump-mpr artifacts. Returns the output path and a flattened " +
                 "list of changes with displayText. Fails if the project is not a git repo or has no uncommitted .mpr changes.")]
    public static string ExportChanges(
        [Description("Absolute path to the Mendix app git working copy that contains the .mpr file.")]
        string projectPath,
        [Description("Optional absolute path to the mendix-data data root. Defaults to ACM_DATA_ROOT / MENDIX_GIT_DATA_ROOT env, then the built-in default.")]
        string? dataRoot = null)
    {
        try
        {
            var resolvedDataRoot = ResolveDataRoot(dataRoot);
            var payload = AutoCommitMessageChangeService.ReadChanges(
                projectPath,
                persistModelDumps: true,
                dataRootBasePath: resolvedDataRoot);

            var outputPath = AutoCommitMessageExportService.ExportChanges(payload, projectPath, resolvedDataRoot);

            return Json(new
            {
                success = true,
                outputPath,
                branch = payload.BranchName,
                changeCount = payload.Changes.Count,
                dataRoot = resolvedDataRoot,
                changes = Flatten(payload),
            });
        }
        catch (Exception ex)
        {
            return Error(ex);
        }
    }

    [McpServerTool(Name = "store_commit_message")]
    [Description("Store a commit message under mendix-data/Commit messages as <storyId>_<signature>_<date>.txt, " +
                 "prefixed with the project's current git short hash. Use after authoring the message from export_changes output.")]
    public static string StoreCommitMessage(
        [Description("Absolute path to the Mendix app git working copy.")]
        string projectPath,
        [Description("Story/ticket id, e.g. SH-2086.")]
        string storyId,
        [Description("Author signature, e.g. SvR.")]
        string signature,
        [Description("The full commit message body to store.")]
        string message,
        [Description("Optional absolute path to the mendix-data data root. Defaults to env / built-in default.")]
        string? dataRoot = null)
    {
        try
        {
            var resolvedDataRoot = ResolveDataRoot(dataRoot);
            var shortHash = GitShortHash(projectPath);
            if (string.IsNullOrWhiteSpace(shortHash))
            {
                return Json(new { success = false, error = "Unable to determine git commit hash for the project." });
            }

            var outputPath = AutoCommitMessageCommitMessageStoreService.StoreCommitMessage(
                message,
                storyId,
                signature,
                shortHash,
                projectPath,
                commitMessagesBasePath: resolvedDataRoot);

            return Json(new { success = true, outputPath, storyId, signature, shortHash });
        }
        catch (Exception ex)
        {
            return Error(ex);
        }
    }

    [McpServerTool(Name = "list_apps")]
    [Description("List the Mendix customers/apps/branches known to the registry (mendix-data/apps-registry.json), with " +
                 "their on-disk working-copy paths, plus the installed Mendix versions and their mx.exe locations. " +
                 "Lets a client resolve an app by name without the user supplying a path.")]
    public static string ListApps(
        [Description("Optional absolute path to the mendix-data data root. Defaults to env / built-in default.")]
        string? dataRoot = null)
    {
        try
        {
            var registry = LoadRegistry(ResolveDataRoot(dataRoot));
            return Json(new
            {
                success = true,
                customers = registry?.Customers ?? new List<CustomerEntry>(),
                mendixVersions = registry?.MendixVersions ?? new Dictionary<string, string>(),
            });
        }
        catch (Exception ex)
        {
            return Error(ex);
        }
    }

    [McpServerTool(Name = "resolve_app")]
    [Description("Resolve a Mendix app from the registry by app name or by a story id (its prefix maps to an app). " +
                 "Returns the customer, app, the resolved branch (with projectPath), and the mx.exe location for the " +
                 "branch's Mendix version, so the parser can be driven without a path from the user.")]
    public static string ResolveApp(
        [Description("App name (e.g. SelektBouw) or a story id (e.g. SH-2086, whose prefix SH maps to an app).")]
        string appOrStoryId,
        [Description("Optional absolute path to the mendix-data data root. Defaults to env / built-in default.")]
        string? dataRoot = null)
    {
        try
        {
            var registry = LoadRegistry(ResolveDataRoot(dataRoot));
            var customers = registry?.Customers ?? new List<CustomerEntry>();
            var query = appOrStoryId.Trim();
            var prefix = query.Contains('-') ? query[..query.IndexOf('-')] : query;

            var hit = customers
                .SelectMany(c => (c.Apps ?? new List<AppEntry>()).Select(a => (Customer: c, App: a)))
                .FirstOrDefault(x =>
                    string.Equals(x.App.Name, query, StringComparison.OrdinalIgnoreCase) ||
                    x.App.StoryPrefixes?.Any(p => string.Equals(p, prefix, StringComparison.OrdinalIgnoreCase)) == true);

            if (hit.App is null)
            {
                return Json(new { success = false, error = $"No app found for '{appOrStoryId}'.", customers });
            }

            var branches = hit.App.Branches ?? new List<BranchEntry>();

            // Prefer the branch that knows this story id; otherwise the most recently used branch.
            var branch =
                branches.FirstOrDefault(b => b.KnownStoryIds?.Any(s => string.Equals(s, query, StringComparison.OrdinalIgnoreCase)) == true) ??
                branches.OrderByDescending(b => b.LastUsed ?? string.Empty).FirstOrDefault();

            var versions = registry?.MendixVersions ?? new Dictionary<string, string>();
            string? mxExePath = null;
            if (!string.IsNullOrWhiteSpace(branch?.MendixVersion))
            {
                versions.TryGetValue(branch!.MendixVersion!, out mxExePath);
            }

            return Json(new
            {
                success = true,
                customer = hit.Customer.Name,
                app = hit.App.Name,
                storyPrefixes = hit.App.StoryPrefixes,
                branch,
                projectPath = branch?.ProjectPath,
                mendixVersion = branch?.MendixVersion,
                mxExePath,
            });
        }
        catch (Exception ex)
        {
            return Error(ex);
        }
    }

    private static IEnumerable<object> Flatten(AutoCommitMessagePayload payload)
    {
        var rows = new List<object>();
        foreach (var change in payload.Changes)
        {
            var groups = change.ModelChangesByModule;
            if (groups is null)
            {
                continue;
            }

            foreach (var group in groups)
            {
                AddRows(rows, change.FilePath, group.Module, "domainModel", group.DomainModel);
                AddRows(rows, change.FilePath, group.Module, "microflows", group.Microflows);
                AddRows(rows, change.FilePath, group.Module, "pages", group.Pages);
                AddRows(rows, change.FilePath, group.Module, "nanoflows", group.Nanoflows);
                AddRows(rows, change.FilePath, group.Module, "resources", group.Resources);
            }
        }

        return rows;
    }

    private static void AddRows(
        List<object> rows,
        string filePath,
        string module,
        string category,
        IReadOnlyList<MendixModelChange>? changes)
    {
        if (changes is null)
        {
            return;
        }

        foreach (var change in changes)
        {
            rows.Add(new
            {
                file = filePath,
                module,
                category,
                changeType = change.ChangeType,
                elementType = change.ElementType,
                elementName = change.ElementName,
                details = change.Details,
                displayText = MendixModelChangeDisplayTextFormatter.Format(change),
            });
        }
    }

    private static string ResolveDataRoot(string? dataRoot)
    {
        if (!string.IsNullOrWhiteSpace(dataRoot))
        {
            return dataRoot;
        }

        return Environment.GetEnvironmentVariable("ACM_DATA_ROOT")
            ?? Environment.GetEnvironmentVariable("MENDIX_GIT_DATA_ROOT")
            ?? ExtensionDataPaths.DataRoot;
    }

    private static AppsRegistry? LoadRegistry(string dataRoot)
    {
        var path = Path.Combine(dataRoot, "apps-registry.json");
        if (!File.Exists(path))
        {
            return null;
        }

        using var stream = File.OpenRead(path);
        return JsonSerializer.Deserialize<AppsRegistry>(stream, JsonIn);
    }

    private static string? GitShortHash(string projectPath)
    {
        if (!Repository.IsValid(projectPath))
        {
            return null;
        }

        using var repo = new Repository(projectPath);
        return repo.Head?.Tip?.Sha?[..8];
    }

    private static string Json(object value) => JsonSerializer.Serialize(value, JsonOut);

    private static string Error(Exception ex) => Json(new { success = false, error = ex.Message });

    private sealed record AppsRegistry(
        string? SchemaVersion,
        Dictionary<string, string>? MendixVersions,
        List<CustomerEntry>? Customers);

    private sealed record CustomerEntry(string Name, List<AppEntry>? Apps);

    private sealed record AppEntry(string Name, List<string>? StoryPrefixes, List<BranchEntry>? Branches);

    private sealed record BranchEntry(
        string? Name,
        string? ProjectPath,
        string? MendixVersion,
        List<string>? KnownStoryIds,
        string? LastUsed);
}
