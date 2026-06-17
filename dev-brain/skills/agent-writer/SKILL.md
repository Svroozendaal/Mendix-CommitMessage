---
name: agent-writer
description: Writes and validates agents according to CONVENTIONS.md format; can produce both full and skeleton output.
status: full
---

# SKILL: agent-writer

## Purpose

Write a new agent or update an existing one, in accordance with the format described in [CONVENTIONS.md](../../CONVENTIONS.md). Use this skill whenever you want to create a new agent file or validate the integrity of an existing one.

## Input

- **name** — kebab-case name of the agent (e.g. `brain-builder`, `development-agent`)
- **type** — `full` or `skeleton`
- **purpose** — one paragraph: what does the agent do, when is it invoked, what is it responsible for?
- *(full only)* input, behaviour, output, gates

## Output

- New or updated file: `.agents/agents/<name>.md`
- Updated entry in [manifest.md](../../manifest.md)

## Steps

1. **Confirm the need for an agent**: agents represent roles with ongoing responsibility. Is this a one-off procedure? → Create a skill (see `skill-writer`). Is this an ongoing role with defined inputs, behaviour and outputs? → Create an agent.

2. **Validate the name**: kebab-case, unique, descriptive. Check that no existing agent with that name exists in `manifest.md`.

3. **Write the file** at the correct path: `.agents/agents/<name>.md`.

   **Skeleton format** (`status: skeleton`):
   ```markdown
   ---
   name: <name>
   description: <one sentence — used for routing>
   status: skeleton
   ---

   # Agent: <name>

   ## Purpose
   [One paragraph: what does this agent do and when is it invoked?]
   ```

   **Full format** (`status: full`):
   ```markdown
   ---
   name: <name>
   description: <one sentence — used for routing>
   status: full
   ---

   # Agent: <name>

   ## Purpose
   [One paragraph.]

   ## Input
   [Bullet list of files or data the agent must read before acting.]

   ## Behaviour
   [Numbered rules the agent always follows.]

   ## Output
   [What does the agent produce or write?]

   ## Gates
   [Explicit pause points requiring user approval, or `none`.]
   ```

4. **Register** in `manifest.md` under the Agents table. Format:
   ```
   | <name> | `.agents/agents/<name>.md` | <one-line description> | <full|skeleton> |
   ```

5. **Validate**:
   - Frontmatter present (`name`, `description`, `status`)?
   - Skeleton has only `## Purpose`?
   - Full agent has all sections?
   - Entry in manifest correct?

## Notes

- All agents are generic and live in `.agents/agents/`. App-specific roles are encoded as skills that reference `/.app-info`.
- Agents may invoke skills but must not directly depend on other specific agents.
- All agent files must be written in English.
