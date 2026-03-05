# Module: SmartExpenses

Category: Custom
Module roles: Admin, Anonymous, Parent, User

## Summary

- Entities: 10
- Flows: 39
- Pages: 29
- Constants: 0

## Purpose

- Export-backed: module category and inventory from overview export.
- Inferred: module role is app-specific business behaviour.
- Unknown: product-owner intent text is not included in export.

## Capability Map

| Capability prefix | Flow count | Representative flow |
|---|---:|---|
| ACR | 1 | SmartExpenses.ACR_FBGProfile_setStandardBudgets |
| ACT | 17 | SmartExpenses.ACT_Balance_Create |
| BCO | 1 | SmartExpenses.BCO_Transaction |
| BD | 1 | SmartExpenses.BD_Transaction |
| DS | 5 | SmartExpenses.DS_BudgetTerm_New |
| OCH | 5 | SmartExpenses.OCH_BulkEditHelper_setBalance |
| OTHER | 1 | SmartExpenses.Nanoflow |
| SUB | 5 | SmartExpenses.SUB_Balance_Recalculate |
| VAL | 3 | SmartExpenses.VAL_Balance_NewEdit |

## Primary User Journeys

| Entry flow | UI result | Entities touched |
|---|---|---|
| SmartExpenses.ACR_FBGProfile_setStandardBudgets | no page | SmartExpenses.BudgetTerm, SmartExpenses.BudgetType, SmartExpenses.StandardBudget |
| SmartExpenses.ACT_Balance_Create | SmartExpenses.Balance_NewEdit | SmartExpenses.Balance |
| SmartExpenses.ACT_Balance_NewEdit | no page | no entity evidence |
| SmartExpenses.ACT_BudgetTerm_BudgetType_Edit | SmartExpenses.BudgetTerm_NewEdit | SmartExpenses.Logo |
| SmartExpenses.ACT_BudgetTerm_New | no page | SmartExpenses.BudgetTerm |
| SmartExpenses.ACT_BudgetTerm_setStartdateOnInterval | no page | no entity evidence |
| SmartExpenses.ACT_BudgetType_New | SmartExpenses.BudgetTerm_NewEdit | SmartExpenses.BudgetTerm, SmartExpenses.BudgetType, SmartExpenses.Logo |
| SmartExpenses.ACT_BudgetType_OpenOverviewPAge | SmartExpenses.BudgetType_Overview | no entity evidence |

## Top risks/unknowns in model understanding
- Some flows have behavioural actions without explicit entity name tokens (parser gap).
- Some pages have no explicit ShowPageAction evidence in exported flows.

## Navigation

- [DOMAIN.md](DOMAIN.md)
- [FLOWS.md](FLOWS.md)
- [PAGES.md](PAGES.md)
- [RESOURCES.md](RESOURCES.md)

## Cross-Module Dependencies

- Calls to: none
- Called by: ImporterHelper
- Shared entities via associations: none

## Source

- Export module: SmartExpenses
- Run folder: cli_2026-03-04T20-44-47.917Z
