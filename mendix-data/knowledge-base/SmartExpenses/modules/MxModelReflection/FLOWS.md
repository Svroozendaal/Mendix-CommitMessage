# Flows: MxModelReflection

## Flow Catalogue

### Action Flows (ACT_*)

| Flow | Nodes | Key Actions | Pages Shown |
|---|---:|---|---|
| ACT_ShowMemberPage | 9 | CastAction (output=ObjectName, errorHandlingType=Rollback), show page MxModelReflection.MemberEnum_View | Unknown |

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
| AssociationIsReferenceSet | Rule | 18 | ListOperationAction (output=IsMxObjectEndpointInParent, errorHandlingType=Rollback), ListOperationAction (output=IsMxObjectStartpointInChild, errorHandlingType=Rollback) |
| ASu_CheckMetamodel | Microflow | 4 | call java action MxModelReflection.SyncObjects -> none |
| BCo_MxObjectMember_CreateCompleteMemberName | Microflow | 8 | change MxObjectMember (CompleteName=' / ' + $MxObjectMember/AttributeName; refreshInClient=false), change MxObjectMember (CompleteName=$MxObjectType/CompleteName + ' / ' + $MxObjectMember/AttributeName; refreshInClient=false) |
| BCo_MxObjectReference | Microflow | 6 | change MxObjectReference (ReadableName=$MxObjectReference/CompleteName; refreshInClient=false) |
| BCo_MxObjectType | Microflow | 6 | change MxObjectType (ReadableName=$MxObjectType/Name + ' from the ' + $MxObjectType/Module + ' module'; refreshInClient=false) |
| BCo_Token | Microflow | 21 | change Token (CombinedToken=$Token/Prefix + $Token/Token + $Token/Suffix, Description=if $Token/Description = empty or $Token/Description = '' then $Token/Token else $Token/Description; refreshInClient=false), change Token (Status=MxModelReflection.Status.Invalid; refreshInClient=false) |
| BDe_MxObjectType | Microflow | 12 | AggregateListAction (output=count, errorHandlingType=Rollback), delete Reference (refreshInClient=false) |
| Ch_Member | Microflow | 5 | change Token (FindMember=$MxObjectMember/AttributeName, FindMemberReference=$MxObjectMember/AttributeName; refreshInClient=true), retrieve MxObjectMember over association Token_MxObjectMember from Token |
| Ch_ObjecttypeReference | Microflow | 5 | change Token (FindObjectReference=$MxObjectType/CompleteName; refreshInClient=true), retrieve MxObjectType over association Token_MxObjectType_Referenced from Token |
| Ch_ObjectTypeStart | Microflow | 5 | change Token (FindObjectStart=$MxObjectType/CompleteName; refreshInClient=true), retrieve MxObjectType over association Token_MxObjectType_Start from Token |
| Ch_Reference | Microflow | 5 | change Token (FindReference=$MxObjectReference/CompleteName; refreshInClient=true), retrieve MxObjectReference over association Token_MxObjectReference from Token |
| DeleteDbSizeEstimate | Microflow | 4 | delete DbSizeEstimate (refreshInClient=true) |
| DeleteToken | Microflow | 4 | delete Token (refreshInClient=true) |
| DSL_Modules | Microflow | 6 | call microflow MxModelReflection.IVK_SyncObjects, retrieve ModuleList from MxModelReflection.Module |
| DSO_InheritsFromContainer | Microflow | 10 | change NewInheritsFromContainer (Summary=if $NewInheritsFromContainer/Summary = empty then $IteratorMxObjectType/CompleteName else $NewInheritsFromContainer/Summary +', ' + $IteratorMxObjectType/CompleteName; refreshInClient=false), change NewInheritsFromContainer (Summary='None (make sure a related module is synchronized too)'; refreshInClient=false) |
| EnumValueCaptions | Microflow | 5 | call java action MxModelReflection.JA_EnumValueCaptions -> captions, create MxModelReflection.StringValue as NewStringValue (Value=$captions) |
| EnumValueLanguages | Microflow | 5 | call java action MxModelReflection.JA_EnumValueLanguages -> languages, create MxModelReflection.StringValue as NewStringValue (Value=$languages) |
| FindMember | Microflow | 11 | create variable MemberSearch=replaceAll(trim( $MemberSearchString), ' ', ''), retrieve RetrievedMember from MxModelReflection.MxObjectMember |
| FindMicroflow | Microflow | 9 | retrieve RetrievedMicroflow from MxModelReflection.Microflows, retrieve RetrievedMicroflow_1 from MxModelReflection.Microflows |
| FindObjectType | Microflow | 18 | retrieve RetrievedObjectType from MxModelReflection.MxObjectType, retrieve RetrievedObjectType_1 from MxModelReflection.MxObjectType |
| FindReference | Microflow | 18 | retrieve RetrievedReference from MxModelReflection.MxObjectReference, retrieve RetrievedReference_1 from MxModelReflection.MxObjectReference |
| IVK_deleteAll | Microflow | 8 | delete allMicroflows (refreshInClient=true), delete allObjTypes (refreshInClient=true) |
| IVK_MxObjectTypeCommit | Microflow | 4 | commit MxObjectType (refreshInClient=false, withEvents=true) |
| IVK_OpenReferencedMendixObject | Microflow | 9 | retrieve MendixObject over association MxObjectReference_MxObjectType from Selection, show page MxModelReflection.MxObject_Details |
| IVK_RecalculateSize | Microflow | 27 | AggregateListAction (output=count, errorHandlingType=Rollback), change Estimate (CalculatedSizeInBytes=$Size, CalculatedSizeInKiloBytes=ceil($Size : 1024); refreshInClient=false) |
| IVK_SyncObjects | Microflow | 3 | call java action MxModelReflection.SyncObjects -> none |
| IVK_ToggleModule | Microflow | 4 | commit Module (refreshInClient=false, withEvents=false) |
| Log | Microflow | 10 | change variable Comparator=$Comparator * $Base |
| MB_TestThePattern | Microflow | 7 | call java action MxModelReflection.TestThePattern -> ignore, LogMessageAction (errorHandlingType=Rollback) |
| MB_TestTokenPattern | Microflow | 5 | create MxModelReflection.TestPattern as NewTestPattern (DisplayPattern=$Token/DisplayPattern), show page MxModelReflection.TestPattern_Edit |
| OC_FindObjectType | Microflow | 5 | call microflow MxModelReflection.FindObjectType -> MxObjectType, change DbSizeEstimate (DbSizeEstimate_MxObjectType=$MxObjectType, FindObjectType=if $MxObjectType != empty then $MxObjectType/CompleteName else $DbSizeEstimate/FindObjectType; refreshInClient=true) |
| ReferenceObjects | Microflow | 5 | call java action MxModelReflection.JA_ReferenceObjects -> ReturnValueName, create MxModelReflection.StringValue as NewStringValue (Value=$ReturnValueName) |

