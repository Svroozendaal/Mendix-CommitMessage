# Pages: ExcelImporter

## Page Inventory

| Page | Title | Allowed roles | Parameters | Popup |
|---|---|---|---|---|
| ExcelImporter.Column_Hover | New column mapping selection | none | Column:ExcelImporter.Column | True |
| ExcelImporter.Column_NewEdit | Column details | none | Column:ExcelImporter.Column | True |
| ExcelImporter.Credits | ExcelImporterTemplateExporter module | none | none | False |
| ExcelImporter.ExcelImportOverview | Excel Importer | ExcelImporter.Configurator | none | False |
| ExcelImporter.Import_Overview | Import files overview | ExcelImporter.Configurator | none | False |
| ExcelImporter.ImportXML_Upload | Upload template | ExcelImporter.Configurator | XMLDocumentTemplate:ExcelImporter.XMLDocumentTemplate | True |
| ExcelImporter.ReferenceHandling_NewEdit | Edit the reference handling | ExcelImporter.Configurator | ReferenceHandling:ExcelImporter.ReferenceHandling | True |
| ExcelImporter.Template_Edit | Import template settings | ExcelImporter.Configurator | Template:ExcelImporter.Template | False |
| ExcelImporter.Template_New | Import template settings | ExcelImporter.Configurator | Template:ExcelImporter.Template | True |
| ExcelImporter.Template_New_FromDocument | New template by Excel file | ExcelImporter.Configurator | TemplateDocument:ExcelImporter.TemplateDocument | True |
| ExcelImporter.TemplateDocument_NewEdit | Edit Template document | ExcelImporter.Configurator | TemplateDocument:ExcelImporter.TemplateDocument | True |
| ExcelImporter.Templates_Overview | Import templates | ExcelImporter.Configurator | none | False |
| ExcelImporter.VerificationForm | Verification form | ExcelImporter.Configurator | none | False |

## Page-Flow Links

| Page | Shown by flows |
|---|---|
| ExcelImporter.Column_Hover | none (no show-page evidence) |
| ExcelImporter.Column_NewEdit | ExcelImporter.IVK_ColumnEdit, ExcelImporter.IVK_ColumnNew |
| ExcelImporter.Credits | none (no show-page evidence) |
| ExcelImporter.ExcelImportOverview | none (no show-page evidence) |
| ExcelImporter.Import_Overview | none (no show-page evidence) |
| ExcelImporter.ImportXML_Upload | ExcelImporter.IVK_ImportXML_Upload |
| ExcelImporter.ReferenceHandling_NewEdit | none (no show-page evidence) |
| ExcelImporter.Template_Edit | ExcelImporter.IVK_SaveContinue_CreateTemplateFromDoc, ExcelImporter.IVK_SaveNewTemplate |
| ExcelImporter.Template_New | none (no show-page evidence) |
| ExcelImporter.Template_New_FromDocument | ExcelImporter.IVK_Template_NewFromFile |
| ExcelImporter.TemplateDocument_NewEdit | none (no show-page evidence) |
| ExcelImporter.Templates_Overview | none (no show-page evidence) |
| ExcelImporter.VerificationForm | none (no show-page evidence) |

## Journey Fragments

| User intent group | Pages |
|---|---|
| Column | ExcelImporter.Column_Hover, ExcelImporter.Column_NewEdit |
| General | ExcelImporter.Credits, ExcelImporter.ExcelImportOverview, ExcelImporter.VerificationForm |
| Import | ExcelImporter.Import_Overview |
| ImportXML | ExcelImporter.ImportXML_Upload |
| ReferenceHandling | ExcelImporter.ReferenceHandling_NewEdit |
| Template | ExcelImporter.Template_Edit, ExcelImporter.Template_New, ExcelImporter.Template_New_FromDocument |
| TemplateDocument | ExcelImporter.TemplateDocument_NewEdit |
| Templates | ExcelImporter.Templates_Overview |

## Snippets

Snippet-level page widget behaviour is not exported in current overview contract.
