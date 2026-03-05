# Domain Model: WorkflowCommons

## Entities

| Entity | Persistable | Attributes | Access Rules |
|---|---|---:|---:|
| WorkflowCommons.AssignmentHelper | False |  |  |
| WorkflowCommons.AuditTrailViewer | False | 2 |  |
| WorkflowCommons.CleanupHelper | False | 8 |  |
| WorkflowCommons.Configuration | True | 4 |  |
| WorkflowCommons.DashboardContext | False | 5 |  |
| WorkflowCommons.DefinitionHelper | False | 2 |  |
| WorkflowCommons.DurationHelper | False |  |  |
| WorkflowCommons.NotificationArea | False | 3 |  |
| WorkflowCommons.TaskAssignmentHelper | False | 3 |  |
| WorkflowCommons.TaskCount | False | 5 |  |
| WorkflowCommons.TaskSeries | False | 6 |  |
| WorkflowCommons.TaskSummary | False | 7 |  |
| WorkflowCommons.TimelineViewer | False | 0 |  |
| WorkflowCommons.UserTaskOutcomeView | True | 2 | 2 |
| WorkflowCommons.UserTaskTimeLine | False | 7 |  |
| WorkflowCommons.UserTaskView | True | 9 | 2 |
| WorkflowCommons.WorkflowAttachment | True | 0 | 3 |
| WorkflowCommons.WorkflowAuditTrailRecord | True | 15 |  |
| WorkflowCommons.WorkflowComment | True |  | 3 |
| WorkflowCommons.WorkflowCommentHelper | False |  |  |
| WorkflowCommons.WorkflowDefinitionHelper | False |  |  |
| WorkflowCommons.WorkflowSelectionHelper | False | 0 |  |
| WorkflowCommons.WorkflowSeries | False | 5 |  |
| WorkflowCommons.WorkflowSummary | False | 6 |  |
| WorkflowCommons.WorkflowTaskDetail | False | 2 |  |
| WorkflowCommons.WorkflowView | True | 8 | 2 |

## Associations

| Association | Parent | Child | Type |
|---|---|---|---|
| WorkflowCommons.DashboardContext_DefinitionHelperTask | WorkflowCommons.DashboardContext | WorkflowCommons.DefinitionHelper | Reference (1-1) |
| WorkflowCommons.DashboardContext_DefinitionHelperWorkflow | WorkflowCommons.DashboardContext | WorkflowCommons.DefinitionHelper | Reference (1-1) |
| WorkflowCommons.DashboardContext_TaskSummary | WorkflowCommons.DashboardContext | WorkflowCommons.TaskSummary | Reference (1-1) |
| WorkflowCommons.DashboardContext_WorkflowSummary | WorkflowCommons.DashboardContext | WorkflowCommons.WorkflowSummary | Reference (1-1) |
| WorkflowCommons.DefinitionHelper_DashboardContext | WorkflowCommons.DefinitionHelper | WorkflowCommons.DashboardContext | Reference (*-1) |
| WorkflowCommons.DefinitionHelper_DefinitionHelperParent | WorkflowCommons.DefinitionHelper | WorkflowCommons.DefinitionHelper | Reference (*-1) |
| WorkflowCommons.TaskSeries_DashboardContext | WorkflowCommons.TaskSeries | WorkflowCommons.DashboardContext | Reference (*-1) |
| WorkflowCommons.UserTaskOutcomeView_UserTaskView | WorkflowCommons.UserTaskOutcomeView | WorkflowCommons.UserTaskView | Reference (*-1) |
| WorkflowCommons.UserTaskView_WorkflowView | WorkflowCommons.UserTaskView | WorkflowCommons.WorkflowView | Reference (*-1) |
| WorkflowCommons.WorkflowAttachment_WorkflowComment | WorkflowCommons.WorkflowAttachment | WorkflowCommons.WorkflowComment | Reference (*-1) |
| WorkflowCommons.WorkflowComment_WorkflowView | WorkflowCommons.WorkflowComment | WorkflowCommons.WorkflowView | Reference (*-1) |
| WorkflowCommons.WorkflowCommentHelper_WorkflowComment | WorkflowCommons.WorkflowCommentHelper | WorkflowCommons.WorkflowComment | Reference (*-1) |
| WorkflowCommons.WorkflowSelectionHelper_WorkflowView | WorkflowCommons.WorkflowSelectionHelper | WorkflowCommons.WorkflowView | Reference (*-1) |
| WorkflowCommons.WorkflowSeries_DashboardContext | WorkflowCommons.WorkflowSeries | WorkflowCommons.DashboardContext | Reference (*-1) |
| WorkflowCommons.WorkflowTaskDetail_DashboardContext | WorkflowCommons.WorkflowTaskDetail | WorkflowCommons.DashboardContext | Reference (*-1) |

## Enumerations

| Enumeration | Values |
|---|---|
| WorkflowCommons.Enum_AuditTrail_EventLevel | ActivityEvent, WorkflowEvent |
| WorkflowCommons.Enum_AuditTrail_View | _Default, All, Minimal |
| WorkflowCommons.Enum_DashboardTimeFrame | Last_3_months, Last_4_weeks, Last_6_months, Last_7_days, This_year |
| WorkflowCommons.Enum_DurationUnit | Days, Hours, Minutes, Seconds |
| WorkflowCommons.Enum_LogNode | WorkflowCommons |
| WorkflowCommons.Enum_NotificationArea_RenderAs | Error, Info, Success, Warning |
| WorkflowCommons.Enum_TimeFrameStepUnit | Day, Month, Week |