## Cross-Module Calls

| Flow | Calls | Target Module |
|---|---|---|
| none | none | none |

## Flow Details

| Flow | Kind | Nodes | Calls Out | Called By |
|---|---|---:|---:|---:|
| ACT_ShowMemberPage | Microflow | 9 | 0 | 0 |
| AssociationIsReferenceSet | Rule | 18 | 0 | 0 |
| ASu_CheckMetamodel | Microflow | 4 | 0 | 1 |
| BCo_MxObjectMember_CreateCompleteMemberName | Microflow | 8 | 0 | 0 |
| BCo_MxObjectReference | Microflow | 6 | 0 | 0 |
| BCo_MxObjectType | Microflow | 6 | 0 | 0 |
| BCo_Token | Microflow | 21 | 0 | 0 |
| BDe_MxObjectType | Microflow | 12 | 0 | 0 |
| Ch_Member | Microflow | 5 | 0 | 0 |
| Ch_ObjecttypeReference | Microflow | 5 | 0 | 0 |
| Ch_ObjectTypeStart | Microflow | 5 | 0 | 0 |
| Ch_Reference | Microflow | 5 | 0 | 0 |
| DeleteDbSizeEstimate | Microflow | 4 | 0 | 0 |
| DeleteToken | Microflow | 4 | 0 | 0 |
| DSL_Modules | Microflow | 6 | 1 | 0 |
| DSO_InheritsFromContainer | Microflow | 10 | 0 | 0 |
| EnumValueCaptions | Microflow | 5 | 0 | 0 |
| EnumValueLanguages | Microflow | 5 | 0 | 0 |
| FindMember | Microflow | 11 | 0 | 3 |
| FindMicroflow | Microflow | 9 | 0 | 2 |
| FindObjectType | Microflow | 18 | 0 | 3 |
| FindReference | Microflow | 18 | 0 | 2 |
| IVK_deleteAll | Microflow | 8 | 0 | 0 |
| IVK_MxObjectTypeCommit | Microflow | 4 | 0 | 0 |
| IVK_OpenReferencedMendixObject | Microflow | 9 | 0 | 0 |
| IVK_RecalculateSize | Microflow | 27 | 1 | 0 |
| IVK_SyncObjects | Microflow | 3 | 0 | 1 |
| IVK_ToggleModule | Microflow | 4 | 0 | 0 |
| Log | Microflow | 10 | 0 | 1 |
| MB_TestThePattern | Microflow | 7 | 0 | 0 |
| MB_TestTokenPattern | Microflow | 5 | 0 | 0 |
| OC_FindObjectType | Microflow | 5 | 1 | 0 |
| ReferenceObjects | Microflow | 5 | 0 | 0 |

