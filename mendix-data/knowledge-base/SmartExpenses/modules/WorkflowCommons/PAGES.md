# Pages: WorkflowCommons

## Page Inventory

| Page | Title | Allowed roles | Parameters | Popup |
|---|---|---|---|---|
| WorkflowCommons.CompletedUserTaskView | Expense request approval | WorkflowCommons.Administrator, WorkflowCommons.User | UserTaskView:WorkflowCommons.UserTaskView | False |
| WorkflowCommons.CompletedWorkflowView | Default workflow admin | WorkflowCommons.Administrator | WorkflowView:WorkflowCommons.WorkflowView | False |
| WorkflowCommons.DefaultWorkflowAdmin | Default workflow admin | WorkflowCommons.Administrator | Workflow:System.Workflow | False |
| WorkflowCommons.ManageTaskAssignments | Manage Task Assignments | WorkflowCommons.Administrator | TaskAssignmentHelper:WorkflowCommons.TaskAssignmentHelper | False |
| WorkflowCommons.MyInitiatedWorkflows | My Initiated Workflows | WorkflowCommons.Administrator, WorkflowCommons.User | none | False |
| WorkflowCommons.TaskAssignmentHelper_UserTask_Reassign | User Search | WorkflowCommons.Administrator | TaskAssignmentHelper:WorkflowCommons.TaskAssignmentHelper | True |
| WorkflowCommons.TaskAssignmentHelper_UserTask_Reassign_TargetUserOptions | Reassign task(s) | WorkflowCommons.Administrator | NewAssignee:Administration.Account, TaskManagementHelper:WorkflowCommons.TaskAssignmentHelper | True |
| WorkflowCommons.TaskAssignmentHelper_UserTask_Retarget | User Search | WorkflowCommons.Administrator | TaskAssignmentHelper:WorkflowCommons.TaskAssignmentHelper | True |
| WorkflowCommons.TaskAssignmentHelper_UserTask_Retarget_TargetUserOptions | Retarget task(s) | WorkflowCommons.Administrator | NewTargetUser:Administration.Account, TaskAssignmentHelper:WorkflowCommons.TaskAssignmentHelper | True |
| WorkflowCommons.TaskAssignmentHelper_UserTask_Unassign_TargetUserOptions | Unassign task(s) | WorkflowCommons.Administrator | TaskAssignmentHelper:WorkflowCommons.TaskAssignmentHelper | True |
| WorkflowCommons.TaskDashboard | My task dashboard | WorkflowCommons.Administrator, WorkflowCommons.User | none | False |
| WorkflowCommons.TaskInbox | Task Inbox | WorkflowCommons.Administrator, WorkflowCommons.User | none | False |
| WorkflowCommons.UserTask_Assign | Assign User | WorkflowCommons.Administrator | WorkflowUserTask:System.WorkflowUserTask | True |
| WorkflowCommons.UserTask_Target | Manage target users | WorkflowCommons.Administrator | WorkflowUserTask:System.WorkflowUserTask | True |
| WorkflowCommons.Workflow_ActionConfirmation | Confirmation | none | Workflow:System.Workflow | True |
| WorkflowCommons.Workflow_Dashboard | Workflow Dashboard | WorkflowCommons.Administrator | none | False |
| WorkflowCommons.Workflow_JumpTo_Options | Jump to activity | none | WorkflowJumpToDetails:System.WorkflowJumpToDetails | True |
| WorkflowCommons.Workflow_Retry_Options | Retry Workflow | none | Workflow:System.Workflow | True |
| WorkflowCommons.Workflow_WithdrawConfirmation | Withdraw Workflow | none | WorkflowComment:WorkflowCommons.WorkflowComment | True |
| WorkflowCommons.WorkflowAdminCenter | Workflow Admin Center | WorkflowCommons.Administrator | none | False |
| WorkflowCommons.WorkflowAttachment_New | New attachment | WorkflowCommons.Administrator, WorkflowCommons.User | WorkflowAttachment:WorkflowCommons.WorkflowAttachment | True |
| WorkflowCommons.WorkflowAttachment_View | Attachment | WorkflowCommons.Administrator, WorkflowCommons.User | WorkflowAttachment:WorkflowCommons.WorkflowAttachment | True |
| WorkflowCommons.WorkflowAuditTrailRecord_Overview | Workflow Audit trail | WorkflowCommons.Administrator | none | False |
| WorkflowCommons.WorkflowComment_Edit_Admin | Edit Comment | WorkflowCommons.Administrator | WorkflowComment:WorkflowCommons.WorkflowComment | True |
| WorkflowCommons.WorkflowCommentHelper_Edit | Edit Comment | WorkflowCommons.Administrator, WorkflowCommons.User | WorkflowCommentHelper:WorkflowCommons.WorkflowCommentHelper | True |
| WorkflowCommons.WorkflowDefinition_ActionConfirmation | Confirmation | none | WorkflowDefinition:System.WorkflowDefinition | True |
| WorkflowCommons.WorkflowDefinition_CleanUp | Clean-up workflow instances | WorkflowCommons.Administrator | CleanupHelper:WorkflowCommons.CleanupHelper | True |
| WorkflowCommons.WorkflowDefinition_CleanUp_Preview | Clean-up workflow instances | WorkflowCommons.Administrator | CleanupHelper:WorkflowCommons.CleanupHelper | True |
| WorkflowCommons.WorkflowDefinition_Lock | Lock workflow | WorkflowCommons.Administrator | WorkflowDefinitionHelper:WorkflowCommons.WorkflowDefinitionHelper | True |
| WorkflowCommons.WorkflowDefinition_Overview | Workflow Definitions | WorkflowCommons.Administrator | none | False |
| WorkflowCommons.WorkflowDefinition_Unlock | Unlock workflow | WorkflowCommons.Administrator | WorkflowDefinitionHelper:WorkflowCommons.WorkflowDefinitionHelper | True |
| WorkflowCommons.WorkflowDefinition_View | Workflow Definition | WorkflowCommons.Administrator | WorkflowDefinition:System.WorkflowDefinition | False |

