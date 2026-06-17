# Commands

One sentence per command. See [CONVENTIONS.md](CONVENTIONS.md) for the full description.

---

## Development

| Command | Description |
|---|---|
| `/dev-task` | Ad-hoc change via development-agent in loose-mode against the live ACM codebase. Grounded in app-context. Delivers diff.md review. |
| `/parser-upgrader` | Activate parser-upgrader: improve the parser by classifying and closing DIFF/CONVERTER gaps between dump-diff extraction and displayText formatting; additive Dxxx/Cxxx/Axxx rules, approved per rule. |

## Meta commands

| Command | Description |
|---|---|
| `/improve` | Activate flow-developer: reads improvement-backlog.md and applies approved entries to /.agents and /.app-info/libraries/ via builder skills. |
| `/skill-write <name>` | Write or update a skill via the skill-writer skill. |
| `/agent-write <name>` | Write or update an agent via the agent-writer skill. |
| `/kb-search <query>` | Search the knowledgebase by feature, tag, or keyword. |

## Brain-builder commands

| Command | Description |
|---|---|
| `/brain` | No arguments: layered explanation of the full brain workings, then offers maintenance. |
| `/brain skill <name>` | Write a new skill or promote an existing one (skeleton → full). |
| `/brain agent <name>` | Write a new agent or promote an existing one (skeleton → full). |
| `/brain kb <topic>` | Add or update knowledgebase content for a topic (via kb-builder / kb-sync). |
| `/brain validate` | Structural check: manifest rows ↔ files on disk, frontmatter, SKILLS.md coverage. |

Commands are wired into Claude Code via `.claude/commands/`. Each command file loads the relevant agent/skill by explicit path.
