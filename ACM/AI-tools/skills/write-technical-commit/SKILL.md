---
name: write-technical-commit
description: Writes concise technical Mendix commit messages from ACM parser output, with a short human summary followed by module-grouped technical changes.
status: full
---

# SKILL: write-technical-commit

## Purpose

Write a technical commit message from AutoCommitMessage (ACM) parser output. Use this skill when an
agent has `modelChangesByModule` / `displayText` from `read_changes` or an explicitly persisted
export and needs to draft a commit message that is useful to a Mendix developer: brief functional
intent first, then module-grouped technical changes.

## Input

- `storyId` - ticket id, for example `FIN-1582` or `SH-2032`.
- `signature` - author signature, for example `SvR`.
- Parser output - live `read_changes` payload or export JSON containing `changes[*].modelChangesByModule`.
- Optional user context - story title, branch name, user-provided intent, or known acceptance context.
- Optional storage target - only needed if another skill or agent will store the final message.

## Output

A commit message body in this shape:

```text
<storyId> <signature>
<short functional summary>

changes:
[<Module>]
- <ABBR> <ElementName> : <technical change summary>
- <ABBR> <ElementName> : <technical change summary>
```

For messages that will be stored by ACM, the storage layer may prepend `#commit:<shortHash>`.

## Steps

1. **Read the parser output.** Use only changes present in `modelChangesByModule` / `displayText`.
   Do not invent changed elements, modules, actions, pages, entities, or resources.

2. **Identify the story header.** Start with exactly `<storyId> <signature>` on the first visible
   message line. Do not include branch names, commit hashes, or dates in this line.

3. **Write the short summary.** Add one short functional summary after the header:
   - Prefer 1 sentence for small changes.
   - Use 2-3 short lines only when multiple functional outcomes changed.
   - Mention business behavior, not implementation trivia.
   - Keep it direct and specific.

4. **Add the technical section.** Write `changes:` on its own line, then group rows by module:
   - Module heading format is `[ModuleName]`.
   - Keep the parser module names unchanged.
   - Sort modules in the parser order unless a deterministic alphabetical order is already required
     by the upstream formatter.
   - Do not add empty module sections.

5. **Render changed elements.** Under each module, write one dash-prefixed row per relevant changed
   element:
   - Preserve parser abbreviations when available: `MF`, `NF`, `PG`, `SNP`, `NEW`, `DEL`.
   - Keep technical element names explicit.
   - Remove noisy module prefixes from element names when the parser already provides a short name.
   - Use `: <details>` for meaningful technical details.
   - If parser detail text is too verbose, compact it without changing meaning.

6. **Use domain subsections only when helpful.** If the parser output separates domain-model rows,
   include `Domain model:` under the module before entity rows. Do not write `Domain mode`.

7. **Filter noise carefully.** Exclude or compact low-signal rows only when they do not affect the
   story-level technical message:
   - Annotation-only changes can be omitted or summarized as `modified` if they are the only signal.
   - Unknown project security/resource metadata should be omitted unless it is clearly relevant.
   - Access-rule changes are relevant when the parser explicitly reports `Accessrules changed`.

8. **Keep the language consistent.**
   - Use English.
   - Prefer present tense or concise past tense.
   - Keep wording technical and understated.
   - Do not add marketing phrasing, speculation, or test claims.

9. **Validate before returning.**
   - Header is present.
   - Summary is present unless the user explicitly asked for technical rows only.
   - `changes:` section is present.
   - Every technical row starts with `-`.
   - Every row maps to parser-reported changes.
   - No generated file storage is implied unless the caller requested storage.

## Notes

Good example:

```text
FIN-1582 SvR
Internal costs in the faculty report now use monthly calculation for partial-year personnel and route totals via the prognose reporting flow.

changes:
[General]
- MF SUB_ProjectResourceList_SumInternalCosts : added year filter and partial-year handling; now calculates internal costs from yearly or monthly basis
- MF SUB_ProjectPrognoseReporting_Calculate : routes resource types to the correct budget totals and calls the monthly-cost helper
- MF SUB_ProjectResourceList_SumInternalCosts_caclbyMonths : used for monthly internal-cost calculation
```

Existing examples live in `mendix-data/Commit messages`. Use them for style calibration, but do not
copy old content into a new message.
