# Flows: WorkflowCommons

## Flow Catalogue

### Action Flows (ACT_*)

| Flow | Nodes | Key Actions | Pages Shown |
|---|---:|---|---|
| ACT_Assignee_Migrate | 5 | none | none |
| ACT_Attachment_Create | 5 | WorkflowCommons.WorkflowAttachment | WorkflowCommons.WorkflowAttachment_New |
| ACT_Attachment_Download | 4 | none | none |
| ACT_Attachment_Save | 14 | none | none |
| ACT_Attachment_Save_Admin | 8 | none | none |
| ACT_AuditTrailViewer_All | 4 | none | none |
| ACT_AuditTrailViewer_Default | 4 | none | none |
| ACT_AuditTrailViewer_Minimal | 4 | none | none |
| ACT_Comment_Delete | 4 | none | none |
| ACT_DashboardContext_Refresh | 4 | none | none |
| ACT_DoNothing | 5 | none | none |
| ACT_Key_Migrate | 7 | none | none |
| ACT_TaskAssignment_Show | 4 | WorkflowCommons.TaskAssignmentHelper | WorkflowCommons.ManageTaskAssignments |
| ACT_TaskAssignmentHelper_Reassign | 10 | none | none |
| ACT_TaskAssignmentHelper_Reassign_Show | 9 | none | WorkflowCommons.TaskAssignmentHelper_UserTask_Reassign |
| ACT_TaskAssignmentHelper_Retarget | 10 | none | none |
| ACT_TaskAssignmentHelper_Retarget_Show | 9 | none | WorkflowCommons.TaskAssignmentHelper_UserTask_Retarget |
| ACT_TaskAssignmentHelper_Unassign | 8 | none | none |
| ACT_TaskAssignmentHelper_Unassign_Show | 9 | none | WorkflowCommons.TaskAssignmentHelper_UserTask_Unassign_TargetUserOptions |
| ACT_TaskCount_Refresh | 9 | none | none |
| ACT_TaskCount_Update | 4 | none | none |
| ACT_TimelineViewer_OpenSubWorkflow | 11 | none | none |
| ACT_TimelineViewer_OpenWorkflow | 7 | none | none |
| ACT_UserTask_AssignToMe | 4 | none | none |
| ACT_UserTask_AssignToMe_UpdateTaskCount | 9 | none | none |
| ACT_UserTask_AssignToUser | 5 | none | none |
| ACT_UserTask_AssignToUsers | 8 | none | none |
| ACT_UserTask_ShowDefaultAdminPage | 8 | none | WorkflowCommons.DefaultWorkflowAdmin |
| ACT_UserTask_Unassign | 4 | none | none |
| ACT_UserTaskView_ShowUserTaskPage | 8 | none | WorkflowCommons.CompletedUserTaskView |
| ACT_UserTaskView_ShowWorkflowAdminPage | 5 | none | none |
| ACT_Workflow_Abort | 8 | none | WorkflowCommons.Workflow_ActionConfirmation |
| ACT_Workflow_CloseActionConfirmation | 8 | none | none |
| ACT_Workflow_Continue | 5 | none | WorkflowCommons.Workflow_ActionConfirmation |
| ACT_Workflow_JumpTo | 8 | none | WorkflowCommons.Workflow_JumpTo_Options |
| ACT_Workflow_OpenParentWorkflow | 5 | none | none |
| ACT_Workflow_Pause | 5 | none | WorkflowCommons.Workflow_ActionConfirmation |
| ACT_Workflow_Restart | 5 | none | WorkflowCommons.Workflow_ActionConfirmation |
| ACT_Workflow_Retry | 9 | System.WorkflowUserTask | WorkflowCommons.Workflow_Retry_Options |
| ACT_Workflow_Retry_KeepTargetedUsers | 5 | none | none |
| ACT_Workflow_Retry_RerunUserTargeting | 9 | System.WorkflowUserTask | none |
| ACT_Workflow_Unpause | 5 | none | WorkflowCommons.Workflow_ActionConfirmation |
| ACT_Workflow_WithdrawConfirmation | 17 | none | none |
| ACT_WorkflowAuditTrailRecord_ExportToExcel | 3 | none | none |
| ACT_WorkflowAuditTrailRecord_Refresh | 3 | none | none |
| ACT_WorkflowComment_Edit | 9 | WorkflowCommons.WorkflowCommentHelper | WorkflowCommons.WorkflowCommentHelper_Edit |
| ACT_WorkflowCommentHelper_Edit_Save | 12 | none | none |
| ACT_WorkflowCommentHelper_SaveNew | 11 | WorkflowCommons.WorkflowComment | none |
| ACT_WorkflowCommentHelper_SaveNew_Admin | 8 | WorkflowCommons.WorkflowComment | none |
| ACT_WorkflowDefinition_CleanUp_Execute | 7 | none | none |
| ACT_WorkflowDefinition_CleanUp_Open | 6 | WorkflowCommons.CleanupHelper | WorkflowCommons.WorkflowDefinition_CleanUp |
| ACT_WorkflowDefinition_CloseActionConfirmation | 5 | none | none |
| ACT_WorkflowDefinition_Delete | 13 | System.Workflow | none |
| ACT_WorkflowDefinition_Lock | 10 | none | WorkflowCommons.WorkflowDefinition_ActionConfirmation |
| ACT_WorkflowDefinition_Unlock | 10 | none | WorkflowCommons.WorkflowDefinition_ActionConfirmation |
| ACT_WorkflowDefinitionHelper_ShowLockPage | 5 | none | WorkflowCommons.WorkflowDefinition_Lock |
| ACT_WorkflowDefinitionHelper_ShowUnlockPage | 5 | none | WorkflowCommons.WorkflowDefinition_Unlock |
| ACT_WorkflowJumpToDetails_Apply | 9 | none | WorkflowCommons.Workflow_ActionConfirmation |
| ACT_WorkflowSelectionHelper_Select | 5 | none | none |
| ACT_WorkflowUserTask_Assign | 6 | none | none |
| ACT_WorkflowUserTask_Assignees_Add | 6 | none | none |
| ACT_WorkflowUserTask_Assignees_Remove | 6 | none | none |
| ACT_WorkflowUserTask_TargetUsers_Add | 6 | none | none |
| ACT_WorkflowUserTask_TargetUsers_Remove | 6 | none | none |
| ACT_WorkflowUserTask_Unassign | 5 | none | none |
| ACT_WorkflowView_ShowWorkflowAdminPage | 4 | none | none |
| ACT_WorkflowView_WithdrawWorkflow | 6 | WorkflowCommons.WorkflowComment | WorkflowCommons.Workflow_WithdrawConfirmation |

