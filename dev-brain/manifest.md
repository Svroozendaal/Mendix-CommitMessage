# Manifest
## Registry — all agents + skills

See [CONVENTIONS.md](CONVENTIONS.md) for the full format description.

---

## Agents

| Agent | File | Function | Status |
|---|---|---|---|
| brain-builder | `.agents/agents/brain-builder.md` | Meta-agent for developing and maintaining /.agents and /.app-info; writes agents, skills, kb content and command wiring; explains the brain when invoked bare | full |
| development-agent | `.agents/agents/development-agent.md` | Executor for loose-mode ad-hoc changes against the live ACM codebase; grounded in app-context; consults the dev-library; writes diff.md as the review artifact | full |
| parser-upgrader | `.agents/agents/parser-upgrader.md` | ACM-specific agent dedicated to improving the parser: closes DIFF/CONVERTER gaps between dump-diff extraction (`MendixModelDiffService`) and displayText formatting (`MendixModelChangeDisplayTextFormatter`); additive Dxxx/Cxxx/Axxx rules, gated per rule | full |
| flow-developer | `.agents/agents/flow-developer.md` | Processes improvement-backlog.md, applies approved changes to /.agents and /.app-info/libraries/ via skill-writer and agent-writer, maintains changelog | full |

---

## Skills

### Meta

| Skill | File | Function | Status |
|---|---|---|---|
| skill-writer | `.agents/skills/skill-writer/SKILL.md` | Writes and validates skills; enforces format | full |
| agent-writer | `.agents/skills/agent-writer/SKILL.md` | Writes and validates agents; enforces format | full |

### Knowledge

| Skill | File | Function | Status |
|---|---|---|---|
| kb-search | `.agents/skills/kb-search/SKILL.md` | Navigates the knowledgebase via index/glossary/summaries; returns answer at requested depth | full |
| kb-builder | `.agents/skills/kb-builder/SKILL.md` | Adds, restructures, or fills manually-authored KB content; updates index files | full |
| kb-sync | `.agents/skills/kb-sync/SKILL.md` | Copies changed source documentation files into the knowledgebase, generates _summary.md, updates kb-manifest | full |
| best-practices | `.agents/skills/best-practices/SKILL.md` | Serves coding standards and platform rules from /.app-info/libraries/best-practices/ at the requested depth | full |

### Development

| Skill | File | Function | Status |
|---|---|---|---|
| code-review | `.agents/skills/code-review/SKILL.md` | Self-review before diff.md: quality checklist + MUST/SHOULD/NICE severity + verdict | full |
| dev-library | `.agents/skills/dev-library/index.md` | Registry of topic skills for the development-agent; topic skills register only in this index | full |

---

## App-context

ACM application knowledge that grounds the `development-agent` and `parser-upgrader` (integrated from the former `Dev_brain`). Not a skill registry — read directly.

| File | Function |
|---|---|
| `app-context/overview.md` | What ACM is, current shape (localhost web app), and the `ACM/` path map |
| `app-context/PRODUCT_PLAN.md` | Product intent, capabilities, boundaries, improvement backlog |
| `app-context/features/FEATURES.md` | Feature registry + per-feature behaviour and implementation references |

Canonical technical docs live in `.app-info/docs/`; Mendix domain skills live in `ACM/AI-tools/skills/`.
