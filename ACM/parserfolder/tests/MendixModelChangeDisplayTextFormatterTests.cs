using Xunit;

namespace AutoCommitMessage.Tests;

/// <summary>
/// Unit tests for deterministic display-text formatting.
/// </summary>
public class MendixModelChangeDisplayTextFormatterTests
{
    [Fact]
    public void DisplayText_FlowDecisionChanges_AreFoldedIntoActionBuckets()
    {
        var details =
            "flows count 19->21; " +
            "actions delta: added 0, removed 1, modified 0; " +
            "actions removed (1): ValidationFeedbackAction x1; " +
            "removed action details: ValidationFeedbackAction: action payload changed; " +
            "decisions delta: added 0, removed 1, modified 1; " +
            "decisions removed (1): EntryType is TWK? expression=$JournalItem/EntryType = TWK.Enum.TWK; " +
            "decisions modified (1): is TWK? expression=$JournalItem/EntryType = TWK.Enum_JournalItemType.TWK -> is TWK? expression=$JournalItem/EntryType";

        var change = new MendixModelChange(
            ChangeType: "Modified",
            ElementType: "Microflow",
            ElementName: "TWK.SUB_JournalItem_QuantityChange",
            Details: details);

        var displayText = change.DisplayText;

        Assert.Contains("removed: decision EntryType is TWK?", displayText);
        Assert.Contains("modified: decision is TWK?", displayText);
        Assert.DoesNotContain("validation feedback", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("decisions:", displayText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DisplayText_FlowDecisionAdded_AppearsInsideAddedBucket()
    {
        var details =
            "actions delta: added 1, removed 0, modified 0; " +
            "actions added (1): ChangeVariableAction x1; " +
            "added action details: ChangeVariableAction: change variable isOk=false; " +
            "decisions delta: added 1, removed 0, modified 0; " +
            "decisions added (1): amount valid? expression=$Value > 0";

        var change = new MendixModelChange(
            ChangeType: "Modified",
            ElementType: "Microflow",
            ElementName: "TWK.VAL_Helper",
            Details: details);

        var displayText = change.DisplayText;

        Assert.Contains("added: change variable, decision amount valid?", displayText);
        Assert.DoesNotContain("decisions:", displayText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DisplayText_FlowAnnotationOnlyChange_IsSuppressedAndRendersModified()
    {
        var details =
            "annotations delta: added 1, removed 0, modified 0; " +
            "annotations added (1): text=Need validation";

        var change = new MendixModelChange(
            ChangeType: "Modified",
            ElementType: "Microflow",
            ElementName: "New_Module.ACO_new",
            Details: details);

        var displayText = change.DisplayText;

        Assert.EndsWith(": modified", displayText);
        Assert.DoesNotContain("annotation", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Need validation", displayText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DisplayText_FlowAnnotationAlongsideActions_DropsOnlyAnnotation()
    {
        var details =
            "actions delta: added 1, removed 0, modified 0; " +
            "actions added (1): ChangeVariableAction x1; " +
            "added action details: ChangeVariableAction: change variable isOk=false; " +
            "annotations delta: added 1, removed 0, modified 0; " +
            "annotations added (1): text=Need validation";

        var change = new MendixModelChange(
            ChangeType: "Modified",
            ElementType: "Microflow",
            ElementName: "New_Module.ACO_new",
            Details: details);

        var displayText = change.DisplayText;

        Assert.Contains("added: change variable", displayText);
        Assert.DoesNotContain("annotation", displayText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DisplayText_EntityAccessRulesMetadata_IsNormalized()
    {
        var change = new MendixModelChange(
            ChangeType: "Modified",
            ElementType: "Entity",
            ElementName: "IXAGOProject.CallId",
            Details: "entity metadata: accessRules=2, exportLevel=Hidden");

        var displayText = change.DisplayText;

        Assert.Contains("Accessrules changed", displayText);
        Assert.DoesNotContain("accessRules=2", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("exportLevel=Hidden", displayText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DisplayText_ExportLevelHidden_IsSuppressedFromFlowMetadata()
    {
        var change = new MendixModelChange(
            ChangeType: "Modified",
            ElementType: "Microflow",
            ElementName: "IXAGOProject.ACT_Test",
            Details: "flow metadata: exportLevel=Hidden, allowedModuleRoles=3");

        var displayText = change.DisplayText;

        Assert.Contains("allowedModuleRoles=3", displayText);
        Assert.DoesNotContain("exportLevel=Hidden", displayText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DisplayText_FlowAllowedRolesOnlyChange_IsCompacted()
    {
        var change = new MendixModelChange(
            ChangeType: "Modified",
            ElementType: "Microflow",
            ElementName: "IXAGOProject.DS_ResearcherProfile_SelectableSections",
            Details: "allowedModuleRoles count 3->4; flow structure: actions=2, loops=0, decisions=0, sequenceFlows=3; flow metadata: modelType=Microflow, applyEntityAccess=false, allowConcurrentExecution=true, markAsUsed=false, exportLevel=Hidden, allowedModuleRoles=4");

        var displayText = change.DisplayText;

        Assert.Contains("modified allowed roles", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("flow structure", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("flow metadata", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("exportLevel=Hidden", displayText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DisplayText_FlowAllowedRolesOnly_WithGenericFallbackNoise_IsCompacted()
    {
        var change = new MendixModelChange(
            ChangeType: "Modified",
            ElementType: "Microflow",
            ElementName: "IXAGOProject.ACT_ResearcherProfile_Save",
            Details: "allowedModuleRoles count 2->3; modelType=Microflow; resource metadata: excluded=false, markAsUsed=false, applyEntityAccess=true, allowConcurrentExecution=true, allowedModuleRoles=3, flows=6; nested types (33): BezierCurve x6, SequenceFlow x6, ActionActivity x3");

        var displayText = change.DisplayText;

        Assert.Contains("modified allowed roles", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("modelType=Microflow", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("resource metadata:", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("nested types", displayText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DisplayText_PageAdded_ShowsAddedListAndWidgetExplanation()
    {
        var details =
            "layout=Atlas_Core.Atlas_Default; title=Pagina test; url=<empty>; popup=0x0 resizable=true; " +
            "actions used (5): PageClientAction x2, CreateObjectClientAction x1, DeleteClientAction x1, MicroflowClientAction x1; " +
            "action targets: microflow=SmartExpenses.ACT_Balance_NewEdit, page=SmartExpenses.Balance_NewEdit; " +
            "widgets used (18): ActionButton x4, IconCollectionIcon x3, PageSettings x3, ConditionalVisibilitySettings x1, +3 more; " +
            "functional widgets (8): ActionButton x4, DataView x1, DataGrid x1, DataGrid2 x1, Snippet x1";

        var change = new MendixModelChange(
            ChangeType: "Added",
            ElementType: "Page",
            ElementName: "New_Module.Pagina_test",
            Details: details);

        var displayText = change.DisplayText;

        Assert.Contains("added: button, list, DG, DG2, snippet", displayText);
        Assert.Contains("widget details:", displayText);
        Assert.Contains("button call MF ACT_Balance_NewEdit", displayText);
        Assert.Contains("button show page Balance_NewEdit", displayText);
        Assert.Contains("DG <unknown source>", displayText);
        Assert.Contains("DG2 <unknown source>", displayText);
        Assert.Contains("list <unknown source>", displayText);
        Assert.DoesNotContain("layout=", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("actions used", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("widgets used", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("functional widgets", displayText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DisplayText_PageModified_ShowsAddedWidgetListAndDetails()
    {
        var details =
            "added DataGrid2(grid) (source=Module.DS_Grid); " +
            "modified ActionButton(Save) (onClick Module.ACT_Old->Module.ACT_New); " +
            "removed DivContainer";

        var change = new MendixModelChange(
            ChangeType: "Modified",
            ElementType: "Page",
            ElementName: "New_Module.Pagina_test",
            Details: details);

        var displayText = change.DisplayText;

        Assert.Contains("added: DG2", displayText);
        Assert.Contains("widget details: DG2 DS_Grid", displayText);
        Assert.DoesNotContain("DivContainer", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("modified ActionButton", displayText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DisplayText_PageModified_FormatsButtonActionsAndGridSource()
    {
        var details =
            "added ActionButton(Open) (showPage=Module.Page_Details); " +
            "added ActionButton(Save) (onClick=Module.ACT_Save); " +
            "added ActionButton(Run) (onClick=Module.NF_Run); " +
            "added DataGrid(Orders) (source=Module.Order)";

        var change = new MendixModelChange(
            ChangeType: "Modified",
            ElementType: "Page",
            ElementName: "New_Module.Pagina_test",
            Details: details);

        var displayText = change.DisplayText;

        Assert.Contains("added: button, DG", displayText);
        Assert.Contains("button show page Page_Details", displayText);
        Assert.Contains("button call MF ACT_Save", displayText);
        Assert.Contains("button call nanoflow NF_Run", displayText);
        Assert.Contains("DG Order", displayText);
        Assert.DoesNotContain("functional widgets", displayText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DisplayText_PageModified_WidgetsUsedStyle_ProducesCompactModifiedSummary()
    {
        // Matches real raw-changes output: Modified page with "layoutCall updated; widgets used (N): ..." details
        var details =
            "layoutCall updated; layout=General.VU_PopupLayout; title=Page Title; url=<empty>; popup=0x0 resizable=true; " +
            "actions used (6): CallNanoflowClientAction x4, CancelChangesClientAction x1, MicroflowClientAction x1; " +
            "action targets: microflow=TWK.ACT_JournalItem_Save, nanoflow=TWK.OCh_JournalItem_AmountChange; " +
            "widgets used (67): ConditionalVisibilitySettings x10, WidgetValidation x10, DataView x2, DataGrid x1, +13 more";

        var change = new MendixModelChange(
            ChangeType: "Modified",
            ElementType: "Page",
            ElementName: "TWK.JournalItem_Edit",
            Details: details);

        var displayText = change.DisplayText;

        Assert.Contains("modified:", displayText);
        Assert.Contains("dataview x2", displayText);
        Assert.Contains("datagrid x1", displayText);
        Assert.DoesNotContain("url=<empty>", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("layoutCall updated", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("widgets used", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("actions used", displayText, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DisplayText_PageAdded_WidgetsUsedStyle_ProducesCompactAddedSummary()
    {
        // Added page where details only have "widgets used" (no "functional widgets" segment)
        var details =
            "layout=Atlas_Core.Atlas_Default; title=Overview; url=<empty>; popup=0x0 resizable=true; " +
            "widgets used (10): DataGrid x1, ActionButton x2, PageSettings x3, IconCollectionIcon x4";

        var change = new MendixModelChange(
            ChangeType: "Added",
            ElementType: "Page",
            ElementName: "Module.Overview_Page",
            Details: details);

        var displayText = change.DisplayText;

        Assert.Contains("added:", displayText);
        Assert.Contains("datagrid x1", displayText);
        Assert.DoesNotContain("url=<empty>", displayText, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("widgets used", displayText, StringComparison.OrdinalIgnoreCase);
    }
}
