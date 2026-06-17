---
name: best-practices
description: Reads the best-practices library in /.app-info/libraries/best-practices/ and serves coding standards and platform rules at the requested depth.
status: full
---

# SKILL: best-practices

## Purpose

Read the best-practices library in `/.app-info/libraries/best-practices/` and return the coding standards and platform rules that apply to the code being written. Used by `development-agent` (before and during implementation) and `code-review` (as the rulebook to check against). Unlike the topic-scoped dev-library, this library applies to **every** change.

## Input

- **query** — what the caller needs (e.g. "naming conventions", "security rules", "error handling", "escalation triggers")
- **depth** — `surface` (the rule, one line) | `summary` (rule + rationale + example) | `full` (the complete relevant file)
- *(optional)* **area** — narrow scope to one file (e.g. `php`, `javascript`, `html`, `css`, `accessibility`, `platform`)

## Output

- Relevant rules from the library files, at the requested depth, citing which file they come from
- For escalation-rule hits: the explicit **STOP** instruction quoted verbatim
- If nothing matches: explicit "not covered" + a suggestion to add the rule to the library if it will recur

## Steps

1. **Read the index** at `/.app-info/libraries/best-practices/INDEX.md` to map the query to file(s). The index lists all available standard files and their scope.

2. **Read the matching file(s)** and filter to the rules relevant to the query.

3. **Return at the requested depth.** Always include the file reference so the caller can read further.

4. **Surface escalations proactively:** if the query touches security-sensitive areas, data writes, or integration calls, include any matching STOP or escalation rules from the platform rules file even if not asked.

5. **Flag gaps:** if the query concerns a rule the library doesn't cover, say so and suggest adding it to the library.

## Notes

- This skill is read-only; library content is maintained directly (no creator skill needed — write the file and update INDEX.md).
- Library boundaries: *how code is written* lives here; *what it looks like* in the styling library; *what to reuse* in the component library; *per-topic deep-dives* in the dev-library. Don't duplicate across them — cross-reference.

---

## Setup

When setting up a new brain, populate `/.app-info/libraries/best-practices/` with files appropriate to your tech stack. Suggested structure for a web app:

```
/.app-info/libraries/best-practices/
  INDEX.md
  coding-standards/
    <language>.md       # one file per language (php, javascript, python, etc.)
    html.md
    css.md
    accessibility.md
  platform/
    platform-rules.md   # app-specific rules, integration safety, escalation triggers
```

Each file should contain: the rule name, the rule itself (one line), and optional rationale + example.
