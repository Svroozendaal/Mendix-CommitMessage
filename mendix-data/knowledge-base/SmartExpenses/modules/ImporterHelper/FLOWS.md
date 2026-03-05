# Flows: ImporterHelper

## Flow Catalogue

### Action Flows (ACT_*)

| Flow | Nodes | Key Actions | Pages Shown |
|---|---:|---|---|
| ACT_ExcelFileImport_Create | 7 | ImporterHelper.ExcelFileImport | ImporterHelper.ExcelFileImport_Upload |
| ACT_ExcelFileImport_ImportToNP | 13 | none | none |
| ACT_ImportTransaction_AcceptTransactions | 16 | SmartExpenses.Transaction | none |
| ACT_ImportTransaction_Refreshpage | 7 | none | none |
| ACT_ImportTransaction_ShowPage | 5 | ImporterHelper.ImportTransactionHelper | ImporterHelper.ImportTransaction_Overview |

### Data Sources (DS_*)

| Flow | Nodes | Key Actions | Returns |
|---|---:|---|---|
| none | 0 | none | none |

### Validation Flows (VAL_*)

| Flow | Nodes | Key Actions |
|---|---:|---|
| none | 0 | none |

### Other Flows

| Flow | Type | Nodes | Key Actions |
|---|---|---:|---|
| CWS_GetProducts | Microflow | 10 | none |
| SUB_ImportTemplateDocument | Microflow | 14 | ExcelImporter.Template |

## Cross-Module Calls

| Flow | Calls | Target Module |
|---|---|---|
| ACT_ImportTransaction_AcceptTransactions | SmartExpenses.SUB_Transaction_setStatus | SmartExpenses |

## Flow Details

| Flow | Kind | Nodes | Tier | Calls Out | Called By |
|---|---|---:|---:|---:|---:|
| ACT_ExcelFileImport_Create | Microflow | 7 | 1 | 0 | 0 |
| ACT_ExcelFileImport_ImportToNP | Microflow | 13 | 1 | 1 | 0 |
| ACT_ImportTransaction_AcceptTransactions | Microflow | 16 | 1 | 1 | 1 |
| ACT_ImportTransaction_Refreshpage | Nanoflow | 7 | 1 | 1 | 0 |
| ACT_ImportTransaction_ShowPage | Microflow | 5 | 1 | 0 | 0 |
| CWS_GetProducts | Microflow | 10 | 2 | 0 | 0 |
| SUB_ImportTemplateDocument | Microflow | 14 | 2 | 0 | 1 |

## Tier 1 Deep Narratives

### ImporterHelper.ACT_ExcelFileImport_Create

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: ImporterHelper.ExcelFileImport.
- UI interactions (shown pages): ImporterHelper.ExcelFileImport_Upload.
- Calls/called-by: out=0, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### ImporterHelper.ACT_ExcelFileImport_ImportToNP

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): none.
- Calls/called-by: out=1, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: Rollback hints detected in node detail.
### ImporterHelper.ACT_ImportTransaction_AcceptTransactions

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: SmartExpenses.Transaction.
- UI interactions (shown pages): none.
- Calls/called-by: out=1, in=1.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: Rollback hints detected in node detail.
### ImporterHelper.ACT_ImportTransaction_Refreshpage

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: none from export token evidence.
- UI interactions (shown pages): none.
- Calls/called-by: out=1, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
### ImporterHelper.ACT_ImportTransaction_ShowPage

- Intent: User action flow.
- Trigger/entry: microflow/nanoflow entry based on caller or UI action.
- Inputs/outputs: derived from flow node graph; explicit parameter typing is not fully exported.
- Read/write entities: ImporterHelper.ImportTransactionHelper.
- UI interactions (shown pages): ImporterHelper.ImportTransaction_Overview.
- Calls/called-by: out=0, in=0.
- Security constraints touched: module roles derived via page permissions and entity access rules.
- Failure/rollback notes: No explicit rollback hint in flow node detail.
