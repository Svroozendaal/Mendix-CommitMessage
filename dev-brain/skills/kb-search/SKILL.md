---
name: kb-search
description: Navigates the knowledgebase using its own index, glossary, and summary files to answer a query; returns only what is needed at the right depth.
status: full
---

# SKILL: kb-search

## Purpose

Answer a query about the application by navigating the knowledgebase at `<brain-root>/.app-info/knowledgebase/`. The knowledgebase is self-describing — its own index and glossary files tell you what exists and where to look. This skill describes how to move through those files efficiently: start broad, go deep only when needed. Do not read the live application codebase; do not guess at content.

> **Setup note:** replace `<brain-root>` with the actual absolute path to this brain folder.

## Input

- **query** — a feature name, domain term, keyword, or question
- **depth** — how much detail the caller needs: `surface` (one-liner), `summary` (≤30 lines), or `full` (complete info file). Defaults to `summary` if omitted.

## Output

- A structured answer drawn from KB files, clearly citing which file(s) were read.
- A `Gaps` note if the query touches something not yet documented in the KB.

## Steps

### 1. Identify the query type

| Query type | Start here |
|---|---|
| Domain term or app-specific word | `glossary.md` |
| Feature name or module | `index.md` |
| Architecture / how-does-it-fit question | `overview.md` |
| Unknown — anything else | `index.md` (scan the description column) |

### 2. Read the entry-point file

**Top-level entry points** (all at `knowledgebase/`):

| File | Use for |
|---|---|
| `index.md` | Quick-scan table: maps every section to its index file. Always start here. |
| `overview.md` | High-level app summary — read for architecture/fit questions. |
| `glossary.md` | Domain terms, role slugs, field keys — read for vocabulary questions. |

**Section indexes** (drill down from `index.md`): follow the links in `index.md` to reach section-level index files.

These files are small. Read the whole file — do not skip to a section.

### 3. Locate the relevant module(s)

From `index.md`, identify the row(s) whose description matches the query. A query may touch multiple modules.

### 4. Read at the right depth

| Depth | What to read |
|---|---|
| `surface` | The 10-word description from `index.md` is enough. Stop here. |
| `summary` | Read the `_summary.md` file linked from `index.md`. ≤30 lines covering key concepts, hooks, entry points, dependencies. |
| `full` | Read the full `info_*.md` or equivalent file. Only when you need function signatures, exact data keys, or operational detail not visible in the summary. |

Always try `summary` before escalating to `full`.

### 5. Return the answer

```
**Query:** <original query>
**Source(s):** <list of files read, relative to knowledgebase root>

<answer — drawn from those files>

**Gaps:** <anything the query touched that is not yet documented, or "none">
```

If the KB does not contain relevant information, say so explicitly. Do not fabricate.

## Notes

- Never read the live application codebase — this skill is KB-only.
- If a module `_summary.md` is missing, fall back to the full file directly.
- `glossary.md` and `overview.md` may be empty stubs in a new brain. Note this as a gap and fall back to `index.md`.
- Related skills: `kb-builder` (maintains manually authored KB content), `kb-sync` (syncs source documentation into KB).
