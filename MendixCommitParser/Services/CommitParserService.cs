using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using MendixCommitParser.Models;

namespace MendixCommitParser.Services;

/// <summary>
/// Parses raw commit export files into structured commit data.
/// </summary>
public static class CommitParserService
{
    private const string StructuredSchemaVersion = "2.0";
    private const string BinaryDiffMessage = "Binary file changed - diff not available";

    private static readonly JsonSerializerOptions DeserializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private static readonly Regex ActionSummaryRegex = new(
        @"actions\s+used\s*\(\s*\d+\s*\)\s*:\s*(?<summary>[^;]+)",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex ActionCountEntryRegex = new(
        @"(?<action>[A-Za-z0-9_]+)\s*x(?<count>\d+)",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex ActionDetailsRegex = new(
        @"action\s+details\s*:\s*(?<details>.+)$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex ActionDetailEntryRegex = new(
        @"(?<action>[A-Za-z0-9_]+)\s*:\s*(?<detail>[^;]+)",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static readonly Regex AddedAttributesRegex = new(
        @"attributes\s+added\s*\(\s*\d+\s*\)\s*:\s*(?<attributes>[^;]+)",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>
    /// Reads and transforms one raw export file into a structured commit record.
    /// </summary>
    public static StructuredCommitData ProcessFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Export file not found.", filePath);
        }

        var json = File.ReadAllText(filePath, Encoding.UTF8);
        var raw = JsonSerializer.Deserialize<RawCommitData>(json, DeserializerOptions);

        if (raw is null)
        {
            throw new JsonException("Could not deserialize export file.");
        }

        var timestamp = raw.Timestamp ?? string.Empty;
        var projectName = raw.ProjectName ?? string.Empty;
        var branchName = raw.BranchName ?? string.Empty;
        var userName = raw.UserName ?? string.Empty;
        var userEmail = raw.UserEmail ?? string.Empty;

        var changes = raw.Changes ?? Array.Empty<RawFileChange>();
        var files = BuildStructuredFiles(changes);
        var modelChanges = BuildModelChanges(changes);
        var modelDumpArtifacts = BuildModelDumpArtifacts(changes);
        var modelSummary = BuildModelSummary(modelChanges);
        var entities = EntityExtractorService.ExtractEntities(changes);
        var metrics = BuildMetrics(changes);
        var affectedFiles = files
            .Select(file => file.FilePath)
            .Where(path => !string.IsNullOrWhiteSpace(path))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
            .ToArray();
        var commitMessageContext = BuildCommitMessageContext(
            files,
            modelChanges,
            modelSummary,
            metrics,
            projectName);

        var commitId = BuildCommitId(
            timestamp,
            projectName,
            branchName,
            userEmail,
            affectedFiles);

        return new StructuredCommitData(
            StructuredSchemaVersion,
            commitId,
            Path.GetFileName(filePath),
            timestamp,
            projectName,
            branchName,
            userName,
            userEmail,
            entities,
            affectedFiles,
            metrics,
            files,
            modelChanges,
            modelSummary,
            modelDumpArtifacts,
            commitMessageContext);
    }

    private static StructuredFileChange[] BuildStructuredFiles(RawFileChange[] changes)
    {
        if (changes.Length == 0)
        {
            return Array.Empty<StructuredFileChange>();
        }

        return changes.Select(change =>
        {
            var filePath = NormalizePath(change.FilePath);
            var fileName = Path.GetFileName(filePath);
            var folderPath = GetFolderPath(filePath);
            var status = change.Status ?? "Modified";
            var changeKind = NormalizeChangeKind(status);
            var diffText = change.DiffText ?? string.Empty;
            var isBinaryDiff = diffText.Contains(BinaryDiffMessage, StringComparison.OrdinalIgnoreCase);
            var diffLineCount = CountDiffLines(diffText);
            var modelChangeCount = change.ModelChanges?.Length ?? 0;
            var tags = BuildFileTags(change, filePath, changeKind, isBinaryDiff, modelChangeCount);

            return new StructuredFileChange(
                filePath,
                string.IsNullOrWhiteSpace(fileName) ? filePath : fileName,
                folderPath,
                status,
                change.IsStaged,
                changeKind,
                isBinaryDiff,
                diffLineCount,
                modelChangeCount,
                tags);
        }).ToArray();
    }

    private static StructuredModelChange[] BuildModelChanges(RawFileChange[] changes)
    {
        if (changes.Length == 0)
        {
            return Array.Empty<StructuredModelChange>();
        }

        return changes
            .Where(change => change.ModelChanges is { Length: > 0 })
            .SelectMany(change =>
                (change.ModelChanges ?? Array.Empty<RawModelChange>())
                .Select(modelChange => new StructuredModelChange(
                    NormalizePath(change.FilePath),
                    NormalizeChangeKind(modelChange.ChangeType),
                    modelChange.ElementType ?? "Unknown",
                    modelChange.ElementName ?? "Unknown",
                    modelChange.Details)))
            .ToArray();
    }

    private static StructuredModelDumpArtifact[] BuildModelDumpArtifacts(RawFileChange[] changes)
    {
        if (changes.Length == 0)
        {
            return Array.Empty<StructuredModelDumpArtifact>();
        }

        return changes
            .Where(change => change.ModelDumpArtifact is not null)
            .Select(change => new StructuredModelDumpArtifact(
                NormalizePath(change.FilePath),
                change.ModelDumpArtifact!.FolderPath,
                change.ModelDumpArtifact.WorkingDumpPath,
                change.ModelDumpArtifact.HeadDumpPath))
            .ToArray();
    }

    private static ModelChangeSummary BuildModelSummary(StructuredModelChange[] modelChanges)
    {
        var byElementType = BuildBreakdown(
            modelChanges,
            change => change.ElementType,
            defaultKey: "Unknown");
        var byChangeType = BuildBreakdown(
            modelChanges,
            change => change.ChangeType,
            defaultKey: "Modified");
        var byFile = BuildBreakdown(
            modelChanges,
            change => change.FilePath,
            defaultKey: "<unknown>");
        var microflowActions = BuildMicroflowActionSummary(modelChanges);
        var domainSummary = BuildDomainModelSummary(modelChanges);

        return new ModelChangeSummary(
            modelChanges.Length,
            byElementType,
            byChangeType,
            byFile,
            microflowActions,
            domainSummary);
    }

    private static ModelChangeBreakdown[] BuildBreakdown(
        IEnumerable<StructuredModelChange> modelChanges,
        Func<StructuredModelChange, string?> keySelector,
        string defaultKey)
    {
        return modelChanges
            .GroupBy(change =>
            {
                var key = keySelector(change);
                return string.IsNullOrWhiteSpace(key) ? defaultKey : key.Trim();
            }, StringComparer.OrdinalIgnoreCase)
            .Select(group => new ModelChangeBreakdown(group.Key, group.Count()))
            .OrderByDescending(item => item.Count)
            .ThenBy(item => item.Key, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private static MicroflowActionSummary[] BuildMicroflowActionSummary(StructuredModelChange[] modelChanges)
    {
        var actionCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        var actionExamples = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

        foreach (var modelChange in modelChanges)
        {
            if (!string.Equals(modelChange.ElementType, "Microflow", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var details = modelChange.Details;
            if (string.IsNullOrWhiteSpace(details))
            {
                continue;
            }

            var extractedCounts = ExtractActionCounts(details);
            foreach (var (actionType, count) in extractedCounts)
            {
                if (!actionCounts.ContainsKey(actionType))
                {
                    actionCounts[actionType] = 0;
                }

                actionCounts[actionType] += count;
            }

            var extractedDetails = ExtractActionDetailExamples(details);
            foreach (var (actionType, detailExample) in extractedDetails)
            {
                if (!actionExamples.TryGetValue(actionType, out var examples))
                {
                    examples = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    actionExamples[actionType] = examples;
                }

                examples.Add(detailExample);

                if (!actionCounts.ContainsKey(actionType))
                {
                    actionCounts[actionType] = 1;
                }
            }
        }

        return actionCounts
            .OrderByDescending(pair => pair.Value)
            .ThenBy(pair => pair.Key, StringComparer.OrdinalIgnoreCase)
            .Select(pair => new MicroflowActionSummary(
                pair.Key,
                pair.Value,
                actionExamples.TryGetValue(pair.Key, out var examples)
                    ? examples
                        .OrderBy(example => example, StringComparer.OrdinalIgnoreCase)
                        .Take(4)
                        .ToArray()
                    : Array.Empty<string>()))
            .ToArray();
    }

    private static Dictionary<string, int> ExtractActionCounts(string details)
    {
        var counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        var summaryMatch = ActionSummaryRegex.Match(details);
        if (!summaryMatch.Success)
        {
            return counts;
        }

        var summary = summaryMatch.Groups["summary"].Value;
        foreach (Match countMatch in ActionCountEntryRegex.Matches(summary))
        {
            var action = countMatch.Groups["action"].Value.Trim();
            if (string.IsNullOrWhiteSpace(action))
            {
                continue;
            }

            if (!int.TryParse(countMatch.Groups["count"].Value, out var count))
            {
                continue;
            }

            counts[action] = count;
        }

        return counts;
    }

    private static Dictionary<string, string> ExtractActionDetailExamples(string details)
    {
        var examples = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var detailMatch = ActionDetailsRegex.Match(details);
        if (!detailMatch.Success)
        {
            return examples;
        }

        var detailSection = detailMatch.Groups["details"].Value;
        foreach (Match detailEntryMatch in ActionDetailEntryRegex.Matches(detailSection))
        {
            var action = detailEntryMatch.Groups["action"].Value.Trim();
            var detail = NormalizeInlineText(detailEntryMatch.Groups["detail"].Value, maxLength: 220);
            if (string.IsNullOrWhiteSpace(action) || string.IsNullOrWhiteSpace(detail))
            {
                continue;
            }

            examples[action] = detail;
        }

        return examples;
    }

    private static DomainModelSummary BuildDomainModelSummary(StructuredModelChange[] modelChanges)
    {
        var entityChanges = modelChanges
            .Where(change => string.Equals(change.ElementType, "Entity", StringComparison.OrdinalIgnoreCase))
            .ToArray();

        var addedEntities = entityChanges
            .Where(change => string.Equals(change.ChangeType, "Added", StringComparison.OrdinalIgnoreCase))
            .Select(change => change.ElementName)
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var modifiedEntities = entityChanges
            .Where(change => string.Equals(change.ChangeType, "Modified", StringComparison.OrdinalIgnoreCase))
            .Select(change => change.ElementName)
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        var attributeChangeMap = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
        var attributeChangeType = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var entityChange in entityChanges)
        {
            var addedAttributes = ExtractAddedAttributes(entityChange.Details);
            if (addedAttributes.Length == 0)
            {
                continue;
            }

            var entityName = entityChange.ElementName;
            if (!attributeChangeMap.TryGetValue(entityName, out var attributeSet))
            {
                attributeSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                attributeChangeMap[entityName] = attributeSet;
            }

            foreach (var attribute in addedAttributes)
            {
                attributeSet.Add(attribute);
            }

            attributeChangeType[entityName] = entityChange.ChangeType;
        }

        var attributeChanges = attributeChangeMap
            .OrderBy(pair => pair.Key, StringComparer.OrdinalIgnoreCase)
            .Select(pair => new DomainEntityAttributeSummary(
                pair.Key,
                attributeChangeType.TryGetValue(pair.Key, out var changeType)
                    ? changeType
                    : "Modified",
                pair.Value.OrderBy(attribute => attribute, StringComparer.OrdinalIgnoreCase).ToArray()))
            .ToArray();

        return new DomainModelSummary(addedEntities, modifiedEntities, attributeChanges);
    }

    private static string[] ExtractAddedAttributes(string? details)
    {
        if (string.IsNullOrWhiteSpace(details))
        {
            return Array.Empty<string>();
        }

        var match = AddedAttributesRegex.Match(details);
        if (!match.Success)
        {
            return Array.Empty<string>();
        }

        var rawAttributes = match.Groups["attributes"].Value;
        if (string.IsNullOrWhiteSpace(rawAttributes))
        {
            return Array.Empty<string>();
        }

        return rawAttributes
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(attribute => attribute.Trim())
            .Where(attribute => !string.IsNullOrWhiteSpace(attribute))
            .Where(attribute => !attribute.StartsWith("+", StringComparison.Ordinal))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(attribute => attribute, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private static CommitMessageContext BuildCommitMessageContext(
        StructuredFileChange[] files,
        StructuredModelChange[] modelChanges,
        ModelChangeSummary modelSummary,
        CommitMetrics metrics,
        string projectName)
    {
        var suggestedScopes = ExtractSuggestedScopes(modelChanges, files, projectName);
        var suggestedType = InferSuggestedType(metrics, modelSummary);
        var highlights = BuildHighlights(metrics, modelSummary);
        var risks = BuildRisks(files, modelSummary);
        var suggestedSubject = BuildSuggestedSubject(suggestedType, suggestedScopes, metrics, modelSummary);

        return new CommitMessageContext(
            suggestedType,
            suggestedScopes,
            suggestedSubject,
            highlights,
            risks);
    }

    private static string[] ExtractSuggestedScopes(
        StructuredModelChange[] modelChanges,
        StructuredFileChange[] files,
        string projectName)
    {
        var scopes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var modelChange in modelChanges)
        {
            var scope = ExtractScopeFromModelElementName(modelChange.ElementName);
            if (!string.IsNullOrWhiteSpace(scope))
            {
                scopes.Add(scope);
            }
        }

        foreach (var file in files)
        {
            var scope = ExtractScopeFromFilePath(file.FilePath);
            if (!string.IsNullOrWhiteSpace(scope))
            {
                scopes.Add(scope);
            }
        }

        if (scopes.Count == 0 && !string.IsNullOrWhiteSpace(projectName))
        {
            var fallbackScope = projectName
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(fallbackScope))
            {
                scopes.Add(fallbackScope);
            }
        }

        return scopes
            .OrderBy(scope => scope, StringComparer.OrdinalIgnoreCase)
            .Take(5)
            .ToArray();
    }

    private static string? ExtractScopeFromModelElementName(string? elementName)
    {
        if (string.IsNullOrWhiteSpace(elementName))
        {
            return null;
        }

        var separatorIndex = elementName.IndexOf('.');
        if (separatorIndex <= 0)
        {
            return null;
        }

        return elementName[..separatorIndex].Trim();
    }

    private static string? ExtractScopeFromFilePath(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return null;
        }

        var normalized = NormalizePath(filePath);
        var segments = normalized.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (segments.Length == 0)
        {
            return null;
        }

        if (segments.Length == 1 && segments[0].EndsWith(".mpr", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        return segments[0];
    }

    private static string InferSuggestedType(CommitMetrics metrics, ModelChangeSummary modelSummary)
    {
        if (modelSummary.DomainModel.AddedEntities.Length > 0 || metrics.Added > 0)
        {
            return "feat";
        }

        if (metrics.Deleted > 0)
        {
            return "refactor";
        }

        if (modelSummary.TotalModelChanges > 0)
        {
            return "chore";
        }

        return "chore";
    }

    private static string[] BuildHighlights(CommitMetrics metrics, ModelChangeSummary modelSummary)
    {
        var highlights = new List<string>
        {
            $"{metrics.TotalFiles} file(s) changed ({metrics.Added} added, {metrics.Modified} modified, {metrics.Deleted} deleted, {metrics.Renamed} renamed)."
        };

        if (modelSummary.DomainModel.AddedEntities.Length > 0)
        {
            highlights.Add($"Added domain entities: {FormatInlineList(modelSummary.DomainModel.AddedEntities, 4)}.");
        }

        if (modelSummary.DomainModel.AttributeChanges.Length > 0)
        {
            var attributeHighlights = modelSummary.DomainModel.AttributeChanges
                .Take(3)
                .Select(change =>
                    $"{change.EntityName} ({FormatInlineList(change.AddedAttributes, 3)})");
            highlights.Add($"Entity attributes added: {string.Join("; ", attributeHighlights)}.");
        }

        if (modelSummary.MicroflowActions.Length > 0)
        {
            var actionHighlights = modelSummary.MicroflowActions
                .Take(5)
                .Select(action => $"{action.ActionType} x{action.Count}");
            highlights.Add($"Microflow actions used: {string.Join(", ", actionHighlights)}.");
        }

        if (modelSummary.ByElementType.Length > 0)
        {
            var elementHighlights = modelSummary.ByElementType
                .Take(4)
                .Select(item => $"{item.Key} x{item.Count}");
            highlights.Add($"Top element changes: {string.Join(", ", elementHighlights)}.");
        }

        return highlights.Take(8).ToArray();
    }

    private static string[] BuildRisks(StructuredFileChange[] files, ModelChangeSummary modelSummary)
    {
        var risks = new List<string>();

        if (files.Any(file => file.IsBinaryDiff))
        {
            risks.Add("Binary model file changes detected; plain git diff is unavailable.");
        }

        if (files.Any(file => file.FilePath.EndsWith(".mpr", StringComparison.OrdinalIgnoreCase)) &&
            modelSummary.TotalModelChanges == 0)
        {
            risks.Add("Model file changed without parsed model changes; verify dump/analysis availability.");
        }

        var pageTemplateNoise = modelSummary.ByElementType
            .FirstOrDefault(item =>
                string.Equals(item.Key, "PageTemplate", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(item.Key, "BuildingBlock", StringComparison.OrdinalIgnoreCase));
        if (pageTemplateNoise is not null && pageTemplateNoise.Count >= 20)
        {
            risks.Add("High-volume template/building-block churn may obscure functional changes.");
        }

        if (modelSummary.DomainModel.AttributeChanges.Length > 0)
        {
            risks.Add("Domain attribute changes may require validation, migration, or backward-compatibility checks.");
        }

        return risks.ToArray();
    }

    private static string BuildSuggestedSubject(
        string suggestedType,
        string[] suggestedScopes,
        CommitMetrics metrics,
        ModelChangeSummary modelSummary)
    {
        if (modelSummary.DomainModel.AddedEntities.Length > 0)
        {
            return $"add {modelSummary.DomainModel.AddedEntities.Length} domain entit{(modelSummary.DomainModel.AddedEntities.Length == 1 ? "y" : "ies")} and update related flows";
        }

        if (modelSummary.MicroflowActions.Length > 0)
        {
            var topAction = modelSummary.MicroflowActions[0];
            return $"update microflow logic around {topAction.ActionType}";
        }

        if (metrics.TotalFiles == 1)
        {
            var scope = suggestedScopes.FirstOrDefault() ?? "model";
            return $"update {scope} change set";
        }

        return suggestedType == "feat"
            ? "introduce model changes"
            : "refine model change set";
    }

    private static CommitMetrics BuildMetrics(RawFileChange[] changes)
    {
        var added = 0;
        var modified = 0;
        var deleted = 0;
        var renamed = 0;

        foreach (var change in changes)
        {
            switch (NormalizeChangeKind(change.Status))
            {
                case "Added":
                    added++;
                    break;
                case "Deleted":
                    deleted++;
                    break;
                case "Renamed":
                    renamed++;
                    break;
                default:
                    modified++;
                    break;
            }
        }

        return new CommitMetrics(changes.Length, added, modified, deleted, renamed);
    }

    private static string NormalizeChangeKind(string? status)
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

    private static string[] BuildFileTags(
        RawFileChange change,
        string filePath,
        string changeKind,
        bool isBinaryDiff,
        int modelChangeCount)
    {
        var tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            changeKind.ToLowerInvariant(),
            change.IsStaged ? "staged" : "unstaged"
        };

        if (filePath.EndsWith(".mpr", StringComparison.OrdinalIgnoreCase))
        {
            tags.Add("mendix-model");
        }

        if (filePath.EndsWith(".mprops", StringComparison.OrdinalIgnoreCase))
        {
            tags.Add("mendix-settings");
        }

        if (isBinaryDiff)
        {
            tags.Add("binary-diff");
        }

        if (modelChangeCount > 0)
        {
            tags.Add("has-model-changes");
        }

        return tags
            .OrderBy(tag => tag, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }

    private static int CountDiffLines(string diffText)
    {
        if (string.IsNullOrWhiteSpace(diffText) || diffText.Contains(BinaryDiffMessage, StringComparison.OrdinalIgnoreCase))
        {
            return 0;
        }

        var count = 0;
        var lines = diffText.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
        foreach (var line in lines)
        {
            if (line.Length == 0)
            {
                continue;
            }

            if (line.StartsWith("+++", StringComparison.Ordinal) || line.StartsWith("---", StringComparison.Ordinal))
            {
                continue;
            }

            if (line.StartsWith("+", StringComparison.Ordinal) || line.StartsWith("-", StringComparison.Ordinal))
            {
                count++;
            }
        }

        return count;
    }

    private static string NormalizePath(string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return "<unknown>";
        }

        return path.Replace('\\', '/').Trim();
    }

    private static string GetFolderPath(string filePath)
    {
        var separatorIndex = filePath.LastIndexOf('/');
        return separatorIndex <= 0 ? string.Empty : filePath[..separatorIndex];
    }

    private static string BuildCommitId(
        string timestamp,
        string projectName,
        string branchName,
        string userEmail,
        IEnumerable<string> affectedFiles)
    {
        var fileSeed = string.Join("|", affectedFiles.OrderBy(path => path, StringComparer.OrdinalIgnoreCase));
        var seed = $"{timestamp}|{projectName}|{branchName}|{userEmail}|{fileSeed}";
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(seed));
        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }

    private static string FormatInlineList(IEnumerable<string> values, int maxItems)
    {
        var ordered = values
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
            .ToArray();
        if (ordered.Length == 0)
        {
            return "<none>";
        }

        var visible = ordered.Take(maxItems).ToList();
        var remaining = ordered.Length - visible.Count;
        if (remaining > 0)
        {
            visible.Add($"+{remaining} more");
        }

        return string.Join(", ", visible);
    }

    private static string NormalizeInlineText(string value, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var normalized = string.Join(" ", value.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries)).Trim();
        if (normalized.Length > maxLength)
        {
            return $"{normalized[..maxLength]}...";
        }

        return normalized;
    }
}