### Data Sources (DS_*)

| Flow | Nodes | Key Actions | Returns |
|---|---:|---|---|
| DS_AuditTrailViewer | 4 | WorkflowCommons.AuditTrailViewer | inferred from node actions |
| DS_Configuration | 9 | none | inferred from node actions |
| DS_TaskAssignmentHelper_Account | 5 | Administration.Account | inferred from node actions |
| DS_TaskCount | 4 | WorkflowCommons.TaskCount | inferred from node actions |
| DS_TaskCount_Admin | 3 | WorkflowCommons.TaskCount | inferred from node actions |
| DS_TaskDashboard | 4 | none | inferred from node actions |
| DS_TaskSeries | 5 | none | inferred from node actions |
| DS_TimelineViewer_WorkflowActivityRecords_Full | 9 | none | inferred from node actions |
| DS_TimelineViewer_WorkflowActivityRecords_Tasks | 8 | none | inferred from node actions |
| DS_Workflow_LoadNotificationArea | 11 | WorkflowCommons.NotificationArea | inferred from node actions |
| DS_Workflow_TimelineViewer | 4 | WorkflowCommons.TimelineViewer | inferred from node actions |
| DS_Workflow_WorkflowView | 4 | none | inferred from node actions |
| DS_WorkflowActivityRecord_ActivityDuration | 4 | none | inferred from node actions |
| DS_WorkflowActivityRecord_OverdueTime | 6 | none | inferred from node actions |
| DS_WorkflowCommentHelper_InitializeNew | 3 | WorkflowCommons.WorkflowCommentHelper | inferred from node actions |
| DS_WorkflowCurrentActivity_Options | 4 | none | inferred from node actions |
| DS_WorkflowDashboard | 4 | none | inferred from node actions |
| DS_WorkflowDefinition_Overview | 7 | System.WorkflowDefinition, WorkflowCommons.WorkflowSummary | inferred from node actions |
| DS_WorkflowDefinition_SelectableImplementation | 12 | System.WorkflowDefinition, WorkflowCommons.DefinitionHelper | inferred from node actions |
| DS_WorkflowSelectionHelper | 4 | WorkflowCommons.WorkflowSelectionHelper, WorkflowCommons.WorkflowView | inferred from node actions |
| DS_WorkflowSeries | 5 | none | inferred from node actions |
| DS_WorkflowTask_AssignedToUser_Timeline | 14 | WorkflowCommons.UserTaskTimeLine | inferred from node actions |
| DS_WorkflowTask_LoadNotificationArea | 12 | WorkflowCommons.NotificationArea | inferred from node actions |
| DS_WorkflowTaskDefinition_Selectable_Administrator | 7 | System.WorkflowUserTaskDefinition | inferred from node actions |
| DS_WorkflowTaskDefinition_Selectable_UserImplementation | 17 | System.WorkflowUserTaskDefinition, WorkflowCommons.DefinitionHelper | inferred from node actions |
| DS_WorkflowTaskDetail | 4 | none | inferred from node actions |
| DS_WorkflowUserTask_AssigneeHelper | 6 | WorkflowCommons.AssignmentHelper | inferred from node actions |
| DS_WorkflowUserTask_WorkflowView | 5 | none | inferred from node actions |
| DS_WorkflowView_LoadNotificationArea | 11 | WorkflowCommons.NotificationArea | inferred from node actions |
| DS_WorkflowView_TimelineViewer | 5 | WorkflowCommons.TimelineViewer | inferred from node actions |

