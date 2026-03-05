# Domain: Administration

## Entities

| Entity | Persistable | Attribute count | Access rule count |
|---|---|---:|---:|
| Administration.Account | True | 3 | 3 |
| Administration.AccountPasswordData | False | 3 | 1 |

Confidence: Export-backed

## Entity Lifecycle Matrix

| Entity | Create flows | Update flows | Delete flows | Read flows |
|---|---|---|---|---|
| Administration.Account | Administration.NewAccount, Administration.NewWebServiceAccount, SmartExpenses.DS_FBGProfile_Retreive_current | Administration.NewWebServiceAccount | none | SmartExpenses.DS_FBGProfile_Retreive_current, WorkflowCommons.DS_TaskAssignmentHelper_Account |
| Administration.AccountPasswordData | Administration.NewAccount, Administration.NewWebServiceAccount, Administration.ShowMyPasswordForm, Administration.ShowPasswordForm | Administration.NewWebServiceAccount | none | none |

Confidence: Inferred

## Role impacts per sensitive entity

| Entity | Module roles | Default member rights | XPath constraint |
|---|---|---|---|
| Administration.Account | Administration.Administrator | None | none |
| Administration.Account | Administration.User | ReadOnly | none |
| Administration.Account | Administration.User | None | [id='[%CurrentUser%]'] |
| Administration.AccountPasswordData | Administration.Administrator, Administration.User | ReadWrite | none |

Confidence: Export-backed

## Associations

| Association | Parent | Child | Cardinality | Type | Owner |
|---|---|---|---|---|---|
| Administration.AccountPasswordData_Account | Administration.AccountPasswordData | Administration.Account | *-1 | Reference | Default |

## Enumerations

| Enumeration | Value count | Sample values |
|---|---:|---|
| none | 0 | none |
