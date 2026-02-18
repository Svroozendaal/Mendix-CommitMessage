namespace MendixCommitParser.Services;

internal static class ParserDataPaths
{
    private const string DataRootEnvironmentVariable = "MENDIX_GIT_DATA_ROOT";
    private const string SolutionFileName = "Mendix-autoCommit.sln";

    public static readonly string DataRoot = ResolveDataRoot();
    public static readonly string ExportFolder = Path.Combine(DataRoot, "exports");
    public static readonly string ProcessedFolder = Path.Combine(DataRoot, "processed");
    public static readonly string ErrorsFolder = Path.Combine(DataRoot, "errors");
    public static readonly string StructuredFolder = Path.Combine(DataRoot, "structured");

    private static string ResolveDataRoot()
    {
        var fromEnvironment = Environment.GetEnvironmentVariable(DataRootEnvironmentVariable);
        if (!string.IsNullOrWhiteSpace(fromEnvironment))
        {
            return Path.GetFullPath(fromEnvironment);
        }

        var repositoryRoot = TryFindRepositoryRoot(AppContext.BaseDirectory);
        if (!string.IsNullOrWhiteSpace(repositoryRoot))
        {
            return Path.Combine(repositoryRoot, "mendix-data");
        }

        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "MendixAutoCommitMessage",
            "mendix-data");
    }

    private static string? TryFindRepositoryRoot(string startPath)
    {
        if (string.IsNullOrWhiteSpace(startPath))
        {
            return null;
        }

        var directory = new DirectoryInfo(Path.GetFullPath(startPath));
        while (directory is not null)
        {
            var solutionPath = Path.Combine(directory.FullName, SolutionFileName);
            if (File.Exists(solutionPath))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        return null;
    }
}
