# Domain Model: New_Module

## Entities

| Entity | Persistable | Attributes | Access Rules |
|---|---|---:|---:|
| New_Module.Entity2 | True |  | 0 |
| New_Module.Entity3assossiatatedwithEntity2 | True | 0 | 0 |
| New_Module.Entity5 | False |  | 0 |
| New_Module.Entitywith10attributes | True | 3 |  |
| New_Module.GeneralizationEntityImage | True | 0 | 0 |

## Associations

| Association | Parent | Child | Type |
|---|---|---|---|
| New_Module.Entity3assossiatatedwithEntity2_Entity2 | New_Module.Entity3assossiatatedwithEntity2 | New_Module.Entity2 | Reference (*-1) |
| New_Module.Entitywith10attributes_Entity2 | New_Module.Entitywith10attributes | New_Module.Entity2 | Reference (*-1) |

## Enumerations

none

