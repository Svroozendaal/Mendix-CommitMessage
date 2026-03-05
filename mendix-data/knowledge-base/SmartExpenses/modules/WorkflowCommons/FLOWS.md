# Flows: WorkflowCommons

## Flow Catalogue

### Action Flows (ACT_*)

| Flow | Nodes | Key Actions | Pages Shown |
|---|---:|---|---|
| ACT_Assignee_Migrate | 5 | call microflow WorkflowCommons.SUB_Assignee_Migrate -> TotalCount, call microflow WorkflowCommons.SUB_Configuration_FindOrCreate -> Configuration | Unknown |
| ACT_Attachment_Create | 5 | create WorkflowCommons.WorkflowAttachment as NewWorkflowAttachment (WorkflowAttachment_WorkflowComment=$WorkflowComment), show page WorkflowCommons.WorkflowAttachment_New | Unknown |
| ACT_Attachment_Download | 4 | DownloadFileAction (errorHandlingType=Rollback) | Unknown |
| ACT_Attachment_Save | 14 | close page, commit WorkflowAttachment (refreshInClient=true, withEvents=true) | Unknown |
| ACT_Attachment_Save_Admin | 8 | close page, commit WorkflowAttachment (refreshInClient=true, withEvents=true) | Unknown |
| ACT_AuditTrailViewer_All | 4 | change AuditTrailViewer (Configuration='{ "name": "dataGrid2_auditTrail", "schemaVersion": 2, "settingsHash": "4081041333", "columns": [ { "columnId": "0", "hidden": false }, { "columnId": "1", "hidden": false }, { "col..., ActiveView=WorkflowCommons.Enum_AuditTrail_View.All; refreshInClient=true) | Unknown |
| ACT_AuditTrailViewer_Default | 4 | call nanoflow WorkflowCommons.SUB_AuditTrailViewer_Default | Unknown |
| ACT_AuditTrailViewer_Minimal | 4 | change AuditTrailViewer (Configuration='{ "name": "dataGrid2_auditTrail", "schemaVersion": 2, "settingsHash": "4081041333", "columns": [ { "columnId": "0", "hidden": false }, { "columnId": "1", "hidden": false }, { "col..., ActiveView=WorkflowCommons.Enum_AuditTrail_View.Minimal; refreshInClient=true) | Unknown |
| ACT_Comment_Delete | 4 | delete WorkflowComment (refreshInClient=true) | Unknown |
| ACT_DashboardContext_Refresh | 4 | change DashboardContext (refreshInClient=true) | Unknown |
| ACT_DoNothing | 5 | none | Unknown |
| ACT_Key_Migrate | 7 | call microflow WorkflowCommons.SUB_Configuration_FindOrCreate -> Configuration, change Configuration (VerifiedKeyMigration=true, ShowKeyMigration=false; refreshInClient=true) | Unknown |
| ACT_TaskAssignment_Show | 4 | create WorkflowCommons.TaskAssignmentHelper as NewTaskAssignmentHelper, show page WorkflowCommons.ManageTaskAssignments | Unknown |
| ACT_TaskAssignmentHelper_Reassign | 10 | change NewAssignee (refreshInClient=true), close page (pagesToClose=2) | Unknown |
| ACT_TaskAssignmentHelper_Reassign_Show | 9 | change TaskAssignmentHelper (TaskAssignmentHelper_WorkflowUserTask=$WorkflowUserTaskList; refreshInClient=false), show message (text=Select one or more tasks to continue., type=Error, blocking=true) | Unknown |
| ACT_TaskAssignmentHelper_Retarget | 10 | change NewTargetUser (refreshInClient=true), close page (pagesToClose=2) | Unknown |
| ACT_TaskAssignmentHelper_Retarget_Show | 9 | change TaskAssignmentHelper (TaskAssignmentHelper_WorkflowUserTask=$WorkflowUserTaskList; refreshInClient=false), show message (text=Select one or more tasks to continue., type=Error, blocking=true) | Unknown |
| ACT_TaskAssignmentHelper_Unassign | 8 | call microflow WorkflowCommons.SUB_TaskAssignmentHelper_TaskCount -> TaskCount, close page (pagesToClose=1) | Unknown |
| ACT_TaskAssignmentHelper_Unassign_Show | 9 | change TaskAssignmentHelper (TaskAssignmentHelper_WorkflowUserTask=$WorkflowUserTaskList; refreshInClient=false), show message (text=Select one or more tasks to continue., type=Error, blocking=true) | Unknown |
| ACT_TaskCount_Refresh | 9 | call javascript action NanoflowCommons.RefreshEntity -> ReturnValueName, change TaskCount (refreshInClient=true) | Unknown |
| ACT_TaskCount_Update | 4 | call microflow WorkflowCommons.SUB_TaskCount_Update | Unknown |
| ACT_TimelineViewer_OpenSubWorkflow | 11 | change TimelineViewer (TimelineViewer_Workflow=$Workflow; refreshInClient=true), retrieve Workflow over association WorkflowRecord_Workflow from WorkflowRecordForSubWorkflow | Unknown |
| ACT_TimelineViewer_OpenWorkflow | 7 | change TimelineViewer (TimelineViewer_Workflow=$Workflow; refreshInClient=true) | Unknown |
| ACT_UserTask_AssignToMe | 4 | call microflow WorkflowCommons.SUB_UserTask_Assign -> IsAssigned | Unknown |
| ACT_UserTask_AssignToMe_UpdateTaskCount | 9 | call microflow WorkflowCommons.SUB_TaskCount_Update, change TaskCount (refreshInClient=true) | Unknown |
| ACT_UserTask_AssignToUser | 5 | call microflow WorkflowCommons.SUB_UserTask_Assign -> IsAssigned | Unknown |
| ACT_UserTask_AssignToUsers | 8 | call microflow WorkflowCommons.SUB_UserTask_Assignees_Add, show message (text=This action is only available for multi-user tasks., type=Error, blocking=true) | Unknown |
| ACT_UserTask_ShowDefaultAdminPage | 8 | LogMessageAction (errorHandlingType=Rollback), OpenWorkflowAction (errorHandlingType=CustomWithoutRollBack) | Unknown |
| ACT_UserTask_Unassign | 4 | call microflow WorkflowCommons.SUB_UserTask_Assignee_Remove | Unknown |
| ACT_UserTaskView_ShowUserTaskPage | 8 | OpenUserTaskAction (errorHandlingType=Rollback), retrieve WorkflowUserTask over association UserTaskView_WorkflowUserTask from UserTaskView | Unknown |
| ACT_UserTaskView_ShowWorkflowAdminPage | 5 | call microflow WorkflowCommons.SUB_WorkflowView_ShowWorkflowAdminPage, retrieve WorkflowView over association UserTaskView_WorkflowView from UserTaskView | Unknown |
| ACT_Workflow_Abort | 8 | LogMessageAction (errorHandlingType=Rollback), show message (text=Failed to abort workflow, please contact your system administrator., type=Information, blocking=true) | Unknown |
| ACT_Workflow_CloseActionConfirmation | 8 | change IteratorWorkflowUserTask (refreshInClient=true), change Workflow (refreshInClient=true) | Unknown |
| ACT_Workflow_Continue | 5 | show page WorkflowCommons.Workflow_ActionConfirmation, WorkflowOperationAction (errorHandlingType=Rollback) | Unknown |
| ACT_Workflow_JumpTo | 8 | GenerateJumpToOptionsAction (output=WorkflowJumpToDetails, errorHandlingType=Rollback), show message (text=It is not allowed to apply jump to activity for this workflow., type=Information, blocking=true) | Unknown |
| ACT_Workflow_OpenParentWorkflow | 5 | call microflow WorkflowCommons.SUB_Workflow_ShowWorkflowAdminPage, retrieve ParentWorkflow over association Workflow_ParentWorkflow from Workflow | Unknown |
| ACT_Workflow_Pause | 5 | show page WorkflowCommons.Workflow_ActionConfirmation, WorkflowOperationAction (errorHandlingType=Rollback) | Unknown |
| ACT_Workflow_Restart | 5 | show page WorkflowCommons.Workflow_ActionConfirmation, WorkflowOperationAction (errorHandlingType=Rollback) | Unknown |
| ACT_Workflow_Retry | 9 | AggregateListAction (output=Count, errorHandlingType=Rollback), call microflow WorkflowCommons.SUB_Workflow_Retry | Unknown |
| ACT_Workflow_Retry_KeepTargetedUsers | 5 | call microflow WorkflowCommons.SUB_Workflow_Retry, close page | Unknown |
| ACT_Workflow_Retry_RerunUserTargeting | 9 | change IteratorWorkflowUserTask (WorkflowUserTask_TargetUsers=empty; refreshInClient=false), close page | Unknown |
| ACT_Workflow_Unpause | 5 | show page WorkflowCommons.Workflow_ActionConfirmation, WorkflowOperationAction (errorHandlingType=Rollback) | Unknown |
| ACT_Workflow_WithdrawConfirmation | 17 | change WorkflowView (State=$Workflow/State, Reason=$Workflow/Reason, EndTime=$Workflow/EndTime; refreshInClient=true), close page | Unknown |
| ACT_WorkflowAuditTrailRecord_ExportToExcel | 3 | call javascript action DataWidgets.Export_To_Excel -> ExportSuccess | Unknown |
| ACT_WorkflowAuditTrailRecord_Refresh | 3 | call javascript action NanoflowCommons.RefreshEntity -> Variable | Unknown |
| ACT_WorkflowComment_Edit | 9 | create WorkflowCommons.WorkflowCommentHelper as NewWorkflowCommentHelper (WorkflowCommentHelper_WorkflowComment=$WorkflowComment, Content=$WorkflowComment/Content), show message (text=You can no longer edit this comment for security reasons., type=Information, blocking=true) | Unknown |
| ACT_WorkflowCommentHelper_Edit_Save | 12 | change WorkflowComment (Content=$WorkflowCommentHelper/Content; refreshInClient=true), close page | Unknown |
| ACT_WorkflowCommentHelper_SaveNew | 11 | change WorkflowCommentHelper (Content=empty; refreshInClient=false), create WorkflowCommons.WorkflowComment as NewWorkflowComment (Content=$WorkflowCommentHelper/Content, WorkflowComment_WorkflowView=$WorkflowView, WorkflowComment_Workflow=$WorkflowView/WorkflowCommons.WorkflowView_Workflow) | Unknown |
| ACT_WorkflowCommentHelper_SaveNew_Admin | 8 | change WorkflowCommentHelper (Content=empty; refreshInClient=false), create WorkflowCommons.WorkflowComment as NewWorkflowComment (Content=$WorkflowCommentHelper/Content, WorkflowComment_WorkflowView=$WorkflowView, WorkflowComment_Workflow=$WorkflowView/WorkflowCommons.WorkflowView_Workflow) | Unknown |
| ACT_WorkflowDefinition_CleanUp_Execute | 7 | call microflow WorkflowCommons.SUB_CleanupHelper_Execute_Workflow -> WorkflowCount, close page | Unknown |
| ACT_WorkflowDefinition_CleanUp_Open | 6 | call microflow WorkflowCommons.OCh_CleanupHelper_UpdateCount, create WorkflowCommons.CleanupHelper as NewCleanupHelper (CleanupHelper_WorkflowDefinition=$WorkflowDefinition) | Unknown |
| ACT_WorkflowDefinition_CloseActionConfirmation | 5 | change WorkflowDefinition (refreshInClient=true), close page | Unknown |
| ACT_WorkflowDefinition_Delete | 13 | AggregateListAction (output=WorkflowCount, errorHandlingType=Rollback), close page | Unknown |
| ACT_WorkflowDefinition_Lock | 10 | close page, LockWorkflowAction (errorHandlingType=Rollback) | Unknown |
| ACT_WorkflowDefinition_Unlock | 10 | close page, retrieve WorkflowDefinition over association WorkflowDefinitionHelper_WorkflowDefinition from WorkflowDefinitionHelper | Unknown |
| ACT_WorkflowDefinitionHelper_ShowLockPage | 5 | call microflow WorkflowCommons.SUB_WorkflowDefinitionHelper_FindOrCreate -> WorkflowDefinitionHelper, show page WorkflowCommons.WorkflowDefinition_Lock | Unknown |
| ACT_WorkflowDefinitionHelper_ShowUnlockPage | 5 | call microflow WorkflowCommons.SUB_WorkflowDefinitionHelper_FindOrCreate -> WorkflowDefinitionHelper, show page WorkflowCommons.WorkflowDefinition_Unlock | Unknown |
| ACT_WorkflowJumpToDetails_Apply | 9 | ApplyJumpToOptionAction (errorHandlingType=Rollback), call microflow WorkflowCommons.SUB_WorkflowJumpToDetails_Validate -> Valid | Unknown |
| ACT_WorkflowSelectionHelper_Select | 5 | change WorkflowSelectionHelper (WorkflowSelectionHelper_WorkflowView=$WorkflowView; refreshInClient=true) | Unknown |
| ACT_WorkflowUserTask_Assign | 6 | call microflow WorkflowCommons.SUB_UserTask_Assignee_Add, close page | Unknown |
| ACT_WorkflowUserTask_Assignees_Add | 6 | call microflow WorkflowCommons.SUB_UserTask_Assignees_Add, close page | Unknown |
| ACT_WorkflowUserTask_Assignees_Remove | 6 | call microflow WorkflowCommons.SUB_UserTask_Assignees_Remove, close page | Unknown |
| ACT_WorkflowUserTask_TargetUsers_Add | 6 | call microflow WorkflowCommons.SUB_UserTask_TargetUsers_Add, close page | Unknown |
| ACT_WorkflowUserTask_TargetUsers_Remove | 6 | call microflow WorkflowCommons.SUB_UserTask_TargetUsers_Remove, close page | Unknown |
| ACT_WorkflowUserTask_Unassign | 5 | call microflow WorkflowCommons.SUB_UserTask_Assignees_Remove, retrieve UserList over association WorkflowUserTask_Assignees from WorkflowUserTask | Unknown |
| ACT_WorkflowView_ShowWorkflowAdminPage | 4 | call microflow WorkflowCommons.SUB_WorkflowView_ShowWorkflowAdminPage | Unknown |
| ACT_WorkflowView_WithdrawWorkflow | 6 | create WorkflowCommons.WorkflowComment as NewWorkflowComment (WorkflowComment_Workflow=$Workflow, WorkflowComment_WorkflowView=$WorkflowView), retrieve Workflow over association WorkflowView_Workflow from WorkflowView | Unknown |

### Data Sources (DS_*)

| Flow | Nodes | Key Actions | Returns |
|---|---:|---|---|
| DS_AuditTrailViewer | 4 | call nanoflow WorkflowCommons.SUB_AuditTrailViewer_Default, create WorkflowCommons.AuditTrailViewer as NewAuditTrailViewer | Unknown |
| DS_Configuration | 9 | call microflow WorkflowCommons.SUB_AssigneeMigration_Verify -> Variable, call microflow WorkflowCommons.SUB_Configuration_FindOrCreate -> Configuration | Unknown |
| DS_TaskAssignmentHelper_Account | 5 | retrieve AccountList from Administration.Account, retrieve SelectedUser over association TaskAssignmentHelper_Account from TaskAssignmentHelper | Unknown |
| DS_TaskCount | 4 | call microflow WorkflowCommons.SUB_TaskCount_Update, create WorkflowCommons.TaskCount as NewTaskCount | Unknown |
| DS_TaskCount_Admin | 3 | create WorkflowCommons.TaskCount as NewTaskCount | Unknown |
| DS_TaskDashboard | 4 | call microflow WorkflowCommons.SUB_DashboardContext_RetrieveOrCreate -> DashboardContext, call microflow WorkflowCommons.SUB_TaskDashboard_Update | Unknown |
| DS_TaskSeries | 5 | ListOperationAction (output=SortedTaskSeriesList, errorHandlingType=Rollback), retrieve TaskSeriesList over association TaskSeries_DashboardContext from DashboardContext | Unknown |
| DS_TimelineViewer_WorkflowActivityRecords_Full | 9 | GetWorkflowActivityRecordsAction (output=WorkflowActivityRecords, errorHandlingType=Rollback), ListOperationAction (output=WorkflowActivityRecordList, errorHandlingType=Rollback) | Unknown |
| DS_TimelineViewer_WorkflowActivityRecords_Tasks | 8 | GetWorkflowActivityRecordsAction (output=WorkflowActivityRecords, errorHandlingType=Rollback), ListOperationAction (output=UserTaskActivityRecordList, errorHandlingType=Rollback) | Unknown |
| DS_Workflow_LoadNotificationArea | 11 | create WorkflowCommons.NotificationArea as NewNotificationAreaBlocked (AlertTitle='Workflow is blocked', AlertMessage='See state information for more details. Please contact the development team to fix this or as an admin abort/initiator withdraw this workflow.', RenderAs=WorkflowCommons.Enum_NotificationArea_RenderAs.Error), create WorkflowCommons.NotificationArea as NewNotificationAreaPaused (AlertTitle='Workflow is paused', AlertMessage='This workflow was paused an administrator is able to unpause and allow further execution.', RenderAs=WorkflowCommons.Enum_NotificationArea_RenderAs.Warning) | Unknown |
| DS_Workflow_TimelineViewer | 4 | create WorkflowCommons.TimelineViewer as NewTimelineViewer (TimelineViewer_Workflow=$Workflow) | Unknown |
| DS_Workflow_WorkflowView | 4 | call microflow WorkflowCommons.SUB_WorkflowView_FindOrCreate -> WorkflowView | Unknown |
| DS_WorkflowActivityRecord_ActivityDuration | 4 | call nanoflow WorkflowCommons.SUB_Duration_Calculate -> DurationHelper | Unknown |
| DS_WorkflowActivityRecord_OverdueTime | 6 | call nanoflow WorkflowCommons.SUB_Duration_Calculate -> DurationHelper | Unknown |
| DS_WorkflowCommentHelper_InitializeNew | 3 | create WorkflowCommons.WorkflowCommentHelper as NewWorkflowCommentHelper | Unknown |
| DS_WorkflowCurrentActivity_Options | 4 | retrieve WorkflowActivityDetailsList over association WorkflowCurrentActivity_ApplicableTargets from WorkflowCurrentActivity | Unknown |
| DS_WorkflowDashboard | 4 | call microflow WorkflowCommons.SUB_DashboardContext_RetrieveOrCreate -> DashboardContext, call microflow WorkflowCommons.SUB_WorkflowDashboard_Update | Unknown |
| DS_WorkflowDefinition_Overview | 7 | change WorkflowSummaryList (type=Add, value=$WorkflowSummary), CreateListAction (output=WorkflowSummaryList, entity=WorkflowCommons.WorkflowSummary, errorHandlingType=Rollback) | Unknown |
| DS_WorkflowDefinition_SelectableImplementation | 12 | change NewWorkflowDefinitionHelperList (type=Add, value=$NewDefinitionHelper), CreateListAction (output=NewWorkflowDefinitionHelperList, entity=WorkflowCommons.DefinitionHelper, errorHandlingType=Rollback) | Unknown |
| DS_WorkflowSelectionHelper | 4 | create WorkflowCommons.WorkflowSelectionHelper as NewWorkflowSelectionHelper (WorkflowSelectionHelper_WorkflowView=$WorkflowView), retrieve WorkflowView from WorkflowCommons.WorkflowView | Unknown |
| DS_WorkflowSeries | 5 | ListOperationAction (output=SortedWorkflowSeriesList, errorHandlingType=Rollback), retrieve WorkflowSeriesList over association WorkflowSeries_DashboardContext from DashboardContext | Unknown |
| DS_WorkflowTask_AssignedToUser_Timeline | 14 | CreateListAction (output=UserTaskTimeLineList, entity=WorkflowCommons.UserTaskTimeLine, errorHandlingType=Rollback), ListOperationAction (output=NewUserTaskTimeLineList_Sorted, errorHandlingType=Rollback) | Unknown |
| DS_WorkflowTask_LoadNotificationArea | 12 | create WorkflowCommons.NotificationArea as NewNotificationAreaBlockedTask (AlertTitle='Task is blocked', AlertMessage='This task cannot be completed as a result of changes in the workflow. Please contact your workflow administrator.', RenderAs=WorkflowCommons.Enum_NotificationArea_RenderAs.Error), create WorkflowCommons.NotificationArea as NewNotificationAreaPaused (AlertTitle='Task is paused', AlertMessage='This workflow was paused by an administrator. Please contact your workflow administrator.', RenderAs=WorkflowCommons.Enum_NotificationArea_RenderAs.Warning) | Unknown |
| DS_WorkflowTaskDefinition_Selectable_Administrator | 7 | retrieve WorkflowDefinition_Selected over association DashboardContext_WorkflowDefinition from DashboardContext, retrieve WorkflowTaskDefinition from System.WorkflowUserTaskDefinition | Unknown |
| DS_WorkflowTaskDefinition_Selectable_UserImplementation | 17 | change NewWorkflowDefinitionHelperList (type=Add, value=$NewDefinitionHelper), CreateListAction (output=NewWorkflowDefinitionHelperList, entity=WorkflowCommons.DefinitionHelper, errorHandlingType=Rollback) | Unknown |
| DS_WorkflowTaskDetail | 4 | retrieve WorkflowTaskDetail over association WorkflowTaskDetail_DashboardContext from DashboardContext | Unknown |
| DS_WorkflowUserTask_AssigneeHelper | 6 | create WorkflowCommons.AssignmentHelper as NewAssignmentHelper (IsAssignedUser=$CurrentUserAssignee != empty), ListOperationAction (output=CurrentUserAssignee, errorHandlingType=Abort) | Unknown |
| DS_WorkflowUserTask_WorkflowView | 5 | call microflow WorkflowCommons.SUB_WorkflowView_FindOrCreate -> WorkflowView, retrieve Workflow over association WorkflowUserTask_Workflow from WorkflowUserTask | Unknown |
| DS_WorkflowView_LoadNotificationArea | 11 | create WorkflowCommons.NotificationArea as NewNotificationAreaBlocked (AlertTitle='Workflow is blocked', AlertMessage='See state information for more details. Please contact the development team to fix this or as an admin abort/initiator withdraw this workflow.', RenderAs=WorkflowCommons.Enum_NotificationArea_RenderAs.Error), create WorkflowCommons.NotificationArea as NewNotificationAreaPaused (AlertTitle='Workflow is paused', AlertMessage='This workflow was paused an administrator is able to unpause and allow further execution.', RenderAs=WorkflowCommons.Enum_NotificationArea_RenderAs.Warning) | Unknown |
| DS_WorkflowView_TimelineViewer | 5 | create WorkflowCommons.TimelineViewer as NewTimelineViewer (TimelineViewer_Workflow=$Workflow), retrieve Workflow over association WorkflowView_Workflow from WorkflowView | Unknown |

### Validation Flows (VAL_*)

| Flow | Nodes | Key Actions |
|---|---:|---|
| none | 0 | none |

### Other Flows

| Flow | Type | Nodes | Key Actions |
|---|---|---:|---|
| ASu_Assignee_Migrate | Microflow | 10 | LogMessageAction (errorHandlingType=Rollback) |
| ASu_Key_Migrate | Microflow | 12 | change Configuration (VerifiedKeyMigration=true, ShowKeyMigration=false; refreshInClient=true), LogMessageAction (errorHandlingType=Rollback) |
| DashboardContext_GetSelectedWorkflowDefinition | Microflow | 10 | retrieve DefinitionHelper over association DashboardContext_DefinitionHelperWorkflow from DashboardContext, retrieve WorkflowDefinition over association DashboardContext_WorkflowDefinition from DashboardContext |
| DashboardContext_GetSelectedWorkflowTaskDefinition | Microflow | 10 | retrieve DefinitionHelper over association DashboardContext_DefinitionHelperTask from DashboardContext, retrieve WorkflowTaskDefinitionFromHelper from System.WorkflowUserTaskDefinition |
| OCh_CleanupHelper_UpdateCount | Microflow | 15 | AggregateListAction (output=TotalCount, errorHandlingType=Rollback), change CleanupHelper (TotalCount=$TotalCount; refreshInClient=true) |
| OCh_DashboardContext_UpdateTaskDashboard | Microflow | 10 | change DashboardContext (DashboardContext_DefinitionHelperTask=empty; refreshInClient=false), change DashboardContext (refreshInClient=true) |
| OCh_DashboardContext_UpdateWorkflowDashboard | Microflow | 5 | call microflow WorkflowCommons.SUB_WorkflowDashboard_Update, change DashboardContext (refreshInClient=true) |
| OCh_Workflow_State | Microflow | 6 | change WorkflowView (EndTime=$Workflow/EndTime, DueDate=$Workflow/DueDate, State=$Workflow/State, Reason=$Workflow/Reason; refreshInClient=true), LogMessageAction (errorHandlingType=Rollback) |
| OCh_WorkflowCurrentActivity_Target | Nanoflow | 7 | change WorkflowCurrentActivity (Action=System.WorkflowCurrentActivityAction.DoNothing; refreshInClient=true), change WorkflowCurrentActivity (Action=System.WorkflowCurrentActivityAction.JumpTo; refreshInClient=true) |
| OCh_WorkflowUserTask_State | Microflow | 9 | change UserTaskView (UserTaskView_TargetUsers=$UserTask/System.WorkflowUserTask_TargetUsers, UserTaskView_Assignees=$UserTask/System.WorkflowUserTask_Assignees, EndTime=$UserTask/EndTime, DueDate=$UserTask/DueDate, Outcome=$UserTask/Outcome, State=$UserTask/State, CompletionType=$UserTask/CompletionType; refreshInClient=true), LogMessageAction (errorHandlingType=Rollback) |
| OCl_WorkflowSummary | Microflow | 5 | retrieve WorkflowDefinition over association WorkflowSummary_WorkflowDefinition from WorkflowSummary, show page WorkflowCommons.WorkflowDefinition_View |
| SE_WorkflowAuditTrailRecord_CleanUp | Microflow | 3 | call microflow WorkflowCommons.SUB_WorkflowAuditTrailRecord_CleanUp |
| SUB_Assignee_Migrate | Microflow | 14 | AggregateListAction (output=Count, errorHandlingType=Rollback), change Configuration (VerifiedAssigneeMigration=true, ShowAssigneeMigration=false; refreshInClient=true) |
| SUB_AssigneeMigration_Verify | Microflow | 9 | AggregateListAction (output=CountUserTaskView, errorHandlingType=Rollback), change Configuration (ShowAssigneeMigration=$MigrationRequired, VerifiedAssigneeMigration=true; refreshInClient=false) |
| SUB_AuditTrailViewer_Default | Nanoflow | 4 | change AuditTrailViewer (Configuration='{ "name": "dataGrid2_auditTrail", "schemaVersion": 2, "settingsHash": "4081041333", "columns": [ { "columnId": "0", "hidden": false }, { "columnId": "1", "hidden": false }, { "col..., ActiveView=WorkflowCommons.Enum_AuditTrail_View._Default; refreshInClient=true) |
| SUB_CleanupHelper_Execute_Workflow | Microflow | 19 | AggregateListAction (output=BatchCount, errorHandlingType=Rollback), AggregateListAction (output=TotalCount, errorHandlingType=Rollback) |
| SUB_CleanupHelper_Execute_WorkflowView | Microflow | 19 | AggregateListAction (output=BatchCount, errorHandlingType=Rollback), AggregateListAction (output=TotalCount, errorHandlingType=Rollback) |
| SUB_CleanupHelper_Validate | Microflow | 14 | change variable IsValid=false |
| SUB_Configuration_FindOrCreate | Microflow | 6 | create WorkflowCommons.Configuration as NewConfiguration, retrieve Configuration from WorkflowCommons.Configuration |
| SUB_DashboardContext_RetrieveOrCreate | Microflow | 7 | create WorkflowCommons.DashboardContext as NewDashboardContext (DashboardContext_Session=$currentSession), ListOperationAction (output=DashboardContext, errorHandlingType=Rollback) |
| SUB_DashboardContext_UpdateSettings | Microflow | 6 | change DashboardContext (TimeFrame=if $DashboardContext/TimeFrame = empty then WorkflowCommons.Enum_DashboardTimeFrame.Last_7_days else $DashboardContext/TimeFrame; refreshInClient=false), change DashboardContext (TimeFrameEnd=[%EndOfCurrentDay%], TimeFrameStart=if $DashboardContext/TimeFrame = WorkflowCommons.Enum_DashboardTimeFrame.Last_7_days then addDays([%BeginOfCurrentDay%],-6) else if $DashboardContext/TimeFrame = WorkflowCommons.En..., TimeFrameStepUnit=if $DashboardContext/TimeFrame = WorkflowCommons.Enum_DashboardTimeFrame.Last_7_days then WorkflowCommons.Enum_TimeFrameStepUnit.Day else if $DashboardContext/TimeFrame = WorkflowC..., LastUpdate=[%CurrentDateTime%], DashboardContext_WorkflowTaskDefinition=if $DashboardContext/WorkflowCommons.DashboardContext_WorkflowDefinition = empty then empty else $DashboardContext/WorkflowCommons.DashboardContext_WorkflowTaskDefinition; refreshInClient=false) |
| SUB_Duration_Calculate | Nanoflow | 20 | change NewDurationHelper (Duration=$DurationInSeconds + ' ' + getCaption(WorkflowCommons.Enum_DurationUnit.Seconds); refreshInClient=true), change NewDurationHelper (Duration=ceil($DurationInSeconds div (60 * 60 * 24)) + ' ' + getCaption(WorkflowCommons.Enum_DurationUnit.Days); refreshInClient=true) |
| SUB_KeyMigration_Verify | Microflow | 11 | AggregateListAction (output=CountUserTaskView, errorHandlingType=Rollback), AggregateListAction (output=CountWorkflowView, errorHandlingType=Rollback) |
| SUB_TaskAssignmentHelper_Reassign | Microflow | 13 | call microflow WorkflowCommons.SUB_UserTask_Assignee_Add, call microflow WorkflowCommons.SUB_UserTask_Assignee_Remove |
| SUB_TaskAssignmentHelper_Retarget | Microflow | 12 | call microflow WorkflowCommons.SUB_UserTask_TargetUser_Add, call microflow WorkflowCommons.SUB_UserTask_TargetUser_Remove |
| SUB_TaskAssignmentHelper_TaskCount | Microflow | 5 | AggregateListAction (output=Count, errorHandlingType=Rollback), retrieve WorkflowUserTaskList over association TaskAssignmentHelper_WorkflowUserTask from TaskAssignmentHelper |
| SUB_TaskAssignmentHelper_Unassign | Microflow | 11 | call microflow WorkflowCommons.SUB_UserTask_Assignee_Remove, call microflow WorkflowCommons.SUB_UserTask_TargetUser_Remove |
| SUB_TaskCount_Update | Microflow | 13 | AggregateListAction (output=AllOpenTaskCount, errorHandlingType=Rollback), AggregateListAction (output=CompletedTasksCount, errorHandlingType=Rollback) |
| SUB_TaskDashboard_Update | Microflow | 9 | call microflow WorkflowCommons.DashboardContext_GetSelectedWorkflowDefinition -> WorkflowDefinition_Selected, call microflow WorkflowCommons.DashboardContext_GetSelectedWorkflowTaskDefinition -> TaskDefinition_Selected |
| SUB_TaskKey_Migrate | Microflow | 15 | AggregateListAction (output=Count, errorHandlingType=Rollback), change variable Offset=$Offset + $Limit |
| SUB_TaskSeries_CreateOrUpdate | Microflow | 18 | change variable BeginOfStep_InTimeFrame=if $DashboardContext/TimeFrameStepUnit = WorkflowCommons.Enum_TimeFrameStepUnit.Day then addDays($BeginOfStep_InTimeFrame,-1) else if $DashboardContext/TimeFrameStepUnit = WorkflowCommons.Enum_TimeFrameStepUnit.Week then..., change variable EndOfStep_InTimeFrame=if $DashboardContext/TimeFrameStepUnit = WorkflowCommons.Enum_TimeFrameStepUnit.Day then addDays($EndOfStep_InTimeFrame,-1) else if $DashboardContext/TimeFrameStepUnit = WorkflowCommons.Enum_TimeFrameStepUnit.Week then a... |
| SUB_TaskSeriesList_Delete | Microflow | 5 | delete TaskSeriesList (refreshInClient=false), retrieve TaskSeriesList over association TaskSeries_DashboardContext from DashboardContext |
| SUB_TaskSummary_CreateOrUpdate | Microflow | 19 | change TaskSummary (NumberOfTasksCompleted=$UserTask_CountCompleted, NumberOfTasksInProgress=$UserTask_CountInProgress, NumberOfTasksAlmostDue=$UserTask_CountAlmostDue, NumberOfTasksOverdue=$UserTask_CountOverdue, NumberOfTasksFailed=$UserTask_CountFailed, DashboardContext_TaskSummary=$DashboardContext; refreshInClient=false), change TaskSummary (NumberOfTasksCompleted=$UserTask_CountCompleted, TaskAverageHandlingTime=$UserTask_AverageHandlingTime, TasksHandledInTime=if $UserTask_CountCompleted = 0 then 0 else round($UserTask_CountCompletedOnTime:$UserTask_CountCompleted*100,2), DashboardContext_TaskSummary=$DashboardContext; refreshInClient=false) |
| SUB_TaskSummary_RetrieveOrCreate | Microflow | 7 | create WorkflowCommons.TaskSummary as NewTaskSummary (DashboardContext_TaskSummary=$DashboardContext), retrieve TaskSummary over association DashboardContext_TaskSummary from DashboardContext |
| SUB_User_GetAccount | Microflow | 7 | CastAction (output=Account, errorHandlingType=Rollback) |
| SUB_UserTask_Assign | Microflow | 13 | AggregateListAction (output=AssigneeCount, errorHandlingType=Rollback), change WorkflowUserTask (refreshInClient=true) |
| SUB_UserTask_AssignedToUser | Microflow | 8 | retrieve UserTaskViewList from WorkflowCommons.UserTaskView |
| SUB_UserTask_Assignee_Add | Microflow | 7 | change UserList (type=Add, value=$User), CreateListAction (output=UserList, entity=System.User, errorHandlingType=Rollback) |
| SUB_UserTask_Assignee_Remove | Microflow | 7 | change UserList (type=Add, value=$User), CreateListAction (output=UserList, entity=System.User, errorHandlingType=Rollback) |
| SUB_UserTask_Assignees_Add | Microflow | 15 | change UserTaskView (UserTaskView_Assignees=$UserList, UserTaskView_TargetUsers=$UserList; refreshInClient=true), change Workflow (refreshInClient=true) |
| SUB_UserTask_Assignees_Remove | Microflow | 15 | change UserTaskView (UserTaskView_Assignees=$UserList; refreshInClient=true), change Workflow (refreshInClient=true) |
| SUB_UserTask_AverageHandlingTime | Microflow | 12 | change variable TotalHandlingTimeInDays=$TotalHandlingTimeInDays + daysBetween($IteratorUserTask/StartTime, $IteratorUserTask/EndTime), create variable TotalHandlingTimeInDays=0 |
| SUB_UserTask_CountAlmostDue | Microflow | 11 | AggregateListAction (output=CountInProgressAlmostDue, errorHandlingType=Rollback), create variable AlmostDueDate=addDays([%CurrentDateTime%], @WorkflowCommons.DueDateExpirationInDays) |
| SUB_UserTask_CountCompleted | Microflow | 9 | AggregateListAction (output=CountCompleted, errorHandlingType=Rollback), retrieve UserTaskView_Completed from WorkflowCommons.UserTaskView |
| SUB_UserTask_CountCompletedOnTime | Microflow | 9 | AggregateListAction (output=CountCompletedOnTime, errorHandlingType=Rollback), retrieve UserTaskView_CompletedOnTime from WorkflowCommons.UserTaskView |
| SUB_UserTask_CountCompletedOverdue | Microflow | 9 | AggregateListAction (output=CountCompletedOverdue, errorHandlingType=Rollback), retrieve UserTaskView_CompletedOverdue from WorkflowCommons.UserTaskView |
| SUB_UserTask_CountFailed | Microflow | 9 | AggregateListAction (output=CountFailed, errorHandlingType=Rollback), retrieve UserTaskView_Failed from WorkflowCommons.UserTaskView |
| SUB_UserTask_CountInProgress | Microflow | 9 | AggregateListAction (output=CountInProgress, errorHandlingType=Rollback), retrieve UserTask_InProgress from System.WorkflowUserTask |
| SUB_UserTask_CountOverdue | Microflow | 13 | AggregateListAction (output=CountInProgressOverdue, errorHandlingType=Rollback), AggregateListAction (output=CountOverdue, errorHandlingType=Rollback) |
| SUB_UserTask_TargetUser_Add | Microflow | 7 | change UserList (type=Add, value=$User), CreateListAction (output=UserList, entity=System.User, errorHandlingType=Rollback) |
| SUB_UserTask_TargetUser_Remove | Microflow | 7 | change UserList (type=Add, value=$User), CreateListAction (output=UserList, entity=System.User, errorHandlingType=Rollback) |
| SUB_UserTask_TargetUsers_Add | Microflow | 15 | change UserTaskView (UserTaskView_TargetUsers=$UserList; refreshInClient=true), change Workflow (refreshInClient=true) |
| SUB_UserTask_TargetUsers_Remove | Microflow | 15 | change UserTaskView (UserTaskView_TargetUsers=$UserList; refreshInClient=true), change Workflow (refreshInClient=true) |
| SUB_UserTaskOutcome_AssignedToUser | Microflow | 8 | retrieve UserTaskOutcomeList from System.WorkflowUserTaskOutcome |
| SUB_UserTaskOutcomeView_AssignedToUser | Microflow | 8 | retrieve UserTaskOutcomeViewList from WorkflowCommons.UserTaskOutcomeView |
| SUB_UserTaskOutcomeView_FindOrCreate | Microflow | 8 | create WorkflowCommons.UserTaskOutcomeView as NewUserTaskOutcomeView (UserTaskOutcomeView_UserTaskView=$UserTaskView, UserTaskOutcomeView_WorkflowUserTaskOutcome=$WorkflowUserTaskOutcome, UserTaskOutcomeView_User=$WorkflowUserTaskOutcome/System.WorkflowUserTaskOutcome_User, Outcome=$WorkflowUserTaskOutcome/Outcome, Time=$WorkflowUserTaskOutcome/Time), retrieve UserTaskOutcomeView from WorkflowCommons.UserTaskOutcomeView |
| SUB_UserTaskView_FindOrCreate | Microflow | 9 | call java action WorkflowCommons.JA_WorkflowUserTask_GetKey -> TaskKey, create WorkflowCommons.UserTaskView as NewUserTaskView (UserTaskView_WorkflowUserTask=$WorkflowUserTask, UserTaskView_WorkflowUserTaskDefinition=$WorkflowUserTask/System.WorkflowUserTask_WorkflowUserTaskDefinition, UserTaskView_TargetUsers=$WorkflowUserTask/System.WorkflowUserTask_TargetUsers, UserTaskView_Assignees=$WorkflowUserTask/System.WorkflowUserTask_Assignees, UserTaskView_WorkflowView=$WorkflowView, TaskKey=$TaskKey, Name=$WorkflowUserTask/Name, Description=$WorkflowUserTask/Description, +6 more) |
| SUB_UserTaskView_UpdateKey | Microflow | 9 | call java action WorkflowCommons.JA_WorkflowUserTask_GetKey -> TaskKey, change UserTaskView (TaskKey=$TaskKey; refreshInClient=false) |
| SUB_Workflow_AverageHandlingTime | Microflow | 12 | change variable TotalHandlingTimeInDays=$TotalHandlingTimeInDays + daysBetween($IteratorWorkflow/StartTime,$IteratorWorkflow/EndTime), create variable IgnoreCompletedAfter=$CompletedAfter = empty |
| SUB_Workflow_CountAlmostDue | Microflow | 11 | AggregateListAction (output=CountInProgressAlmostDue, errorHandlingType=Rollback), create variable AlmostDueDate=addDays([%CurrentDateTime%], @WorkflowCommons.DueDateExpirationInDays) |
| SUB_Workflow_CountCompleted | Microflow | 9 | AggregateListAction (output=CountInCompleted, errorHandlingType=Rollback), create variable IgnoreStartedAfter=$StartedAfter = empty |
| SUB_Workflow_CountCompletedOnTime | Microflow | 9 | AggregateListAction (output=CountCompletedOnTime, errorHandlingType=Rollback), create variable IgnoreCompletedAfter=$CompletedAfter = empty |
| SUB_Workflow_CountCompletedOverdue | Microflow | 9 | AggregateListAction (output=CountCompletedOverdue, errorHandlingType=Rollback), create variable IgnoreCompletedAfter=$CompletedAfter = empty |
| SUB_Workflow_CountInProgress | Microflow | 9 | AggregateListAction (output=CountInProgress, errorHandlingType=Rollback), create variable IgnoreStartedAfter=$StartedAfter = empty |
| SUB_Workflow_CountOverdue | Microflow | 13 | AggregateListAction (output=CountInProgressOverdue, errorHandlingType=Rollback), AggregateListAction (output=CountOverdue, errorHandlingType=Rollback) |
| SUB_Workflow_Retry | Microflow | 5 | show page WorkflowCommons.Workflow_ActionConfirmation, WorkflowOperationAction (errorHandlingType=Rollback) |
| SUB_Workflow_ShowWorkflowAdminPage | Microflow | 7 | LogMessageAction (errorHandlingType=Rollback), OpenWorkflowAction (errorHandlingType=CustomWithoutRollBack) |
| SUB_WorkflowAuditTrailRecord_CleanUp | Microflow | 16 | AggregateListAction (output=WorkflowInstanceCount, errorHandlingType=Rollback), change variable TotalRecordCount=$TotalRecordCount + $RecordCount |
| SUB_WorkflowAuditTrailRecord_DeleteByKey | Microflow | 6 | AggregateListAction (output=RecordCount, errorHandlingType=Rollback), delete WorkflowAuditTrailRecordList (refreshInClient=false) |
| SUB_WorkflowDashboard_Update | Microflow | 12 | call microflow WorkflowCommons.DashboardContext_GetSelectedWorkflowDefinition -> WorkflowDefinition_Selected, call microflow WorkflowCommons.DashboardContext_GetSelectedWorkflowTaskDefinition -> TaskDefinition_Selected |
| SUB_WorkflowDefinitionHelper_FindOrCreate | Microflow | 9 | change WorkflowDefinitionHelperHead (UpdateInstances=false; refreshInClient=false), create WorkflowCommons.WorkflowDefinitionHelper as NewWorkflowDefinitionHelper (WorkflowDefinitionHelper_WorkflowDefinition=$WorkflowDefinition) |
| SUB_WorkflowEvent_AuditTrail | Microflow | 10 | create variable ActivityKey=if $WorkflowActivityRecord/ActivityType != System.WorkflowActivityType.UserTask and $WorkflowActivityRecord/ActivityType != System.WorkflowActivityType.MultiInputUserTask then $WorkflowActivityRecord/ActivityKey else emp..., create WorkflowCommons.WorkflowAuditTrailRecord as NewWorkflowAuditTrail (EventTimestamp=$WorkflowEvent/EventTime, EventType=$WorkflowEvent/EventType, EventLevel=$EventLevel, ActivityCaption=$WorkflowActivityRecord/Caption, EventInitiator=$WorkflowEvent/System.WorkflowEvent_Initiator/System.User/Name, WorkflowKey=$WorkflowRecord/WorkflowKey, WorkflowName=$WorkflowRecord/Name, WorkflowState=$WorkflowRecord/State, +9 more) |
| SUB_WorkflowJumpToDetails_Validate | Microflow | 8 | ListOperationAction (output=ActivityWithTarget, errorHandlingType=Rollback), retrieve WorkflowCurrentActivityList over association WorkflowJumpToDetails_CurrentActivities from WorkflowJumpToDetails |
| SUB_WorkflowKey_Migrate | Microflow | 15 | AggregateListAction (output=Count, errorHandlingType=Rollback), change variable Offset=$Offset + $Limit |
| SUB_WorkflowSeries_CreateOrUpdate | Microflow | 15 | change variable BeginOfStep_InTimeFrame=if $DashboardContext/TimeFrameStepUnit = WorkflowCommons.Enum_TimeFrameStepUnit.Day then addDays($BeginOfStep_InTimeFrame,-1) else if $DashboardContext/TimeFrameStepUnit = WorkflowCommons.Enum_TimeFrameStepUnit.Week then..., change variable EndOfStep_InTimeFrame=if $DashboardContext/TimeFrameStepUnit = WorkflowCommons.Enum_TimeFrameStepUnit.Day then addDays($EndOfStep_InTimeFrame,-1) else if $DashboardContext/TimeFrameStepUnit = WorkflowCommons.Enum_TimeFrameStepUnit.Week then a... |
| SUB_WorkflowSeriesList_Delete | Microflow | 5 | delete WorkflowSeriesList (refreshInClient=false), retrieve WorkflowSeriesList over association WorkflowSeries_DashboardContext from DashboardContext |
| SUB_WorkflowSummary_CreateOrUpdate | Microflow | 10 | call microflow WorkflowCommons.SUB_WorkflowSummary_RetrieveOrCreate -> WorkflowSummary, change WorkflowSummary (NumberOfWorkflowsInProgress=$Workflow_CountInProgress, NumberOfWorkflowOverdue=$Workflow_CountOverdue, NumberOfWorkflowAlmostDue=$Workflow_CountAlmostDue, NumberOfWorkflowsCompleted=$Workflow_CountCompleted, DashboardContext_WorkflowSummary=$DashboardContext, WorkflowSummary_WorkflowDefinition=$WorkflowDefinition_Selected, IsLocked=if ($WorkflowDefinition_Selected != empty) then $WorkflowDefinition_Selected/IsLocked else false, IsObsolete=if ($WorkflowDefinition_Selected != empty) then $WorkflowDefinition_Selected/IsObsolete else false; refreshInClient=false) |
| SUB_WorkflowSummary_RetrieveOrCreate | Microflow | 10 | create WorkflowCommons.WorkflowSummary as EmptyWorkflowSummary, create WorkflowCommons.WorkflowSummary as NewWorkflowSummary (DashboardContext_WorkflowSummary=$DashboardContext) |
| SUB_WorkflowTask_AverageHandlingTime | Microflow | 10 | AggregateListAction (output=UserTaskView_CountCompleted, errorHandlingType=Rollback), change variable TotalHandlingTimeInDays=$TotalHandlingTimeInDays + daysBetween($IteratorUserTask/StartTime,$IteratorUserTask/EndTime) |
| SUB_WorkflowTaskDetail_CreateOrUpdate | Microflow | 11 | call microflow WorkflowCommons.SUB_WorkflowTaskDetail_Delete, create WorkflowCommons.WorkflowTaskDetail as NewWorkflowTaskDetail (TaskName=$IteratorTaskDefinition/Name, TaskAverageHandlingTime=$WorkflowActivity_AverageHandlingTime, WorkflowTaskDetail_DashboardContext=$DashboardContext) |
| SUB_WorkflowTaskDetail_Delete | Microflow | 5 | delete WorkflowTaskDetailList (refreshInClient=false), retrieve WorkflowTaskDetailList over association WorkflowTaskDetail_DashboardContext from DashboardContext |
| SUB_WorkflowTaskTimeline_Completed | Microflow | 8 | change UserTaskTimeLineList (type=Add, value=$NewUserTaskTimeLine_1), create WorkflowCommons.UserTaskTimeLine as NewUserTaskTimeLine_1 (TaskName=$UserTaskView/Name, CompletedOn=$IteratorUserTaskOutcomeView/Time, CompletionType=$UserTaskView/CompletionType, Outcome=$IteratorUserTaskOutcomeView/Outcome, State=$UserTaskView/State, StartedOn=$UserTaskView/StartTime) |
| SUB_WorkflowTaskTimeline_InProgress | Microflow | 8 | change UserTaskTimeLineList (type=Add, value=$NewUserTaskTimeLine), create WorkflowCommons.UserTaskTimeLine as NewUserTaskTimeLine (TaskName=$WorkflowUserTask/Name, CompletedOn=$IteratorUserTaskOutcome/Time, CompletionType=$WorkflowUserTask/CompletionType, Outcome=$IteratorUserTaskOutcome/Outcome, State=$WorkflowUserTask/State, StartedOn=$WorkflowUserTask/StartTime) |
| SUB_WorkflowView_CommentAttachment_Validate | Microflow | 11 | call microflow WorkflowCommons.SUB_WorkflowView_CurrentUserIsTargeted -> CurrentUserIsTargeted, show message (text=The current Workflow state is '{1}'. You can only {2} comments and/or attachments if the W..., type=Information, blocking=true) |
| SUB_WorkflowView_CurrentUserIsTargeted | Microflow | 7 | AggregateListAction (output=TaskCount, errorHandlingType=Rollback), create variable InTargetUsers=$TaskCount >= 1 |
| SUB_WorkflowView_FindOrCreate | Microflow | 8 | call java action WorkflowCommons.JA_Workflow_GetKey -> WorkflowKey, create WorkflowCommons.WorkflowView as NewWorkflowView (WorkflowView_Workflow=$Workflow, WorkflowView_WorkflowDefinition=$Workflow/System.Workflow_WorkflowDefinition, WorkflowView_Initiator=$Workflow/System.owner, WorkflowKey=$WorkflowKey, Name=$Workflow/Name, Description=$Workflow/Description, StartTime=$Workflow/StartTime, EndTime=$Workflow/EndTime, +3 more) |
| SUB_WorkflowView_ShowWorkflowAdminPage | Microflow | 8 | call microflow WorkflowCommons.SUB_Workflow_ShowWorkflowAdminPage, retrieve Workflow over association WorkflowView_Workflow from WorkflowView |
| SUB_WorkflowView_UpdateKey | Microflow | 9 | call java action WorkflowCommons.JA_Workflow_GetKey -> WorkflowKey, change WorkflowView (WorkflowKey=$WorkflowKey; refreshInClient=false) |
| WFEH_WorkflowEvent_AuditTrail | Microflow | 6 | call microflow WorkflowCommons.SUB_WorkflowEvent_AuditTrail |

## Cross-Module Calls

| Flow | Calls | Target Module |
|---|---|---|
| none | none | none |

## Flow Details

| Flow | Kind | Nodes | Calls Out | Called By |
|---|---|---:|---:|---:|
| ACT_Assignee_Migrate | Microflow | 5 | 2 | 0 |
| ACT_Attachment_Create | Microflow | 5 | 0 | 0 |
| ACT_Attachment_Download | Microflow | 4 | 0 | 0 |
| ACT_Attachment_Save | Microflow | 14 | 1 | 0 |
| ACT_Attachment_Save_Admin | Microflow | 8 | 0 | 0 |
| ACT_AuditTrailViewer_All | Nanoflow | 4 | 0 | 0 |
| ACT_AuditTrailViewer_Default | Nanoflow | 4 | 1 | 0 |
| ACT_AuditTrailViewer_Minimal | Nanoflow | 4 | 0 | 0 |
| ACT_Comment_Delete | Microflow | 4 | 0 | 0 |
| ACT_DashboardContext_Refresh | Microflow | 4 | 0 | 0 |
| ACT_DoNothing | Nanoflow | 5 | 0 | 0 |
| ACT_Key_Migrate | Microflow | 7 | 3 | 0 |
| ACT_TaskAssignment_Show | Microflow | 4 | 0 | 0 |
| ACT_TaskAssignmentHelper_Reassign | Microflow | 10 | 2 | 0 |
| ACT_TaskAssignmentHelper_Reassign_Show | Microflow | 9 | 0 | 0 |
| ACT_TaskAssignmentHelper_Retarget | Microflow | 10 | 2 | 0 |
| ACT_TaskAssignmentHelper_Retarget_Show | Microflow | 9 | 0 | 0 |
| ACT_TaskAssignmentHelper_Unassign | Microflow | 8 | 2 | 0 |
| ACT_TaskAssignmentHelper_Unassign_Show | Microflow | 9 | 0 | 0 |
| ACT_TaskCount_Refresh | Nanoflow | 9 | 1 | 0 |
| ACT_TaskCount_Update | Microflow | 4 | 1 | 1 |
| ACT_TimelineViewer_OpenSubWorkflow | Nanoflow | 11 | 0 | 0 |
| ACT_TimelineViewer_OpenWorkflow | Nanoflow | 7 | 0 | 0 |
| ACT_UserTask_AssignToMe | Microflow | 4 | 1 | 0 |
| ACT_UserTask_AssignToMe_UpdateTaskCount | Microflow | 9 | 2 | 0 |
| ACT_UserTask_AssignToUser | Microflow | 5 | 1 | 0 |
| ACT_UserTask_AssignToUsers | Microflow | 8 | 1 | 0 |
| ACT_UserTask_ShowDefaultAdminPage | Microflow | 8 | 0 | 0 |
| ACT_UserTask_Unassign | Microflow | 4 | 1 | 0 |
| ACT_UserTaskView_ShowUserTaskPage | Microflow | 8 | 0 | 0 |
| ACT_UserTaskView_ShowWorkflowAdminPage | Microflow | 5 | 1 | 0 |
| ACT_Workflow_Abort | Microflow | 8 | 0 | 0 |
| ACT_Workflow_CloseActionConfirmation | Microflow | 8 | 0 | 0 |
| ACT_Workflow_Continue | Microflow | 5 | 0 | 0 |
| ACT_Workflow_JumpTo | Microflow | 8 | 0 | 0 |
| ACT_Workflow_OpenParentWorkflow | Microflow | 5 | 1 | 0 |
| ACT_Workflow_Pause | Microflow | 5 | 0 | 0 |
| ACT_Workflow_Restart | Microflow | 5 | 0 | 0 |
| ACT_Workflow_Retry | Microflow | 9 | 1 | 0 |
| ACT_Workflow_Retry_KeepTargetedUsers | Microflow | 5 | 1 | 0 |
| ACT_Workflow_Retry_RerunUserTargeting | Microflow | 9 | 1 | 0 |
| ACT_Workflow_Unpause | Microflow | 5 | 0 | 0 |
| ACT_Workflow_WithdrawConfirmation | Microflow | 17 | 0 | 0 |
| ACT_WorkflowAuditTrailRecord_ExportToExcel | Nanoflow | 3 | 0 | 0 |
| ACT_WorkflowAuditTrailRecord_Refresh | Nanoflow | 3 | 0 | 0 |
| ACT_WorkflowComment_Edit | Microflow | 9 | 0 | 0 |
| ACT_WorkflowCommentHelper_Edit_Save | Microflow | 12 | 1 | 0 |
| ACT_WorkflowCommentHelper_SaveNew | Microflow | 11 | 1 | 0 |
| ACT_WorkflowCommentHelper_SaveNew_Admin | Microflow | 8 | 0 | 0 |
| ACT_WorkflowDefinition_CleanUp_Execute | Microflow | 7 | 2 | 0 |
| ACT_WorkflowDefinition_CleanUp_Open | Microflow | 6 | 1 | 0 |
| ACT_WorkflowDefinition_CloseActionConfirmation | Microflow | 5 | 0 | 0 |
| ACT_WorkflowDefinition_Delete | Microflow | 13 | 0 | 0 |
| ACT_WorkflowDefinition_Lock | Microflow | 10 | 0 | 0 |
| ACT_WorkflowDefinition_Unlock | Microflow | 10 | 0 | 0 |
| ACT_WorkflowDefinitionHelper_ShowLockPage | Microflow | 5 | 1 | 0 |
| ACT_WorkflowDefinitionHelper_ShowUnlockPage | Microflow | 5 | 1 | 0 |
| ACT_WorkflowJumpToDetails_Apply | Microflow | 9 | 1 | 0 |
| ACT_WorkflowSelectionHelper_Select | Nanoflow | 5 | 0 | 0 |
| ACT_WorkflowUserTask_Assign | Microflow | 6 | 1 | 0 |
| ACT_WorkflowUserTask_Assignees_Add | Microflow | 6 | 1 | 0 |
| ACT_WorkflowUserTask_Assignees_Remove | Microflow | 6 | 1 | 0 |
| ACT_WorkflowUserTask_TargetUsers_Add | Microflow | 6 | 1 | 0 |
| ACT_WorkflowUserTask_TargetUsers_Remove | Microflow | 6 | 1 | 0 |
| ACT_WorkflowUserTask_Unassign | Microflow | 5 | 1 | 0 |
| ACT_WorkflowView_ShowWorkflowAdminPage | Microflow | 4 | 1 | 0 |
| ACT_WorkflowView_WithdrawWorkflow | Microflow | 6 | 0 | 0 |
| ASu_Assignee_Migrate | Microflow | 10 | 3 | 0 |
| ASu_Key_Migrate | Microflow | 12 | 4 | 0 |
| DashboardContext_GetSelectedWorkflowDefinition | Microflow | 10 | 0 | 3 |
| DashboardContext_GetSelectedWorkflowTaskDefinition | Microflow | 10 | 0 | 3 |
| DS_AuditTrailViewer | Nanoflow | 4 | 1 | 0 |
| DS_Configuration | Microflow | 9 | 3 | 0 |
| DS_TaskAssignmentHelper_Account | Microflow | 5 | 0 | 0 |
| DS_TaskCount | Microflow | 4 | 1 | 0 |
| DS_TaskCount_Admin | Microflow | 3 | 0 | 0 |
| DS_TaskDashboard | Microflow | 4 | 2 | 0 |
| DS_TaskSeries | Microflow | 5 | 0 | 0 |
| DS_TimelineViewer_WorkflowActivityRecords_Full | Microflow | 9 | 0 | 0 |
| DS_TimelineViewer_WorkflowActivityRecords_Tasks | Microflow | 8 | 0 | 0 |
| DS_Workflow_LoadNotificationArea | Nanoflow | 11 | 0 | 0 |
| DS_Workflow_TimelineViewer | Nanoflow | 4 | 0 | 0 |
| DS_Workflow_WorkflowView | Microflow | 4 | 1 | 0 |
| DS_WorkflowActivityRecord_ActivityDuration | Nanoflow | 4 | 1 | 0 |
| DS_WorkflowActivityRecord_OverdueTime | Nanoflow | 6 | 1 | 0 |
| DS_WorkflowCommentHelper_InitializeNew | Microflow | 3 | 0 | 0 |
| DS_WorkflowCurrentActivity_Options | Microflow | 4 | 0 | 0 |
| DS_WorkflowDashboard | Microflow | 4 | 2 | 0 |
| DS_WorkflowDefinition_Overview | Microflow | 7 | 1 | 0 |
| DS_WorkflowDefinition_SelectableImplementation | Microflow | 12 | 0 | 0 |
| DS_WorkflowSelectionHelper | Microflow | 4 | 0 | 0 |
| DS_WorkflowSeries | Microflow | 5 | 0 | 0 |
| DS_WorkflowTask_AssignedToUser_Timeline | Microflow | 14 | 6 | 0 |
| DS_WorkflowTask_LoadNotificationArea | Nanoflow | 12 | 0 | 0 |
| DS_WorkflowTaskDefinition_Selectable_Administrator | Microflow | 7 | 0 | 0 |
| DS_WorkflowTaskDefinition_Selectable_UserImplementation | Microflow | 17 | 0 | 0 |
| DS_WorkflowTaskDetail | Microflow | 4 | 0 | 0 |
| DS_WorkflowUserTask_AssigneeHelper | Nanoflow | 6 | 0 | 0 |
| DS_WorkflowUserTask_WorkflowView | Microflow | 5 | 1 | 0 |
| DS_WorkflowView_LoadNotificationArea | Nanoflow | 11 | 0 | 0 |
| DS_WorkflowView_TimelineViewer | Microflow | 5 | 0 | 0 |
| OCh_CleanupHelper_UpdateCount | Microflow | 15 | 1 | 1 |
| OCh_DashboardContext_UpdateTaskDashboard | Microflow | 10 | 1 | 0 |
| OCh_DashboardContext_UpdateWorkflowDashboard | Microflow | 5 | 1 | 0 |
| OCh_Workflow_State | Microflow | 6 | 1 | 0 |
| OCh_WorkflowCurrentActivity_Target | Nanoflow | 7 | 0 | 0 |
| OCh_WorkflowUserTask_State | Microflow | 9 | 2 | 0 |
| OCl_WorkflowSummary | Microflow | 5 | 0 | 0 |
| SE_WorkflowAuditTrailRecord_CleanUp | Microflow | 3 | 1 | 0 |
| SUB_Assignee_Migrate | Microflow | 14 | 0 | 2 |
| SUB_AssigneeMigration_Verify | Microflow | 9 | 0 | 2 |
| SUB_AuditTrailViewer_Default | Nanoflow | 4 | 0 | 2 |
| SUB_CleanupHelper_Execute_Workflow | Microflow | 19 | 0 | 1 |
| SUB_CleanupHelper_Execute_WorkflowView | Microflow | 19 | 0 | 1 |
| SUB_CleanupHelper_Validate | Microflow | 14 | 0 | 1 |
| SUB_Configuration_FindOrCreate | Microflow | 6 | 0 | 5 |
| SUB_DashboardContext_RetrieveOrCreate | Microflow | 7 | 0 | 2 |
| SUB_DashboardContext_UpdateSettings | Microflow | 6 | 0 | 2 |
| SUB_Duration_Calculate | Nanoflow | 20 | 0 | 2 |
| SUB_KeyMigration_Verify | Microflow | 11 | 0 | 2 |
| SUB_TaskAssignmentHelper_Reassign | Microflow | 13 | 3 | 1 |
| SUB_TaskAssignmentHelper_Retarget | Microflow | 12 | 2 | 1 |
| SUB_TaskAssignmentHelper_TaskCount | Microflow | 5 | 0 | 3 |
| SUB_TaskAssignmentHelper_Unassign | Microflow | 11 | 2 | 1 |
| SUB_TaskCount_Update | Microflow | 13 | 0 | 3 |
| SUB_TaskDashboard_Update | Microflow | 9 | 5 | 2 |
| SUB_TaskKey_Migrate | Microflow | 15 | 1 | 2 |
| SUB_TaskSeries_CreateOrUpdate | Microflow | 18 | 4 | 2 |
| SUB_TaskSeriesList_Delete | Microflow | 5 | 0 | 1 |
| SUB_TaskSummary_CreateOrUpdate | Microflow | 19 | 8 | 2 |
| SUB_TaskSummary_RetrieveOrCreate | Microflow | 7 | 0 | 1 |
| SUB_User_GetAccount | Microflow | 7 | 0 | 0 |
| SUB_UserTask_Assign | Microflow | 13 | 1 | 3 |
| SUB_UserTask_AssignedToUser | Microflow | 8 | 0 | 0 |
| SUB_UserTask_Assignee_Add | Microflow | 7 | 1 | 3 |
| SUB_UserTask_Assignee_Remove | Microflow | 7 | 1 | 3 |
| SUB_UserTask_Assignees_Add | Microflow | 15 | 0 | 3 |
| SUB_UserTask_Assignees_Remove | Microflow | 15 | 0 | 3 |
| SUB_UserTask_AverageHandlingTime | Microflow | 12 | 0 | 2 |
| SUB_UserTask_CountAlmostDue | Microflow | 11 | 0 | 1 |
| SUB_UserTask_CountCompleted | Microflow | 9 | 0 | 1 |
| SUB_UserTask_CountCompletedOnTime | Microflow | 9 | 0 | 2 |
| SUB_UserTask_CountCompletedOverdue | Microflow | 9 | 0 | 1 |
| SUB_UserTask_CountFailed | Microflow | 9 | 0 | 1 |
| SUB_UserTask_CountInProgress | Microflow | 9 | 0 | 1 |
| SUB_UserTask_CountOverdue | Microflow | 13 | 0 | 1 |
| SUB_UserTask_TargetUser_Add | Microflow | 7 | 1 | 1 |
| SUB_UserTask_TargetUser_Remove | Microflow | 7 | 1 | 3 |
| SUB_UserTask_TargetUsers_Add | Microflow | 15 | 0 | 2 |
| SUB_UserTask_TargetUsers_Remove | Microflow | 15 | 0 | 2 |
| SUB_UserTaskOutcome_AssignedToUser | Microflow | 8 | 0 | 1 |
| SUB_UserTaskOutcomeView_AssignedToUser | Microflow | 8 | 0 | 1 |
| SUB_UserTaskOutcomeView_FindOrCreate | Microflow | 8 | 0 | 1 |
| SUB_UserTaskView_FindOrCreate | Microflow | 9 | 0 | 1 |
| SUB_UserTaskView_UpdateKey | Microflow | 9 | 0 | 1 |
| SUB_Workflow_AverageHandlingTime | Microflow | 12 | 0 | 1 |
| SUB_Workflow_CountAlmostDue | Microflow | 11 | 0 | 1 |
| SUB_Workflow_CountCompleted | Microflow | 9 | 0 | 1 |
| SUB_Workflow_CountCompletedOnTime | Microflow | 9 | 0 | 1 |
| SUB_Workflow_CountCompletedOverdue | Microflow | 9 | 0 | 1 |
| SUB_Workflow_CountInProgress | Microflow | 9 | 0 | 1 |
| SUB_Workflow_CountOverdue | Microflow | 13 | 0 | 1 |
| SUB_Workflow_Retry | Microflow | 5 | 0 | 3 |
| SUB_Workflow_ShowWorkflowAdminPage | Microflow | 7 | 0 | 2 |
| SUB_WorkflowAuditTrailRecord_CleanUp | Microflow | 16 | 1 | 1 |
| SUB_WorkflowAuditTrailRecord_DeleteByKey | Microflow | 6 | 0 | 1 |
| SUB_WorkflowDashboard_Update | Microflow | 12 | 8 | 2 |
| SUB_WorkflowDefinitionHelper_FindOrCreate | Microflow | 9 | 0 | 2 |
| SUB_WorkflowEvent_AuditTrail | Microflow | 10 | 0 | 1 |
| SUB_WorkflowJumpToDetails_Validate | Microflow | 8 | 0 | 1 |
| SUB_WorkflowKey_Migrate | Microflow | 15 | 1 | 2 |
| SUB_WorkflowSeries_CreateOrUpdate | Microflow | 15 | 4 | 1 |
| SUB_WorkflowSeriesList_Delete | Microflow | 5 | 0 | 1 |
| SUB_WorkflowSummary_CreateOrUpdate | Microflow | 10 | 5 | 2 |
| SUB_WorkflowSummary_RetrieveOrCreate | Microflow | 10 | 0 | 1 |
| SUB_WorkflowTask_AverageHandlingTime | Microflow | 10 | 0 | 1 |
| SUB_WorkflowTaskDetail_CreateOrUpdate | Microflow | 11 | 2 | 1 |
| SUB_WorkflowTaskDetail_Delete | Microflow | 5 | 0 | 1 |
| SUB_WorkflowTaskTimeline_Completed | Microflow | 8 | 0 | 1 |
| SUB_WorkflowTaskTimeline_InProgress | Microflow | 8 | 0 | 1 |
| SUB_WorkflowView_CommentAttachment_Validate | Microflow | 11 | 1 | 3 |
| SUB_WorkflowView_CurrentUserIsTargeted | Microflow | 7 | 0 | 1 |
| SUB_WorkflowView_FindOrCreate | Microflow | 8 | 0 | 3 |
| SUB_WorkflowView_ShowWorkflowAdminPage | Microflow | 8 | 1 | 2 |
| SUB_WorkflowView_UpdateKey | Microflow | 9 | 0 | 1 |
| WFEH_WorkflowEvent_AuditTrail | Microflow | 6 | 1 | 0 |

