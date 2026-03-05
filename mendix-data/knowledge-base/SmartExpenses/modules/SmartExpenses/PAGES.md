# Pages: SmartExpenses

## Page Inventory

| Page | Title | Allowed roles | Parameters | Popup |
|---|---|---|---|---|
| SmartExpenses.Balance_MasterNewEdit | Edit Balance | SmartExpenses.Admin | Balance:SmartExpenses.Balance | True |
| SmartExpenses.Balance_MasterOverview | Balance Overview | SmartExpenses.Admin, SmartExpenses.Parent | none | False |
| SmartExpenses.Balance_NewEdit | Saldo | SmartExpenses.Admin, SmartExpenses.User | Balance:SmartExpenses.Balance | True |
| SmartExpenses.BalanceType_Overview | Overzicht van alle saldo's | SmartExpenses.Admin, SmartExpenses.User | FBGProfile:SmartExpenses.FBGProfile | False |
| SmartExpenses.BudgetTerm_EditQuick | Edit Budget Type | SmartExpenses.Admin, SmartExpenses.User | BudgetTerm:SmartExpenses.BudgetTerm | True |
| SmartExpenses.BudgetTerm_MasterNewEdit | Edit Budget Term | SmartExpenses.Admin | BudgetTerm:SmartExpenses.BudgetTerm | True |
| SmartExpenses.BudgetTerm_MasterOverview | Budget Term Overview | SmartExpenses.Admin | none | False |
| SmartExpenses.BudgetTerm_NewEdit | Budget | SmartExpenses.Admin, SmartExpenses.User | BudgetTerm:SmartExpenses.BudgetTerm, BudgetType:SmartExpenses.BudgetType | True |
| SmartExpenses.BudgetTerm_Overview | Budget Term Overview | SmartExpenses.Admin, SmartExpenses.Parent, SmartExpenses.User | BudgetType:SmartExpenses.BudgetType | False |
| SmartExpenses.BudgetType_NewEdit_Master | Edit Budget Type | SmartExpenses.Admin | BudgetType:SmartExpenses.BudgetType | True |
| SmartExpenses.BudgetType_Overview | Budget Type Overview | SmartExpenses.Admin, SmartExpenses.Parent, SmartExpenses.User | DateHelper:SmartExpenses.DateHelper, FBGProfile:SmartExpenses.FBGProfile | False |
| SmartExpenses.BudgetType_Overview_2 | Budget Type Overview | SmartExpenses.Admin | none | False |
| SmartExpenses.datagid | datagid | none | none | True |
| SmartExpenses.FBGProfile_NewEdit | Edit FBG Profile | SmartExpenses.Admin | FBGProfile:SmartExpenses.FBGProfile | True |
| SmartExpenses.FBGProfile_Overview | FBG Environment Overview | SmartExpenses.Admin | none | False |
| SmartExpenses.FBGProfile_Overview_2 | FBG Profile Overview | SmartExpenses.Admin | none | False |
| SmartExpenses.Home_Parent | Homepage | SmartExpenses.Admin, SmartExpenses.Parent, SmartExpenses.User | FBGProfile:SmartExpenses.FBGProfile | False |
| SmartExpenses.Home_Web | Homepage | SmartExpenses.Admin, SmartExpenses.Parent, SmartExpenses.User | none | False |
| SmartExpenses.Homepage_Admin | Homepage Admin | SmartExpenses.Admin | none | False |
| SmartExpenses.Login_Overview | Login Overview | SmartExpenses.Admin, SmartExpenses.Anonymous | none | True |
| SmartExpenses.StandardBudget_NewEdit | Edit Standard Budget | SmartExpenses.Admin | StandardBudget:SmartExpenses.StandardBudget | True |
| SmartExpenses.StandardBudget_Overview | Standard Budget Overview | SmartExpenses.Admin | none | False |
| SmartExpenses.Transaction_BulkEdit | Transaction Bulk edit | SmartExpenses.Admin, SmartExpenses.User | BulkEditHelper:SmartExpenses.BulkEditHelper, FBGProfile:SmartExpenses.FBGProfile | True |
| SmartExpenses.Transaction_Edit | Pas transactie aan | SmartExpenses.Admin, SmartExpenses.User | Transaction:SmartExpenses.Transaction | True |
| SmartExpenses.Transaction_EditQuick | Snel verwerken | SmartExpenses.Admin, SmartExpenses.User | Transaction:SmartExpenses.Transaction | True |
| SmartExpenses.Transaction_New | Nieuwe transactie | SmartExpenses.Admin, SmartExpenses.User | Transaction:SmartExpenses.Transaction | True |
| SmartExpenses.Transaction_NewEdit | Edit Transaction | SmartExpenses.Admin | Transaction:SmartExpenses.Transaction | True |
| SmartExpenses.Transaction_Overview | Transaction Overview | SmartExpenses.Admin, SmartExpenses.Parent, SmartExpenses.User | FBGProfile:SmartExpenses.FBGProfile | False |
| SmartExpenses.Transaction_Overview_2 | Transaction Overview | SmartExpenses.Admin | none | False |

