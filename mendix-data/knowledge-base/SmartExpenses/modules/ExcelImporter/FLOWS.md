# Flows: ExcelImporter

## Flow Catalogue

### Action Flows (ACT_*)

| Flow | Nodes | Key Actions | Pages Shown |
|---|---:|---|---|
| none | 0 | none | none |

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
| _DocumentationDummyXSD | Microflow | 3 | none |
| _DocumentationExportParseFlows | Microflow | 3 | none |
| _DocumentationImportParseFlows | Microflow | 3 | none |
| ACr_Template | Microflow | 6 | change NewAdditionalProperties (Template_AdditionalProperties=$Template; refreshInClient=false), change Template (Template_AdditionalProperties=$NewAdditionalProperties; refreshInClient=false) |
| ASu_CheckModelAndTemplates | Microflow | 8 | call microflow ExcelImporter.ValidateTemplate -> valid, change iTemplate (Status=if( $iTemplate/Status = ExcelImporter.Status.INFO ) then ExcelImporter.Status.INFO else if( $valid ) then ExcelImporter.Status.VALID else ExcelImporter.Status.INVALID; refreshInClient=false) |
| BCo_Column | Microflow | 44 | change pColumn (FindAttribute=empty, FindReference=empty, FindObjectType=empty, FindMicroflow=empty, Column_MxObjectType_Reference=empty, Column_MxObjectMember=empty, Column_MxObjectMember_Reference=empty, Column_MxObjectReference=empty, +4 more; refreshInClient=false), change pColumn (IsReferenceKey=if( $pColumn/MappingType = ExcelImporter.MappingType.Attribute ) then if( $pColumn/IsKey = ExcelImporter.YesNo.Yes ) then ExcelImporter.ReferenceKeyType.YesOnlyMainObject else Exce...; refreshInClient=false) |
| BDe_Column | Microflow | 6 | call microflow ExcelImporter.prepareReferenceHandling, change Column (Column_MxObjectType_Reference=empty, Column_MxObjectType=empty, Column_MxObjectMember=empty, Column_MxObjectMember_Reference=empty, Column_MxObjectReference=empty, MappingType=ExcelImporter.MappingType.DoNotUse; refreshInClient=false) |
| Ch_Column_SetDefaultObject | Microflow | 10 | call microflow ExcelImporter.Ch_SetReference, change Columns (Column_MxObjectType_Reference=empty, Column_MxObjectMember_Reference=empty; refreshInClient=true) |
| Ch_FindAttribute | Microflow | 13 | change Column (Column_MxObjectMember=$Member, FindAttribute=$Member/AttributeName, AttributeTypeEnum=$Member/AttributeTypeEnum; refreshInClient=true), change Column (Column_MxObjectMember=empty, AttributeTypeEnum=empty; refreshInClient=true) |
| Ch_FindAttribute_Reference | Microflow | 16 | change Column (Column_MxObjectMember=empty, AttributeTypeEnum=empty; refreshInClient=true) |
| Ch_FindMicroflow | Microflow | 12 | change Column (Column_Microflows=$Microflow, FindMicroflow=$Microflow/CompleteName; refreshInClient=true), change Column (Column_Microflows=empty; refreshInClient=true) |
| Ch_FindObjectType_Reference | Microflow | 13 | change Column (Column_MxObjectType_Reference=empty; refreshInClient=true) |
| Ch_FindReference | Microflow | 14 | change Column (Column_MxObjectReference=$Reference, FindReference=$Reference/CompleteName; refreshInClient=true), change Column (Column_MxObjectReference=empty; refreshInClient=true) |
| Ch_SetAttribute | Microflow | 8 | change column (FindAttribute=$Member/AttributeName, Column_ValueType=$Member/MxModelReflection.MxObjectMember_Type, AttributeTypeEnum=$Member/AttributeTypeEnum; refreshInClient=true), change column (FindAttribute=empty, Column_ValueType=empty, AttributeTypeEnum=empty; refreshInClient=true) |
| Ch_SetAttribute_Reference | Microflow | 8 | change column (FindAttribute=$Member/AttributeName, Column_ValueType=$Member/MxModelReflection.MxObjectMember_Type, AttributeTypeEnum=$Member/AttributeTypeEnum; refreshInClient=true), change column (FindAttribute=empty, Column_ValueType=empty, AttributeTypeEnum=empty; refreshInClient=true) |
| Ch_SetMicroflow | Microflow | 8 | change column (FindMicroflow=$Microflow/CompleteName; refreshInClient=true), change column (FindMicroflow=empty; refreshInClient=true) |
| Ch_SetObjectType_Reference | Microflow | 8 | change column (FindObjectType=$MxObjectType/CompleteName; refreshInClient=true), change column (FindObjectType=empty; refreshInClient=true) |
| Ch_SetReference | Microflow | 8 | change column (FindReference=$Reference/CompleteName; refreshInClient=true), change column (FindReference=empty; refreshInClient=true) |
| Ch_Template_ChangeObjectType | Microflow | 8 | change Iterator (Column_MxObjectType=$Template/ExcelImporter.Template_MxObjectType; refreshInClient=false), change Iterator (refreshInClient=true) |
| Ch_Template_CheckNrs | Microflow | 4 | call microflow ExcelImporter.SF_Template_CheckNrs -> IsValid |
| CleanupOldRefHandling | Microflow | 8 | delete IteratorReferenceHandling (refreshInClient=false), retrieve ReferenceHandlingList over association ReferenceHandling_Template from Template |
| Column_SetCorrectRefObjectType | Microflow | 21 | AggregateListAction (output=NrOfObjects, errorHandlingType=Rollback), change Column (Column_MxObjectType_Reference=$Iterator; refreshInClient=true) |
| Column_SetDetails | Microflow | 13 | change pColumn (Details=if( $Member != empty ) then 'Attribute: ' + $Member/AttributeName + ', type: ' + $Member/AttributeType else 'Nothing selected'; refreshInClient=false), change pColumn (Details='Reference: ' + (if $Reference != empty then $Reference/CompleteName else 'nothing selected' )+ '/' + (if $ReferenceObjectType != empty then $ReferenceObjectType/CompleteName else ...; refreshInClient=false) |
| ColumnDataSourceToString | Microflow | 3 | none |
| ColumnMappingTypeToString | Microflow | 5 | create variable MappingTypeString=getKey($MappingType) |
| ColumnReferenceKeyTypeToString | Microflow | 5 | create variable ReferenceKeyTypeString=getKey($ReferenceKeyType) |
| ColumnYesNoToString | Microflow | 5 | create variable YesNoString=getKey($YesNo) |
| Example_SetupImportTemplate | Microflow | 12 | commit ColumnList (refreshInClient=false, withEvents=true), commit Template (refreshInClient=false, withEvents=true) |
| ExcelImporterTemplateXSD | Microflow | 4 | retrieve Template from ExcelImporter.Template |
| ExcelTemplate_ExportToXML | Microflow | 12 | change NewXMLDocumentTemplate (Name='ExcelTemplate_' + $Template/Title + '_' + formatDateTime([%CurrentDateTime%], 'yyyy-MM-dd_HHmm') + '.xml'; refreshInClient=false), change NewXMLDocumentTemplate (XMLDocumentTemplate_Template=$Template; refreshInClient=false) |
| ExcelTemplate_ImportFromXml | Microflow | 12 | change NewLogXMLToDom (Log_XMLDocumentTemplate=$XMLDocumentTemplate, Logline='The mapping from XML to Domain throwed an error. Be sure the MxModelReflection and selected DB is synchronized. ' + 'LastErrorMessage: ' + $latestError/Message + '. ' + '' + 'Last...; refreshInClient=false), close page |
| FormatInteger | Microflow | 5 | create variable FormattedInt=if( contains( $Unformatted, '.') ) then substring($Unformatted, 0, find($Unformatted,'.')) else $Unformatted |
| GetAddProperties | Microflow | 9 | change NewAdditionalProperties (Template_AdditionalProperties=$Template; refreshInClient=false), change Template (Template_AdditionalProperties=$NewAdditionalProperties; refreshInClient=false) |
| GetCorrectString | Microflow | 7 | none |
| IVK_CancelTemplate | Microflow | 12 | close page |
| IVK_Column_Save | Microflow | 55 | change Column (IsKey=ExcelImporter.YesNo.No, IsReferenceKey=ExcelImporter.ReferenceKeyType.NoKey, ColNumber=999; refreshInClient=false), change Column (refreshInClient=true) |
| IVK_ColumnEdit | Microflow | 8 | show message (text=A MetaObject must be selected before a column can be created or changed., type=Error, blocking=true), show page ExcelImporter.Column_NewEdit |
| IVK_ColumnNew | Microflow | 9 | change NewColAttributeRelation (Column_MxObjectType=$EnclosingContext/ExcelImporter.Template_MxObjectType, Column_Template=$EnclosingContext; refreshInClient=false), create ExcelImporter.Column as NewColAttributeRelation |
| IVK_DuplicateTemplate | Microflow | 17 | change AdditionalProperties_Copy (PrintStatisticsMessages=$AdditionalProperties/PrintStatisticsMessages, PrintNotFoundMessages_MainObject=$AdditionalProperties/PrintNotFoundMessages_MainObject, IgnoreEmptyKeys=$AdditionalProperties/IgnoreEmptyKeys, CommitUnchangedObjects_MainObject=$AdditionalProperties/CommitUnchangedObjects_MainObject, RemoveUnsyncedObjects=$AdditionalProperties/RemoveUnsyncedObjects, ResetEmptyAssociations=$AdditionalProperties/ResetEmptyAssociations, AdditionalProperties_MxObjectMember_RemoveIndicator=$AdditionalProperties/ExcelImporter.AdditionalProperties_MxObjectMember_RemoveIndicator; refreshInClient=true), change NewColumn (ColNumber=$Column/ColNumber, Text=$Column/Text, MappingType=$Column/MappingType, IsKey=$Column/IsKey, IsReferenceKey=$Column/IsReferenceKey, Status=$Column/Status, Details=$Column/Details, CaseSensitive=$Column/CaseSensitive, +13 more; refreshInClient=true) |
| IVK_ImportTemplateDocument | Microflow | 14 | call java action ExcelImporter.StartImportByTemplate -> rowCount, LogMessageAction (errorHandlingType=Rollback) |
| IVK_ImportXML_Upload | Microflow | 6 | change NewXMLDocumentTemplate (refreshInClient=false), create ExcelImporter.XMLDocumentTemplate as NewXMLDocumentTemplate |
| IVK_SaveContinue_CreateTemplateFromDoc | Microflow | 16 | change Template (refreshInClient=true), close page |
| IVK_SaveNewTemplate | Microflow | 7 | call microflow ExcelImporter.IVK_SaveTemplate -> Valid, show page ExcelImporter.Template_Edit |
| IVK_SaveNewTemplate_CreateColumns | Microflow | 5 | call microflow ExcelImporter.IVK_SaveNewTemplate, call microflow ExcelImporter.Sub_CreateColumnsFromTemplate |
| IVK_SaveTemplate | Microflow | 12 | change AddProperties (refreshInClient=true), change Template (Status=ExcelImporter.Status.INVALID; refreshInClient=true) |
| IVK_Template_ConnectMatchingAttributes | Microflow | 25 | AggregateListAction (output=count, errorHandlingType=Rollback), change Column (MappingType=ExcelImporter.MappingType.Attribute, FindAttribute=$MxObjectMember/AttributeName, Column_MxObjectMember=$MxObjectMember; refreshInClient=true) |
| IVK_Template_NewFromFile | Microflow | 6 | change NewTemplateDocument (TemplateDocument_Template=$NewTemplate; refreshInClient=false), create ExcelImporter.Template as NewTemplate |
| IVK_TemplateDoc_Cancel | Microflow | 7 | close page, retrieve Template over association TemplateDocument_Template from TemplateDocument |
| MxObjectAttributeTypesEnumToString | Microflow | 4 | create variable AttributeTypeString=getKey($AttributeTypes) |
| MxObjectReferenceAssociationOwnerToString | Microflow | 5 | create variable AssociationOwnerString=getKey($AssociationOwnerEnum) |
| MxObjectReferenceReferenceTypeToString | Microflow | 5 | create variable ReferenceTypeString=getKey($ReferenceTypeEnum) |
| ParseEnumToString_StatisticLevel | Microflow | 5 | none |
| ParseStringToEnum_StatisticsLevel | Microflow | 11 | none |
| prepareReferenceHandling | Microflow | 22 | AggregateListAction (output=Count, errorHandlingType=Rollback), change vNewReferenceHandling (ReferenceHandling_Template=$pColumn/ExcelImporter.Column_Template, ReferenceHandling_MxObjectReference=$vMxReference; refreshInClient=true) |
| ReferenceHandlingEnumToString | Microflow | 5 | create variable ReferenceHandlingEnumString=getKey($ReferenceHandlingEnum) |
| SetColumnStatus | Microflow | 14 | change Column (Status=ExcelImporter.Status.INFO; refreshInClient=false), change Column (Status=ExcelImporter.Status.INVALID; refreshInClient=false) |
| SetupColumn | Microflow | 20 | change Column (Column_Microflows=$Microflows; refreshInClient=false), change ColumnList (type=Add, value=$NewColumn) |
| SetupTemplate | Microflow | 16 | change Template (SheetIndex=1, HeaderRowNumber=if $DataRowNr > 0 then $DataRowNr - 1 else 0, FirstDataRowNumber=$DataRowNr, ImportAction=$ImportActions, TemplateType=ExcelImporter.TemplateType.Normal, Template_MxObjectType=$MxObjectType, Template_MxObjectReference_ParentAssociation=$MxObjectReference; refreshInClient=false), commit AdditionalProperties (refreshInClient=false, withEvents=true) |
| SF_Template_CheckNrs | Microflow | 35 | change variable IsValid=false |
| StringToColumnMappingType | Microflow | 5 | create variable Enum=if $Input = 'Attribute' then ExcelImporter.MappingType.Attribute else if $Input = 'DoNotUse' then ExcelImporter.MappingType.DoNotUse else if $Input = 'Reference' then ExcelImporter.MappingType.Reference else empty |
| StringToColumnReferenceKeyType | Microflow | 5 | create variable Enum=if $Input = 'NoKey' then ExcelImporter.ReferenceKeyType.NoKey else if $Input = 'YesMainAndAssociatedObject' then ExcelImporter.ReferenceKeyType.YesMainAndAssociatedObject else if $Input = 'YesOnlyAssociatedObject' then E... |
| StringToColumnYesNo | Microflow | 5 | create variable Enum=if $Input = 'No' then ExcelImporter.YesNo.No else if $Input = 'Yes' then ExcelImporter.YesNo.Yes else empty |
| StringToDataSource | Microflow | 9 | none |
| StringToMxObjectAttributeTypesEnum | Microflow | 4 | create variable Enum=if $Input = 'AutoNumber' then MxModelReflection.PrimitiveTypes.AutoNumber else if $Input = 'BooleanType' then MxModelReflection.PrimitiveTypes.BooleanType else if $Input = 'DateTime' then MxModelReflection.PrimitiveTypes... |
| StringToMxObjectReferenceAssociationOwner | Microflow | 5 | create variable Enum=if $Input = 'Both' then MxModelReflection.AssociationOwner.Both else if $Input = '_Default' then MxModelReflection.AssociationOwner._Default else empty |
| StringToMxObjectReferenceReferenceType | Microflow | 5 | create variable Enum=if $Input = 'Reference' then MxModelReflection.ReferenceType.Reference else if $Input = 'ReferenceSet' then MxModelReflection.ReferenceType.ReferenceSet else empty |
| StringToReferenceDataHandling | Microflow | 5 | none |
| StringToReferenceHandlingEnum | Microflow | 5 | create variable Enum=if $Input = 'CreateEverything' then ExcelImporter.ReferenceHandlingEnum.CreateEverything else if $Input = 'FindCreate' then ExcelImporter.ReferenceHandlingEnum.FindCreate else if $Input = 'FindIgnore' then ExcelImporter.... |
| StringToTemplateImportActions | Microflow | 5 | create variable Enum=if $Input = 'CreateObjects' then ExcelImporter.ImportActions.CreateObjects else if $Input = 'OnlyCreateNewObjects' then ExcelImporter.ImportActions.OnlyCreateNewObjects else if $Input = 'SynchronizeObjects' then ExcelImp... |
| StringToTemplateRemoveIndicator | Microflow | 9 | none |
| StringToTemplateStatusEnum | Microflow | 5 | create variable StatusEnum=if $Input = 'INFO' then ExcelImporter.Status.INFO else if $Input = 'INVALID' then ExcelImporter.Status.INVALID else if $Input = 'VALID' then ExcelImporter.Status.VALID else empty |
| Sub_CreateColumnsFromTemplate | Microflow | 18 | AggregateListAction (output=NextColNumber, errorHandlingType=Rollback), change ColumnList (type=Add, value=$NewColumn) |
| TemplateImportActionsToString | Microflow | 5 | create variable ImportActionString=getKey($ImportActions) |
| TemplateReferenceDataHandlingEnumToString | Microflow | 3 | none |
| TemplateReferenceHandlingEnumToString | Microflow | 5 | create variable RefString=getKey($ReferenceHandling) |
| TemplateRemoveIndicatorToString | Microflow | 3 | none |
| TemplateStatusEnumToString | Microflow | 5 | create variable StatusEnumString=getKey($Input) |
| Validate_TemplateDocument | Microflow | 13 | create variable documentName=toLowerCase($TemplateDocument/Name), show message (text=The uploaded excel file should be of the type .xls,.xlsx or .xlsm, type=Warning, blocking=true) |
| ValidateColumn | Rule | 44 | AggregateListAction (output=NrOfMembers, errorHandlingType=Rollback), AggregateListAction (output=NrOfMembers_Ref, errorHandlingType=Rollback) |
| ValidateColumnMF | Rule | 23 | AggregateListAction (output=Count, errorHandlingType=Rollback), retrieve ValueType_Member over association MxObjectMember_Type from vMember |
| ValidateTemplate | Microflow | 73 | AggregateListAction (output=Count, errorHandlingType=Rollback), AggregateListAction (output=Count_1, errorHandlingType=Rollback) |

## Cross-Module Calls

| Flow | Calls | Target Module |
|---|---|---|
| ASu_CheckModelAndTemplates | ASu_CheckMetamodel | MxModelReflection |
| Ch_FindAttribute | FindMember | MxModelReflection |
| Ch_FindAttribute_Reference | FindMember | MxModelReflection |
| Ch_FindMicroflow | FindMicroflow | MxModelReflection |
| Ch_FindObjectType_Reference | FindObjectType | MxModelReflection |
| Ch_FindReference | FindReference | MxModelReflection |
| SetupColumn | FindMember, FindMicroflow | MxModelReflection |
| SetupTemplate | FindObjectType, FindReference | MxModelReflection |

## Flow Details

| Flow | Kind | Nodes | Calls Out | Called By |
|---|---|---:|---:|---:|
| _DocumentationDummyXSD | Microflow | 3 | 0 | 0 |
| _DocumentationExportParseFlows | Microflow | 3 | 0 | 0 |
| _DocumentationImportParseFlows | Microflow | 3 | 0 | 0 |
| ACr_Template | Microflow | 6 | 0 | 0 |
| ASu_CheckModelAndTemplates | Microflow | 8 | 2 | 0 |
| BCo_Column | Microflow | 44 | 2 | 1 |
| BDe_Column | Microflow | 6 | 1 | 0 |
| Ch_Column_SetDefaultObject | Microflow | 10 | 2 | 0 |
| Ch_FindAttribute | Microflow | 13 | 1 | 0 |
| Ch_FindAttribute_Reference | Microflow | 16 | 1 | 0 |
| Ch_FindMicroflow | Microflow | 12 | 1 | 0 |
| Ch_FindObjectType_Reference | Microflow | 13 | 1 | 0 |
| Ch_FindReference | Microflow | 14 | 2 | 0 |
| Ch_SetAttribute | Microflow | 8 | 0 | 0 |
| Ch_SetAttribute_Reference | Microflow | 8 | 0 | 0 |
| Ch_SetMicroflow | Microflow | 8 | 0 | 0 |
| Ch_SetObjectType_Reference | Microflow | 8 | 0 | 1 |
| Ch_SetReference | Microflow | 8 | 0 | 1 |
| Ch_Template_ChangeObjectType | Microflow | 8 | 1 | 0 |
| Ch_Template_CheckNrs | Microflow | 4 | 1 | 0 |
| CleanupOldRefHandling | Microflow | 8 | 0 | 1 |
| Column_SetCorrectRefObjectType | Microflow | 21 | 1 | 2 |
| Column_SetDetails | Microflow | 13 | 0 | 1 |
| ColumnDataSourceToString | Microflow | 3 | 0 | 0 |
| ColumnMappingTypeToString | Microflow | 5 | 0 | 0 |
| ColumnReferenceKeyTypeToString | Microflow | 5 | 0 | 0 |
| ColumnYesNoToString | Microflow | 5 | 0 | 0 |
| Example_SetupImportTemplate | Microflow | 12 | 2 | 0 |
| ExcelImporterTemplateXSD | Microflow | 4 | 0 | 0 |
| ExcelTemplate_ExportToXML | Microflow | 12 | 0 | 0 |
| ExcelTemplate_ImportFromXml | Microflow | 12 | 0 | 0 |
| FormatInteger | Microflow | 5 | 0 | 0 |
| GetAddProperties | Microflow | 9 | 0 | 3 |
| GetCorrectString | Microflow | 7 | 0 | 1 |
| IVK_CancelTemplate | Microflow | 12 | 1 | 0 |
| IVK_Column_Save | Microflow | 55 | 1 | 0 |
| IVK_ColumnEdit | Microflow | 8 | 0 | 0 |
| IVK_ColumnNew | Microflow | 9 | 0 | 0 |
| IVK_DuplicateTemplate | Microflow | 17 | 0 | 0 |
| IVK_ImportTemplateDocument | Microflow | 14 | 0 | 0 |
| IVK_ImportXML_Upload | Microflow | 6 | 0 | 0 |
| IVK_SaveContinue_CreateTemplateFromDoc | Microflow | 16 | 2 | 0 |
| IVK_SaveNewTemplate | Microflow | 7 | 1 | 1 |
| IVK_SaveNewTemplate_CreateColumns | Microflow | 5 | 2 | 0 |
| IVK_SaveTemplate | Microflow | 12 | 3 | 1 |
| IVK_Template_ConnectMatchingAttributes | Microflow | 25 | 0 | 0 |
| IVK_Template_NewFromFile | Microflow | 6 | 0 | 0 |
| IVK_TemplateDoc_Cancel | Microflow | 7 | 0 | 0 |
| MxObjectAttributeTypesEnumToString | Microflow | 4 | 0 | 0 |
| MxObjectReferenceAssociationOwnerToString | Microflow | 5 | 0 | 0 |
| MxObjectReferenceReferenceTypeToString | Microflow | 5 | 0 | 0 |
| ParseEnumToString_StatisticLevel | Microflow | 5 | 0 | 0 |
| ParseStringToEnum_StatisticsLevel | Microflow | 11 | 0 | 0 |
| prepareReferenceHandling | Microflow | 22 | 0 | 2 |
| ReferenceHandlingEnumToString | Microflow | 5 | 0 | 0 |
| SetColumnStatus | Microflow | 14 | 0 | 2 |
| SetupColumn | Microflow | 20 | 2 | 1 |
| SetupTemplate | Microflow | 16 | 3 | 1 |
| SF_Template_CheckNrs | Microflow | 35 | 0 | 3 |
| StringToColumnMappingType | Microflow | 5 | 0 | 0 |
| StringToColumnReferenceKeyType | Microflow | 5 | 0 | 0 |
| StringToColumnYesNo | Microflow | 5 | 0 | 0 |
| StringToDataSource | Microflow | 9 | 0 | 0 |
| StringToMxObjectAttributeTypesEnum | Microflow | 4 | 0 | 0 |
| StringToMxObjectReferenceAssociationOwner | Microflow | 5 | 0 | 0 |
| StringToMxObjectReferenceReferenceType | Microflow | 5 | 0 | 0 |
| StringToReferenceDataHandling | Microflow | 5 | 0 | 0 |
| StringToReferenceHandlingEnum | Microflow | 5 | 0 | 0 |
| StringToTemplateImportActions | Microflow | 5 | 0 | 0 |
| StringToTemplateRemoveIndicator | Microflow | 9 | 0 | 0 |
| StringToTemplateStatusEnum | Microflow | 5 | 0 | 0 |
| Sub_CreateColumnsFromTemplate | Microflow | 18 | 0 | 1 |
| TemplateImportActionsToString | Microflow | 5 | 0 | 0 |
| TemplateReferenceDataHandlingEnumToString | Microflow | 3 | 0 | 0 |
| TemplateReferenceHandlingEnumToString | Microflow | 5 | 0 | 0 |
| TemplateRemoveIndicatorToString | Microflow | 3 | 0 | 0 |
| TemplateStatusEnumToString | Microflow | 5 | 0 | 0 |
| Validate_TemplateDocument | Microflow | 13 | 0 | 1 |
| ValidateColumn | Rule | 44 | 0 | 0 |
| ValidateColumnMF | Rule | 23 | 0 | 0 |
| ValidateTemplate | Microflow | 73 | 4 | 3 |

