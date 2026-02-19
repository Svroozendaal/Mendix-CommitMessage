using MendixCommitParser.Models;

namespace MendixCommitParser.Services;

/// <summary>
/// Extracts high-level Mendix entities from changed file paths.
/// </summary>
public static class EntityExtractorService
{
    /// <summary>
    /// Converts raw file changes to extracted entity records.
    /// </summary>
    public static ExtractedEntity[] ExtractEntities(RawFileChange[] changes)
    {
        if (changes is null || changes.Length == 0)
        {
            return Array.Empty<ExtractedEntity>();
        }

        var entities = new List<ExtractedEntity>(changes.Length);
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var change in changes)
        {
            if (change.ModelChanges is { Length: > 0 })
            {
                foreach (var modelChange in change.ModelChanges)
                {
                    var entityType = string.IsNullOrWhiteSpace(modelChange.ElementType)
                        ? "Model"
                        : modelChange.ElementType;
                    var entityName = string.IsNullOrWhiteSpace(modelChange.ElementName)
                        ? "Unknown"
                        : modelChange.ElementName;
                    var entityAction = NormalizeAction(modelChange.ChangeType ?? change.Status);

                    AddEntity(entities, seen, entityType, entityName, entityAction);
                }

                continue;
            }

            var filePath = change.FilePath ?? string.Empty;
            var normalizedPath = filePath.Replace('\\', '/');
            var segments = normalizedPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var action = NormalizeAction(change.Status);

            if (TryExtractByFolder(segments, "Domain", includeExtension: false, out var domainName))
            {
                AddEntity(entities, seen, "Domain", domainName, action);
                continue;
            }

            if (TryExtractByFolder(segments, "Pages", includeExtension: false, out var pageName))
            {
                AddEntity(entities, seen, "Page", pageName, action);
                continue;
            }

            if (TryExtractByFolder(segments, "Microflows", includeExtension: false, out var microflowName))
            {
                AddEntity(entities, seen, "Microflow", microflowName, action);
                continue;
            }

            if (TryExtractByFolder(segments, "Resources", includeExtension: true, out var resourceName))
            {
                AddEntity(entities, seen, "Resource", resourceName, action);
                continue;
            }

            var fallbackName = Path.GetFileName(filePath);
            if (string.IsNullOrWhiteSpace(fallbackName))
            {
                fallbackName = "Unknown";
            }

            AddEntity(entities, seen, "Unknown", fallbackName, action);
        }

        return entities.ToArray();
    }

    private static void AddEntity(
        ICollection<ExtractedEntity> target,
        ISet<string> seen,
        string type,
        string name,
        string action)
    {
        var normalizedType = string.IsNullOrWhiteSpace(type) ? "Unknown" : type.Trim();
        var normalizedName = string.IsNullOrWhiteSpace(name) ? "Unknown" : name.Trim();
        var normalizedAction = string.IsNullOrWhiteSpace(action) ? "Modified" : action.Trim();
        var key = $"{normalizedType}|{normalizedName}|{normalizedAction}";
        if (!seen.Add(key))
        {
            return;
        }

        target.Add(new ExtractedEntity(normalizedType, normalizedName, normalizedAction));
    }

    private static bool TryExtractByFolder(string[] segments, string folderName, bool includeExtension, out string entityName)
    {
        entityName = string.Empty;
        if (segments.Length == 0)
        {
            return false;
        }

        for (var i = 0; i < segments.Length - 1; i++)
        {
            if (!segments[i].Equals(folderName, StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var candidate = segments[^1];
            entityName = includeExtension ? candidate : Path.GetFileNameWithoutExtension(candidate);
            if (string.IsNullOrWhiteSpace(entityName))
            {
                entityName = includeExtension ? candidate : "Unknown";
            }

            return true;
        }

        return false;
    }

    private static string NormalizeAction(string? status)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            return "Modified";
        }

        if (status.Contains("added", StringComparison.OrdinalIgnoreCase))
        {
            return "Added";
        }

        if (status.Contains("deleted", StringComparison.OrdinalIgnoreCase))
        {
            return "Deleted";
        }

        if (status.Contains("renamed", StringComparison.OrdinalIgnoreCase))
        {
            return "Renamed";
        }

        return "Modified";
    }
}
