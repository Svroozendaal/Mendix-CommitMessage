# Domain Model: SmartExpenses

## Entities

| Entity | Persistable | Attributes | Access Rules |
|---|---|---:|---:|
| SmartExpenses.Balance | True | 4 | 3 |
| SmartExpenses.BudgetTerm | True | 5 | 3 |
| SmartExpenses.BudgetType | True | 3 | 3 |
| SmartExpenses.BulkEditHelper | False |  |  |
| SmartExpenses.DateHelper | False |  | 2 |
| SmartExpenses.FBGProfile | True | 2 | 3 |
| SmartExpenses.Logo | True | 0 | 2 |
| SmartExpenses.New_entity | True |  | 0 |
| SmartExpenses.StandardBudget | True | 3 | 2 |
| SmartExpenses.Transaction | True | 8 | 3 |

## Associations

| Association | Parent | Child | Type |
|---|---|---|---|
| SmartExpenses.Balance_FBGProfile | SmartExpenses.Balance | SmartExpenses.FBGProfile | Reference (*-1) |
| SmartExpenses.BudgetTerm_BudgetType | SmartExpenses.BudgetTerm | SmartExpenses.BudgetType | Reference (*-1) |
| SmartExpenses.BudgetType_FBGProfile | SmartExpenses.BudgetType | SmartExpenses.FBGProfile | Reference (*-1) |
| SmartExpenses.BulkEditHelper_Balance | SmartExpenses.BulkEditHelper | SmartExpenses.Balance | Reference (*-1) |
| SmartExpenses.BulkEditHelper_BudgetTerm | SmartExpenses.BulkEditHelper | SmartExpenses.BudgetTerm | Reference (*-1) |
| SmartExpenses.BulkEditHelper_Transaction | SmartExpenses.BulkEditHelper | SmartExpenses.Transaction | ReferenceSet (*-*) |
| SmartExpenses.DateHelper_FBGProfile | SmartExpenses.DateHelper | SmartExpenses.FBGProfile | Reference (*-1) |
| SmartExpenses.Logo_BudgetType | SmartExpenses.Logo | SmartExpenses.BudgetType | Reference (1-1) |
| SmartExpenses.Logo_StandardBudget | SmartExpenses.Logo | SmartExpenses.StandardBudget | Reference (1-1) |
| SmartExpenses.Transaction_Balance | SmartExpenses.Transaction | SmartExpenses.Balance | Reference (*-1) |
| SmartExpenses.Transaction_BudgetTerm | SmartExpenses.Transaction | SmartExpenses.BudgetTerm | Reference (*-1) |
| SmartExpenses.Transaction_FBGProfile | SmartExpenses.Transaction | SmartExpenses.FBGProfile | Reference (*-1) |

## Enumerations

| Enumeration | Values |
|---|---|
| SmartExpenses.ENUM_BudgetIcons | Chisel, fabric, hamburger, machine, paint, sewing, supplies, test, zip |
| SmartExpenses.ENUM_BudgetInterval | Month, Week, Year |
| SmartExpenses.ENUM_BudgetStatus | Active, Archived |
| SmartExpenses.ENUM_TransactionSort | expenditure, income |
| SmartExpenses.ENUM_TransactionStatus | Archived, Pending, Processed |

