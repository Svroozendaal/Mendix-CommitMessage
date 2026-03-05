# Domain Model: ExcelImporter

## Entities

| Entity | Persistable | Attributes | Access Rules |
|---|---|---:|---:|
| ExcelImporter.AdditionalProperties | True | 6 | 2 |
| ExcelImporter.Column | True | 15 | 2 |
| ExcelImporter.Log | True |  |  |
| ExcelImporter.ReferenceHandling | True | 5 | 2 |
| ExcelImporter.Template | True | 9 | 2 |
| ExcelImporter.TemplateDocument | True | 0 | 2 |
| ExcelImporter.XMLDocumentTemplate | True | 0 |  |

## Associations

| Association | Parent | Child | Type |
|---|---|---|---|
| ExcelImporter.Column_MasterColumn | ExcelImporter.Column | ExcelImporter.Column | Reference (*-1) |
| ExcelImporter.Column_Template | ExcelImporter.Column | ExcelImporter.Template | Reference (*-1) |
| ExcelImporter.Log_XMLDocumentTemplate | ExcelImporter.Log | ExcelImporter.XMLDocumentTemplate | Reference (*-1) |
| ExcelImporter.ReferenceHandling_Template | ExcelImporter.ReferenceHandling | ExcelImporter.Template | Reference (*-1) |
| ExcelImporter.Template_AdditionalProperties | ExcelImporter.Template | ExcelImporter.AdditionalProperties | Reference (1-1) |
| ExcelImporter.Template_MasterTemplate | ExcelImporter.Template | ExcelImporter.Template | Reference (*-1) |
| ExcelImporter.TemplateDocument_Template | ExcelImporter.TemplateDocument | ExcelImporter.Template | Reference (*-1) |
| ExcelImporter.XMLDocumentTemplate_Template | ExcelImporter.XMLDocumentTemplate | ExcelImporter.Template | Reference (*-1) |

## Enumerations

| Enumeration | Values |
|---|---|
| ExcelImporter.DataSource | CellValue, DocumentPropertyRowNr, DocumentPropertySheetNr, StaticValue |
| ExcelImporter.ImportActions | CreateObjects, OnlyCreateNewObjects, SynchronizeObjects, SynchronizeOnlyExisting |
| ExcelImporter.MappingType | Attribute, DoNotUse, Reference |
| ExcelImporter.ReferenceDataHandling | Append, Overwrite |
| ExcelImporter.ReferenceHandlingEnum | CreateEverything, FindCreate, FindIgnore, OnlyCreateNewObjects |
| ExcelImporter.ReferenceKeyType | NoKey, YesMainAndAssociatedObject, YesOnlyAssociatedObject, YesOnlyMainObject |
| ExcelImporter.RemoveIndicator | Nothing, RemoveUnchangedObjects, TrackChanges |
| ExcelImporter.StatisticsLevel | AllStatistics, NoStatistics, OnlyFinalStatistics |
| ExcelImporter.Status | INFO, INVALID, VALID |
| ExcelImporter.TemplateType | Normal, Wizard |
| ExcelImporter.ValidationResult | InvalidAttribute, InvalidAutoNumberSelection, InvalidReference, InvalidReferencedObject, NoAssociationKeys, NoAttributeSelected, NoReferencedObjectSelected, NoReferenceSelected, UnUsed, ValidAttribute +1 more |
| ExcelImporter.ValidationResult2 | NoInputParams, Valid, WrongNrOfInputParams, WrongReturnType |
| ExcelImporter.YesNo | No, Yes |

