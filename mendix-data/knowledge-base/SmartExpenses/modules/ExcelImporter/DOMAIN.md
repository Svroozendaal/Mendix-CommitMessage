# Domain: ExcelImporter

## Entities

| Entity | Persistable | Attribute count | Access rule count |
|---|---|---:|---:|
| ExcelImporter.AdditionalProperties | True | 6 | 2 |
| ExcelImporter.Column | True | 15 | 2 |
| ExcelImporter.Log | True | 1 | 1 |
| ExcelImporter.ReferenceHandling | True | 5 | 2 |
| ExcelImporter.Template | True | 9 | 2 |
| ExcelImporter.TemplateDocument | True | 0 | 2 |
| ExcelImporter.XMLDocumentTemplate | True | 0 | 1 |

Confidence: Export-backed

## Entity Lifecycle Matrix

| Entity | Create flows | Update flows | Delete flows | Read flows |
|---|---|---|---|---|
| ExcelImporter.AdditionalProperties | ExcelImporter.ACr_Template, ExcelImporter.GetAddProperties | ExcelImporter.ACr_Template, ExcelImporter.GetAddProperties | none | ExcelImporter.GetAddProperties |
| ExcelImporter.Column | ExcelImporter.IVK_ColumnNew, ExcelImporter.IVK_DuplicateTemplate, ExcelImporter.prepareReferenceHandling, ExcelImporter.SetupColumn, ExcelImporter.Sub_CreateColumnsFromTemplate | ExcelImporter.BCo_Column, ExcelImporter.Ch_Template_ChangeObjectType, ExcelImporter.IVK_ColumnNew, ExcelImporter.IVK_DuplicateTemplate, ExcelImporter.IVK_Template_ConnectMatchingAttributes, ExcelImporter.prepareReferenceHandling, ExcelImporter.SetupColumn, ExcelImporter.Sub_CreateColumnsFromTemplate, ExcelImporter.ValidateTemplate | ExcelImporter.prepareReferenceHandling | ExcelImporter.BCo_Column, ExcelImporter.Ch_Template_ChangeObjectType, ExcelImporter.IVK_DuplicateTemplate, ExcelImporter.IVK_Template_ConnectMatchingAttributes, ExcelImporter.prepareReferenceHandling, ExcelImporter.SetupColumn, ExcelImporter.Sub_CreateColumnsFromTemplate, ExcelImporter.ValidateTemplate |
| ExcelImporter.Log | ExcelImporter.ExcelTemplate_ImportFromXml | ExcelImporter.ExcelTemplate_ImportFromXml | none | none |
| ExcelImporter.ReferenceHandling | ExcelImporter.IVK_DuplicateTemplate, ExcelImporter.prepareReferenceHandling | ExcelImporter.IVK_DuplicateTemplate, ExcelImporter.prepareReferenceHandling, ExcelImporter.ValidateTemplate | ExcelImporter.prepareReferenceHandling | ExcelImporter.IVK_DuplicateTemplate, ExcelImporter.prepareReferenceHandling, ExcelImporter.ValidateTemplate |
| ExcelImporter.Template | ExcelImporter.IVK_DuplicateTemplate, ExcelImporter.IVK_Template_NewFromFile, ExcelImporter.SetupTemplate | ExcelImporter.ASu_CheckModelAndTemplates, ExcelImporter.IVK_DuplicateTemplate, ExcelImporter.IVK_Template_NewFromFile, ExcelImporter.SetupTemplate | none | ExcelImporter.ASu_CheckModelAndTemplates, ExcelImporter.ExcelImporterTemplateXSD, ExcelImporter.IVK_DuplicateTemplate, ExcelImporter.SetupTemplate, ImporterHelper.SUB_ImportTemplateDocument |
| ExcelImporter.TemplateDocument | ExcelImporter.IVK_Template_NewFromFile | ExcelImporter.IVK_Template_NewFromFile | none | none |
| ExcelImporter.XMLDocumentTemplate | ExcelImporter.ExcelTemplate_ExportToXML, ExcelImporter.IVK_ImportXML_Upload | ExcelImporter.ExcelTemplate_ExportToXML, ExcelImporter.IVK_ImportXML_Upload | none | none |

Confidence: Inferred

