using System.ComponentModel.Composition;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using LibGit2Sharp;
using Mendix.StudioPro.ExtensionsAPI.UI.WebServer;

namespace AutoCommitMessage;

[Export(typeof(WebServerExtension))]
public sealed class AutoCommitMessageWebServerExtension : WebServerExtension
{
    public override void InitializeWebServer(IWebServer webServer)
    {
        webServer.AddRoute(ExtensionConstants.WebServerRoutePrefix, HandleRequestAsync);
    }

    public static async Task HandleRequestAsync(
        HttpListenerRequest request,
        HttpListenerResponse response,
        CancellationToken cancellationToken)
    {
        var projectPath = NormalizeProjectPath(ReadQueryParameter(request.Url, ExtensionConstants.ProjectPathQueryKey));
        var action = ReadQueryParameter(request.Url, ExtensionConstants.ActionQueryKey);
        var dataRootBasePath = ReadQueryParameter(request.Url, ExtensionConstants.DataRootBasePathQueryKey);
        var commitMessagesBasePath = ReadQueryParameter(request.Url, ExtensionConstants.CommitMessagesBasePathQueryKey);
        var persistDumps = ReadBooleanQueryParameter(request.Url, ExtensionConstants.PersistDumpsQueryKey, defaultValue: true);
        var persistRawChanges = ReadBooleanQueryParameter(
            request.Url,
            ExtensionConstants.PersistRawChangesQueryKey,
            defaultValue: true);
        var headDumpCacheEnabled = ReadBooleanQueryParameter(
            request.Url,
            ExtensionConstants.HeadDumpCacheEnabledQueryKey,
            defaultValue: true);
        var moduleFilter = ParseModuleList(ReadQueryParameter(request.Url, ExtensionConstants.ModuleFilterQueryKey));

        if (string.Equals(action, ExtensionConstants.ExportActionValue, StringComparison.OrdinalIgnoreCase))
        {
            await HandleExportRequestAsync(
                projectPath,
                dataRootBasePath,
                persistDumps,
                persistRawChanges,
                response,
                cancellationToken,
                headDumpCacheEnabled,
                moduleFilter);
            return;
        }

        if (string.Equals(action, ExtensionConstants.StoreCommitMessageActionValue, StringComparison.OrdinalIgnoreCase))
        {
            await HandleStoreCommitMessageRequestAsync(
                request,
                projectPath,
                commitMessagesBasePath,
                response,
                cancellationToken);
            return;
        }

        if (string.Equals(action, ExtensionConstants.RefreshActionValue, StringComparison.OrdinalIgnoreCase))
        {
            await HandleRefreshRequestAsync(projectPath, response, cancellationToken, headDumpCacheEnabled, moduleFilter);
            return;
        }

        if (string.Equals(action, ExtensionConstants.ListCommitMessagesActionValue, StringComparison.OrdinalIgnoreCase))
        {
            await HandleListCommitMessagesRequestAsync(projectPath, commitMessagesBasePath, response, cancellationToken);
            return;
        }

        if (string.Equals(action, ExtensionConstants.ReadCommitMessageActionValue, StringComparison.OrdinalIgnoreCase))
        {
            var filePath = ReadQueryParameter(request.Url, ExtensionConstants.FilePathQueryKey);
            await HandleReadCommitMessageRequestAsync(projectPath, filePath, commitMessagesBasePath, response, cancellationToken);
            return;
        }

        if (string.Equals(action, ExtensionConstants.ListChangeModulesActionValue, StringComparison.OrdinalIgnoreCase))
        {
            await HandleListChangeModulesRequestAsync(projectPath, response, cancellationToken);
            return;
        }

        if (string.Equals(action, ExtensionConstants.BrowseFolderActionValue, StringComparison.OrdinalIgnoreCase))
        {
            await HandleBrowseFolderRequestAsync(response, cancellationToken);
            return;
        }

        // Handle Mendix installation detection API endpoint.
        if (request.Url?.AbsolutePath.Contains("/api/detection", StringComparison.OrdinalIgnoreCase) == true)
        {
            await HandleDetectionRequestAsync(projectPath, request.Url, response, cancellationToken);
            return;
        }

        var payload = new AutoCommitMessagePayload
        {
            IsGitRepo = false,
            BranchName = string.Empty,
            Changes = Array.Empty<AutoCommitMessageFileChange>(),
            Error = null,
        };
        var html = AutoCommitMessagePanelHtml.Render(payload, projectPath);
        var content = Encoding.UTF8.GetBytes(html);

        ApplyNoCacheHeaders(response);
        response.ContentType = "text/html; charset=utf-8";
        response.StatusCode = 200;
        response.ContentLength64 = content.Length;
        await response.OutputStream.WriteAsync(content, cancellationToken);
    }

