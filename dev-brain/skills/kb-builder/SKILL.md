---
name: kb-builder
description: Adds, restructures, or fills content in the knowledgebase; updates index files, glossary, and overview; flags when kb-search or kb-sync must also change.
status: full
---

# SKILL: kb-builder

## Purpose

Build and maintain the content of the knowledgebase at `<brain-root>/.app-info/knowledgebase/`. This includes adding new topic files, filling stub files with real content, restructuring folders, and keeping all index files consistent. It does not sync source documentation files — that is `kb-sync`'s job. This skill works on manually authored content: platform docs, architecture overviews, data models, environments, feature landscapes, and anything that does not originate from a source `info_*.md` or equivalent file.

> **Setup note:** replace `<brain-root>` with the actual absolute path to this brain folder.

## Input

- **action** — one of: `add-topic`, `fill-stub`, `restructure`, `fill-index`, `fill-overview`
- **target** — the file or folder path relative to `knowledgebase/`
- **source** — where the content comes from: existing KB files or inline content provided in conversation
- **scope** — *(restructure only)* a description of what moves where

## Output

- New or updated files in `knowledgebase/`
- Updated `index.md` file(s) in any affected folder
- A list of which files changed, shown to the user
- *(when applicable)* A flag telling the user which downstream skills need updating

## Steps

### 1. Read the current index

Before writing anything, read `knowledgebase/index.md`. For work inside a subfolder, also read that folder's `index.md`.

### 2. Identify what needs to change

| Action | What to do |
|---|---|
| `add-topic` | Create a new `<topic>.md` file under the correct folder; add a row to the folder's `index.md` |
| `fill-stub` | Read the stub file; replace placeholder content with real content drawn from the source |
| `restructure` | Move files as described in scope; update all `index.md` files that reference moved paths; see step 5 |
| `fill-index` | Rewrite or extend the `index.md` for a folder to reflect its current actual contents |
| `fill-overview` | Write or rewrite `knowledgebase/overview.md` or `knowledgebase/glossary.md` |

### 3. Write the content

**Topic files** follow this format:

```markdown
# <Title>

> One-line summary of what this file covers.

## <Section>
...
```

- Keep each file focused on one area. Do not combine unrelated topics.
- Source content faithfully from provided docs. Do not invent facts.
- All prose and headings must be in English; domain terms may be preserved in the original language.

**Index files** (`index.md` in any folder) follow this format:

```markdown
# <Folder> — Index

> <One-line description of this folder's scope.>

| Topic | Description | File |
|---|---|---|
| <name> | <10-word description> | [link](file.md) |
```

- Every file in the folder must have a row.
- The description must be ≤10 words, enough for an agent to decide whether to read the file.

### 4. Update parent index files

After writing or moving any file, update its parent folder's `index.md`. Also update the top-level `knowledgebase/index.md` if a new top-level section was added.

### 5. Restructure checklist

When action is `restructure`, follow this checklist before writing anything:

- [ ] List every file that will move (old path → new path)
- [ ] List every `index.md` that references those paths
- [ ] Show the full move plan to the user and get confirmation before executing
- [ ] After moving, update all `index.md` files
- [ ] Update `kb-manifest.md` entries whose paths have changed

### 6. Check downstream impact

After every kb-builder run, check whether the following skills are still accurate:

| Condition | Action |
|---|---|
| Top-level folder structure changed | Flag: `kb-search` entry-point file list may be stale |
| `kb-manifest.md` paths changed | Flag: `kb-sync` paths must be reviewed |
| `glossary.md` or `overview.md` changed substantially | Flag: `kb-search` query-type routing may need an update |

Report flags to the user at the end of every run. If no downstream review is needed, say "No downstream skill changes required."

## Notes

- Never read the live application codebase. Source content from `knowledgebase/` files and conversation input only.
- `kb-sync` handles source `info_*.md` files and their `_summary.md` copies. Do not duplicate that work here.
- Related skills: `kb-search` (reads the KB), `kb-sync` (syncs source documentation into KB).
