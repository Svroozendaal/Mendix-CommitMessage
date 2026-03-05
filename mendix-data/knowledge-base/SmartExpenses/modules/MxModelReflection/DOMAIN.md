# Domain Model: MxModelReflection

## Entities

| Entity | Persistable | Attributes | Access Rules |
|---|---|---:|---:|
| MxModelReflection.DbSizeEstimate | True | 4 |  |
| MxModelReflection.InheritsFromContainer | False |  |  |
| MxModelReflection.Microflows | True | 3 | 2 |
| MxModelReflection.Module | True | 2 | 2 |
| MxModelReflection.MxObjectEnum | True | 0 | 2 |
| MxModelReflection.MxObjectEnumCaptions | True | 3 | 2 |
| MxModelReflection.MxObjectEnumValue | True |  | 2 |
| MxModelReflection.MxObjectMember | True | 7 | 2 |
| MxModelReflection.MxObjectReference | True | 7 | 2 |
| MxModelReflection.MxObjectType | True | 5 | 2 |
| MxModelReflection.Parameter | True |  | 2 |
| MxModelReflection.StringValue | False |  |  |
| MxModelReflection.TestPattern | False | 9 |  |
| MxModelReflection.Token | True | 15 | 2 |
| MxModelReflection.ValueType | True | 2 | 2 |

## Associations

| Association | Parent | Child | Type |
|---|---|---|---|
| MxModelReflection.Captions | MxModelReflection.MxObjectEnumValue | MxModelReflection.MxObjectEnumCaptions | ReferenceSet (*-*) |
| MxModelReflection.DbSizeEstimate_MxObjectType | MxModelReflection.DbSizeEstimate | MxModelReflection.MxObjectType | Reference (*-1) |
| MxModelReflection.Microflows_InputParameter | MxModelReflection.Microflows | MxModelReflection.Parameter | ReferenceSet (*-*) |
| MxModelReflection.Microflows_Module | MxModelReflection.Microflows | MxModelReflection.Module | Reference (*-1) |
| MxModelReflection.Microflows_Output_Type | MxModelReflection.Microflows | MxModelReflection.ValueType | Reference (*-1) |
| MxModelReflection.MxObjectMember_MxObjectType | MxModelReflection.MxObjectMember | MxModelReflection.MxObjectType | Reference (*-1) |
| MxModelReflection.MxObjectMember_Type | MxModelReflection.MxObjectMember | MxModelReflection.ValueType | Reference (*-1) |
| MxModelReflection.MxObjectReference_Module | MxModelReflection.MxObjectReference | MxModelReflection.Module | Reference (*-1) |
| MxModelReflection.MxObjectReference_MxObjectType | MxModelReflection.MxObjectReference | MxModelReflection.MxObjectType | ReferenceSet (*-*) |
| MxModelReflection.MxObjectReference_MxObjectType_Child | MxModelReflection.MxObjectReference | MxModelReflection.MxObjectType | ReferenceSet (*-*) |
| MxModelReflection.MxObjectReference_MxObjectType_Parent | MxModelReflection.MxObjectReference | MxModelReflection.MxObjectType | ReferenceSet (*-*) |
| MxModelReflection.MxObjectType_Module | MxModelReflection.MxObjectType | MxModelReflection.Module | Reference (*-1) |
| MxModelReflection.MxObjectType_SubClassOf_MxObjectType | MxModelReflection.MxObjectType | MxModelReflection.MxObjectType | ReferenceSet (*-*) |
| MxModelReflection.Parameter_MxObjectType | MxModelReflection.Parameter | MxModelReflection.MxObjectType | Reference (*-1) |
| MxModelReflection.Parameter_ValueType | MxModelReflection.Parameter | MxModelReflection.ValueType | Reference (*-1) |
| MxModelReflection.Token_MxObjectMember | MxModelReflection.Token | MxModelReflection.MxObjectMember | Reference (*-1) |
| MxModelReflection.Token_MxObjectReference | MxModelReflection.Token | MxModelReflection.MxObjectReference | Reference (*-1) |
| MxModelReflection.Token_MxObjectType_Referenced | MxModelReflection.Token | MxModelReflection.MxObjectType | Reference (*-1) |
| MxModelReflection.Token_MxObjectType_Start | MxModelReflection.Token | MxModelReflection.MxObjectType | Reference (*-1) |
| MxModelReflection.Values | MxModelReflection.MxObjectEnum | MxModelReflection.MxObjectEnumValue | ReferenceSet (*-*) |
| MxModelReflection.ValueType_MxObjectType | MxModelReflection.ValueType | MxModelReflection.MxObjectType | Reference (*-1) |

## Enumerations

| Enumeration | Values |
|---|---|
| MxModelReflection.AssociationOwner | _Default, Both |
| MxModelReflection.AttributeTypes | AutoNumber, BooleanType, DateTime, Decimal, EnumType, HashString, IntegerType, LongType, StringType |
| MxModelReflection.PersistenceType | Non_persistent, Persistable |
| MxModelReflection.PrimitiveTypes | AutoNumber, BooleanType, DateTime, Decimal, EnumType, HashString, IntegerType, LongType, ObjectList, ObjectType +1 more |
| MxModelReflection.ReferenceType | Reference, ReferenceSet |
| MxModelReflection.Status | Invalid, Valid |
| MxModelReflection.TokenType | Attribute, Reference |

