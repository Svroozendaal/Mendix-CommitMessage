# Flows: SmartExpenses

## Flow Catalogue

### Action Flows (ACT_*)

| Flow | Nodes | Key Actions | Pages Shown |
|---|---:|---|---|
| ACT_Balance_Create | 9 | SmartExpenses.Balance | SmartExpenses.Balance_NewEdit |
| ACT_Balance_NewEdit | 11 | none | none |
| ACT_BudgetTerm_BudgetType_Edit | 12 | SmartExpenses.Logo | SmartExpenses.BudgetTerm_NewEdit |
| ACT_BudgetTerm_New | 12 | SmartExpenses.BudgetTerm | none |
| ACT_BudgetTerm_setStartdateOnInterval | 6 | none | none |
| ACT_BudgetType_New | 8 | SmartExpenses.BudgetTerm, SmartExpenses.BudgetType, SmartExpenses.Logo | SmartExpenses.BudgetTerm_NewEdit |
| ACT_BudgetType_OpenOverviewPAge | 6 | none | SmartExpenses.BudgetType_Overview |
| ACT_BudgetType_Save | 12 | none | none |
| ACT_DateHelper_Create | 5 | SmartExpenses.DateHelper | none |
| ACT_FBGProfile_showParentPage | 4 | SmartExpenses.FBGProfile | SmartExpenses.Home_Parent |
| ACT_StandardBudget_Edit | 12 | SmartExpenses.Logo, SmartExpenses.StandardBudget | SmartExpenses.StandardBudget_NewEdit |
| ACT_StandardBudget_New | 5 | SmartExpenses.Logo, SmartExpenses.StandardBudget | SmartExpenses.StandardBudget_NewEdit |
| ACT_Transaction_BulkEditCreate | 7 | SmartExpenses.BulkEditHelper | SmartExpenses.Transaction_BulkEdit |
| ACT_Transaction_BulkEditSave | 18 | SmartExpenses.Transaction | none |
| ACT_Transaction_Create | 7 | SmartExpenses.Transaction | SmartExpenses.Transaction_New |
| ACT_Transaction_NewEdit_Save | 10 | none | none |
| ACT_Transaction_Recalculate_all | 18 | SmartExpenses.Balance, SmartExpenses.BudgetType | none |

### Data Sources (DS_*)

| Flow | Nodes | Key Actions | Returns |
|---|---:|---|---|
| DS_BudgetTerm_New | 11 | SmartExpenses.BudgetTerm | inferred from node actions |
| DS_BudgetTerm_Retrieve_current | 6 | SmartExpenses.BudgetTerm | inferred from node actions |
| DS_BudgetType_Retrieve | 6 | SmartExpenses.BudgetType | inferred from node actions |
| DS_FBGProfile_Retreive_current | 8 | Administration.Account, SmartExpenses.FBGProfile | inferred from node actions |
| DS_TotalBalance_Calculate | 6 | SmartExpenses.Balance | inferred from node actions |

### Validation Flows (VAL_*)

| Flow | Nodes | Key Actions |
|---|---:|---|
| VAL_Balance_NewEdit | 13 | none |
| VAL_BudgetTypeTerm_New | 21 | none |
| VAL_Transaction_NewEdit | 28 | none |

### Other Flows

| Flow | Type | Nodes | Key Actions |
|---|---|---:|---|
| ACR_FBGProfile_setStandardBudgets | Microflow | 14 | SmartExpenses.BudgetTerm, SmartExpenses.BudgetType, SmartExpenses.StandardBudget |
| BCO_Transaction | Microflow | 6 | none |
| BD_Transaction | Microflow | 6 | none |
| Nanoflow | Nanoflow | 6 | none |
| OCH_BulkEditHelper_setBalance | Nanoflow | 5 | none |
| OCH_BulkEditHelper_setBudgetTerm | Nanoflow | 5 | none |
| OCH_Transaction_setBalance | Nanoflow | 5 | none |
| OCH_Transaction_setBudgetTerm | Nanoflow | 5 | none |
| OCH_Transaction_setBudgetTerm_och_BudgetType | Nanoflow | 6 | SmartExpenses.BudgetTerm |
| SUB_Balance_Recalculate | Microflow | 10 | SmartExpenses.Transaction |
| SUB_BudgetTerm_Recalculate | Microflow | 10 | SmartExpenses.Transaction |
| SUB_Transaction_CalculateBalance | Microflow | 8 | none |
| SUB_Transaction_CalculateBudgetTerm | Microflow | 8 | none |
| SUB_Transaction_setStatus | Microflow | 5 | none |

## Cross-Module Calls

| Flow | Calls | Target Module |
|---|---|---|
| none | none | none |

