# Flows: ImporterHelper

## Flow Catalogue

### Action Flows (ACT_*)

| Flow | Nodes | Key Actions | Pages Shown |
|---|---:|---|---|
| ACT_ExcelFileImport_Create | 7 | change ImportTransactionHelper (ImportTransactionHelper_ExcelFileImport=$NewExcelFileImport; refreshInClient=false), create ImporterHelper.ExcelFileImport as NewExcelFileImport | Unknown |
| ACT_ExcelFileImport_ImportToNP | 13 | close page (pagesToClose=1), commit ExcelFileImport (refreshInClient=true, withEvents=true) | Unknown |
| ACT_ImportTransaction_AcceptTransactions | 16 | change ImportTransactionList (type=Remove, value=$IteratorImportTransaction), change TransactionList (type=Add, value=$Transaction) | Unknown |
| ACT_ImportTransaction_Refreshpage | 7 | call javascript action Toast.showToast -> ReturnValueName, call microflow ImporterHelper.ACT_ImportTransaction_AcceptTransactions | Unknown |
| ACT_ImportTransaction_ShowPage | 5 | create ImporterHelper.ImportTransactionHelper as NewImportTransactionHelper, show page ImporterHelper.ImportTransaction_Overview | Unknown |

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
| CWS_GetProducts | Microflow | 10 | AggregateListAction (output=CountTransactions, errorHandlingType=Rollback), commit Response (refreshInClient=true, withEvents=true) |
| SUB_ImportTemplateDocument | Microflow | 14 | call java action ExcelImporter.StartImportByTemplate -> rowCount, LogMessageAction (errorHandlingType=Rollback) |

## Cross-Module Calls

| Flow | Calls | Target Module |
|---|---|---|
| ACT_ImportTransaction_AcceptTransactions | SUB_Transaction_setStatus | SmartExpenses |

## Flow Details

| Flow | Kind | Nodes | Calls Out | Called By |
|---|---|---:|---:|---:|
| ACT_ExcelFileImport_Create | Microflow | 7 | 0 | 0 |
| ACT_ExcelFileImport_ImportToNP | Microflow | 13 | 1 | 0 |
| ACT_ImportTransaction_AcceptTransactions | Microflow | 16 | 1 | 1 |
| ACT_ImportTransaction_Refreshpage | Nanoflow | 7 | 1 | 0 |
| ACT_ImportTransaction_ShowPage | Microflow | 5 | 0 | 0 |
| CWS_GetProducts | Microflow | 10 | 0 | 0 |
| SUB_ImportTemplateDocument | Microflow | 14 | 0 | 1 |