## Role impacts per sensitive entity

| Entity | Module roles | Default member rights | XPath constraint |
|---|---|---|---|
| ExcelImporter.AdditionalProperties | ExcelImporter.Configurator | ReadWrite | none |
| ExcelImporter.AdditionalProperties | ExcelImporter.Readonly | None | none |
| ExcelImporter.Column | ExcelImporter.Readonly | ReadOnly | none |
| ExcelImporter.Column | ExcelImporter.Configurator | ReadWrite | none |
| ExcelImporter.Log | ExcelImporter.Configurator | ReadWrite | none |
| ExcelImporter.ReferenceHandling | ExcelImporter.Configurator | ReadWrite | none |
| ExcelImporter.ReferenceHandling | ExcelImporter.Readonly | ReadOnly | none |
| ExcelImporter.Template | ExcelImporter.Readonly | None | none |
| ExcelImporter.Template | ExcelImporter.Configurator | ReadWrite | none |
| ExcelImporter.TemplateDocument | ExcelImporter.Configurator | None | none |
| ExcelImporter.TemplateDocument | ExcelImporter.Readonly | None | none |
| ExcelImporter.XMLDocumentTemplate | ExcelImporter.Configurator | ReadWrite | none |

Confidence: Export-backed

## Associations

| Association | Parent | Child | Cardinality | Type | Owner |
|---|---|---|---|---|---|
| ExcelImporter.Column_MasterColumn | ExcelImporter.Column | ExcelImporter.Column | *-1 | Reference | Default |
| ExcelImporter.Column_Template | ExcelImporter.Column | ExcelImporter.Template | *-1 | Reference | Default |
| ExcelImporter.Log_XMLDocumentTemplate | ExcelImporter.Log | ExcelImporter.XMLDocumentTemplate | *-1 | Reference | Default |
| ExcelImporter.ReferenceHandling_Template | ExcelImporter.ReferenceHandling | ExcelImporter.Template | *-1 | Reference | Default |
| ExcelImporter.TemplateDocument_Template | ExcelImporter.TemplateDocument | ExcelImporter.Template | *-1 | Reference | Default |
| ExcelImporter.Template_AdditionalProperties | ExcelImporter.Template | ExcelImporter.AdditionalProperties | 1-1 | Reference | Both |
| ExcelImporter.Template_MasterTemplate | ExcelImporter.Template | ExcelImporter.Template | *-1 | Reference | Default |
| ExcelImporter.XMLDocumentTemplate_Template | ExcelImporter.XMLDocumentTemplate | ExcelImporter.Template | *-1 | Reference | Default |

## Enumerations

| Enumeration | Value count | Sample values |
|---|---:|---|
| ExcelImporter.DataSource | 4 | CellValue, DocumentPropertyRowNr, DocumentPropertySheetNr, StaticValue |
| ExcelImporter.ImportActions | 4 | CreateObjects, OnlyCreateNewObjects, SynchronizeObjects, SynchronizeOnlyExisting |
| ExcelImporter.MappingType | 3 | Attribute, DoNotUse, Reference |
| ExcelImporter.ReferenceDataHandling | 2 | Append, Overwrite |
| ExcelImporter.ReferenceHandlingEnum | 4 | CreateEverything, FindCreate, FindIgnore, OnlyCreateNewObjects |
| ExcelImporter.ReferenceKeyType | 4 | NoKey, YesMainAndAssociatedObject, YesOnlyAssociatedObject, YesOnlyMainObject |
| ExcelImporter.RemoveIndicator | 3 | Nothing, RemoveUnchangedObjects, TrackChanges |
| ExcelImporter.StatisticsLevel | 3 | AllStatistics, NoStatistics, OnlyFinalStatistics |
| ExcelImporter.Status | 3 | INFO, INVALID, VALID |
| ExcelImporter.TemplateType | 2 | Normal, Wizard |
| ExcelImporter.ValidationResult | 11 | InvalidAttribute, InvalidAutoNumberSelection, InvalidReference, InvalidReferencedObject |
| ExcelImporter.ValidationResult2 | 4 | NoInputParams, Valid, WrongNrOfInputParams, WrongReturnType |
| ExcelImporter.YesNo | 2 | No, Yes |
