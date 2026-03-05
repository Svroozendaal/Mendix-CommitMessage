# Flows: SmartExpenses

## Flow Catalogue

### Action Flows (ACT_*)

| Flow | Nodes | Key Actions | Pages Shown |
|---|---:|---|---|
| ACT_Balance_Create | 9 | change currentSession (SessionId='test'; refreshInClient=false), commit currentSession (refreshInClient=false, withEvents=true) | Unknown |
| ACT_Balance_NewEdit | 11 | close page, commit Balance (refreshInClient=true, withEvents=true) | Unknown |
| ACT_BudgetTerm_BudgetType_Edit | 12 | change BudgetType (Logo_BudgetType=$NewLogo; refreshInClient=false), create SmartExpenses.Logo as NewLogo | Unknown |
| ACT_BudgetTerm_New | 12 | change BudgetTermList (type=Add, value=$BudgetTerm), commit BudgetTermList (refreshInClient=true, withEvents=true) | Unknown |
| ACT_BudgetTerm_setStartdateOnInterval | 6 | change BudgetTerm (EndDate=if ($BudgetType/Interval = SmartExpenses.ENUM_BudgetInterval.Week) then [%EndOfCurrentWeek%] else if ($BudgetType/Interval = SmartExpenses.ENUM_BudgetInterval.Month) then [%EndOfCu...; refreshInClient=true), change BudgetTerm (StartDate=if ($BudgetType/Interval = SmartExpenses.ENUM_BudgetInterval.Week) then [%BeginOfCurrentWeek%] else if ($BudgetType/Interval = SmartExpenses.ENUM_BudgetInterval.Month) then [%Begin...; refreshInClient=false) | Unknown |
| ACT_BudgetType_New | 8 | create SmartExpenses.BudgetTerm as NewBudgetTerm (BudgetTerm_BudgetType=$NewBudgetType, StartDate=[%BeginOfCurrentMonth%], EndDate=[%EndOfCurrentMonth%]), create SmartExpenses.BudgetType as NewBudgetType (BudgetType_FBGProfile=$FBGProfile, Interval=SmartExpenses.ENUM_BudgetInterval.Month, Logo_BudgetType=$NewLogo) | Unknown |
| ACT_BudgetType_OpenOverviewPAge | 6 | call microflow SmartExpenses.ACT_DateHelper_Create -> DateHelper, show page SmartExpenses.BudgetType_Overview | Unknown |
| ACT_BudgetType_Save | 12 | change BudgetTerm (Name=if ($BudgetType/Interval = SmartExpenses.ENUM_BudgetInterval.Week) then 'weekbudget' else if ($BudgetType/Interval = SmartExpenses.ENUM_BudgetInterval.Month) then formatDateTime($B...; refreshInClient=false), close page | Unknown |
| ACT_DateHelper_Create | 5 | create SmartExpenses.DateHelper as NewDateHelper (SelectedDate=[%BeginOfCurrentDay%], DateHelper_FBGProfile=$FBGProfile) | Unknown |
| ACT_FBGProfile_showParentPage | 4 | retrieve FBGProfile from SmartExpenses.FBGProfile, show page SmartExpenses.Home_Parent | Unknown |
| ACT_StandardBudget_Edit | 12 | change StandardBudget (Logo_StandardBudget=$NewLogo_1; refreshInClient=false), create SmartExpenses.Logo as NewLogo | Unknown |
| ACT_StandardBudget_New | 5 | create SmartExpenses.Logo as NewLogo, create SmartExpenses.StandardBudget as StandardBudget (Logo_StandardBudget=$NewLogo) | Unknown |
| ACT_Transaction_BulkEditCreate | 7 | create SmartExpenses.BulkEditHelper as NewBulkEditHelper (BulkEditHelper_Transaction=$TransactionList), show page SmartExpenses.Transaction_BulkEdit | Unknown |
| ACT_Transaction_BulkEditSave | 18 | change IteratorTransaction (Transaction_Balance=if $BulkEditHelper/SmartExpenses.BulkEditHelper_Balance != empty then $BulkEditHelper/SmartExpenses.BulkEditHelper_Balance else $IteratorTransaction/SmartExpenses.Transaction_Balan..., Transaction_BudgetTerm=if $BulkEditHelper/SmartExpenses.BulkEditHelper_BudgetTerm != empty then $BulkEditHelper/SmartExpenses.BulkEditHelper_BudgetTerm else $IteratorTransaction/SmartExpenses.Transaction..., InOut=if $BulkEditHelper/InOut != empty then $BulkEditHelper/InOut else $IteratorTransaction/InOut; refreshInClient=false), close page | Unknown |
| ACT_Transaction_Create | 7 | create SmartExpenses.Transaction as NewTransaction (EntryDate=[%BeginOfCurrentDay%], TransactionDate=[%BeginOfCurrentDay%], Status=SmartExpenses.ENUM_TransactionStatus.Pending, Transaction_FBGProfile=$FBGProfile, TransactionCode=formatDateTime([%CurrentDateTime%], 'dmmyyyyHHmmss'), InOut=$TransactionSort), show page SmartExpenses.Transaction_New | Unknown |
| ACT_Transaction_NewEdit_Save | 10 | close page, commit Transaction (refreshInClient=true, withEvents=true) | Unknown |
| ACT_Transaction_Recalculate_all | 18 | change FBGProfile (BalanceTotal=$NewTotalBalance; refreshInClient=true), commit BalanceList (refreshInClient=true, withEvents=true) | Unknown |

### Data Sources (DS_*)

| Flow | Nodes | Key Actions | Returns |
|---|---:|---|---|
| DS_BudgetTerm_New | 11 | create SmartExpenses.BudgetTerm as NewBudgetTerm (BudgetTerm_BudgetType=$BudgetType, Name=if ($BudgetType/Interval = SmartExpenses.ENUM_BudgetInterval.Week) then 'weekbudget' else if ($BudgetType/Interval = SmartExpenses.ENUM_BudgetInterval.Month) then formatDateTime($D..., BudgetAmount=$OldBudgetTerm/BudgetAmount, StartDate=if ($BudgetType/Interval = SmartExpenses.ENUM_BudgetInterval.Week) then beginOfWeek($Date) else if ($BudgetType/Interval = SmartExpenses.ENUM_BudgetInterval.Month) then beginOfMont..., EndDate=if ($BudgetType/Interval = SmartExpenses.ENUM_BudgetInterval.Week) then endOfWeek($Date) else if ($BudgetType/Interval = SmartExpenses.ENUM_BudgetInterval.Month) then endOfMonth($D...), retrieve BudgetTermList from SmartExpenses.BudgetTerm | Unknown |
| DS_BudgetTerm_Retrieve_current | 6 | retrieve BudgetTerm from SmartExpenses.BudgetTerm | Unknown |
| DS_BudgetType_Retrieve | 6 | retrieve BudgetTypeList from SmartExpenses.BudgetType | Unknown |
| DS_FBGProfile_Retreive_current | 8 | create SmartExpenses.FBGProfile as NewFBGProfile (FBGProfile_Account=$Account), retrieve Account from Administration.Account | Unknown |
| DS_TotalBalance_Calculate | 6 | AggregateListAction (output=SumCurrentAmount, errorHandlingType=Rollback), retrieve BalanceList from SmartExpenses.Balance | Unknown |

### Validation Flows (VAL_*)

| Flow | Nodes | Key Actions |
|---|---:|---|
| VAL_Balance_NewEdit | 13 | change variable IsValid=false |
| VAL_BudgetTypeTerm_New | 21 | change variable BudgetTerm_BudgetAmountValidationFeedback=$BudgetTerm_BudgetAmountValidationFeedback + 'Vul een hoeveelheid in', change variable BudgetTerm_BudgetAmountValidationFeedback=if trim($BudgetTerm_BudgetAmountValidationFeedback) = '' then 'Hoeveelheid mag niet lager zijn dan 0' else $BudgetTerm_BudgetAmountValidationFeedback + ' ' + 'Hoeveelheid mag niet lager zijn dan 0' |
| VAL_Transaction_NewEdit | 28 | change variable IsValid=false |

### Other Flows

| Flow | Type | Nodes | Key Actions |
|---|---|---:|---|
| ACR_FBGProfile_setStandardBudgets | Microflow | 14 | change BudgetTermList (type=Add, value=$BudgetTerm), change CurrentBudgetTypeList (type=Add, value=$NewBudgetType) |
| BCO_Transaction | Microflow | 6 | call microflow SmartExpenses.SUB_Transaction_CalculateBalance, call microflow SmartExpenses.SUB_Transaction_CalculateBudgetTerm |
| BD_Transaction | Microflow | 6 | call microflow SmartExpenses.SUB_Transaction_CalculateBalance, call microflow SmartExpenses.SUB_Transaction_CalculateBudgetTerm |
| Nanoflow | Nanoflow | 6 | call javascript action Toast.showToast -> ReturnValueName, show page SmartExpenses.BudgetType_Overview |
| OCH_BulkEditHelper_setBalance | Nanoflow | 5 | change BulkEditHelper (BulkEditHelper_Balance=$Balance; refreshInClient=true) |
| OCH_BulkEditHelper_setBudgetTerm | Nanoflow | 5 | change BulkEditHelper (BulkEditHelper_BudgetTerm=$BudgetTerm; refreshInClient=true) |
| OCH_Transaction_setBalance | Nanoflow | 5 | change Transaction (Transaction_Balance=$Balance; refreshInClient=false) |
| OCH_Transaction_setBudgetTerm | Nanoflow | 5 | change Transaction (Transaction_BudgetTerm=$BudgetTerm; refreshInClient=true) |
| OCH_Transaction_setBudgetTerm_och_BudgetType | Nanoflow | 6 | change Transaction (Transaction_BudgetTerm=$BudgetTerm; refreshInClient=true), retrieve BudgetTerm from SmartExpenses.BudgetTerm |
| SUB_Balance_Recalculate | Microflow | 10 | AggregateListAction (output=SumValue_Balance_expenditures, errorHandlingType=Rollback), AggregateListAction (output=SumValue_Balance_income, errorHandlingType=Rollback) |
| SUB_BudgetTerm_Recalculate | Microflow | 10 | AggregateListAction (output=SumValue_BudgetTerm_Expenditure, errorHandlingType=Rollback), AggregateListAction (output=SumValue_BudgetTerm_Income, errorHandlingType=Rollback) |
| SUB_Transaction_CalculateBalance | Microflow | 8 | call microflow SmartExpenses.SUB_Balance_Recalculate -> SumBalance, retrieve Balance over association Transaction_Balance from Transaction |
| SUB_Transaction_CalculateBudgetTerm | Microflow | 8 | call microflow SmartExpenses.SUB_BudgetTerm_Recalculate -> SumBudget, retrieve BudgetTerm over association Transaction_BudgetTerm from Transaction |
| SUB_Transaction_setStatus | Microflow | 5 | change Transaction (Status=if $Transaction/SmartExpenses.Transaction_Balance = empty then SmartExpenses.ENUM_TransactionStatus.Pending else if $Transaction/InOut = SmartExpenses.ENUM_TransactionSort.income t...; refreshInClient=false) |

## Cross-Module Calls

| Flow | Calls | Target Module |
|---|---|---|
| none | none | none |

## Flow Details

| Flow | Kind | Nodes | Calls Out | Called By |
|---|---|---:|---:|---:|
| ACR_FBGProfile_setStandardBudgets | Microflow | 14 | 1 | 0 |
| ACT_Balance_Create | Microflow | 9 | 0 | 0 |
| ACT_Balance_NewEdit | Microflow | 11 | 1 | 0 |
| ACT_BudgetTerm_BudgetType_Edit | Microflow | 12 | 0 | 0 |
| ACT_BudgetTerm_New | Microflow | 12 | 1 | 0 |
| ACT_BudgetTerm_setStartdateOnInterval | Nanoflow | 6 | 0 | 0 |
| ACT_BudgetType_New | Microflow | 8 | 0 | 0 |
| ACT_BudgetType_OpenOverviewPAge | Microflow | 6 | 1 | 0 |
| ACT_BudgetType_Save | Microflow | 12 | 1 | 0 |
| ACT_DateHelper_Create | Microflow | 5 | 0 | 1 |
| ACT_FBGProfile_showParentPage | Microflow | 4 | 0 | 0 |
| ACT_StandardBudget_Edit | Microflow | 12 | 0 | 0 |
| ACT_StandardBudget_New | Microflow | 5 | 0 | 0 |
| ACT_Transaction_BulkEditCreate | Microflow | 7 | 0 | 0 |
| ACT_Transaction_BulkEditSave | Microflow | 18 | 3 | 0 |
| ACT_Transaction_Create | Microflow | 7 | 0 | 0 |
| ACT_Transaction_NewEdit_Save | Microflow | 10 | 2 | 0 |
| ACT_Transaction_Recalculate_all | Microflow | 18 | 3 | 0 |
| BCO_Transaction | Microflow | 6 | 2 | 0 |
| BD_Transaction | Microflow | 6 | 2 | 0 |
| DS_BudgetTerm_New | Microflow | 11 | 0 | 2 |
| DS_BudgetTerm_Retrieve_current | Microflow | 6 | 0 | 0 |
| DS_BudgetType_Retrieve | Microflow | 6 | 0 | 0 |
| DS_FBGProfile_Retreive_current | Microflow | 8 | 0 | 0 |
| DS_TotalBalance_Calculate | Microflow | 6 | 0 | 1 |
| Nanoflow | Nanoflow | 6 | 0 | 0 |
| OCH_BulkEditHelper_setBalance | Nanoflow | 5 | 0 | 0 |
| OCH_BulkEditHelper_setBudgetTerm | Nanoflow | 5 | 0 | 0 |
| OCH_Transaction_setBalance | Nanoflow | 5 | 0 | 0 |
| OCH_Transaction_setBudgetTerm | Nanoflow | 5 | 0 | 0 |
| OCH_Transaction_setBudgetTerm_och_BudgetType | Nanoflow | 6 | 0 | 0 |
| SUB_Balance_Recalculate | Microflow | 10 | 0 | 3 |
| SUB_BudgetTerm_Recalculate | Microflow | 10 | 0 | 3 |
| SUB_Transaction_CalculateBalance | Microflow | 8 | 1 | 2 |
| SUB_Transaction_CalculateBudgetTerm | Microflow | 8 | 1 | 2 |
| SUB_Transaction_setStatus | Microflow | 5 | 0 | 3 |
| VAL_Balance_NewEdit | Microflow | 13 | 0 | 1 |
| VAL_BudgetTypeTerm_New | Microflow | 21 | 0 | 1 |
| VAL_Transaction_NewEdit | Microflow | 28 | 0 | 1 |