    private static async Task HandleRefreshRequestAsync(
        string projectPath,
        HttpListenerResponse response,
        CancellationToken cancellationToken,
        bool headDumpCacheEnabled = true,
        IReadOnlyList<string>? moduleFilter = null)
    {
        var payload = await Task.Run(() => AutoCommitMessageChangeService.ReadChanges(projectPath, headDumpCacheEnabled: headDumpCacheEnabled, selectedModules: moduleFilter), cancellationToken);
        await WriteJsonResponseAsync(response, 200, payload, cancellationToken);
    }

    private static async Task HandleExportRequestAsync(
        string projectPath,
        string? dataRootBasePath,
        bool persistDumps,
        bool persistRawChanges,
        HttpListenerResponse response,
        CancellationToken cancellationToken,
        bool headDumpCacheEnabled = true,
        IReadOnlyList<string>? moduleFilter = null)
    {
        if (!persistDumps && !persistRawChanges)
        {
            await WriteJsonResponseAsync(
                response,
                400,
                new { success = false, message = "No export outputs selected." },
                cancellationToken);
            return;
        }

        var payload = await Task.Run(
            () => AutoCommitMessageChangeService.ReadChanges(
                projectPath,
                persistModelDumps: persistDumps,
                dataRootBasePath,
                headDumpCacheEnabled,
                moduleFilter),
            cancellationToken);
        if (!payload.IsGitRepo)
        {
            await WriteJsonResponseAsync(
                response,
                400,
                new { success = false, message = "Current project is not a Git repository." },
                cancellationToken);
            return;
        }

        if (!string.IsNullOrWhiteSpace(payload.Error))
        {
            await WriteJsonResponseAsync(
                response,
                500,
                new { success = false, message = payload.Error },
                cancellationToken);
            return;
        }

        if ((persistRawChanges || persistDumps) && payload.Changes.Count == 0)
        {
            await WriteJsonResponseAsync(
                response,
                400,
                new { success = false, message = "No uncommitted changes to export." },
                cancellationToken);
            return;
        }

        try
        {
            string? rawChangesOutputPath = null;
            if (persistRawChanges)
            {
                rawChangesOutputPath = await Task.Run(
                    () => AutoCommitMessageExportService.ExportChanges(payload, projectPath, dataRootBasePath),
                    cancellationToken);
            }

            var outputLabels = new List<string>();
            if (persistRawChanges)
            {
                outputLabels.Add("Raw changes");
            }

            if (persistDumps)
            {
                outputLabels.Add("Dumps");
            }

            await WriteJsonResponseAsync(
                response,
                200,
                new
                {
                    success = true,
                    message = $"Stored {string.Join(", ", outputLabels)}.",
                    outputPath = rawChangesOutputPath,
                    changeCount = payload.Changes.Count,
                    exportFolder = ExtensionDataPaths.GetExportFolder(projectPath, dataRootBasePath),
                    dumpsFolder = ExtensionDataPaths.GetDumpsFolder(projectPath, dataRootBasePath),
                },
                cancellationToken);
        }
        catch (Exception exception)
        {
            await WriteJsonResponseAsync(
                response,
                500,
                new { success = false, message = exception.Message },
                cancellationToken);
        }
    }

