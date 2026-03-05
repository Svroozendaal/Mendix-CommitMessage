# Domain Model: System

## Entities

| Entity | Persistable | Attributes | Access Rules |
|---|---|---:|---:|
| System.ConsumedODataConfiguration | False | 6 |  |
| System.Error | False | 3 | 0 |
| System.FileDocument | True | 6 | 0 |
| System.HttpHeader | False | 2 |  |
| System.HttpMessage | False | 2 |  |
| System.HttpRequest | True |  |  |
| System.HttpResponse | True | 2 |  |
| System.Image | True | 2 | 0 |
| System.Language | True | 2 |  |
| System.ODataResponse | False |  |  |
| System.Paging | False | 5 |  |
| System.ProcessedQueueTask | True | 19 |  |
| System.QueuedTask | True | 17 |  |
| System.ScheduledEventInformation | True | 5 |  |
| System.Session | True | 3 |  |
| System.SoapFault | True | 5 | 0 |
| System.SynchronizationError | True | 4 |  |
| System.SynchronizationErrorFile | True | 0 |  |
| System.TaskQueueToken | True | 3 |  |
| System.TimeZone | True | 3 |  |
| System.TokenInformation | True | 3 |  |
| System.User | True | 9 |  |
| System.UserReportInfo | True | 2 | 0 |
| System.UserRole | True | 3 |  |
| System.Workflow | True | 10 | 2 |
| System.WorkflowActivityDetails | False | 4 |  |
| System.WorkflowActivityRecord | False | 17 |  |
| System.WorkflowCurrentActivity | False |  |  |
| System.WorkflowDefinition | True | 4 |  |
| System.WorkflowEvent | False | 2 |  |
| System.WorkflowJumpToDetails | False |  |  |
| System.WorkflowRecord | False | 8 |  |
| System.WorkflowUserTask | True | 8 | 2 |
| System.WorkflowUserTaskDefinition | True | 2 |  |
| System.WorkflowUserTaskOutcome | True | 2 | 2 |
| System.XASInstance | True | 5 |  |

## Associations

