# Display Text Formatting

## Purpose

`Formatting/` turns `MendixModelChange` records into short deterministic `displayText` lines for commit-message tooling.

## Main File

`MendixModelChangeDisplayTextFormatter.cs` is intentionally rule-heavy. It normalizes element names, applies element abbreviations, removes noisy details, and compacts raw parser details into readable phrases.

## Output Shape

The basic format is:

```text
<change marker> <element abbreviation> <element name without module prefix> : <details>
```

Examples of markers/abbreviations:

- Added elements usually get `NEW`.
- Deleted flows get `DEL`.
- Microflows use `MF`, nanoflows use `NF`, pages use `PG`, task queues use `TQ`, enumerations use `ENUM`, export/import mappings use `EM`/`IM`.
- Entities usually omit an abbreviation.

## Rule Areas

- Entity access-rule changes become compact entity details.
- Flow action, decision, loop, variable, retrieve, create, commit, and UI-action changes are summarized.
- Page and snippet widget changes are compacted into functional widget summaries.
- Constants, mappings, REST services, scheduled events, Java actions, and generic resources receive focused summaries where possible.
- Annotation-only or zero-only noise is suppressed.

## Maintenance Notes

- Formatter tests are the contract for wording.
- Keep display text deterministic: same input should produce the same output order and wording.
- New parser detail phrases should either format acceptably by default or get an explicit formatter rule.