    private static async Task HandleStoreCommitMessageRequestAsync(
        HttpListenerRequest request,
        string projectPath,
        string? commitMessagesBasePath,
        HttpListenerResponse response,
        CancellationToken cancellationToken)
    {
        try
        {
            var requestBody = await ReadRequestBodyAsStringAsync(request, cancellationToken);
            if (string.IsNullOrWhiteSpace(requestBody))
            {
                await WriteJsonResponseAsync(
                    response,
                    400,
                    new { success = false, message = "Request body is empty." },
                    cancellationToken);
                return;
            }

            using var requestJson = JsonDocument.Parse(requestBody);
            var root = requestJson.RootElement;
            var commitMessage = root.TryGetProperty("message", out var messageProperty) &&
                                messageProperty.ValueKind == JsonValueKind.String
                ? messageProperty.GetString()
                : null;
            if (string.IsNullOrWhiteSpace(commitMessage))
            {
                await WriteJsonResponseAsync(
                    response,
                    400,
                    new { success = false, message = "Commit message is empty." },
                    cancellationToken);
                return;
            }

            // Read storyId and signature from query parameters.
            var storyId = ReadQueryParameter(request.Url, ExtensionConstants.StoryIdQueryKey) ?? string.Empty;
            var signature = ReadQueryParameter(request.Url, ExtensionConstants.SignatureQueryKey) ?? string.Empty;

            // Get the short commit hash from the git repository.
            var shortHash = await Task.Run(() => TryGetGitShortHash(projectPath), cancellationToken);
            if (string.IsNullOrWhiteSpace(shortHash))
            {
                await WriteJsonResponseAsync(
                    response,
                    400,
                    new { success = false, message = "Unable to determine git commit hash." },
                    cancellationToken);
                return;
            }

            var outputPath = await Task.Run(
                () => AutoCommitMessageCommitMessageStoreService.StoreCommitMessage(
                    commitMessage,
                    storyId,
                    signature,
                    shortHash,
                    projectPath,
                    commitMessagesBasePath),
                cancellationToken);

            await WriteJsonResponseAsync(
                response,
                200,
                new
                {
                    success = true,
                    message = "Commit message stored.",
                    outputPath,
                    outputFolder = ExtensionDataPaths.GetCommitMessagesFolder(commitMessagesBasePath, projectPath),
                },
                cancellationToken);
        }
        catch (Exception exception)
        {
            await WriteJsonResponseAsync(
                response,
                500,
                new { success = false, message = exception.Message },
                cancellationToken);
        }
    }

    private static async Task HandleDetectionRequestAsync(
        string projectPath,
        Uri? requestUrl,
        HttpListenerResponse response,
        CancellationToken cancellationToken)
    {
        try
        {
            // Read the optional override parameter from query string.
            var installRootOverride = ReadQueryParameter(requestUrl, "override");
            if (!string.IsNullOrWhiteSpace(installRootOverride))
            {
                ExtensionConfigurationService.SetInstallRootOverride(installRootOverride);
            }

            // Find the .mpr file in the project directory.
            var mprPath = FindMprFile(projectPath);
            if (string.IsNullOrWhiteSpace(mprPath))
            {
                await WriteJsonResponseAsync(
                    response,
                    400,
                    new
                    {
                        success = false,
                        message = "No .mpr file found in the project directory.",
                    },
                    cancellationToken);
                return;
            }

            // Run detection.
            var detector = new MendixInstallationDetectorService();
            var detectionResult = await Task.Run(
                () => detector.Detect(mprPath, installRootOverride),
                cancellationToken);

            ExtensionConfigurationService.SetDetectionResult(detectionResult);

            await WriteJsonResponseAsync(
                response,
                200,
                new
                {
                    success = detectionResult.Success,
                    detectedVersion = detectionResult.DetectedVersion,
                    mxExePath = detectionResult.MxExePath,
                    installRoot = detectionResult.InstallRoot,
                    failureReason = detectionResult.FailureReason,
                    warningReason = detectionResult.WarningReason,
                },
                cancellationToken);
        }
        catch (Exception exception)
        {
            await WriteJsonResponseAsync(
                response,
                500,
                new
                {
                    success = false,
                    message = exception.Message,
                },
                cancellationToken);
        }
    }

