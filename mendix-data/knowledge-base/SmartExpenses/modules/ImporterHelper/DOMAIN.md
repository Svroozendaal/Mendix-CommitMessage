# Domain: ImporterHelper

## Entities

| Entity | Persistable | Attribute count | Access rule count |
|---|---|---:|---:|
| ImporterHelper.ExcelFileImport | True | 0 | 1 |
| ImporterHelper.ImportTransaction | False | 6 | 1 |
| ImporterHelper.ImportTransactionHelper | False | 0 | 1 |

Confidence: Export-backed

## Entity Lifecycle Matrix

| Entity | Create flows | Update flows | Delete flows | Read flows |
|---|---|---|---|---|
| ImporterHelper.ExcelFileImport | ImporterHelper.ACT_ExcelFileImport_Create | ImporterHelper.ACT_ExcelFileImport_Create | none | none |
| ImporterHelper.ImportTransaction | none | none | none | none |
| ImporterHelper.ImportTransactionHelper | ImporterHelper.ACT_ImportTransaction_ShowPage | none | none | none |

Confidence: Inferred

## Role impacts per sensitive entity

| Entity | Module roles | Default member rights | XPath constraint |
|---|---|---|---|
| ImporterHelper.ExcelFileImport | ImporterHelper.ExcelImporter | ReadWrite | none |
| ImporterHelper.ImportTransaction | ImporterHelper.ExcelImporter, ImporterHelper.RESTImporter | ReadWrite | none |
| ImporterHelper.ImportTransactionHelper | ImporterHelper.ExcelImporter, ImporterHelper.RESTImporter | ReadWrite | none |

Confidence: Export-backed

## Associations

| Association | Parent | Child | Cardinality | Type | Owner |
|---|---|---|---|---|---|
| ImporterHelper.ImportTransactionHelper_ExcelFileImport | ImporterHelper.ImportTransactionHelper | ImporterHelper.ExcelFileImport | *-1 | Reference | Default |
| ImporterHelper.ImportTransaction_ImportTransactionHelper | ImporterHelper.ImportTransaction | ImporterHelper.ImportTransactionHelper | *-1 | Reference | Default |

## Enumerations

| Enumeration | Value count | Sample values |
|---|---:|---|
| none | 0 | none |