## Page-Flow Links

| Page | Shown by flows |
|---|---|
| WorkflowCommons.CompletedUserTaskView | WorkflowCommons.ACT_UserTaskView_ShowUserTaskPage |
| WorkflowCommons.CompletedWorkflowView | WorkflowCommons.SUB_WorkflowView_ShowWorkflowAdminPage |
| WorkflowCommons.DefaultWorkflowAdmin | WorkflowCommons.ACT_UserTask_ShowDefaultAdminPage, WorkflowCommons.SUB_Workflow_ShowWorkflowAdminPage |
| WorkflowCommons.ManageTaskAssignments | WorkflowCommons.ACT_TaskAssignment_Show |
| WorkflowCommons.MyInitiatedWorkflows | none (no show-page evidence) |
| WorkflowCommons.TaskAssignmentHelper_UserTask_Reassign | WorkflowCommons.ACT_TaskAssignmentHelper_Reassign_Show |
| WorkflowCommons.TaskAssignmentHelper_UserTask_Reassign_TargetUserOptions | none (no show-page evidence) |
| WorkflowCommons.TaskAssignmentHelper_UserTask_Retarget | WorkflowCommons.ACT_TaskAssignmentHelper_Retarget_Show |
| WorkflowCommons.TaskAssignmentHelper_UserTask_Retarget_TargetUserOptions | none (no show-page evidence) |
| WorkflowCommons.TaskAssignmentHelper_UserTask_Unassign_TargetUserOptions | WorkflowCommons.ACT_TaskAssignmentHelper_Unassign_Show |
| WorkflowCommons.TaskDashboard | none (no show-page evidence) |
| WorkflowCommons.TaskInbox | none (no show-page evidence) |
| WorkflowCommons.UserTask_Assign | none (no show-page evidence) |
| WorkflowCommons.UserTask_Target | none (no show-page evidence) |
| WorkflowCommons.Workflow_ActionConfirmation | WorkflowCommons.ACT_Workflow_Abort, WorkflowCommons.ACT_Workflow_Continue, WorkflowCommons.ACT_Workflow_Pause, WorkflowCommons.ACT_Workflow_Restart, WorkflowCommons.ACT_Workflow_Unpause, WorkflowCommons.ACT_WorkflowJumpToDetails_Apply, WorkflowCommons.SUB_Workflow_Retry |
| WorkflowCommons.Workflow_Dashboard | none (no show-page evidence) |
| WorkflowCommons.Workflow_JumpTo_Options | WorkflowCommons.ACT_Workflow_JumpTo |
| WorkflowCommons.Workflow_Retry_Options | WorkflowCommons.ACT_Workflow_Retry |
| WorkflowCommons.Workflow_WithdrawConfirmation | WorkflowCommons.ACT_WorkflowView_WithdrawWorkflow |
| WorkflowCommons.WorkflowAdminCenter | none (no show-page evidence) |
| WorkflowCommons.WorkflowAttachment_New | WorkflowCommons.ACT_Attachment_Create |
| WorkflowCommons.WorkflowAttachment_View | none (no show-page evidence) |
| WorkflowCommons.WorkflowAuditTrailRecord_Overview | none (no show-page evidence) |
| WorkflowCommons.WorkflowComment_Edit_Admin | none (no show-page evidence) |
| WorkflowCommons.WorkflowCommentHelper_Edit | WorkflowCommons.ACT_WorkflowComment_Edit |
| WorkflowCommons.WorkflowDefinition_ActionConfirmation | WorkflowCommons.ACT_WorkflowDefinition_Lock, WorkflowCommons.ACT_WorkflowDefinition_Unlock |
| WorkflowCommons.WorkflowDefinition_CleanUp | WorkflowCommons.ACT_WorkflowDefinition_CleanUp_Open |
| WorkflowCommons.WorkflowDefinition_CleanUp_Preview | none (no show-page evidence) |
| WorkflowCommons.WorkflowDefinition_Lock | WorkflowCommons.ACT_WorkflowDefinitionHelper_ShowLockPage |
| WorkflowCommons.WorkflowDefinition_Overview | none (no show-page evidence) |
| WorkflowCommons.WorkflowDefinition_Unlock | WorkflowCommons.ACT_WorkflowDefinitionHelper_ShowUnlockPage |
| WorkflowCommons.WorkflowDefinition_View | WorkflowCommons.OCl_WorkflowSummary |

