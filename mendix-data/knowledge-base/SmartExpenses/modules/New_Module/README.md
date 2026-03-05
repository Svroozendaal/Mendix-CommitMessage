# Module: New_Module

Category: Custom
Module roles: ModuleRole

## Summary

- Entities: 5
- Flows: 2
- Pages: 1
- Constants: 0

## Purpose

- Export-backed: module category and inventory from overview export.
- Inferred: module role is app-specific business behaviour.
- Unknown: product-owner intent text is not included in export.

## Capability Map

| Capability prefix | Flow count | Representative flow |
|---|---:|---|
| ACO | 1 | New_Module.ACO_new |
| BCO | 1 | New_Module.BCO_new |

## Primary User Journeys

| Entry flow | UI result | Entities touched |
|---|---|---|
| none | none | none |

## Top risks/unknowns in model understanding
- Some flows have behavioural actions without explicit entity name tokens (parser gap).
- Some pages have no explicit ShowPageAction evidence in exported flows.

## Navigation

- [DOMAIN.md](DOMAIN.md)
- [FLOWS.md](FLOWS.md)
- [PAGES.md](PAGES.md)
- [RESOURCES.md](RESOURCES.md)

## Cross-Module Dependencies

- Calls to: none
- Called by: none
- Shared entities via associations: none

## Source

- Export module: New_Module
- Run folder: cli_2026-03-04T20-44-47.917Z