### Validation Flows (VAL_*)

| Flow | Nodes | Key Actions |
|---|---:|---|
| none | 0 | none |

### Other Flows

| Flow | Type | Nodes | Key Actions |
|---|---|---:|---|
| ASu_Assignee_Migrate | Microflow | 10 | none |
| ASu_Key_Migrate | Microflow | 12 | none |
| DashboardContext_GetSelectedWorkflowDefinition | Microflow | 10 | System.WorkflowDefinition |
| DashboardContext_GetSelectedWorkflowTaskDefinition | Microflow | 10 | System.WorkflowUserTaskDefinition |
| OCh_CleanupHelper_UpdateCount | Microflow | 15 | WorkflowCommons.WorkflowView |
| OCh_DashboardContext_UpdateTaskDashboard | Microflow | 10 | none |
| OCh_DashboardContext_UpdateWorkflowDashboard | Microflow | 5 | none |
| OCh_Workflow_State | Microflow | 6 | none |
| OCh_WorkflowCurrentActivity_Target | Nanoflow | 7 | none |
| OCh_WorkflowUserTask_State | Microflow | 9 | none |
| OCl_WorkflowSummary | Microflow | 5 | none |
| SE_WorkflowAuditTrailRecord_CleanUp | Microflow | 3 | none |
| SUB_Assignee_Migrate | Microflow | 14 | WorkflowCommons.UserTaskView |
| SUB_AssigneeMigration_Verify | Microflow | 9 | WorkflowCommons.UserTaskView |
| SUB_AuditTrailViewer_Default | Nanoflow | 4 | none |
| SUB_CleanupHelper_Execute_Workflow | Microflow | 19 | System.Workflow |
| SUB_CleanupHelper_Execute_WorkflowView | Microflow | 19 | WorkflowCommons.WorkflowView |
| SUB_CleanupHelper_Validate | Microflow | 14 | none |
| SUB_Configuration_FindOrCreate | Microflow | 6 | WorkflowCommons.Configuration |
| SUB_DashboardContext_RetrieveOrCreate | Microflow | 7 | WorkflowCommons.DashboardContext |
| SUB_DashboardContext_UpdateSettings | Microflow | 6 | none |
| SUB_Duration_Calculate | Nanoflow | 20 | WorkflowCommons.DurationHelper |
| SUB_KeyMigration_Verify | Microflow | 11 | WorkflowCommons.UserTaskView, WorkflowCommons.WorkflowView |
| SUB_TaskAssignmentHelper_Reassign | Microflow | 13 | none |
| SUB_TaskAssignmentHelper_Retarget | Microflow | 12 | none |
| SUB_TaskAssignmentHelper_TaskCount | Microflow | 5 | none |
| SUB_TaskAssignmentHelper_Unassign | Microflow | 11 | none |
| SUB_TaskCount_Update | Microflow | 13 | System.WorkflowUserTask, WorkflowCommons.UserTaskView |
| SUB_TaskDashboard_Update | Microflow | 9 | none |
| SUB_TaskKey_Migrate | Microflow | 15 | WorkflowCommons.UserTaskView |
| SUB_TaskSeries_CreateOrUpdate | Microflow | 18 | WorkflowCommons.TaskSeries |
| SUB_TaskSeriesList_Delete | Microflow | 5 | none |
| SUB_TaskSummary_CreateOrUpdate | Microflow | 19 | none |
| SUB_TaskSummary_RetrieveOrCreate | Microflow | 7 | WorkflowCommons.TaskSummary |
| SUB_User_GetAccount | Microflow | 7 | none |
| SUB_UserTask_Assign | Microflow | 13 | System.User |
| SUB_UserTask_AssignedToUser | Microflow | 8 | WorkflowCommons.UserTaskView |
| SUB_UserTask_Assignee_Add | Microflow | 7 | System.User |
| SUB_UserTask_Assignee_Remove | Microflow | 7 | System.User |
| SUB_UserTask_Assignees_Add | Microflow | 15 | none |
| SUB_UserTask_Assignees_Remove | Microflow | 15 | none |
| SUB_UserTask_AverageHandlingTime | Microflow | 12 | WorkflowCommons.UserTaskView |
| SUB_UserTask_CountAlmostDue | Microflow | 11 | System.WorkflowUserTask |
| SUB_UserTask_CountCompleted | Microflow | 9 | WorkflowCommons.UserTaskView |
| SUB_UserTask_CountCompletedOnTime | Microflow | 9 | WorkflowCommons.UserTaskView |
| SUB_UserTask_CountCompletedOverdue | Microflow | 9 | WorkflowCommons.UserTaskView |
| SUB_UserTask_CountFailed | Microflow | 9 | WorkflowCommons.UserTaskView |
| SUB_UserTask_CountInProgress | Microflow | 9 | System.WorkflowUserTask |
| SUB_UserTask_CountOverdue | Microflow | 13 | System.WorkflowUserTask, WorkflowCommons.UserTaskView |
| SUB_UserTask_TargetUser_Add | Microflow | 7 | System.User |
| SUB_UserTask_TargetUser_Remove | Microflow | 7 | System.User |
| SUB_UserTask_TargetUsers_Add | Microflow | 15 | none |
| SUB_UserTask_TargetUsers_Remove | Microflow | 15 | none |
| SUB_UserTaskOutcome_AssignedToUser | Microflow | 8 | System.WorkflowUserTaskOutcome |
| SUB_UserTaskOutcomeView_AssignedToUser | Microflow | 8 | WorkflowCommons.UserTaskOutcomeView |
| SUB_UserTaskOutcomeView_FindOrCreate | Microflow | 8 | WorkflowCommons.UserTaskOutcomeView |
| SUB_UserTaskView_FindOrCreate | Microflow | 9 | WorkflowCommons.UserTaskView, WorkflowCommons.WorkflowView |
| SUB_UserTaskView_UpdateKey | Microflow | 9 | none |
| SUB_Workflow_AverageHandlingTime | Microflow | 12 | WorkflowCommons.WorkflowView |
| SUB_Workflow_CountAlmostDue | Microflow | 11 | System.Workflow |
| SUB_Workflow_CountCompleted | Microflow | 9 | WorkflowCommons.WorkflowView |
| SUB_Workflow_CountCompletedOnTime | Microflow | 9 | WorkflowCommons.WorkflowView |
| SUB_Workflow_CountCompletedOverdue | Microflow | 9 | WorkflowCommons.WorkflowView |
| SUB_Workflow_CountInProgress | Microflow | 9 | System.Workflow |
| SUB_Workflow_CountOverdue | Microflow | 13 | System.Workflow, WorkflowCommons.WorkflowView |
| SUB_Workflow_Retry | Microflow | 5 | none |
| SUB_Workflow_ShowWorkflowAdminPage | Microflow | 7 | none |
| SUB_WorkflowAuditTrailRecord_CleanUp | Microflow | 16 | WorkflowCommons.WorkflowAuditTrailRecord |
| SUB_WorkflowAuditTrailRecord_DeleteByKey | Microflow | 6 | WorkflowCommons.WorkflowAuditTrailRecord |
| SUB_WorkflowDashboard_Update | Microflow | 12 | none |
| SUB_WorkflowDefinitionHelper_FindOrCreate | Microflow | 9 | WorkflowCommons.WorkflowDefinitionHelper |
| SUB_WorkflowEvent_AuditTrail | Microflow | 10 | System.User, WorkflowCommons.WorkflowAuditTrailRecord |
| SUB_WorkflowJumpToDetails_Validate | Microflow | 8 | none |
| SUB_WorkflowKey_Migrate | Microflow | 15 | WorkflowCommons.WorkflowView |
| SUB_WorkflowSeries_CreateOrUpdate | Microflow | 15 | WorkflowCommons.WorkflowSeries |
| SUB_WorkflowSeriesList_Delete | Microflow | 5 | none |
| SUB_WorkflowSummary_CreateOrUpdate | Microflow | 10 | none |
| SUB_WorkflowSummary_RetrieveOrCreate | Microflow | 10 | WorkflowCommons.WorkflowSummary |
| SUB_WorkflowTask_AverageHandlingTime | Microflow | 10 | WorkflowCommons.UserTaskView |
| SUB_WorkflowTaskDetail_CreateOrUpdate | Microflow | 11 | System.WorkflowUserTaskDefinition, WorkflowCommons.WorkflowTaskDetail |
| SUB_WorkflowTaskDetail_Delete | Microflow | 5 | none |
| SUB_WorkflowTaskTimeline_Completed | Microflow | 8 | WorkflowCommons.UserTaskTimeLine |
| SUB_WorkflowTaskTimeline_InProgress | Microflow | 8 | WorkflowCommons.UserTaskTimeLine |
| SUB_WorkflowView_CommentAttachment_Validate | Microflow | 11 | none |
| SUB_WorkflowView_CurrentUserIsTargeted | Microflow | 7 | WorkflowCommons.UserTaskView |
| SUB_WorkflowView_FindOrCreate | Microflow | 8 | WorkflowCommons.WorkflowView |
| SUB_WorkflowView_ShowWorkflowAdminPage | Microflow | 8 | none |
| SUB_WorkflowView_UpdateKey | Microflow | 9 | none |
| WFEH_WorkflowEvent_AuditTrail | Microflow | 6 | none |

