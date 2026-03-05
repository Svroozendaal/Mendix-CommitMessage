# Call Graph

## Summary

- Export-backed: Total call edges: 
225
- Export-backed: Cross-module call edges: 
22

## Cross-Module Calls

| Source -> Target | Call Edges |
|---|---:|
| ExcelImporter->MxModelReflection | 20 |
| ImporterHelper->SmartExpenses | 2 |

## Hub Modules

- ImporterHelper (outbound modules: 1, inbound modules: 0)
- SmartExpenses (outbound modules: 0, inbound modules: 1)
- ExcelImporter (outbound modules: 1, inbound modules: 0)
- MxModelReflection (outbound modules: 0, inbound modules: 1)
- New_Module (outbound modules: 0, inbound modules: 0)

## Notes

- Export-backed: Call edges come from module `flows.json` artifacts.
- Inferred: High-degree modules act as orchestration hubs.
- Unknown: Runtime call frequency is not available.

