# Flows: Administration

## Flow Catalogue

### Action Flows (ACT_*)

| Flow | Nodes | Key Actions | Pages Shown |
|---|---:|---|---|
| none | 0 | none | none |

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
| ChangeMyPassword | Microflow | 15 | change Account (Password=$AccountPasswordData/NewPassword; refreshInClient=true), close page |
| ChangePassword | Microflow | 11 | change Account (Password=$AccountPasswordData/NewPassword; refreshInClient=true), close page |
| ManageMyAccount | Microflow | 8 | CastAction (output=Account, errorHandlingType=Rollback), show message (text=No account information is available for anonymous users., type=Information, blocking=true) |
| NewAccount | Microflow | 5 | create Administration.Account as NewAccount, create Administration.AccountPasswordData as AccountPasswordData (AccountPasswordData_Account=$NewAccount) |
| NewWebServiceAccount | Microflow | 6 | change NewAccount (WebServiceUser=true; refreshInClient=false), create Administration.Account as NewAccount |
| SaveNewAccount | Microflow | 11 | change Account (Password=$AccountPasswordData/NewPassword; refreshInClient=true), close page |
| ShowMyPasswordForm | Microflow | 5 | create Administration.AccountPasswordData as AccountPasswordData (AccountPasswordData_Account=$Account), show page Administration.ChangeMyPasswordForm |
| ShowPasswordForm | Microflow | 5 | create Administration.AccountPasswordData as AccountPasswordData (AccountPasswordData_Account=$Account), show page Administration.ChangePasswordForm |

## Cross-Module Calls

| Flow | Calls | Target Module |
|---|---|---|
| none | none | none |

## Flow Details

| Flow | Kind | Nodes | Calls Out | Called By |
|---|---|---:|---:|---:|
| ChangeMyPassword | Microflow | 15 | 0 | 0 |
| ChangePassword | Microflow | 11 | 0 | 0 |
| ManageMyAccount | Microflow | 8 | 0 | 0 |
| NewAccount | Microflow | 5 | 0 | 0 |
| NewWebServiceAccount | Microflow | 6 | 0 | 0 |
| SaveNewAccount | Microflow | 11 | 0 | 0 |
| ShowMyPasswordForm | Microflow | 5 | 0 | 0 |
| ShowPasswordForm | Microflow | 5 | 0 | 0 |

