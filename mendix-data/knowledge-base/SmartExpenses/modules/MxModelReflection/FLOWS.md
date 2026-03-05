# Flows: MxModelReflection

## Flow Catalogue

### Action Flows (ACT_*)

| Flow | Nodes | Key Actions | Pages Shown |
|---|---:|---|---|
| ACT_ShowMemberPage | 9 | none | MxModelReflection.Member_View, MxModelReflection.MemberEnum_View |

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
| AssociationIsReferenceSet | Rule | 18 | none |
| ASu_CheckMetamodel | Microflow | 4 | none |
| BCo_MxObjectMember_CreateCompleteMemberName | Microflow | 8 | none |
| BCo_MxObjectReference | Microflow | 6 | none |
| BCo_MxObjectType | Microflow | 6 | none |
| BCo_Token | Microflow | 21 | none |
| BDe_MxObjectType | Microflow | 12 | none |
| Ch_Member | Microflow | 5 | none |
| Ch_ObjecttypeReference | Microflow | 5 | none |
| Ch_ObjectTypeStart | Microflow | 5 | none |
| Ch_Reference | Microflow | 5 | none |
| DeleteDbSizeEstimate | Microflow | 4 | none |
| DeleteToken | Microflow | 4 | none |
| DSL_Modules | Microflow | 6 | MxModelReflection.Module |
| DSO_InheritsFromContainer | Microflow | 10 | MxModelReflection.InheritsFromContainer |
| EnumValueCaptions | Microflow | 5 | MxModelReflection.StringValue |
| EnumValueLanguages | Microflow | 5 | MxModelReflection.StringValue |
| FindMember | Microflow | 11 | MxModelReflection.MxObjectMember |
| FindMicroflow | Microflow | 9 | MxModelReflection.Microflows |
| FindObjectType | Microflow | 18 | MxModelReflection.MxObjectType |
| FindReference | Microflow | 18 | MxModelReflection.MxObjectReference |
| IVK_deleteAll | Microflow | 8 | MxModelReflection.Microflows, MxModelReflection.MxObjectType, MxModelReflection.ValueType |
| IVK_MxObjectTypeCommit | Microflow | 4 | none |
| IVK_OpenReferencedMendixObject | Microflow | 9 | none |
| IVK_RecalculateSize | Microflow | 27 | MxModelReflection.DbSizeEstimate, MxModelReflection.MxObjectMember |
| IVK_SyncObjects | Microflow | 3 | none |
| IVK_ToggleModule | Microflow | 4 | none |
| Log | Microflow | 10 | none |
| MB_TestThePattern | Microflow | 7 | none |
| MB_TestTokenPattern | Microflow | 5 | MxModelReflection.TestPattern |
| OC_FindObjectType | Microflow | 5 | none |
| ReferenceObjects | Microflow | 5 | MxModelReflection.StringValue |

## Cross-Module Calls

| Flow | Calls | Target Module |
|---|---|---|
| none | none | none |

## Flow Details

| Flow | Kind | Nodes | Tier | Calls Out | Called By |
|---|---|---:|---:|---:|---:|
| ACT_ShowMemberPage | Microflow | 9 | 3 | 0 | 0 |
| AssociationIsReferenceSet | Rule | 18 | 3 | 0 | 0 |
| ASu_CheckMetamodel | Microflow | 4 | 3 | 0 | 1 |
| BCo_MxObjectMember_CreateCompleteMemberName | Microflow | 8 | 3 | 0 | 0 |
| BCo_MxObjectReference | Microflow | 6 | 3 | 0 | 0 |
| BCo_MxObjectType | Microflow | 6 | 3 | 0 | 0 |
| BCo_Token | Microflow | 21 | 3 | 0 | 0 |
| BDe_MxObjectType | Microflow | 12 | 3 | 0 | 0 |
| Ch_Member | Microflow | 5 | 3 | 0 | 0 |
| Ch_ObjecttypeReference | Microflow | 5 | 3 | 0 | 0 |
| Ch_ObjectTypeStart | Microflow | 5 | 3 | 0 | 0 |
| Ch_Reference | Microflow | 5 | 3 | 0 | 0 |
| DeleteDbSizeEstimate | Microflow | 4 | 3 | 0 | 0 |
| DeleteToken | Microflow | 4 | 3 | 0 | 0 |
| DSL_Modules | Microflow | 6 | 3 | 1 | 0 |
| DSO_InheritsFromContainer | Microflow | 10 | 3 | 0 | 0 |
| EnumValueCaptions | Microflow | 5 | 3 | 0 | 0 |
| EnumValueLanguages | Microflow | 5 | 3 | 0 | 0 |
| FindMember | Microflow | 11 | 3 | 0 | 3 |
| FindMicroflow | Microflow | 9 | 3 | 0 | 2 |
| FindObjectType | Microflow | 18 | 3 | 0 | 3 |
| FindReference | Microflow | 18 | 3 | 0 | 2 |
| IVK_deleteAll | Microflow | 8 | 3 | 0 | 0 |
| IVK_MxObjectTypeCommit | Microflow | 4 | 3 | 0 | 0 |
| IVK_OpenReferencedMendixObject | Microflow | 9 | 3 | 0 | 0 |
| IVK_RecalculateSize | Microflow | 27 | 3 | 1 | 0 |
| IVK_SyncObjects | Microflow | 3 | 3 | 0 | 1 |
| IVK_ToggleModule | Microflow | 4 | 3 | 0 | 0 |
| Log | Microflow | 10 | 3 | 0 | 1 |
| MB_TestThePattern | Microflow | 7 | 3 | 0 | 0 |
| MB_TestTokenPattern | Microflow | 5 | 3 | 0 | 0 |
| OC_FindObjectType | Microflow | 5 | 3 | 1 | 0 |
| ReferenceObjects | Microflow | 5 | 3 | 0 | 0 |

## Tier 1 Deep Narratives

No Tier 1 narrative required for this module category.
