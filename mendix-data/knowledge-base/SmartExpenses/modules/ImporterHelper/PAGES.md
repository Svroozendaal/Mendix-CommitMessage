# Pages: ImporterHelper

## Page Inventory

| Page | Title | Allowed roles | Parameters | Popup |
|---|---|---|---|---|
| ImporterHelper.ExcelFileImport_Upload | Excel file import Upload | none | ExcelFileImport:ImporterHelper.ExcelFileImport | True |
| ImporterHelper.ImportTransaction_Edit | Pas transactie aan | ImporterHelper.ExcelImporter | Transaction:ImporterHelper.ImportTransaction | True |
| ImporterHelper.ImportTransaction_Overview | Page | ImporterHelper.ExcelImporter | FBGProfile:SmartExpenses.FBGProfile, ImportTransactionHelper:ImporterHelper.ImportTransactionHelper | False |

## Page-Flow Links

| Page | Shown by flows |
|---|---|
| ImporterHelper.ExcelFileImport_Upload | ImporterHelper.ACT_ExcelFileImport_Create |
| ImporterHelper.ImportTransaction_Edit | none (no show-page evidence) |
| ImporterHelper.ImportTransaction_Overview | ImporterHelper.ACT_ImportTransaction_ShowPage |

## Journey Fragments

| User intent group | Pages |
|---|---|
| ExcelFileImport | ImporterHelper.ExcelFileImport_Upload |
| ImportTransaction | ImporterHelper.ImportTransaction_Edit, ImporterHelper.ImportTransaction_Overview |

## Snippets

Snippet-level page widget behaviour is not exported in current overview contract.