## Journey Fragments

| User intent group | Pages |
|---|---|
| General | WorkflowCommons.CompletedUserTaskView, WorkflowCommons.CompletedWorkflowView, WorkflowCommons.DefaultWorkflowAdmin, WorkflowCommons.ManageTaskAssignments, WorkflowCommons.MyInitiatedWorkflows, WorkflowCommons.TaskDashboard, WorkflowCommons.TaskInbox, WorkflowCommons.WorkflowAdminCenter |
| TaskAssignmentHelper | WorkflowCommons.TaskAssignmentHelper_UserTask_Reassign, WorkflowCommons.TaskAssignmentHelper_UserTask_Reassign_TargetUserOptions, WorkflowCommons.TaskAssignmentHelper_UserTask_Retarget, WorkflowCommons.TaskAssignmentHelper_UserTask_Retarget_TargetUserOptions, WorkflowCommons.TaskAssignmentHelper_UserTask_Unassign_TargetUserOptions |
| UserTask | WorkflowCommons.UserTask_Assign, WorkflowCommons.UserTask_Target |
| Workflow | WorkflowCommons.Workflow_ActionConfirmation, WorkflowCommons.Workflow_Dashboard, WorkflowCommons.Workflow_JumpTo_Options, WorkflowCommons.Workflow_Retry_Options, WorkflowCommons.Workflow_WithdrawConfirmation |
| WorkflowAttachment | WorkflowCommons.WorkflowAttachment_New, WorkflowCommons.WorkflowAttachment_View |
| WorkflowAuditTrailRecord | WorkflowCommons.WorkflowAuditTrailRecord_Overview |
| WorkflowComment | WorkflowCommons.WorkflowComment_Edit_Admin |
| WorkflowCommentHelper | WorkflowCommons.WorkflowCommentHelper_Edit |
| WorkflowDefinition | WorkflowCommons.WorkflowDefinition_ActionConfirmation, WorkflowCommons.WorkflowDefinition_CleanUp, WorkflowCommons.WorkflowDefinition_CleanUp_Preview, WorkflowCommons.WorkflowDefinition_Lock, WorkflowCommons.WorkflowDefinition_Overview, WorkflowCommons.WorkflowDefinition_Unlock, WorkflowCommons.WorkflowDefinition_View |

## Snippets

Snippet-level page widget behaviour is not exported in current overview contract.
