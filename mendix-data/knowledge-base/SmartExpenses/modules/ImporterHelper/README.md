# Module: ImporterHelper

Category: Custom
Module roles: ExcelImporter, RESTImporter

## Summary

- Entities: 3
- Flows: 7
- Pages: 3
- Constants: 1

## Purpose

- Export-backed: module category and inventory from overview export.
- Inferred: module role is app-specific business behaviour.
- Unknown: product-owner intent text is not included in export.

## Capability Map

| Capability prefix | Flow count | Representative flow |
|---|---:|---|
| ACT | 5 | ImporterHelper.ACT_ExcelFileImport_Create |
| CWS | 1 | ImporterHelper.CWS_GetProducts |
| SUB | 1 | ImporterHelper.SUB_ImportTemplateDocument |

## Primary User Journeys

| Entry flow | UI result | Entities touched |
|---|---|---|
| ImporterHelper.ACT_ExcelFileImport_Create | ImporterHelper.ExcelFileImport_Upload | ImporterHelper.ExcelFileImport |
| ImporterHelper.ACT_ExcelFileImport_ImportToNP | no page | no entity evidence |
| ImporterHelper.ACT_ImportTransaction_AcceptTransactions | no page | SmartExpenses.Transaction |
| ImporterHelper.ACT_ImportTransaction_Refreshpage | no page | no entity evidence |
| ImporterHelper.ACT_ImportTransaction_ShowPage | ImporterHelper.ImportTransaction_Overview | ImporterHelper.ImportTransactionHelper |

## Top risks/unknowns in model understanding
- Some flows have behavioural actions without explicit entity name tokens (parser gap).
- Some pages have no explicit ShowPageAction evidence in exported flows.

## Navigation

- [DOMAIN.md](DOMAIN.md)
- [FLOWS.md](FLOWS.md)
- [PAGES.md](PAGES.md)
- [RESOURCES.md](RESOURCES.md)

## Cross-Module Dependencies

- Calls to: SmartExpenses
- Called by: none
- Shared entities via associations: none

## Source

- Export module: ImporterHelper
- Run folder: cli_2026-03-04T20-44-47.917Z
