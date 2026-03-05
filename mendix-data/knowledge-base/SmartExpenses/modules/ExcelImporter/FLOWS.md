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
| ACr_Template | Microflow | 6 | ExcelImporter.AdditionalProperties |
| ASu_CheckModelAndTemplates | Microflow | 8 | ExcelImporter.Template |
| BCo_Column | Microflow | 44 | ExcelImporter.Column |
| BDe_Column | Microflow | 6 | none |
| Ch_Column_SetDefaultObject | Microflow | 10 | none |
| Ch_FindAttribute | Microflow | 13 | none |
| Ch_FindAttribute_Reference | Microflow | 16 | none |
| Ch_FindMicroflow | Microflow | 12 | none |
| Ch_FindObjectType_Reference | Microflow | 13 | none |
| Ch_FindReference | Microflow | 14 | none |
| Ch_SetAttribute | Microflow | 8 | none |
| Ch_SetAttribute_Reference | Microflow | 8 | none |
| Ch_SetMicroflow | Microflow | 8 | none |
| Ch_SetObjectType_Reference | Microflow | 8 | none |
| Ch_SetReference | Microflow | 8 | none |
| Ch_Template_ChangeObjectType | Microflow | 8 | ExcelImporter.Column |
| Ch_Template_CheckNrs | Microflow | 4 | none |
| CleanupOldRefHandling | Microflow | 8 | none |
| Column_SetCorrectRefObjectType | Microflow | 21 | MxModelReflection.MxObjectType |
| Column_SetDetails | Microflow | 13 | none |
| ColumnDataSourceToString | Microflow | 3 | none |
| ColumnMappingTypeToString | Microflow | 5 | none |
| ColumnReferenceKeyTypeToString | Microflow | 5 | none |
| ColumnYesNoToString | Microflow | 5 | none |
| Example_SetupImportTemplate | Microflow | 12 | none |
| ExcelImporterTemplateXSD | Microflow | 4 | ExcelImporter.Template |
| ExcelTemplate_ExportToXML | Microflow | 12 | ExcelImporter.XMLDocumentTemplate |
| ExcelTemplate_ImportFromXml | Microflow | 12 | ExcelImporter.Log |
| FormatInteger | Microflow | 5 | none |
| GetAddProperties | Microflow | 9 | ExcelImporter.AdditionalProperties |
| GetCorrectString | Microflow | 7 | none |
| IVK_CancelTemplate | Microflow | 12 | none |
| IVK_Column_Save | Microflow | 55 | none |
| IVK_ColumnEdit | Microflow | 8 | none |
| IVK_ColumnNew | Microflow | 9 | ExcelImporter.Column |
| IVK_DuplicateTemplate | Microflow | 17 | ExcelImporter.Column, ExcelImporter.ReferenceHandling, ExcelImporter.Template |
| IVK_ImportTemplateDocument | Microflow | 14 | none |
| IVK_ImportXML_Upload | Microflow | 6 | ExcelImporter.XMLDocumentTemplate |
| IVK_SaveContinue_CreateTemplateFromDoc | Microflow | 16 | none |
| IVK_SaveNewTemplate | Microflow | 7 | none |
| IVK_SaveNewTemplate_CreateColumns | Microflow | 5 | none |
| IVK_SaveTemplate | Microflow | 12 | none |
| IVK_Template_ConnectMatchingAttributes | Microflow | 25 | ExcelImporter.Column, MxModelReflection.MxObjectMember |
| IVK_Template_NewFromFile | Microflow | 6 | ExcelImporter.Template, ExcelImporter.TemplateDocument |
| IVK_TemplateDoc_Cancel | Microflow | 7 | none |
| MxObjectAttributeTypesEnumToString | Microflow | 4 | none |
| MxObjectReferenceAssociationOwnerToString | Microflow | 5 | none |
| MxObjectReferenceReferenceTypeToString | Microflow | 5 | none |
| ParseEnumToString_StatisticLevel | Microflow | 5 | none |
| ParseStringToEnum_StatisticsLevel | Microflow | 11 | none |
| prepareReferenceHandling | Microflow | 22 | ExcelImporter.Column, ExcelImporter.ReferenceHandling |
| ReferenceHandlingEnumToString | Microflow | 5 | none |
| SetColumnStatus | Microflow | 14 | none |
| SetupColumn | Microflow | 20 | ExcelImporter.Column |
| SetupTemplate | Microflow | 16 | ExcelImporter.Template |
| SF_Template_CheckNrs | Microflow | 35 | none |
| StringToColumnMappingType | Microflow | 5 | none |
| StringToColumnReferenceKeyType | Microflow | 5 | none |
| StringToColumnYesNo | Microflow | 5 | none |
| StringToDataSource | Microflow | 9 | none |
| StringToMxObjectAttributeTypesEnum | Microflow | 4 | none |
| StringToMxObjectReferenceAssociationOwner | Microflow | 5 | none |
| StringToMxObjectReferenceReferenceType | Microflow | 5 | none |
| StringToReferenceDataHandling | Microflow | 5 | none |
| StringToReferenceHandlingEnum | Microflow | 5 | none |
| StringToTemplateImportActions | Microflow | 5 | none |
| StringToTemplateRemoveIndicator | Microflow | 9 | none |
| StringToTemplateStatusEnum | Microflow | 5 | none |
| Sub_CreateColumnsFromTemplate | Microflow | 18 | ExcelImporter.Column, MxModelReflection.MxObjectMember |
| TemplateImportActionsToString | Microflow | 5 | none |
| TemplateReferenceDataHandlingEnumToString | Microflow | 3 | none |
| TemplateReferenceHandlingEnumToString | Microflow | 5 | none |
| TemplateRemoveIndicatorToString | Microflow | 3 | none |
| TemplateStatusEnumToString | Microflow | 5 | none |
| Validate_TemplateDocument | Microflow | 13 | none |
| ValidateColumn | Rule | 44 | MxModelReflection.MxObjectMember, MxModelReflection.MxObjectReference, MxModelReflection.MxObjectType |
| ValidateColumnMF | Rule | 23 | none |
| ValidateTemplate | Microflow | 73 | ExcelImporter.Column, ExcelImporter.ReferenceHandling |

