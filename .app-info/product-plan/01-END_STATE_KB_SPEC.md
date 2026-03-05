# End-State KB Specification

## Objective

Define what a "good" generated KB looks like without changing the current folder and file contract.

## Fixed Folder Contract

The generator must continue to produce:

1. Root:
   - `READER.md`
   - `ROUTING.md`
2. App layer:
   - `app/APP_OVERVIEW.md`
   - `app/MODULE_LANDSCAPE.md`
   - `app/SECURITY.md`
   - `app/CALL_GRAPH.md`
3. Module layer:
   - `modules/<Module>/README.md`
   - `modules/<Module>/DOMAIN.md`
   - `modules/<Module>/FLOWS.md`
   - `modules/<Module>/PAGES.md`
   - `modules/<Module>/RESOURCES.md`
4. Routing layer:
   - `routes/by-entity.md`
   - `routes/by-page.md`
   - `routes/by-flow.md`
   - `routes/cross-module.md`
5. Source metadata:
   - `_sources/manifest.json`
   - `_sources/SOURCE_REF.md`

## Content Density Contract

1. Do not leave `Unknown` where value can be derived from export JSON.
2. Every app-level file must contain explicit confidence markers:
   - `Export-backed`
   - `Inferred`
   - `Unknown`
3. Custom-module docs must be behaviour-first, not inventory-only.
4. Indexes must be evidence-backed and cross-linked.

## Scope Policy

1. `Custom` modules:
   - deep behavioural extraction
   - tiered flow narratives
   - lifecycle and security impact coverage.
2. `Marketplace` and `System` modules:
   - concise inventory
   - dependency relevance to custom modules
   - known impact notes.
3. Cross-module edges touching custom modules are always fully documented.

## Mandatory App-Level Content

### `APP_OVERVIEW.md`

Must include:

1. App mission summary inferred from custom modules.
2. Core capabilities mapped to custom modules.
3. Top behavioural entry points (top 10 by impact score).
4. Security posture summary.

### `MODULE_LANDSCAPE.md`

Must include:

1. Category and complexity profile per module.
2. Custom module priority ranking.
3. Marketplace/system grouping by support function.

### `SECURITY.md`

Must include:

1. Role-to-module-role matrix.
2. Entity access summary for custom entities.
3. XPath constraints translated into plain-language meaning.

### `CALL_GRAPH.md`

Must include:

1. Cross-module dependency table with counts.
2. Key flows for major edges.
3. Custom boundary section:
   - inbound dependencies
   - outbound dependencies
   - hub and leaf classification.

## Mandatory Module-Level Content

For `Custom` modules specifically:

1. `README.md`:
   - capability map
   - primary user journeys
   - risks and known unknowns.
2. `DOMAIN.md`:
   - entity/attribute/access inventory
   - entity lifecycle matrix (`create/read/update/delete`)
   - sensitive role impacts.
3. `FLOWS.md`:
   - catalogue by naming conventions
   - tiered narrative details for high-impact flows.
4. `PAGES.md`:
   - page inventory with roles/parameters
   - page-flow links (no `Unknown` if derivable)
   - journey fragments by intent.
5. `RESOURCES.md`:
   - constants/events/resources
   - automation impact notes where applicable.

For non-custom modules:

1. Preserve same file structure and required headings.
2. Keep summaries concise and dependency-aware.

## Mandatory Routing Content

1. `routes/by-entity.md`:
   - non-`Unknown` `Used by Flows`/`Shown on Pages` when derivable.
2. `routes/by-page.md`:
   - non-`Unknown` `Shown by Flows` when derivable.
3. `routes/by-flow.md`:
   - non-`Unknown` `Shows Pages` and `Touches Entities` for custom flow evidence.
4. `routes/cross-module.md`:
   - call-edge and association-edge views
   - hub/leaf modules
   - custom-boundary dependency lens.

## Determinism Rules

1. Sort module output alphabetically.
2. Sort index rows deterministically.
3. Use stable tie-breakers for ranking lists.
4. Same input export must produce equivalent markdown output.
