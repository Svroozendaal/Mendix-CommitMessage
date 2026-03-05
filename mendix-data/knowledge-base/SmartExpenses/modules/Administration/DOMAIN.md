# Domain Model: Administration

## Entities

| Entity | Persistable | Attributes | Access Rules |
|---|---|---:|---:|
| Administration.Account | True | 3 | 3 |
| Administration.AccountPasswordData | False | 3 |  |

## Associations

| Association | Parent | Child | Type |
|---|---|---|---|
| Administration.AccountPasswordData_Account | Administration.AccountPasswordData | Administration.Account | Reference (*-1) |

## Enumerations

none

