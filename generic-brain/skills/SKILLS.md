# Skills overview

One-line reference for every skill in `/.agents/skills/`. Maintained by `skill-writer` — update this file whenever a skill is added or changed.

---

## Meta

| Skill | What it does | Status |
|---|---|---|
| [skill-writer](skill-writer/SKILL.md) | Writes and validates skills according to CONVENTIONS.md format; can produce both full and skeleton output. | full |
| [agent-writer](agent-writer/SKILL.md) | Writes and validates agents according to CONVENTIONS.md format; can produce both full and skeleton output. | full |

## Knowledge

| Skill | What it does | Status |
|---|---|---|
| [kb-search](kb-search/SKILL.md) | Navigates the knowledgebase using its own index, glossary, and summary files to answer a query; returns only what is needed at the right depth. | full |
| [kb-builder](kb-builder/SKILL.md) | Adds, restructures, or fills manually-authored content in the knowledgebase; updates index files and flags downstream skill changes. | full |
| [kb-sync](kb-sync/SKILL.md) | Manually-triggered sync that copies changed source documentation files into the knowledgebase, generates _summary.md files, and updates kb-manifest.md. | full |
| [best-practices](best-practices/SKILL.md) | Reads the best-practices library and serves coding standards and platform rules at the requested depth. | full |

## Development

| Skill | What it does | Status |
|---|---|---|
| [code-review](code-review/SKILL.md) | Self-review before diff.md: quality checklist + MUST/SHOULD/NICE severity classification + explicit verdict. | full |
| [dev-library](dev-library/index.md) | Topic-skill registry for the development-agent; topic skills register only in its own index. | full |
