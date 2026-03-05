# Module: MxModelReflection

Category: Marketplace
Module roles: ModelAdministrator, Readonly, TokenUser

## Summary

- Entities: 15
- Flows: 33
- Pages: 17
- Constants: 0

## Purpose

- Export-backed: module category and inventory from overview export.
- Inferred: module role is support capability.
- Unknown: product-owner intent text is not included in export.

## Capability Map

| Capability prefix | Flow count | Representative flow |
|---|---:|---|
| ACT | 1 | MxModelReflection.ACT_ShowMemberPage |
| ASU | 1 | MxModelReflection.ASu_CheckMetamodel |
| BCO | 4 | MxModelReflection.BCo_MxObjectMember_CreateCompleteMemberName |
| BDE | 1 | MxModelReflection.BDe_MxObjectType |
| CH | 4 | MxModelReflection.Ch_Member |
| DSL | 1 | MxModelReflection.DSL_Modules |
| DSO | 1 | MxModelReflection.DSO_InheritsFromContainer |
| IVK | 6 | MxModelReflection.IVK_deleteAll |
| MB | 2 | MxModelReflection.MB_TestThePattern |
| OC | 1 | MxModelReflection.OC_FindObjectType |
| OTHER | 11 | MxModelReflection.AssociationIsReferenceSet |

## Primary User Journeys

| Entry flow | UI result | Entities touched |
|---|---|---|
| support module | n/a | dependency-focused summary |

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
- Called by: ExcelImporter
- Shared entities via associations: none

## Source

- Export module: MxModelReflection
- Run folder: cli_2026-03-04T20-44-47.917Z