| Association | Parent | Child | Type |
|---|---|---|---|
| System.grantableRoles | System.UserRole | System.UserRole | ReferenceSet (*-*) |
| System.HttpHeader_ConsumedODataConfiguration | System.HttpHeader | System.ConsumedODataConfiguration | Reference (*-1) |
| System.HttpHeaders | System.HttpHeader | System.HttpMessage | Reference (*-1) |
| System.ScheduledEventInformation_XASInstance | System.ScheduledEventInformation | System.XASInstance | Reference (*-1) |
| System.Session_User | System.Session | System.User | Reference (*-1) |
| System.SynchronizationErrorFile_SynchronizationError | System.SynchronizationErrorFile | System.SynchronizationError | Reference (*-1) |
| System.TokenInformation_User | System.TokenInformation | System.User | Reference (*-1) |
| System.User_Language | System.User | System.Language | Reference (*-1) |
| System.User_TimeZone | System.User | System.TimeZone | Reference (*-1) |
| System.UserReportInfo_User | System.UserReportInfo | System.User | Reference (*-1) |
| System.UserRoles | System.User | System.UserRole | ReferenceSet (*-*) |
| System.Workflow_ParentWorkflow | System.Workflow | System.Workflow | Reference (*-1) |
| System.Workflow_WorkflowDefinition | System.Workflow | System.WorkflowDefinition | Reference (*-1) |
| System.WorkflowActivityRecord_Actor | System.WorkflowActivityRecord | System.User | Reference (*-1) |
| System.WorkflowActivityRecord_PreviousActivity | System.WorkflowActivityRecord | System.WorkflowActivityRecord | Reference (*-1) |
| System.WorkflowActivityRecord_SubWorkflow | System.WorkflowActivityRecord | System.WorkflowRecord | Reference (*-1) |
| System.WorkflowActivityRecord_TaskAssignedUsers | System.WorkflowActivityRecord | System.User | ReferenceSet (*-*) |
| System.WorkflowActivityRecord_TaskTargetedUsers | System.WorkflowActivityRecord | System.User | ReferenceSet (*-*) |
| System.WorkflowActivityRecord_UserTask | System.WorkflowActivityRecord | System.WorkflowUserTask | Reference (*-1) |
| System.WorkflowActivityRecord_WorkflowUserTaskDefinition | System.WorkflowActivityRecord | System.WorkflowUserTaskDefinition | Reference (*-1) |
| System.WorkflowCurrentActivity_ActivityDetails | System.WorkflowCurrentActivity | System.WorkflowActivityDetails | Reference (*-1) |
| System.WorkflowCurrentActivity_ApplicableTargets | System.WorkflowCurrentActivity | System.WorkflowActivityDetails | ReferenceSet (*-*) |
| System.WorkflowCurrentActivity_JumpToTarget | System.WorkflowCurrentActivity | System.WorkflowActivityDetails | Reference (*-1) |
| System.WorkflowEvent_Initiator | System.WorkflowEvent | System.User | Reference (*-1) |
| System.WorkflowJumpToDetails_CurrentActivities | System.WorkflowJumpToDetails | System.WorkflowCurrentActivity | ReferenceSet (*-*) |
| System.WorkflowJumpToDetails_Workflow | System.WorkflowJumpToDetails | System.Workflow | Reference (*-1) |
| System.WorkflowRecord_Owner | System.WorkflowRecord | System.User | Reference (*-1) |
| System.WorkflowRecord_Workflow | System.WorkflowRecord | System.Workflow | Reference (*-1) |
| System.WorkflowRecord_WorkflowDefinition | System.WorkflowRecord | System.WorkflowDefinition | Reference (*-1) |
| System.WorkflowUserTask_Assignees | System.WorkflowUserTask | System.User | ReferenceSet (*-*) |
| System.WorkflowUserTask_TargetUsers | System.WorkflowUserTask | System.User | ReferenceSet (*-*) |
| System.WorkflowUserTask_Workflow | System.WorkflowUserTask | System.Workflow | Reference (*-1) |
| System.WorkflowUserTask_WorkflowUserTaskDefinition | System.WorkflowUserTask | System.WorkflowUserTaskDefinition | Reference (*-1) |
| System.WorkflowUserTaskDefinition_WorkflowDefinition | System.WorkflowUserTaskDefinition | System.WorkflowDefinition | Reference (*-1) |
| System.WorkflowUserTaskOutcome_User | System.WorkflowUserTaskOutcome | System.User | Reference (*-1) |
| System.WorkflowUserTaskOutcome_WorkflowUserTask | System.WorkflowUserTaskOutcome | System.WorkflowUserTask | Reference (*-1) |

## Enumerations

| Enumeration | Values |
|---|---|
| System.ContextType | Anonymous, ScheduledEvent, System, User |
| System.DeviceType | Desktop, Phone, Tablet |
| System.EventStatus | Completed, Error, Running, Stopped |
| System.ProxyConfiguration | NoProxy, Override, UseAppSettings |
| System.QueueTaskStatus | Aborted, Completed, Failed, Idle, Incompatible, Retrying, Running |
| System.UserType | External, Internal |
| System.WorkflowActivityExecutionState | Aborted, Completed, Created, Failed, InProgress, Paused |
| System.WorkflowActivityType | CallMicroflow, CallWorkflow, End, EndOfBoundaryEventPath, ExclusiveSplit, InterruptingTimerEvent, JumpTo, MultiInputUserTask, NonInterruptingTimerEvent, ParallelSplit +6 more |
| System.WorkflowCurrentActivityAction | DoNothing, JumpTo |
| System.WorkflowEventType | CallMicroflowEnded, CallMicroflowStarted, CallWorkflowEnded, CallWorkflowStarted, DecisionExecuted, EndEventExecuted, InterruptingTimerEventExecuted, JumpExecuted, MultiUserTaskOutcomeSelected, NonInterruptingTimerEventExecuted +22 more |
| System.WorkflowState | Aborted, Completed, Failed, Incompatible, InProgress, Paused |
| System.WorkflowUserTaskCompletionType | Consensus, Majority, Microflow, Single, Threshold, Veto |
| System.WorkflowUserTaskState | Aborted, Completed, Created, Failed, InProgress, Paused |