## Flow Details

| Flow | Kind | Nodes | Tier | Calls Out | Called By |
|---|---|---:|---:|---:|---:|
| ACR_FBGProfile_setStandardBudgets | Microflow | 14 | 1 | 1 | 0 |
| ACT_Balance_Create | Microflow | 9 | 1 | 0 | 0 |
| ACT_Balance_NewEdit | Microflow | 11 | 1 | 1 | 0 |
| ACT_BudgetTerm_BudgetType_Edit | Microflow | 12 | 1 | 0 | 0 |
| ACT_BudgetTerm_New | Microflow | 12 | 1 | 1 | 0 |
| ACT_BudgetTerm_setStartdateOnInterval | Nanoflow | 6 | 1 | 0 | 0 |
| ACT_BudgetType_New | Microflow | 8 | 1 | 0 | 0 |
| ACT_BudgetType_OpenOverviewPAge | Microflow | 6 | 1 | 1 | 0 |
| ACT_BudgetType_Save | Microflow | 12 | 1 | 1 | 0 |
| ACT_DateHelper_Create | Microflow | 5 | 1 | 0 | 1 |
| ACT_FBGProfile_showParentPage | Microflow | 4 | 1 | 0 | 0 |
| ACT_StandardBudget_Edit | Microflow | 12 | 1 | 0 | 0 |
| ACT_StandardBudget_New | Microflow | 5 | 1 | 0 | 0 |
| ACT_Transaction_BulkEditCreate | Microflow | 7 | 1 | 0 | 0 |
| ACT_Transaction_BulkEditSave | Microflow | 18 | 1 | 3 | 0 |
| ACT_Transaction_Create | Microflow | 7 | 1 | 0 | 0 |
| ACT_Transaction_NewEdit_Save | Microflow | 10 | 1 | 2 | 0 |
| ACT_Transaction_Recalculate_all | Microflow | 18 | 1 | 3 | 0 |
| BCO_Transaction | Microflow | 6 | 1 | 2 | 0 |
| BD_Transaction | Microflow | 6 | 1 | 2 | 0 |
| DS_BudgetTerm_New | Microflow | 11 | 1 | 0 | 2 |
| DS_BudgetTerm_Retrieve_current | Microflow | 6 | 2 | 0 | 0 |
| DS_BudgetType_Retrieve | Microflow | 6 | 2 | 0 | 0 |
| DS_FBGProfile_Retreive_current | Microflow | 8 | 1 | 0 | 0 |
| DS_TotalBalance_Calculate | Microflow | 6 | 2 | 0 | 1 |
| Nanoflow | Nanoflow | 6 | 1 | 0 | 0 |
| OCH_BulkEditHelper_setBalance | Nanoflow | 5 | 2 | 0 | 0 |
| OCH_BulkEditHelper_setBudgetTerm | Nanoflow | 5 | 2 | 0 | 0 |
| OCH_Transaction_setBalance | Nanoflow | 5 | 2 | 0 | 0 |
| OCH_Transaction_setBudgetTerm | Nanoflow | 5 | 2 | 0 | 0 |
| OCH_Transaction_setBudgetTerm_och_BudgetType | Nanoflow | 6 | 1 | 0 | 0 |
| SUB_Balance_Recalculate | Microflow | 10 | 1 | 0 | 3 |
| SUB_BudgetTerm_Recalculate | Microflow | 10 | 1 | 0 | 3 |
| SUB_Transaction_CalculateBalance | Microflow | 8 | 1 | 1 | 2 |
| SUB_Transaction_CalculateBudgetTerm | Microflow | 8 | 1 | 1 | 2 |
| SUB_Transaction_setStatus | Microflow | 5 | 1 | 0 | 3 |
| VAL_Balance_NewEdit | Microflow | 13 | 1 | 0 | 1 |
| VAL_BudgetTypeTerm_New | Microflow | 21 | 1 | 0 | 1 |
| VAL_Transaction_NewEdit | Microflow | 28 | 1 | 0 | 1 |

## Tier 1 Deep Narratives

### SmartExpenses.ACR_FBGProfile_setStandardBudgets

