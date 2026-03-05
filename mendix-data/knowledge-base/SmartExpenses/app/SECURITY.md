# Security

## Project Security

- Security level: 
CheckEverything
 (Export-backed)
- Guest access enabled: 
True
 (Export-backed)
- Guest user role: 
Anonymous

## User Roles

| App Role | Module Roles | Manage All Roles | Check Security |
|---|---|---|---|
| Administrator | Administration.Administrator, ExcelImporter.Configurator, ImporterHelper.ExcelImporter, ImporterHelper.RESTImporter, MxModelReflection.ModelAdministrator, SmartExpenses.Admin, SmartExpenses.User, System.Administrator | True | True |
| FBG | Administration.User, ImporterHelper.ExcelImporter, ImporterHelper.RESTImporter, SmartExpenses.User, System.User | False | True |
| Anonymous | Administration.User, Atlas_Web_Content.Anonymous, System.User | False | True |
| Parent | Administration.User, SmartExpenses.Parent, System.User | False | True |

## Notes

- Export-backed: Role to module-role mappings are listed above.
- Inferred: User roles represent distinct operating personas.
- Unknown: Runtime policy outcomes per session are not exported.

