# Generic Brain — Setup Guide

A minimal, reusable AI brain for Claude Code. Provides brain development (write/maintain agents + skills) and app development (ad-hoc code changes with review) out of the box. No flow A/B design pipeline, no Jira, no deployment agent — just the bare essentials.

---

## What's included

### Agents
| Agent | Command | Purpose |
|---|---|---|
| `brain-builder` | `/brain` | Write/maintain agents, skills, KB content; explain the brain |
| `development-agent` | `/dev-task` | Execute ad-hoc code changes with approach approval + diff review |
| `flow-developer` | `/improve` | Process improvement-backlog entries one by one with approval |

### Skills
| Skill | Purpose |
|---|---|
| `skill-writer` | Write/validate skill files |
| `agent-writer` | Write/validate agent files |
| `kb-search` | Answer queries from the knowledgebase |
| `kb-builder` | Maintain manually authored KB content |
| `kb-sync` | Sync source documentation files into the KB |
| `best-practices` | Serve coding standards + platform rules |
| `code-review` | Self-review before diff presentation |
| `dev-library` | Registry of per-topic skills (empty — grows with your project) |

---

## Folder structure to create

```
<brain-folder>/
  CONVENTIONS.md
  manifest.md
  commands.md
  CLAUDE.md           ← write this yourself (see below)
  .claude/
    commands/
      brain.md
      dev-task.md
      improve.md
      skill-write.md
      agent-write.md
      kb-search.md
  .agents/
    CONVENTIONS.md    ← same as brain-folder root
    manifest.md
    commands.md
    improvement-backlog.md   ← create empty (## Pending\n\n## Applied)
    CHANGELOG.md             ← create empty
    agents/
      brain-builder.md
      development-agent.md
      flow-developer.md
    skills/
      SKILLS.md
      skill-writer/SKILL.md
      agent-writer/SKILL.md
      kb-search/SKILL.md
      kb-builder/SKILL.md
      kb-sync/SKILL.md
      best-practices/SKILL.md
      code-review/SKILL.md
      dev-library/
        index.md
  .app-info/
    knowledgebase/
      index.md        ← create stub
      overview.md     ← create stub
      glossary.md     ← create stub
      kb-manifest.md  ← create with headers only
    libraries/
      best-practices/
        INDEX.md      ← create stub
        coding-standards/   ← add your language files
        platform/
          platform-rules.md ← add your app-specific rules
  .work/              ← gitignore this folder
```

---

## Setup steps

1. **Copy this folder** as `.agents/` inside your brain folder (and duplicate the top-level files to the brain root).
2. **Write `CLAUDE.md`** for your brain folder — describe the two-folder setup (brain + codebase), point to `.agents/CONVENTIONS.md` as the contract.
3. **Replace path placeholders** in `development-agent.md` and `kb-search.md`/`kb-sync.md` with the actual absolute paths to your brain and codebase.
4. **Populate `/.app-info/`** with content for your project:
   - `knowledgebase/` — describe your app's features, architecture, data model
   - `libraries/best-practices/` — your coding standards and platform rules
5. **Wire commands** — place the `.claude/commands/` files in your brain folder (or alongside the codebase if you prefer split wiring).
6. **Create empty state files:**
   - `.agents/improvement-backlog.md` with `## Pending` and `## Applied` headers
   - `.agents/CHANGELOG.md` empty

---

## What to build next (optional extensions)

The dev-library grows organically — the first time the development-agent encounters a new technology, it will research and draft a topic skill. You can also pre-populate it for your stack.

Other common additions:
- `flow-agent` + full Flow A/B pipeline (intake → brainstorm → proposal → plan → build)
- `assistant` agent with backlog management and issue-tracker integration
- `bug-flow` for structured bug reproduction + regression tests
- `deployment` agent for release management
