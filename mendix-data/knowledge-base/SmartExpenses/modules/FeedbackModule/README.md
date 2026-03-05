# Module: FeedbackModule

Category: Marketplace
Module roles: User

## Summary

- Entities: 2
- Flows: 16
- Pages: 6
- Constants: 1

## Purpose

- Export-backed: module category and inventory from overview export.
- Inferred: module role is support capability.
- Unknown: product-owner intent text is not included in export.

## Capability Map

| Capability prefix | Flow count | Representative flow |
|---|---:|---|
| ACT | 5 | FeedbackModule.ACT_Feedback_ClearForm |
| DS | 1 | FeedbackModule.DS_Feedback_Populate |
| OCH | 1 | FeedbackModule.OCH_Feedback_SaveToLocalStorage |
| OTHER | 3 | FeedbackModule.ConvertBase64String |
| SUB | 5 | FeedbackModule.SUB_Feedback_GetOrCreate |
| VAL | 1 | FeedbackModule.VAL_Feedback |

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

- Calls to: none
- Called by: none
- Shared entities via associations: none

## Source

- Export module: FeedbackModule
- Run folder: cli_2026-03-04T20-44-47.917Z
