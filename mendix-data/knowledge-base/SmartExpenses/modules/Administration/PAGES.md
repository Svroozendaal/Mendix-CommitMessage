# Pages: Administration

## Page Inventory

| Page | Title | Allowed roles | Parameters | Popup |
|---|---|---|---|---|
| Administration.Account_Edit | Edit Account | Administration.Administrator, Administration.User | Account:Administration.Account | True |
| Administration.Account_New | New Account | Administration.Administrator, Administration.User | AccountPasswordData:Administration.AccountPasswordData | True |
| Administration.Account_Overview | Accounts | Administration.Administrator | none | False |
| Administration.ActiveSessions | Active Sessions | Administration.Administrator | none | False |
| Administration.ChangeMyPasswordForm | Change Password | none | AccountPasswordData:Administration.AccountPasswordData | True |
| Administration.ChangePasswordForm | Change Password | none | AccountPasswordData:Administration.AccountPasswordData | True |
| Administration.MyAccount | My Account | none | Account:Administration.Account | True |
| Administration.RuntimeInstances | Runtime Instances | Administration.Administrator | none | False |
| Administration.ScheduledEvents | Scheduled Events | Administration.Administrator | none | False |

## Page-Flow Links

| Page | Shown by flows |
|---|---|
| Administration.Account_Edit | none (no show-page evidence) |
| Administration.Account_New | Administration.NewAccount, Administration.NewWebServiceAccount |
| Administration.Account_Overview | none (no show-page evidence) |
| Administration.ActiveSessions | none (no show-page evidence) |
| Administration.ChangeMyPasswordForm | Administration.ShowMyPasswordForm |
| Administration.ChangePasswordForm | Administration.ShowPasswordForm |
| Administration.MyAccount | Administration.ManageMyAccount |
| Administration.RuntimeInstances | none (no show-page evidence) |
| Administration.ScheduledEvents | none (no show-page evidence) |

## Journey Fragments

| User intent group | Pages |
|---|---|
| Account | Administration.Account_Edit, Administration.Account_New, Administration.Account_Overview |
| General | Administration.ActiveSessions, Administration.ChangeMyPasswordForm, Administration.ChangePasswordForm, Administration.MyAccount, Administration.RuntimeInstances, Administration.ScheduledEvents |

## Snippets

Snippet-level page widget behaviour is not exported in current overview contract.
