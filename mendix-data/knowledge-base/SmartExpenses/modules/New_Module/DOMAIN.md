# Domain: New_Module

## Entities

| Entity | Persistable | Attribute count | Access rule count |
|---|---|---:|---:|
| New_Module.Entity2 | True | 1 | 0 |
| New_Module.Entity3assossiatatedwithEntity2 | True | 0 | 0 |
| New_Module.Entity5 | False | 1 | 0 |
| New_Module.Entitywith10attributes | True | 3 | 1 |
| New_Module.GeneralizationEntityImage | True | 0 | 0 |

Confidence: Export-backed

## Entity Lifecycle Matrix

| Entity | Create flows | Update flows | Delete flows | Read flows |
|---|---|---|---|---|
| New_Module.Entity2 | none | none | none | none |
| New_Module.Entity3assossiatatedwithEntity2 | none | none | none | none |
| New_Module.Entity5 | none | none | none | none |
| New_Module.Entitywith10attributes | none | none | none | none |
| New_Module.GeneralizationEntityImage | none | none | none | none |

Confidence: Inferred

## Role impacts per sensitive entity

| Entity | Module roles | Default member rights | XPath constraint |
|---|---|---|---|
| New_Module.Entitywith10attributes | New_Module.ModuleRole | ReadOnly | none |

Confidence: Export-backed

## Associations

| Association | Parent | Child | Cardinality | Type | Owner |
|---|---|---|---|---|---|
| New_Module.Entity3assossiatatedwithEntity2_Entity2 | New_Module.Entity3assossiatatedwithEntity2 | New_Module.Entity2 | *-1 | Reference | Default |
| New_Module.Entitywith10attributes_Entity2 | New_Module.Entitywith10attributes | New_Module.Entity2 | *-1 | Reference | Default |

## Enumerations

| Enumeration | Value count | Sample values |
|---|---:|---|
| none | 0 | none |
