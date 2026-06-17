---
name: kb-sync
description: Manually-triggered sync that copies changed source documentation files into the knowledgebase, generates _summary.md files, and updates kb-manifest.md.
status: full
---

# SKILL: kb-sync

## Purpose

Synchronise the knowledgebase (`<brain-root>/.app-info/knowledgebase/`) with the live source documentation files (e.g. `info_*.md` files scattered through the application codebase, or equivalent documentation sources). Run this skill after completing a feature or updating any documentation file in the codebase. The skill detects which files have changed (via MD5 hash comparison against `kb-manifest.md`), copies only those files, regenerates their `_summary.md`, and marks the manifest entries as `ok`.

> **Setup note:** replace `<brain-root>` and `<codebase-root>` with the actual absolute paths for your project.

## Input

- **scope** — `all` (check every manifest entry) or a folder path relative to the codebase root to limit the sync to one module.
- **kb-manifest.md** — source of truth for current sync state. Located at `<brain-root>/.app-info/knowledgebase/kb-manifest.md`.

## Output

- Updated documentation files in the mirrored knowledgebase folder structure (copied from codebase).
- New or regenerated `_summary.md` file alongside each copied documentation file.
- Updated `kb-manifest.md`: `source_hash`, `last_synced`, `summary_generated` filled in; `status` set to `ok` for all synced entries.
- A short sync report shown to the user.

## Steps

1. **Read `kb-manifest.md`** — load the full entry table. Note the path constants at the top.

2. **Filter entries by scope** — if scope is `all`, process every entry. If scope is a folder, filter to rows where `source` starts with that folder.

3. **For each entry, check status:**
   a. Resolve the absolute source path using the codebase path constant.
   b. Compute the MD5 hash of the source file.
   c. Compare against `source_hash` in the manifest.
   - If hash matches → skip (already `ok`). Mark as skipped in the report.
   - If hash differs or status is `stale`/`missing` → proceed to step 4.

4. **Copy the source file** to the KB path. Overwrite if it already exists. Preserve the original file name.

5. **Generate or regenerate `_summary.md`** alongside the copied file:
   - Read the full source file content.
   - Write a `_summary.md` with the following sections:
     - `# Summary: <module-path>` — heading using the module's folder path
     - `**What it does:**` — one sentence describing the module's role
     - `**Key concepts:**` — 4–8 bullet points: main patterns, hooks, data keys, classes, or entry points an agent needs to know
     - `**Public entry points:**` (if applicable) — shortcodes, AJAX actions, admin pages, API routes
     - `**Dependencies:**` (if applicable) — other modules or globals this module relies on
     - `→ [Full info](<filename>)` — relative link to the copied source file
   - Target: ≤30 lines. Omit empty sections. Do not include raw code blocks.

6. **Update `kb-manifest.md`** for each synced entry:
   - `source_hash` → new MD5 value
   - `last_synced` → today's ISO date (YYYY-MM-DD)
   - `summary_generated` → today's ISO date
   - `status` → `ok`

7. **Report to user:**
   ```
   KB sync complete — <date>
   Updated  : <n> files
   Unchanged: <n> files
   Missing  : <n> files (source not found — check manifest paths)
   ```

## Notes

- Never modify any file in the live codebase — this skill is read-only with respect to the application.
- If a source file cannot be found at the resolved path, mark the entry as `status: missing` and include it in the Missing count. Do not abort the sync.
- The `_summary.md` must stay within 30 lines. Extract only the highest-signal facts.
- Related skills: `kb-search` (reads the knowledgebase), `kb-builder` (maintains manually authored KB content).
