---
name: skill-writer
description: Writes and validates skills according to CONVENTIONS.md format; can produce both full and skeleton output.
status: full
---

# SKILL: skill-writer

## Purpose

Write a new skill or update an existing one, in accordance with the format described in [CONVENTIONS.md](../../CONVENTIONS.md). Use this skill whenever you want to create a new skill file or validate the integrity of an existing one.

## Input

- **name** — kebab-case name of the skill (e.g. `kb-search`, `code-review`)
- **type** — `full` or `skeleton`
- **purpose** — one paragraph: what does the skill do and when is it used?
- *(full only)* steps, input description, output description, notes

## Output

- New or updated file: `.agents/skills/<name>/SKILL.md`
- Updated entry in [manifest.md](../../manifest.md)
- Updated entry in [SKILLS.md](../SKILLS.md)

## Steps

1. **Classify**: is the skill generic (belongs in `.agents/skills/`) or app-specific (belongs in `.app-info/`)? Ask if unclear.

2. **Validate the name**: kebab-case, unique, descriptive. Check that no existing skill with that name exists in `manifest.md`.

3. **Write the file** at the correct path: `.agents/skills/<name>/SKILL.md`.

   **Skeleton format** (`status: skeleton`):
   ```markdown
   ---
   name: <name>
   description: <one sentence>
   status: skeleton
   ---

   # SKILL: <name>

   ## Purpose
   [One paragraph: what does this skill do and when is it used?]
   ```

   **Full format** (`status: full`):
   ```markdown
   ---
   name: <name>
   description: <one sentence>
   status: full
   ---

   # SKILL: <name>

   ## Purpose
   [One paragraph.]

   ## Input
   [Bullet list or table of required inputs.]

   ## Output
   [What exists or has been produced after execution.]

   ## Steps
   [Numbered steps the agent follows.]

   ## Notes
   [Optional: edge cases, dependencies, related skills.]
   ```

4. **Register** in `manifest.md` under the correct category. Format:
   ```
   | <name> | `.agents/skills/<name>/SKILL.md` | <one-line description> | <full|skeleton> |
   ```

5. **Update `SKILLS.md`** at `.agents/skills/SKILLS.md`:
   - For a **new skill**: add a row to the correct category table — `| [<name>](<name>/SKILL.md) | <description from frontmatter> | <full|skeleton> |`
   - For an **updated skill**: find the existing row and update the description and/or status if either changed.
   - If a new category is needed, add a new `## <Category>` section before the row.

6. **Validate**:
   - Frontmatter present (`name`, `description`, `status`)?
   - Skeleton has only `## Purpose`?
   - Full skill has all sections?
   - Entry in `manifest.md` correct?
   - Row present in `SKILLS.md` with correct description and status?

## Notes

- A skill is a procedure, not an agent. If the task requires a persona or ongoing responsibility, use `agent-writer` instead.
- Skills may reference other skills but must not depend on specific agents.
- App-specific skills (that read `/.app-info`) still live in `.agents/skills/` — they are generic in structure; only their content references `/.app-info`.
- All skill files must be written in English.