## Page-Flow Links

| Page | Shown by flows |
|---|---|
| SmartExpenses.Balance_MasterNewEdit | none (no show-page evidence) |
| SmartExpenses.Balance_MasterOverview | none (no show-page evidence) |
| SmartExpenses.Balance_NewEdit | SmartExpenses.ACT_Balance_Create |
| SmartExpenses.BalanceType_Overview | none (no show-page evidence) |
| SmartExpenses.BudgetTerm_EditQuick | none (no show-page evidence) |
| SmartExpenses.BudgetTerm_MasterNewEdit | none (no show-page evidence) |
| SmartExpenses.BudgetTerm_MasterOverview | none (no show-page evidence) |
| SmartExpenses.BudgetTerm_NewEdit | SmartExpenses.ACT_BudgetTerm_BudgetType_Edit, SmartExpenses.ACT_BudgetType_New |
| SmartExpenses.BudgetTerm_Overview | none (no show-page evidence) |
| SmartExpenses.BudgetType_NewEdit_Master | none (no show-page evidence) |
| SmartExpenses.BudgetType_Overview | SmartExpenses.ACT_BudgetType_OpenOverviewPAge, SmartExpenses.Nanoflow |
| SmartExpenses.BudgetType_Overview_2 | none (no show-page evidence) |
| SmartExpenses.datagid | none (no show-page evidence) |
| SmartExpenses.FBGProfile_NewEdit | none (no show-page evidence) |
| SmartExpenses.FBGProfile_Overview | none (no show-page evidence) |
| SmartExpenses.FBGProfile_Overview_2 | none (no show-page evidence) |
| SmartExpenses.Home_Parent | SmartExpenses.ACT_FBGProfile_showParentPage |
| SmartExpenses.Home_Web | none (no show-page evidence) |
| SmartExpenses.Homepage_Admin | none (no show-page evidence) |
| SmartExpenses.Login_Overview | none (no show-page evidence) |
| SmartExpenses.StandardBudget_NewEdit | SmartExpenses.ACT_StandardBudget_Edit, SmartExpenses.ACT_StandardBudget_New |
| SmartExpenses.StandardBudget_Overview | none (no show-page evidence) |
| SmartExpenses.Transaction_BulkEdit | SmartExpenses.ACT_Transaction_BulkEditCreate |
| SmartExpenses.Transaction_Edit | none (no show-page evidence) |
| SmartExpenses.Transaction_EditQuick | none (no show-page evidence) |
| SmartExpenses.Transaction_New | SmartExpenses.ACT_Transaction_Create |
| SmartExpenses.Transaction_NewEdit | none (no show-page evidence) |
| SmartExpenses.Transaction_Overview | none (no show-page evidence) |
| SmartExpenses.Transaction_Overview_2 | none (no show-page evidence) |

## Journey Fragments

| User intent group | Pages |
|---|---|
| Balance | SmartExpenses.Balance_MasterNewEdit, SmartExpenses.Balance_MasterOverview, SmartExpenses.Balance_NewEdit |
| BalanceType | SmartExpenses.BalanceType_Overview |
| BudgetTerm | SmartExpenses.BudgetTerm_EditQuick, SmartExpenses.BudgetTerm_MasterNewEdit, SmartExpenses.BudgetTerm_MasterOverview, SmartExpenses.BudgetTerm_NewEdit, SmartExpenses.BudgetTerm_Overview |
| BudgetType | SmartExpenses.BudgetType_NewEdit_Master, SmartExpenses.BudgetType_Overview, SmartExpenses.BudgetType_Overview_2 |
| FBGProfile | SmartExpenses.FBGProfile_NewEdit, SmartExpenses.FBGProfile_Overview, SmartExpenses.FBGProfile_Overview_2 |
| General | SmartExpenses.datagid |
| Home | SmartExpenses.Home_Parent, SmartExpenses.Home_Web |
| Homepage | SmartExpenses.Homepage_Admin |
| Login | SmartExpenses.Login_Overview |
| StandardBudget | SmartExpenses.StandardBudget_NewEdit, SmartExpenses.StandardBudget_Overview |
| Transaction | SmartExpenses.Transaction_BulkEdit, SmartExpenses.Transaction_Edit, SmartExpenses.Transaction_EditQuick, SmartExpenses.Transaction_New, SmartExpenses.Transaction_NewEdit, SmartExpenses.Transaction_Overview, SmartExpenses.Transaction_Overview_2 |

## Snippets

Snippet-level page widget behaviour is not exported in current overview contract.
