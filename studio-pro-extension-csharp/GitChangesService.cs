using System.Text;
using LibGit2Sharp;

namespace AutoCommitMessage;

/// <summary>
/// Reads uncommitted Git changes for Mendix project files.
/// </summary>
public static class GitChangesService
{
    private static readonly string[] FilteredPathSpec = { "*.mpr", "*.mprops" };

    private const string StatusModified = "Modified";
    private const string StatusAdded = "Added";
    private const string StatusDeleted = "Deleted";
    private const string StatusRenamed = "Renamed";

    private const string BinaryDiffMessage = "Binary file changed - diff not available";
    private const string DiffUnavailableMessage = "Diff unavailable";

    /// <summary>
    /// Reads the current repository status and diff data for supported Mendix files.
    /// </summary>
    /// <param name="projectPath">The path to the project root.</param>
    /// <returns>A payload containing repository state, change items, and optional errors.</returns>
    public static GitChangesPayload ReadChanges(string projectPath)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(projectPath))
            {
                return new GitChangesPayload
                {
                    IsGitRepo = false,
                    BranchName = string.Empty,
                    Changes = Array.Empty<GitFileChange>(),
                    Error = "Project path is empty.",
                };
            }

            var discoveredPath = Repository.Discover(projectPath);
            if (string.IsNullOrWhiteSpace(discoveredPath))
            {
                return new GitChangesPayload
                {
                    IsGitRepo = false,
                    BranchName = string.Empty,
                    Changes = Array.Empty<GitFileChange>(),
                    Error = null,
                };
            }

            using var repository = new Repository(discoveredPath);
            var repositoryRoot = repository.Info.WorkingDirectory;

            var statusOptions = new StatusOptions
            {
                IncludeIgnored = false,
                IncludeUntracked = true,
                RecurseUntrackedDirs = true,
                PathSpec = FilteredPathSpec,
            };

            var statusEntries = repository.RetrieveStatus(statusOptions);
            var patch = repository.Diff.Compare<Patch>(FilteredPathSpec, includeUntracked: true);

            var changes = new List<GitFileChange>();
            foreach (var entry in statusEntries)
            {
                if (entry.State == FileStatus.Unaltered || entry.State == FileStatus.Ignored)
                {
                    continue;
                }

                var fileChange = new GitFileChange
                {
                    FilePath = entry.FilePath,
                    Status = DetermineStatus(entry.State),
                    IsStaged = IsStaged(entry.State),
                    DiffText = GetDiffText(entry.FilePath, patch),
                };

                if (entry.FilePath.EndsWith(".mpr", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        var modelChanges = AnalyzeMprChanges(
                            repository,
                            repositoryRoot,
                            NormalizeRepositoryPath(entry.FilePath));

                        fileChange = fileChange with { ModelChanges = modelChanges };
                    }
                    catch (Exception exception)
                    {
                        fileChange = fileChange with
                        {
                            ModelChanges = new List<MendixModelChange>
                            {
                                new(
                                    "Modified",
                                    "Model Analysis",
                                    Path.GetFileName(entry.FilePath),
                                    $"Model analysis unavailable: {exception.Message}"),
                            },
                        };
                    }
                }

                changes.Add(fileChange);
            }

            return new GitChangesPayload
            {
                IsGitRepo = true,
                BranchName = repository.Head?.FriendlyName ?? string.Empty,
                Changes = changes,
                Error = null,
            };
        }
        catch (RepositoryNotFoundException)
        {
            return new GitChangesPayload
            {
                IsGitRepo = false,
                BranchName = string.Empty,
                Changes = Array.Empty<GitFileChange>(),
                Error = null,
            };
        }
        catch (Exception exception)
        {
            return new GitChangesPayload
            {
                IsGitRepo = true,
                BranchName = string.Empty,
                Changes = Array.Empty<GitFileChange>(),
                Error = exception.Message,
            };
        }
    }

    private static string DetermineStatus(FileStatus status)
    {
        if ((status & (FileStatus.RenamedInIndex | FileStatus.RenamedInWorkdir)) != 0)
        {
            return StatusRenamed;
        }

        if ((status & (FileStatus.DeletedFromIndex | FileStatus.DeletedFromWorkdir)) != 0)
        {
            return StatusDeleted;
        }

        if ((status & (FileStatus.NewInIndex | FileStatus.NewInWorkdir)) != 0)
        {
            return StatusAdded;
        }

        return StatusModified;
    }

    private static bool IsStaged(FileStatus status)
    {
        const FileStatus stagedMask =
            FileStatus.NewInIndex |
            FileStatus.ModifiedInIndex |
            FileStatus.DeletedFromIndex |
            FileStatus.RenamedInIndex |
            FileStatus.TypeChangeInIndex;

        return (status & stagedMask) != 0;
    }

    private static string GetDiffText(string filePath, Patch patch)
    {
        try
        {
            var patchEntry = patch[filePath];
            if (patchEntry is null)
            {
                return DiffUnavailableMessage;
            }

            if (patchEntry.IsBinaryComparison || filePath.EndsWith(".mpr", StringComparison.OrdinalIgnoreCase))
            {
                return BinaryDiffMessage;
            }

            return string.IsNullOrWhiteSpace(patchEntry.Patch)
                ? DiffUnavailableMessage
                : patchEntry.Patch;
        }
        catch
        {
            return DiffUnavailableMessage;
        }
    }

    private static List<MendixModelChange> AnalyzeMprChanges(
        Repository repository,
        string repositoryRoot,
        string repositoryRelativeMprPath)
    {
        var workingDumpPath = CreateTempPath(".json");
        var headDumpPath = CreateTempPath(".json");
        var headWorkspacePath = CreateTempDirectoryPath();
        var workingMprPath = Path.Combine(repositoryRoot, repositoryRelativeMprPath.Replace('/', Path.DirectorySeparatorChar));

        try
        {
            if (File.Exists(workingMprPath))
            {
                MxToolService.DumpMpr(workingMprPath, workingDumpPath);
            }
            else
            {
                WriteEmptyDump(workingDumpPath);
            }

            if (TryWriteHeadMpr(repository, repositoryRelativeMprPath, workingMprPath, headWorkspacePath, out var headMprPath))
            {
                try
                {
                    MxToolService.DumpMpr(headMprPath, headDumpPath);
                }
                catch (Exception exception) when (LooksLikeHeadDumpEnvironmentIssue(exception))
                {
                    // Some repositories cannot be reconstructed for HEAD snapshot analysis.
                    return new List<MendixModelChange>();
                }
            }
            else
            {
                WriteEmptyDump(headDumpPath);
            }

            return MendixModelDiffService.CompareDumps(workingDumpPath, headDumpPath);
        }
        finally
        {
            TryDeleteFile(workingDumpPath);
            TryDeleteFile(headDumpPath);
            TryDeleteDirectory(headWorkspacePath);
        }
    }

    private static bool TryWriteHeadMpr(
        Repository repository,
        string repositoryRelativeMprPath,
        string workingMprPath,
        string headWorkspacePath,
        out string headMprPath)
    {
        headMprPath = string.Empty;

        var headCommit = repository.Head?.Tip;
        if (headCommit is null)
        {
            return false;
        }

        var treeEntry = headCommit[repositoryRelativeMprPath];
        if (treeEntry?.Target is not Blob headBlob)
        {
            return false;
        }

        Directory.CreateDirectory(headWorkspacePath);
        CopyMprContentsIfPresent(workingMprPath, headWorkspacePath);

        var mprFileName = Path.GetFileName(workingMprPath);
        if (string.IsNullOrWhiteSpace(mprFileName))
        {
            mprFileName = Path.GetFileName(repositoryRelativeMprPath);
        }

        if (string.IsNullOrWhiteSpace(mprFileName))
        {
            mprFileName = "App.mpr";
        }

        headMprPath = Path.Combine(headWorkspacePath, mprFileName);
        using var outputStream = File.Create(headMprPath);
        using var blobStream = headBlob.GetContentStream();
        blobStream.CopyTo(outputStream);
        return true;
    }

    private static string CreateTempPath(string extension) =>
        Path.Combine(Path.GetTempPath(), $"autocommitmessage_{Guid.NewGuid():N}{extension}");

    private static string CreateTempDirectoryPath() =>
        Path.Combine(Path.GetTempPath(), $"autocommitmessage_mpr_{Guid.NewGuid():N}");

    private static string NormalizeRepositoryPath(string path) =>
        path.Replace('\\', '/');

    private static bool LooksLikeHeadDumpEnvironmentIssue(Exception exception)
    {
        var message = exception.Message;
        var missingContents =
            message.IndexOf("mprcontents", StringComparison.OrdinalIgnoreCase) >= 0 &&
            message.IndexOf("Could not find a part of the path", StringComparison.OrdinalIgnoreCase) >= 0;
        var mismatchedMprName =
            message.IndexOf("Cannot open MPR file", StringComparison.OrdinalIgnoreCase) >= 0 &&
            message.IndexOf("refer to MPR file", StringComparison.OrdinalIgnoreCase) >= 0;

        return missingContents || mismatchedMprName;
    }

    private static void WriteEmptyDump(string outputPath)
    {
        const string emptyDumpJson = "{\"units\":[]}";
        File.WriteAllText(outputPath, emptyDumpJson, new UTF8Encoding(false));
    }

    private static void TryDeleteFile(string path)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch
        {
            // Ignore cleanup failures for temp artifacts.
        }
    }

    private static void TryDeleteDirectory(string path)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(path) && Directory.Exists(path))
            {
                Directory.Delete(path, recursive: true);
            }
        }
        catch
        {
            // Ignore cleanup failures for temp artifacts.
        }
    }

    private static void CopyMprContentsIfPresent(string workingMprPath, string headWorkspacePath)
    {
        var workingDirectory = Path.GetDirectoryName(workingMprPath);
        if (string.IsNullOrWhiteSpace(workingDirectory))
        {
            return;
        }

        var sourceMprContentsPath = Path.Combine(workingDirectory, "mprcontents");
        if (!Directory.Exists(sourceMprContentsPath))
        {
            return;
        }

        var targetMprContentsPath = Path.Combine(headWorkspacePath, "mprcontents");
        CopyDirectory(sourceMprContentsPath, targetMprContentsPath);
    }

    private static void CopyDirectory(string sourcePath, string destinationPath)
    {
        Directory.CreateDirectory(destinationPath);

        foreach (var sourceFilePath in Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories))
        {
            var relativePath = Path.GetRelativePath(sourcePath, sourceFilePath);
            var destinationFilePath = Path.Combine(destinationPath, relativePath);
            var destinationDirectory = Path.GetDirectoryName(destinationFilePath);
            if (!string.IsNullOrWhiteSpace(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            File.Copy(sourceFilePath, destinationFilePath, overwrite: true);
        }
    }
}
