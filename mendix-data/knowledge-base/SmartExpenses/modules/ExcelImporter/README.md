# Module: ExcelImporter

Category: Marketplace
Module roles: Configurator, Readonly

## Summary

- Entities: 7
- Flows: 81
- Pages: 13
- Constants: 2

## Purpose

- Export-backed: module category and inventory from overview export.
- Inferred: module role is support capability.
- Unknown: product-owner intent text is not included in export.

## Capability Map

| Capability prefix | Flow count | Representative flow |
|---|---:|---|
| ACR | 1 | ExcelImporter.ACr_Template |
| ASU | 1 | ExcelImporter.ASu_CheckModelAndTemplates |
| BCO | 1 | ExcelImporter.BCo_Column |
| BDE | 1 | ExcelImporter.BDe_Column |
| CH | 13 | ExcelImporter.Ch_Column_SetDefaultObject |
| COLUMN | 2 | ExcelImporter.Column_SetCorrectRefObjectType |
| EXAMPLE | 1 | ExcelImporter.Example_SetupImportTemplate |
| EXCELTEMPLATE | 2 | ExcelImporter.ExcelTemplate_ExportToXML |
| IVK | 14 | ExcelImporter.IVK_CancelTemplate |
| OTHER | 40 | ExcelImporter._DocumentationDummyXSD |
| PARSEENUMTOSTRING | 1 | ExcelImporter.ParseEnumToString_StatisticLevel |
| PARSESTRINGTOENUM | 1 | ExcelImporter.ParseStringToEnum_StatisticsLevel |
| SF | 1 | ExcelImporter.SF_Template_CheckNrs |
| SUB | 1 | ExcelImporter.Sub_CreateColumnsFromTemplate |
| VALIDATE | 1 | ExcelImporter.Validate_TemplateDocument |

## Primary User Journeys

| Entry flow | UI result | Entities touched |
|---|---|---|
| support module | n/a | dependency-focused summary |

## Top risks/unknowns in model understanding
- Some flows have behavioural actions without explicit entity name tokens (parser gap).
- Some pages have no explicit ShowPageAction evidence in exported flows.

## Navigation

- [DOMAIN.md](DOMAIN.md)
- [FLOWS.md](FLOWS.md)
- [PAGES.md](PAGES.md)
- [RESOURCES.md](RESOURCES.md)

## Cross-Module Dependencies

- Calls to: MxModelReflection
- Called by: none
- Shared entities via associations: none

## Source

- Export module: ExcelImporter
- Run folder: cli_2026-03-04T20-44-47.917Z