- Intent: Access/creation orchestration flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.BudgetTerm, SmartExpenses.BudgetType, SmartExpenses.StandardBudget.
- UI interactions (shown pages): none.
- Calls/called-by: out=1, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: Rollback hints detected in node detail.
### SmartExpenses.ACT_Balance_Create

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.Balance.
- UI interactions (shown pages): SmartExpenses.Balance_NewEdit.
- Calls/called-by: out=0, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_Balance_NewEdit

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): none.
- Calls/called-by: out=1, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_BudgetTerm_BudgetType_Edit

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.Logo.
- UI interactions (shown pages): SmartExpenses.BudgetTerm_NewEdit.
- Calls/called-by: out=0, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_BudgetTerm_New

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.BudgetTerm.
- UI interactions (shown pages): none.
- Calls/called-by: out=1, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: Rollback hints detected in node detail.
### SmartExpenses.ACT_BudgetTerm_setStartdateOnInterval

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): none.
- Calls/called-by: out=0, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_BudgetType_New

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.BudgetTerm, SmartExpenses.BudgetType, SmartExpenses.Logo.
- UI interactions (shown pages): SmartExpenses.BudgetTerm_NewEdit.
- Calls/called-by: out=0, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_BudgetType_OpenOverviewPAge

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): SmartExpenses.BudgetType_Overview.
- Calls/called-by: out=1, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_BudgetType_Save

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): none.
- Calls/called-by: out=1, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_DateHelper_Create

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.DateHelper.
- UI interactions (shown pages): none.
- Calls/called-by: out=0, in=1.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_FBGProfile_showParentPage

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.FBGProfile.
- UI interactions (shown pages): SmartExpenses.Home_Parent.
- Calls/called-by: out=0, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_StandardBudget_Edit

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.Logo, SmartExpenses.StandardBudget.
- UI interactions (shown pages): SmartExpenses.StandardBudget_NewEdit.
- Calls/called-by: out=0, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_StandardBudget_New

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.Logo, SmartExpenses.StandardBudget.
- UI interactions (shown pages): SmartExpenses.StandardBudget_NewEdit.
- Calls/called-by: out=0, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_Transaction_BulkEditCreate

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.BulkEditHelper.
- UI interactions (shown pages): SmartExpenses.Transaction_BulkEdit.
- Calls/called-by: out=0, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_Transaction_BulkEditSave

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.Transaction.
- UI interactions (shown pages): none.
- Calls/called-by: out=3, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_Transaction_Create

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.Transaction.
- UI interactions (shown pages): SmartExpenses.Transaction_New.
- Calls/called-by: out=0, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_Transaction_NewEdit_Save

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): none.
- Calls/called-by: out=2, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.ACT_Transaction_Recalculate_all

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.Balance, SmartExpenses.BudgetType.
- UI interactions (shown pages): none.
- Calls/called-by: out=3, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.BCO_Transaction

- Intent: Behaviour-critical flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): none.
- Calls/called-by: out=2, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.BD_Transaction

- Intent: Behaviour-critical flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): none.
- Calls/called-by: out=2, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.DS_BudgetTerm_New

- Intent: Behaviour-critical flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.BudgetTerm.
- UI interactions (shown pages): none.
- Calls/called-by: out=0, in=2.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.DS_FBGProfile_Retreive_current

- Intent: Behaviour-critical flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: Administration.Account, SmartExpenses.FBGProfile.
- UI interactions (shown pages): none.
- Calls/called-by: out=0, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.Nanoflow

- Intent: Behaviour-critical flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): SmartExpenses.BudgetType_Overview.
- Calls/called-by: out=0, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.OCH_Transaction_setBudgetTerm_och_BudgetType

- Intent: Behaviour-critical flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.BudgetTerm.
- UI interactions (shown pages): none.
- Calls/called-by: out=0, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.SUB_Balance_Recalculate

- Intent: Behaviour-critical flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.Transaction.
- UI interactions (shown pages): none.
- Calls/called-by: out=0, in=3.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: Rollback hints detected in node detail.
### SmartExpenses.SUB_BudgetTerm_Recalculate

- Intent: Behaviour-critical flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.Transaction.
- UI interactions (shown pages): none.
- Calls/called-by: out=0, in=3.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: Rollback hints detected in node detail.
### SmartExpenses.SUB_Transaction_CalculateBalance

- Intent: Behaviour-critical flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): none.
- Calls/called-by: out=1, in=2.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.SUB_Transaction_CalculateBudgetTerm

- Intent: Behaviour-critical flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): none.
- Calls/called-by: out=1, in=2.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.SUB_Transaction_setStatus

- Intent: Behaviour-critical flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): none.
- Calls/called-by: out=0, in=3.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### SmartExpenses.VAL_Balance_NewEdit

- Intent: Validation flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): none.
- Calls/called-by: out=0, in=1.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: Rollback hints detected in node detail.
### SmartExpenses.VAL_BudgetTypeTerm_New

- Intent: Validation flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): none.
- Calls/called-by: out=0, in=1.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: Rollback hints detected in node detail.
### SmartExpenses.VAL_Transaction_NewEdit

- Intent: Validation flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): none.
- Calls/called-by: out=0, in=1.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: Rollback hints detected in node detail.