    private static async Task HandleListCommitMessagesRequestAsync(
        string projectPath,
        string? commitMessagesBasePath,
        HttpListenerResponse response,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await Task.Run(
                () => AutoCommitMessageHistoryService.ListMessages(commitMessagesBasePath ?? string.Empty, projectPath),
                cancellationToken);

            await WriteJsonResponseAsync(
                response,
                200,
                new
                {
                    success = true,
                    messages = result.Messages.Select(msg => new
                    {
                        fileName = msg.FileName,
                        storyId = msg.StoryId,
                        signature = msg.Signature,
                        date = msg.Date?.ToString("yyyy-MM-dd"),
                        filePath = msg.FilePath,
                    }),
                    folder = result.Folder,
                    folderExists = result.FolderExists,
                },
                cancellationToken);
        }
        catch (Exception exception)
        {
            await WriteJsonResponseAsync(
                response,
                500,
                new { success = false, message = exception.Message },
                cancellationToken);
        }
    }

    private static async Task HandleReadCommitMessageRequestAsync(
        string projectPath,
        string? filePath,
        string? commitMessagesBasePath,
        HttpListenerResponse response,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                await WriteJsonResponseAsync(
                    response,
                    400,
                    new { success = false, message = "File path is required." },
                    cancellationToken);
                return;
            }

            var result = await Task.Run(
                () => AutoCommitMessageHistoryService.ReadMessage(filePath, commitMessagesBasePath ?? string.Empty, projectPath),
                cancellationToken);

            await WriteJsonResponseAsync(
                response,
                200,
                new
                {
                    success = true,
                    fileName = result.FileName,
                    content = result.Content,
                },
                cancellationToken);
        }
        catch (FileNotFoundException)
        {
            await WriteJsonResponseAsync(
                response,
                404,
                new { success = false, message = "File not found." },
                cancellationToken);
        }
        catch (InvalidOperationException exception)
        {
            await WriteJsonResponseAsync(
                response,
                400,
                new { success = false, message = exception.Message },
                cancellationToken);
        }
        catch (Exception exception)
        {
            await WriteJsonResponseAsync(
                response,
                500,
                new { success = false, message = exception.Message },
                cancellationToken);
        }
    }

    private static async Task HandleListChangeModulesRequestAsync(
        string projectPath,
        HttpListenerResponse response,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!Repository.IsValid(projectPath))
            {
                await WriteJsonResponseAsync(
                    response,
                    400,
                    new
                    {
                        success = false,
                        message = "Current project is not a Git repository.",
                        mprVersion = (string?)null,
                        modules = Array.Empty<string>(),
                        supportsPreFilter = false,
                    },
                    cancellationToken);
                return;
            }

            using var repository = new Repository(projectPath);
            var repositoryRoot = repository.Info.WorkingDirectory;

            // Find the first .mpr file to determine format
            var mprFile = FindMprFile(projectPath);
            var isMprV2 = !string.IsNullOrWhiteSpace(mprFile) && MendixMprFormatDetector.IsMprV2(mprFile);

            // Detect changed modules for v2 projects
            IReadOnlyList<string> changedModules = Array.Empty<string>();
            if (isMprV2)
            {
                // Extract the relative path to mprcontents from the mpr file location
                var mprDirectory = Path.GetDirectoryName(mprFile);
                var relativeMprDir = string.IsNullOrWhiteSpace(mprDirectory)
                    ? string.Empty
                    : Path.GetRelativePath(repositoryRoot, mprDirectory);

                var mprContentsPath = string.IsNullOrWhiteSpace(relativeMprDir) || relativeMprDir == "."
                    ? "mprcontents"
                    : $"{relativeMprDir.Replace(Path.DirectorySeparatorChar, '/')}/mprcontents";

                changedModules = MendixV2ChangedModuleDetector.DetectChangedModules(repository, mprContentsPath);
            }
            var changedModuleItems = BuildChangedModuleItems(mprFile, changedModules);

            await WriteJsonResponseAsync(
                response,
                200,
                new
                {
                    success = true,
                    mprVersion = isMprV2 ? "v2" : "v1",
                    modules = changedModules,
                    moduleItems = changedModuleItems.Select(item => new { id = item.Id, name = item.Name }).ToArray(),
                    supportsPreFilter = isMprV2,
                },
                cancellationToken);
        }
        catch (Exception exception)
        {
            await WriteJsonResponseAsync(
                response,
                500,
                new
                {
                    success = false,
                    message = exception.Message,
                    mprVersion = (string?)null,
                    modules = Array.Empty<string>(),
                    supportsPreFilter = false,
                },
                cancellationToken);
        }
    }

    private static Task HandleBrowseFolderRequestAsync(
        HttpListenerResponse response,
        CancellationToken cancellationToken)
    {
        var tcs = new TaskCompletionSource<string?>();

        var thread = new Thread(() =>
        {
            try
            {
                using var dialog = new FolderBrowserDialog
                {
                    Description = "Select Mendix project folder",
                    UseDescriptionForTitle = true,
                    ShowNewFolderButton = false,
                };
                var result = dialog.ShowDialog();
                tcs.SetResult(result == DialogResult.OK ? dialog.SelectedPath : null);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();

        return tcs.Task.ContinueWith(async task =>
        {
            if (task.Exception is not null)
            {
                await WriteJsonResponseAsync(
                    response,
                    500,
                    new { success = false, path = (string?)null, message = task.Exception.InnerException?.Message ?? task.Exception.Message },
                    cancellationToken);
                return;
            }

            var selectedPath = task.Result;
            await WriteJsonResponseAsync(
                response,
                200,
                new { success = selectedPath is not null, path = selectedPath },
                cancellationToken);
        }, cancellationToken).Unwrap();
    }

    private static string? FindMprFile(string projectPath)
    {
        try
        {
            if (!Directory.Exists(projectPath))
            {
                return null;
            }

            var mprFiles = Directory.EnumerateFiles(projectPath, "*.mpr", SearchOption.TopDirectoryOnly);
            return mprFiles.FirstOrDefault();
        }
        catch
        {
            return null;
        }
    }

    private static IReadOnlyList<ChangedModuleItem> BuildChangedModuleItems(
        string? mprFilePath,
        IReadOnlyList<string> changedModules)
    {
        if (changedModules.Count == 0)
        {
            return Array.Empty<ChangedModuleItem>();
        }

        var moduleNameLookup = TryBuildModuleNameLookup(mprFilePath);
        return changedModules
            .Select(moduleId => new ChangedModuleItem(moduleId, ResolveModuleDisplayName(moduleId, moduleNameLookup)))
            .OrderBy(item => item.Name, StringComparer.OrdinalIgnoreCase)
            .ThenBy(item => item.Id, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private static string ResolveModuleDisplayName(string moduleId, IReadOnlyDictionary<string, string> moduleNameLookup)
    {
        if (moduleNameLookup.TryGetValue(moduleId, out var moduleName) &&
            !string.IsNullOrWhiteSpace(moduleName))
        {
            return moduleName;
        }

        var normalized = moduleId
            .Replace('_', ' ')
            .Replace('-', ' ')
            .Trim();
        return string.IsNullOrWhiteSpace(normalized) ? moduleId : normalized;
    }

    private static Dictionary<string, string> TryBuildModuleNameLookup(string? mprFilePath)
    {
        var moduleNameLookup = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (string.IsNullOrWhiteSpace(mprFilePath) || !File.Exists(mprFilePath))
        {
            return moduleNameLookup;
        }

        var dumpPath = Path.Combine(Path.GetTempPath(), $"autocommitmessage_module_lookup_{Guid.NewGuid():N}.json");
        try
        {
            MxToolService.DumpMpr(mprFilePath, dumpPath);

            using var stream = File.OpenRead(dumpPath);
            using var document = JsonDocument.Parse(stream);
            if (!document.RootElement.TryGetProperty("units", out var units) ||
                units.ValueKind != JsonValueKind.Array)
            {
                return moduleNameLookup;
            }

            foreach (var unit in units.EnumerateArray())
            {
                if (!TryReadJsonStringProperty(unit, "$Type", out var modelType) ||
                    !string.Equals(modelType, "Projects$Module", StringComparison.OrdinalIgnoreCase) ||
                    !TryReadJsonStringProperty(unit, "name", out var moduleName))
                {
                    continue;
                }

                moduleName = moduleName.Trim();
                if (moduleName.Length == 0)
                {
                    continue;
                }

                if (!moduleNameLookup.ContainsKey(moduleName))
                {
                    moduleNameLookup[moduleName] = moduleName;
                }

                if (TryReadJsonStringProperty(unit, "$ID", out var moduleId) &&
                    moduleId.Length > 0 &&
                    !moduleNameLookup.ContainsKey(moduleId))
                {
                    moduleNameLookup[moduleId] = moduleName;
                }
            }
        }
        catch
        {
            // Keep fallback behavior when module-name lookup cannot be resolved.
        }
        finally
        {
            try
            {
                if (File.Exists(dumpPath))
                {
                    File.Delete(dumpPath);
                }
            }
            catch
            {
                // Ignore temporary cleanup failures.
            }
        }

        return moduleNameLookup;
    }

    private static bool TryReadJsonStringProperty(JsonElement element, string propertyName, out string value)
    {
        value = string.Empty;
        if (element.ValueKind != JsonValueKind.Object ||
            !element.TryGetProperty(propertyName, out var property) ||
            property.ValueKind != JsonValueKind.String)
        {
            return false;
        }

        var rawValue = property.GetString();
        if (string.IsNullOrWhiteSpace(rawValue))
        {
            return false;
        }

        value = rawValue.Trim();
        return value.Length > 0;
    }

    private static string? TryGetGitShortHash(string projectPath)
    {
        try
        {
            if (!Repository.IsValid(projectPath))
            {
                return null;
            }

            using var repo = new Repository(projectPath);
            var head = repo.Head.Tip;
            if (head is null)
            {
                return null;
            }

            return head.Sha[..8];
        }
        catch
        {
            return null;
        }
    }

    private static string NormalizeProjectPath(string? projectPath) =>
        string.IsNullOrWhiteSpace(projectPath)
            ? Environment.GetEnvironmentVariable("MENDIX_APP_PATH") ?? Environment.CurrentDirectory
            : projectPath;

    private static IReadOnlyList<string> ParseModuleList(string? rawValue)
    {
        if (string.IsNullOrWhiteSpace(rawValue))
        {
            return Array.Empty<string>();
        }

        return rawValue
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private static string? ReadQueryParameter(Uri? requestUrl, string key)
    {
        if (requestUrl is null || string.IsNullOrWhiteSpace(requestUrl.Query))
        {
            return null;
        }

        var rawQuery = requestUrl.Query.TrimStart('?');
        if (string.IsNullOrWhiteSpace(rawQuery))
        {
            return null;
        }

        var pairs = rawQuery.Split('&', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var pair in pairs)
        {
            var separator = pair.IndexOf('=', StringComparison.Ordinal);
            var rawKey = separator >= 0 ? pair[..separator] : pair;
            var decodedKey = Uri.UnescapeDataString(rawKey.Replace('+', ' '));
            if (!string.Equals(decodedKey, key, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var rawValue = separator >= 0 ? pair[(separator + 1)..] : string.Empty;
            return Uri.UnescapeDataString(rawValue.Replace('+', ' '));
        }

        return null;
    }

    private static bool ReadBooleanQueryParameter(Uri? requestUrl, string key, bool defaultValue)
    {
        var rawValue = ReadQueryParameter(requestUrl, key);
        if (string.IsNullOrWhiteSpace(rawValue))
        {
            return defaultValue;
        }

        if (bool.TryParse(rawValue, out var parsedValue))
        {
            return parsedValue;
        }

        return rawValue.Trim() switch
        {
            "1" => true,
            "0" => false,
            "yes" => true,
            "no" => false,
            "on" => true,
            "off" => false,
            _ => defaultValue,
        };
    }

    private static async Task<string> ReadRequestBodyAsStringAsync(
        HttpListenerRequest request,
        CancellationToken cancellationToken)
    {
        var encoding = request.ContentEncoding ?? Encoding.UTF8;
        using var reader = new StreamReader(request.InputStream, encoding, detectEncodingFromByteOrderMarks: true);
        return await reader.ReadToEndAsync(cancellationToken);
    }

    private static async Task WriteJsonResponseAsync(
        HttpListenerResponse response,
        int statusCode,
        object payload,
        CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(payload);
        var content = Encoding.UTF8.GetBytes(json);

        ApplyNoCacheHeaders(response);
        response.ContentType = "application/json; charset=utf-8";
        response.StatusCode = statusCode;
        response.ContentLength64 = content.Length;
        await response.OutputStream.WriteAsync(content, cancellationToken);
    }

    private static void ApplyNoCacheHeaders(HttpListenerResponse response)
    {
        response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
        response.Headers["Pragma"] = "no-cache";
        response.Headers["Expires"] = "0";
    }

    private sealed record ChangedModuleItem(string Id, string Name);
}
