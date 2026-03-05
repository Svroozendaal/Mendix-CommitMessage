# Module: WorkflowCommons

Category: Marketplace
Module roles: Administrator, User

## Summary

- Entities: 26
- Flows: 186
- Pages: 32
- Constants: 2

## Purpose

- Export-backed: module category and inventory from overview export.
- Inferred: module role is support capability.
- Unknown: product-owner intent text is not included in export.

## Capability Map

| Capability prefix | Flow count | Representative flow |
|---|---:|---|
| ACT | 67 | WorkflowCommons.ACT_Assignee_Migrate |
| ASU | 2 | WorkflowCommons.ASu_Assignee_Migrate |
| DASHBOARDCONTEXT | 2 | WorkflowCommons.DashboardContext_GetSelectedWorkflowDefinition |
| DS | 30 | WorkflowCommons.DS_AuditTrailViewer |
| OCH | 6 | WorkflowCommons.OCh_CleanupHelper_UpdateCount |
| OCL | 1 | WorkflowCommons.OCl_WorkflowSummary |
| SE | 1 | WorkflowCommons.SE_WorkflowAuditTrailRecord_CleanUp |
| SUB | 76 | WorkflowCommons.SUB_Assignee_Migrate |
| WFEH | 1 | WorkflowCommons.WFEH_WorkflowEvent_AuditTrail |

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

- Export module: WorkflowCommons
- Run folder: cli_2026-03-04T20-44-47.917Z
