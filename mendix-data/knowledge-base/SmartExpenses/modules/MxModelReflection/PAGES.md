# Pages: MxModelReflection

## Page Inventory

| Page | Title | Allowed roles | Parameters | Popup |
|---|---|---|---|---|
| MxModelReflection.DbSizeEstimate_NewEdit | Edit Db Size Estimate | MxModelReflection.ModelAdministrator | DbSizeEstimate:MxModelReflection.DbSizeEstimate | True |
| MxModelReflection.EnumValue_Select | Select an enumeration value | none | none | True |
| MxModelReflection.Member_Select | Select an object member | none | none | True |
| MxModelReflection.Member_View | View member | MxModelReflection.ModelAdministrator | MxObjectMember:MxModelReflection.MxObjectMember | True |
| MxModelReflection.MemberEnum_View | View enum member | MxModelReflection.ModelAdministrator | MxObjectEnum:MxModelReflection.MxObjectEnum | True |
| MxModelReflection.Microflow_Select | Select a microflow | none | none | True |
| MxModelReflection.Microflow_View | Microflow details | MxModelReflection.ModelAdministrator | Microflows:MxModelReflection.Microflows | True |
| MxModelReflection.MxObject_Details | Object details | MxModelReflection.ModelAdministrator | MxObjectType:MxModelReflection.MxObjectType | True |
| MxModelReflection.MxObjectReference_Select | Select an object reference | none | none | True |
| MxModelReflection.MxObjectReference_View | Referentie details | MxModelReflection.ModelAdministrator | MxObjectReference:MxModelReflection.MxObjectReference | True |
| MxModelReflection.MxObjects_Overview | Domain model reflection | MxModelReflection.ModelAdministrator | none | False |
| MxModelReflection.MxObjectType_Select | Select an objecttype | none | none | True |
| MxModelReflection.SizeEstimate_Overview | Size estimate | MxModelReflection.ModelAdministrator | none | False |
| MxModelReflection.TestPattern_Edit | Test a Pattern | none | TestPattern:MxModelReflection.TestPattern | True |
| MxModelReflection.Token_NewEdit | Token details | MxModelReflection.ModelAdministrator, MxModelReflection.TokenUser | Token:MxModelReflection.Token | True |
| MxModelReflection.TokenOverview | Tokens | MxModelReflection.ModelAdministrator, MxModelReflection.Readonly | none | False |
| MxModelReflection.ValueType_View | Type details | MxModelReflection.ModelAdministrator | ValueType:MxModelReflection.ValueType | True |

## Page-Flow Links

| Page | Shown by flows |
|---|---|
| MxModelReflection.DbSizeEstimate_NewEdit | none (no show-page evidence) |
| MxModelReflection.EnumValue_Select | none (no show-page evidence) |
| MxModelReflection.Member_Select | none (no show-page evidence) |
| MxModelReflection.Member_View | MxModelReflection.ACT_ShowMemberPage |
| MxModelReflection.MemberEnum_View | MxModelReflection.ACT_ShowMemberPage |
| MxModelReflection.Microflow_Select | none (no show-page evidence) |
| MxModelReflection.Microflow_View | none (no show-page evidence) |
| MxModelReflection.MxObject_Details | MxModelReflection.IVK_OpenReferencedMendixObject |
| MxModelReflection.MxObjectReference_Select | none (no show-page evidence) |
| MxModelReflection.MxObjectReference_View | none (no show-page evidence) |
| MxModelReflection.MxObjects_Overview | none (no show-page evidence) |
| MxModelReflection.MxObjectType_Select | none (no show-page evidence) |
| MxModelReflection.SizeEstimate_Overview | none (no show-page evidence) |
| MxModelReflection.TestPattern_Edit | MxModelReflection.MB_TestTokenPattern |
| MxModelReflection.Token_NewEdit | none (no show-page evidence) |
| MxModelReflection.TokenOverview | none (no show-page evidence) |
| MxModelReflection.ValueType_View | none (no show-page evidence) |

## Journey Fragments

| User intent group | Pages |
|---|---|
| DbSizeEstimate | MxModelReflection.DbSizeEstimate_NewEdit |
| EnumValue | MxModelReflection.EnumValue_Select |
| General | MxModelReflection.TokenOverview |
| Member | MxModelReflection.Member_Select, MxModelReflection.Member_View |
| MemberEnum | MxModelReflection.MemberEnum_View |
| Microflow | MxModelReflection.Microflow_Select, MxModelReflection.Microflow_View |
| MxObject | MxModelReflection.MxObject_Details |
| MxObjectReference | MxModelReflection.MxObjectReference_Select, MxModelReflection.MxObjectReference_View |
| MxObjects | MxModelReflection.MxObjects_Overview |
| MxObjectType | MxModelReflection.MxObjectType_Select |
| SizeEstimate | MxModelReflection.SizeEstimate_Overview |
| TestPattern | MxModelReflection.TestPattern_Edit |
| Token | MxModelReflection.Token_NewEdit |
| ValueType | MxModelReflection.ValueType_View |

## Snippets

Snippet-level page widget behaviour is not exported in current overview contract.
