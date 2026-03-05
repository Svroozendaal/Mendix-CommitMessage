# Domain Model: ImporterHelper

## Entities

| Entity | Persistable | Attributes | Access Rules |
|---|---|---:|---:|
| ImporterHelper.ExcelFileImport | True | 0 |  |
| ImporterHelper.ImportTransaction | False | 6 |  |
| ImporterHelper.ImportTransactionHelper | False | 0 |  |

## Associations

| Association | Parent | Child | Type |
|---|---|---|---|
| ImporterHelper.ImportTransaction_ImportTransactionHelper | ImporterHelper.ImportTransaction | ImporterHelper.ImportTransactionHelper | Reference (*-1) |
| ImporterHelper.ImportTransactionHelper_ExcelFileImport | ImporterHelper.ImportTransactionHelper | ImporterHelper.ExcelFileImport | Reference (*-1) |

## Enumerations

none