## Cross-Module Calls

| Flow | Calls | Target Module |
|---|---|---|
| ASu_CheckModelAndTemplates | MxModelReflection.ASu_CheckMetamodel | MxModelReflection |
| Ch_FindAttribute | MxModelReflection.FindMember | MxModelReflection |
| Ch_FindAttribute_Reference | MxModelReflection.FindMember | MxModelReflection |
| Ch_FindMicroflow | MxModelReflection.FindMicroflow | MxModelReflection |
| Ch_FindObjectType_Reference | MxModelReflection.FindObjectType | MxModelReflection |
| Ch_FindReference | MxModelReflection.FindReference | MxModelReflection |
| SetupColumn | MxModelReflection.FindMember | MxModelReflection |
| SetupColumn | MxModelReflection.FindMicroflow | MxModelReflection |
| SetupTemplate | MxModelReflection.FindObjectType | MxModelReflection |
| SetupTemplate | MxModelReflection.FindReference | MxModelReflection |

## Flow Details

| Flow | Kind | Nodes | Tier | Calls Out | Called By |
|---|---|---:|---:|---:|---:|
| _DocumentationDummyXSD | Microflow | 3 | 3 | 0 | 0 |
| _DocumentationExportParseFlows | Microflow | 3 | 3 | 0 | 0 |
| _DocumentationImportParseFlows | Microflow | 3 | 3 | 0 | 0 |
| ACr_Template | Microflow | 6 | 3 | 0 | 0 |
| ASu_CheckModelAndTemplates | Microflow | 8 | 3 | 2 | 0 |
| BCo_Column | Microflow | 44 | 3 | 2 | 1 |
| BDe_Column | Microflow | 6 | 3 | 1 | 0 |
| Ch_Column_SetDefaultObject | Microflow | 10 | 3 | 2 | 0 |
| Ch_FindAttribute | Microflow | 13 | 3 | 1 | 0 |
| Ch_FindAttribute_Reference | Microflow | 16 | 3 | 1 | 0 |
| Ch_FindMicroflow | Microflow | 12 | 3 | 1 | 0 |
| Ch_FindObjectType_Reference | Microflow | 13 | 3 | 1 | 0 |
| Ch_FindReference | Microflow | 14 | 3 | 2 | 0 |
| Ch_SetAttribute | Microflow | 8 | 3 | 0 | 0 |
| Ch_SetAttribute_Reference | Microflow | 8 | 3 | 0 | 0 |
| Ch_SetMicroflow | Microflow | 8 | 3 | 0 | 0 |
| Ch_SetObjectType_Reference | Microflow | 8 | 3 | 0 | 1 |
| Ch_SetReference | Microflow | 8 | 3 | 0 | 1 |
| Ch_Template_ChangeObjectType | Microflow | 8 | 3 | 1 | 0 |
| Ch_Template_CheckNrs | Microflow | 4 | 3 | 1 | 0 |
| CleanupOldRefHandling | Microflow | 8 | 3 | 0 | 1 |
| Column_SetCorrectRefObjectType | Microflow | 21 | 3 | 1 | 2 |
| Column_SetDetails | Microflow | 13 | 3 | 0 | 1 |
| ColumnDataSourceToString | Microflow | 3 | 3 | 0 | 0 |
| ColumnMappingTypeToString | Microflow | 5 | 3 | 0 | 0 |
| ColumnReferenceKeyTypeToString | Microflow | 5 | 3 | 0 | 0 |
| ColumnYesNoToString | Microflow | 5 | 3 | 0 | 0 |
| Example_SetupImportTemplate | Microflow | 12 | 3 | 2 | 0 |
| ExcelImporterTemplateXSD | Microflow | 4 | 3 | 0 | 0 |
| ExcelTemplate_ExportToXML | Microflow | 12 | 3 | 0 | 0 |
| ExcelTemplate_ImportFromXml | Microflow | 12 | 3 | 0 | 0 |
| FormatInteger | Microflow | 5 | 3 | 0 | 0 |
| GetAddProperties | Microflow | 9 | 3 | 0 | 3 |
| GetCorrectString | Microflow | 7 | 3 | 0 | 1 |
| IVK_CancelTemplate | Microflow | 12 | 3 | 1 | 0 |
| IVK_Column_Save | Microflow | 55 | 3 | 1 | 0 |
| IVK_ColumnEdit | Microflow | 8 | 3 | 0 | 0 |
| IVK_ColumnNew | Microflow | 9 | 3 | 0 | 0 |
| IVK_DuplicateTemplate | Microflow | 17 | 3 | 0 | 0 |
| IVK_ImportTemplateDocument | Microflow | 14 | 3 | 0 | 0 |
| IVK_ImportXML_Upload | Microflow | 6 | 3 | 0 | 0 |
| IVK_SaveContinue_CreateTemplateFromDoc | Microflow | 16 | 3 | 2 | 0 |
| IVK_SaveNewTemplate | Microflow | 7 | 3 | 1 | 1 |
| IVK_SaveNewTemplate_CreateColumns | Microflow | 5 | 3 | 2 | 0 |
| IVK_SaveTemplate | Microflow | 12 | 3 | 3 | 1 |
| IVK_Template_ConnectMatchingAttributes | Microflow | 25 | 3 | 0 | 0 |
| IVK_Template_NewFromFile | Microflow | 6 | 3 | 0 | 0 |
| IVK_TemplateDoc_Cancel | Microflow | 7 | 3 | 0 | 0 |
| MxObjectAttributeTypesEnumToString | Microflow | 4 | 3 | 0 | 0 |
| MxObjectReferenceAssociationOwnerToString | Microflow | 5 | 3 | 0 | 0 |
| MxObjectReferenceReferenceTypeToString | Microflow | 5 | 3 | 0 | 0 |
| ParseEnumToString_StatisticLevel | Microflow | 5 | 3 | 0 | 0 |
| ParseStringToEnum_StatisticsLevel | Microflow | 11 | 3 | 0 | 0 |
| prepareReferenceHandling | Microflow | 22 | 3 | 0 | 2 |
| ReferenceHandlingEnumToString | Microflow | 5 | 3 | 0 | 0 |
| SetColumnStatus | Microflow | 14 | 3 | 0 | 2 |
| SetupColumn | Microflow | 20 | 3 | 2 | 1 |
| SetupTemplate | Microflow | 16 | 3 | 3 | 1 |
| SF_Template_CheckNrs | Microflow | 35 | 3 | 0 | 3 |
| StringToColumnMappingType | Microflow | 5 | 3 | 0 | 0 |
| StringToColumnReferenceKeyType | Microflow | 5 | 3 | 0 | 0 |
| StringToColumnYesNo | Microflow | 5 | 3 | 0 | 0 |
| StringToDataSource | Microflow | 9 | 3 | 0 | 0 |
| StringToMxObjectAttributeTypesEnum | Microflow | 4 | 3 | 0 | 0 |
| StringToMxObjectReferenceAssociationOwner | Microflow | 5 | 3 | 0 | 0 |
| StringToMxObjectReferenceReferenceType | Microflow | 5 | 3 | 0 | 0 |
| StringToReferenceDataHandling | Microflow | 5 | 3 | 0 | 0 |
| StringToReferenceHandlingEnum | Microflow | 5 | 3 | 0 | 0 |
| StringToTemplateImportActions | Microflow | 5 | 3 | 0 | 0 |
| StringToTemplateRemoveIndicator | Microflow | 9 | 3 | 0 | 0 |
| StringToTemplateStatusEnum | Microflow | 5 | 3 | 0 | 0 |
| Sub_CreateColumnsFromTemplate | Microflow | 18 | 3 | 0 | 1 |
| TemplateImportActionsToString | Microflow | 5 | 3 | 0 | 0 |
| TemplateReferenceDataHandlingEnumToString | Microflow | 3 | 3 | 0 | 0 |
| TemplateReferenceHandlingEnumToString | Microflow | 5 | 3 | 0 | 0 |
| TemplateRemoveIndicatorToString | Microflow | 3 | 3 | 0 | 0 |
| TemplateStatusEnumToString | Microflow | 5 | 3 | 0 | 0 |
| Validate_TemplateDocument | Microflow | 13 | 3 | 0 | 1 |
| ValidateColumn | Rule | 44 | 3 | 0 | 0 |
| ValidateColumnMF | Rule | 23 | 3 | 0 | 0 |
| ValidateTemplate | Microflow | 73 | 3 | 4 | 3 |

## Tier 1 Deep Narratives

No Tier 1 narrative required for this module category.
