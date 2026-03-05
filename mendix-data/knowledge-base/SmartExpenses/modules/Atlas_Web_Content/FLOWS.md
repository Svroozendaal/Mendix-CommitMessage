# Flows: Atlas_Web_Content

## Flow Catalogue

### Action Flows (ACT_*)

| Flow | Nodes | Key Actions | Pages Shown |
|---|---:|---|---|
| ACT_Login | 15 | change LoginContext (ValidationMessage=''; refreshInClient=true), change LoginContext (ValidationMessage='No connection, please try again later.', Password=''; refreshInClient=true) | Unknown |

### Data Sources (DS_*)

| Flow | Nodes | Key Actions | Returns |
|---|---:|---|---|
| DS_LoginContext | 3 | create Atlas_Web_Content.LoginContext as NewLoginContext | Unknown |

### Validation Flows (VAL_*)

| Flow | Nodes | Key Actions |
|---|---:|---|
| none | 0 | none |

### Other Flows

| Flow | Type | Nodes | Key Actions |
|---|---|---:|---|
| none | none | 0 | none |

## Cross-Module Calls

| Flow | Calls | Target Module |
|---|---|---|
| none | none | none |

## Flow Details

| Flow | Kind | Nodes | Calls Out | Called By |
|---|---|---:|---:|---:|
| ACT_Login | Nanoflow | 15 | 0 | 0 |
| DS_LoginContext | Nanoflow | 3 | 0 | 0 |