## Cross-Module Calls

| Flow | Calls | Target Module |
|---|---|---|
| none | none | none |

## Flow Details

| Flow | Kind | Nodes | Tier | Calls Out | Called By |
|---|---|---:|---:|---:|---:|
| ACT_Assignee_Migrate | Microflow | 5 | 3 | 2 | 0 |
| ACT_Attachment_Create | Microflow | 5 | 3 | 0 | 0 |
| ACT_Attachment_Download | Microflow | 4 | 3 | 0 | 0 |
| ACT_Attachment_Save | Microflow | 14 | 3 | 1 | 0 |
| ACT_Attachment_Save_Admin | Microflow | 8 | 3 | 0 | 0 |
| ACT_AuditTrailViewer_All | Nanoflow | 4 | 3 | 0 | 0 |
| ACT_AuditTrailViewer_Default | Nanoflow | 4 | 3 | 1 | 0 |
| ACT_AuditTrailViewer_Minimal | Nanoflow | 4 | 3 | 0 | 0 |
| ACT_Comment_Delete | Microflow | 4 | 3 | 0 | 0 |
| ACT_DashboardContext_Refresh | Microflow | 4 | 3 | 0 | 0 |
| ACT_DoNothing | Nanoflow | 5 | 3 | 0 | 0 |
| ACT_Key_Migrate | Microflow | 7 | 3 | 3 | 0 |
| ACT_TaskAssignment_Show | Microflow | 4 | 3 | 0 | 0 |
| ACT_TaskAssignmentHelper_Reassign | Microflow | 10 | 3 | 2 | 0 |
| ACT_TaskAssignmentHelper_Reassign_Show | Microflow | 9 | 3 | 0 | 0 |
| ACT_TaskAssignmentHelper_Retarget | Microflow | 10 | 3 | 2 | 0 |
| ACT_TaskAssignmentHelper_Retarget_Show | Microflow | 9 | 3 | 0 | 0 |
| ACT_TaskAssignmentHelper_Unassign | Microflow | 8 | 3 | 2 | 0 |
| ACT_TaskAssignmentHelper_Unassign_Show | Microflow | 9 | 3 | 0 | 0 |
| ACT_TaskCount_Refresh | Nanoflow | 9 | 3 | 1 | 0 |
| ACT_TaskCount_Update | Microflow | 4 | 3 | 1 | 1 |
| ACT_TimelineViewer_OpenSubWorkflow | Nanoflow | 11 | 3 | 0 | 0 |
| ACT_TimelineViewer_OpenWorkflow | Nanoflow | 7 | 3 | 0 | 0 |
| ACT_UserTask_AssignToMe | Microflow | 4 | 3 | 1 | 0 |
| ACT_UserTask_AssignToMe_UpdateTaskCount | Microflow | 9 | 3 | 2 | 0 |
| ACT_UserTask_AssignToUser | Microflow | 5 | 3 | 1 | 0 |
| ACT_UserTask_AssignToUsers | Microflow | 8 | 3 | 1 | 0 |
| ACT_UserTask_ShowDefaultAdminPage | Microflow | 8 | 3 | 0 | 0 |
| ACT_UserTask_Unassign | Microflow | 4 | 3 | 1 | 0 |
| ACT_UserTaskView_ShowUserTaskPage | Microflow | 8 | 3 | 0 | 0 |
| ACT_UserTaskView_ShowWorkflowAdminPage | Microflow | 5 | 3 | 1 | 0 |
| ACT_Workflow_Abort | Microflow | 8 | 3 | 0 | 0 |
| ACT_Workflow_CloseActionConfirmation | Microflow | 8 | 3 | 0 | 0 |
| ACT_Workflow_Continue | Microflow | 5 | 3 | 0 | 0 |
| ACT_Workflow_JumpTo | Microflow | 8 | 3 | 0 | 0 |
| ACT_Workflow_OpenParentWorkflow | Microflow | 5 | 3 | 1 | 0 |
| ACT_Workflow_Pause | Microflow | 5 | 3 | 0 | 0 |
| ACT_Workflow_Restart | Microflow | 5 | 3 | 0 | 0 |
| ACT_Workflow_Retry | Microflow | 9 | 3 | 1 | 0 |
| ACT_Workflow_Retry_KeepTargetedUsers | Microflow | 5 | 3 | 1 | 0 |
| ACT_Workflow_Retry_RerunUserTargeting | Microflow | 9 | 3 | 1 | 0 |
| ACT_Workflow_Unpause | Microflow | 5 | 3 | 0 | 0 |
| ACT_Workflow_WithdrawConfirmation | Microflow | 17 | 3 | 0 | 0 |
| ACT_WorkflowAuditTrailRecord_ExportToExcel | Nanoflow | 3 | 3 | 0 | 0 |
| ACT_WorkflowAuditTrailRecord_Refresh | Nanoflow | 3 | 3 | 0 | 0 |
| ACT_WorkflowComment_Edit | Microflow | 9 | 3 | 0 | 0 |
| ACT_WorkflowCommentHelper_Edit_Save | Microflow | 12 | 3 | 1 | 0 |
| ACT_WorkflowCommentHelper_SaveNew | Microflow | 11 | 3 | 1 | 0 |
| ACT_WorkflowCommentHelper_SaveNew_Admin | Microflow | 8 | 3 | 0 | 0 |
| ACT_WorkflowDefinition_CleanUp_Execute | Microflow | 7 | 3 | 2 | 0 |
| ACT_WorkflowDefinition_CleanUp_Open | Microflow | 6 | 3 | 1 | 0 |
| ACT_WorkflowDefinition_CloseActionConfirmation | Microflow | 5 | 3 | 0 | 0 |
| ACT_WorkflowDefinition_Delete | Microflow | 13 | 3 | 0 | 0 |
| ACT_WorkflowDefinition_Lock | Microflow | 10 | 3 | 0 | 0 |
| ACT_WorkflowDefinition_Unlock | Microflow | 10 | 3 | 0 | 0 |
| ACT_WorkflowDefinitionHelper_ShowLockPage | Microflow | 5 | 3 | 1 | 0 |
| ACT_WorkflowDefinitionHelper_ShowUnlockPage | Microflow | 5 | 3 | 1 | 0 |
| ACT_WorkflowJumpToDetails_Apply | Microflow | 9 | 3 | 1 | 0 |
| ACT_WorkflowSelectionHelper_Select | Nanoflow | 5 | 3 | 0 | 0 |
| ACT_WorkflowUserTask_Assign | Microflow | 6 | 3 | 1 | 0 |
| ACT_WorkflowUserTask_Assignees_Add | Microflow | 6 | 3 | 1 | 0 |
| ACT_WorkflowUserTask_Assignees_Remove | Microflow | 6 | 3 | 1 | 0 |
| ACT_WorkflowUserTask_TargetUsers_Add | Microflow | 6 | 3 | 1 | 0 |
| ACT_WorkflowUserTask_TargetUsers_Remove | Microflow | 6 | 3 | 1 | 0 |
| ACT_WorkflowUserTask_Unassign | Microflow | 5 | 3 | 1 | 0 |
| ACT_WorkflowView_ShowWorkflowAdminPage | Microflow | 4 | 3 | 1 | 0 |
| ACT_WorkflowView_WithdrawWorkflow | Microflow | 6 | 3 | 0 | 0 |
| ASu_Assignee_Migrate | Microflow | 10 | 3 | 3 | 0 |
| ASu_Key_Migrate | Microflow | 12 | 3 | 4 | 0 |
| DashboardContext_GetSelectedWorkflowDefinition | Microflow | 10 | 3 | 0 | 3 |
| DashboardContext_GetSelectedWorkflowTaskDefinition | Microflow | 10 | 3 | 0 | 3 |
| DS_AuditTrailViewer | Nanoflow | 4 | 3 | 1 | 0 |
| DS_Configuration | Microflow | 9 | 3 | 3 | 0 |
| DS_TaskAssignmentHelper_Account | Microflow | 5 | 3 | 0 | 0 |
| DS_TaskCount | Microflow | 4 | 3 | 1 | 0 |
| DS_TaskCount_Admin | Microflow | 3 | 3 | 0 | 0 |
| DS_TaskDashboard | Microflow | 4 | 3 | 2 | 0 |
| DS_TaskSeries | Microflow | 5 | 3 | 0 | 0 |
| DS_TimelineViewer_WorkflowActivityRecords_Full | Microflow | 9 | 3 | 0 | 0 |
| DS_TimelineViewer_WorkflowActivityRecords_Tasks | Microflow | 8 | 3 | 0 | 0 |
| DS_Workflow_LoadNotificationArea | Nanoflow | 11 | 3 | 0 | 0 |
| DS_Workflow_TimelineViewer | Nanoflow | 4 | 3 | 0 | 0 |
| DS_Workflow_WorkflowView | Microflow | 4 | 3 | 1 | 0 |
| DS_WorkflowActivityRecord_ActivityDuration | Nanoflow | 4 | 3 | 1 | 0 |
| DS_WorkflowActivityRecord_OverdueTime | Nanoflow | 6 | 3 | 1 | 0 |
| DS_WorkflowCommentHelper_InitializeNew | Microflow | 3 | 3 | 0 | 0 |
| DS_WorkflowCurrentActivity_Options | Microflow | 4 | 3 | 0 | 0 |
| DS_WorkflowDashboard | Microflow | 4 | 3 | 2 | 0 |
| DS_WorkflowDefinition_Overview | Microflow | 7 | 3 | 1 | 0 |
| DS_WorkflowDefinition_SelectableImplementation | Microflow | 12 | 3 | 0 | 0 |
| DS_WorkflowSelectionHelper | Microflow | 4 | 3 | 0 | 0 |
| DS_WorkflowSeries | Microflow | 5 | 3 | 0 | 0 |
| DS_WorkflowTask_AssignedToUser_Timeline | Microflow | 14 | 3 | 6 | 0 |
| DS_WorkflowTask_LoadNotificationArea | Nanoflow | 12 | 3 | 0 | 0 |
| DS_WorkflowTaskDefinition_Selectable_Administrator | Microflow | 7 | 3 | 0 | 0 |
| DS_WorkflowTaskDefinition_Selectable_UserImplementation | Microflow | 17 | 3 | 0 | 0 |
| DS_WorkflowTaskDetail | Microflow | 4 | 3 | 0 | 0 |
| DS_WorkflowUserTask_AssigneeHelper | Nanoflow | 6 | 3 | 0 | 0 |
| DS_WorkflowUserTask_WorkflowView | Microflow | 5 | 3 | 1 | 0 |
| DS_WorkflowView_LoadNotificationArea | Nanoflow | 11 | 3 | 0 | 0 |
| DS_WorkflowView_TimelineViewer | Microflow | 5 | 3 | 0 | 0 |
| OCh_CleanupHelper_UpdateCount | Microflow | 15 | 3 | 1 | 1 |
| OCh_DashboardContext_UpdateTaskDashboard | Microflow | 10 | 3 | 1 | 0 |
| OCh_DashboardContext_UpdateWorkflowDashboard | Microflow | 5 | 3 | 1 | 0 |
| OCh_Workflow_State | Microflow | 6 | 3 | 1 | 0 |
| OCh_WorkflowCurrentActivity_Target | Nanoflow | 7 | 3 | 0 | 0 |
| OCh_WorkflowUserTask_State | Microflow | 9 | 3 | 2 | 0 |
| OCl_WorkflowSummary | Microflow | 5 | 3 | 0 | 0 |
| SE_WorkflowAuditTrailRecord_CleanUp | Microflow | 3 | 3 | 1 | 0 |
| SUB_Assignee_Migrate | Microflow | 14 | 3 | 0 | 2 |
| SUB_AssigneeMigration_Verify | Microflow | 9 | 3 | 0 | 2 |
| SUB_AuditTrailViewer_Default | Nanoflow | 4 | 3 | 0 | 2 |
| SUB_CleanupHelper_Execute_Workflow | Microflow | 19 | 3 | 0 | 1 |
| SUB_CleanupHelper_Execute_WorkflowView | Microflow | 19 | 3 | 0 | 1 |
| SUB_CleanupHelper_Validate | Microflow | 14 | 3 | 0 | 1 |
| SUB_Configuration_FindOrCreate | Microflow | 6 | 3 | 0 | 5 |
| SUB_DashboardContext_RetrieveOrCreate | Microflow | 7 | 3 | 0 | 2 |
| SUB_DashboardContext_UpdateSettings | Microflow | 6 | 3 | 0 | 2 |
| SUB_Duration_Calculate | Nanoflow | 20 | 3 | 0 | 2 |
| SUB_KeyMigration_Verify | Microflow | 11 | 3 | 0 | 2 |
| SUB_TaskAssignmentHelper_Reassign | Microflow | 13 | 3 | 3 | 1 |
| SUB_TaskAssignmentHelper_Retarget | Microflow | 12 | 3 | 2 | 1 |
| SUB_TaskAssignmentHelper_TaskCount | Microflow | 5 | 3 | 0 | 3 |
| SUB_TaskAssignmentHelper_Unassign | Microflow | 11 | 3 | 2 | 1 |
| SUB_TaskCount_Update | Microflow | 13 | 3 | 0 | 3 |
| SUB_TaskDashboard_Update | Microflow | 9 | 3 | 5 | 2 |
| SUB_TaskKey_Migrate | Microflow | 15 | 3 | 1 | 2 |
| SUB_TaskSeries_CreateOrUpdate | Microflow | 18 | 3 | 4 | 2 |
| SUB_TaskSeriesList_Delete | Microflow | 5 | 3 | 0 | 1 |
| SUB_TaskSummary_CreateOrUpdate | Microflow | 19 | 3 | 8 | 2 |
| SUB_TaskSummary_RetrieveOrCreate | Microflow | 7 | 3 | 0 | 1 |
| SUB_User_GetAccount | Microflow | 7 | 3 | 0 | 0 |
| SUB_UserTask_Assign | Microflow | 13 | 3 | 1 | 3 |
| SUB_UserTask_AssignedToUser | Microflow | 8 | 3 | 0 | 0 |
| SUB_UserTask_Assignee_Add | Microflow | 7 | 3 | 1 | 3 |
| SUB_UserTask_Assignee_Remove | Microflow | 7 | 3 | 1 | 3 |
| SUB_UserTask_Assignees_Add | Microflow | 15 | 3 | 0 | 3 |
| SUB_UserTask_Assignees_Remove | Microflow | 15 | 3 | 0 | 3 |
| SUB_UserTask_AverageHandlingTime | Microflow | 12 | 3 | 0 | 2 |
| SUB_UserTask_CountAlmostDue | Microflow | 11 | 3 | 0 | 1 |
| SUB_UserTask_CountCompleted | Microflow | 9 | 3 | 0 | 1 |
| SUB_UserTask_CountCompletedOnTime | Microflow | 9 | 3 | 0 | 2 |
| SUB_UserTask_CountCompletedOverdue | Microflow | 9 | 3 | 0 | 1 |
| SUB_UserTask_CountFailed | Microflow | 9 | 3 | 0 | 1 |
| SUB_UserTask_CountInProgress | Microflow | 9 | 3 | 0 | 1 |
| SUB_UserTask_CountOverdue | Microflow | 13 | 3 | 0 | 1 |
| SUB_UserTask_TargetUser_Add | Microflow | 7 | 3 | 1 | 1 |
| SUB_UserTask_TargetUser_Remove | Microflow | 7 | 3 | 1 | 3 |
| SUB_UserTask_TargetUsers_Add | Microflow | 15 | 3 | 0 | 2 |
| SUB_UserTask_TargetUsers_Remove | Microflow | 15 | 3 | 0 | 2 |
| SUB_UserTaskOutcome_AssignedToUser | Microflow | 8 | 3 | 0 | 1 |
| SUB_UserTaskOutcomeView_AssignedToUser | Microflow | 8 | 3 | 0 | 1 |
| SUB_UserTaskOutcomeView_FindOrCreate | Microflow | 8 | 3 | 0 | 1 |
| SUB_UserTaskView_FindOrCreate | Microflow | 9 | 3 | 0 | 1 |
| SUB_UserTaskView_UpdateKey | Microflow | 9 | 3 | 0 | 1 |
| SUB_Workflow_AverageHandlingTime | Microflow | 12 | 3 | 0 | 1 |
| SUB_Workflow_CountAlmostDue | Microflow | 11 | 3 | 0 | 1 |
| SUB_Workflow_CountCompleted | Microflow | 9 | 3 | 0 | 1 |
| SUB_Workflow_CountCompletedOnTime | Microflow | 9 | 3 | 0 | 1 |
| SUB_Workflow_CountCompletedOverdue | Microflow | 9 | 3 | 0 | 1 |
| SUB_Workflow_CountInProgress | Microflow | 9 | 3 | 0 | 1 |
| SUB_Workflow_CountOverdue | Microflow | 13 | 3 | 0 | 1 |
| SUB_Workflow_Retry | Microflow | 5 | 3 | 0 | 3 |
| SUB_Workflow_ShowWorkflowAdminPage | Microflow | 7 | 3 | 0 | 2 |
| SUB_WorkflowAuditTrailRecord_CleanUp | Microflow | 16 | 3 | 1 | 1 |
| SUB_WorkflowAuditTrailRecord_DeleteByKey | Microflow | 6 | 3 | 0 | 1 |
| SUB_WorkflowDashboard_Update | Microflow | 12 | 3 | 8 | 2 |
| SUB_WorkflowDefinitionHelper_FindOrCreate | Microflow | 9 | 3 | 0 | 2 |
| SUB_WorkflowEvent_AuditTrail | Microflow | 10 | 3 | 0 | 1 |
| SUB_WorkflowJumpToDetails_Validate | Microflow | 8 | 3 | 0 | 1 |
| SUB_WorkflowKey_Migrate | Microflow | 15 | 3 | 1 | 2 |
| SUB_WorkflowSeries_CreateOrUpdate | Microflow | 15 | 3 | 4 | 1 |
| SUB_WorkflowSeriesList_Delete | Microflow | 5 | 3 | 0 | 1 |
| SUB_WorkflowSummary_CreateOrUpdate | Microflow | 10 | 3 | 5 | 2 |
| SUB_WorkflowSummary_RetrieveOrCreate | Microflow | 10 | 3 | 0 | 1 |
| SUB_WorkflowTask_AverageHandlingTime | Microflow | 10 | 3 | 0 | 1 |
| SUB_WorkflowTaskDetail_CreateOrUpdate | Microflow | 11 | 3 | 2 | 1 |
| SUB_WorkflowTaskDetail_Delete | Microflow | 5 | 3 | 0 | 1 |
| SUB_WorkflowTaskTimeline_Completed | Microflow | 8 | 3 | 0 | 1 |
| SUB_WorkflowTaskTimeline_InProgress | Microflow | 8 | 3 | 0 | 1 |
| SUB_WorkflowView_CommentAttachment_Validate | Microflow | 11 | 3 | 1 | 3 |
| SUB_WorkflowView_CurrentUserIsTargeted | Microflow | 7 | 3 | 0 | 1 |
| SUB_WorkflowView_FindOrCreate | Microflow | 8 | 3 | 0 | 3 |
| SUB_WorkflowView_ShowWorkflowAdminPage | Microflow | 8 | 3 | 1 | 2 |
| SUB_WorkflowView_UpdateKey | Microflow | 9 | 3 | 0 | 1 |
| WFEH_WorkflowEvent_AuditTrail | Microflow | 6 | 3 | 1 | 0 |

## Tier 1 Deep Narratives

No Tier 1 narrative required for this module category.
