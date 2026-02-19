namespace MendixCommitParser.Models;

/// <summary>
/// Enriched commit information generated from raw export data.
/// </summary>
public sealed record StructuredCommitData(
    string SchemaVersion,
    string CommitId,
    string SourceFileName,
    string Timestamp,
    string ProjectName,
    string BranchName,
    string UserName,
    string UserEmail,
    ExtractedEntity[] Entities,
    string[] AffectedFiles,
    CommitMetrics Metrics,
    StructuredFileChange[] Files,
    StructuredModelChange[] ModelChanges,
    ModelChangeSummary ModelSummary,
    StructuredModelDumpArtifact[] ModelDumpArtifacts,
    CommitMessageContext CommitMessageContext
);

/// <summary>
/// Represents an extracted Mendix entity affected by a file change.
/// </summary>
public sealed record ExtractedEntity(
    string Type,
    string Name,
    string Action
);

/// <summary>
/// One model-level change flattened from raw export files.
/// </summary>
public sealed record StructuredModelChange(
    string FilePath,
    string ChangeType,
    string ElementType,
    string ElementName,
    string? Details
);

/// <summary>
/// One enriched file-level change summary used for commit generation.
/// </summary>
public sealed record StructuredFileChange(
    string FilePath,
    string FileName,
    string FolderPath,
    string Status,
    bool IsStaged,
    string ChangeKind,
    bool IsBinaryDiff,
    int DiffLineCount,
    int ModelChangeCount,
    string[] Tags
);

/// <summary>
/// Aggregated model-level summary for commit-message generation.
/// </summary>
public sealed record ModelChangeSummary(
    int TotalModelChanges,
    ModelChangeBreakdown[] ByElementType,
    ModelChangeBreakdown[] ByChangeType,
    ModelChangeBreakdown[] ByFile,
    MicroflowActionSummary[] MicroflowActions,
    DomainModelSummary DomainModel
);

/// <summary>
/// Generic key/count breakdown.
/// </summary>
public sealed record ModelChangeBreakdown(
    string Key,
    int Count
);

/// <summary>
/// Aggregated microflow action usage extracted from model change details.
/// </summary>
public sealed record MicroflowActionSummary(
    string ActionType,
    int Count,
    string[] Examples
);

/// <summary>
/// Aggregated domain model information extracted from entity changes.
/// </summary>
public sealed record DomainModelSummary(
    string[] AddedEntities,
    string[] ModifiedEntities,
    DomainEntityAttributeSummary[] AttributeChanges
);

/// <summary>
/// Per-entity attribute additions.
/// </summary>
public sealed record DomainEntityAttributeSummary(
    string EntityName,
    string ChangeType,
    string[] AddedAttributes
);

/// <summary>
/// Stored full dump artifact paths for a model file.
/// </summary>
public sealed record StructuredModelDumpArtifact(
    string FilePath,
    string FolderPath,
    string WorkingDumpPath,
    string HeadDumpPath
);

/// <summary>
/// Aggregate file-change metrics derived from raw statuses.
/// </summary>
public sealed record CommitMetrics(
    int TotalFiles,
    int Added,
    int Modified,
    int Deleted,
    int Renamed
);

/// <summary>
/// Pre-computed commit-message guidance derived from the structured payload.
/// </summary>
public sealed record CommitMessageContext(
    string SuggestedType,
    string[] SuggestedScopes,
    string SuggestedSubject,
    string[] Highlights,
    string[] Risks
);
