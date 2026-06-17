# Dev-library — Index

> Topic skills for the development-agent. Each skill covers one technology or recurring pattern: the approach, pitfalls, and best practices. This index is the sole registry for topic skills — they do not appear in `manifest.md` or `SKILLS.md`.

---

## How to use

The development-agent reads this index before every build task, extracts the relevant topics, loads the matching skill files, and follows their approach during implementation. Missing topics trigger the missing-topic procedure (research + user approval → new skill file added here).

---

## Topic skills

| Topic | File | Description | Sources |
|---|---|---|---|
| *(none yet — add your first topic skill here)* | | | |

---

## Topic skill format

Each topic skill lives at `.agents/skills/dev-library/<topic>/SKILL.md` and follows this structure:

```markdown
---
name: dev-library/<topic>
description: <one sentence>
status: full
---

# Dev-library: <topic>

## When to use
[One sentence: what triggers this skill?]

## Approach
[The recommended implementation pattern, step by step.]

## Pitfalls
[Common mistakes to avoid, with brief explanation of why.]

## Best practices
[Positive rules and conventions.]

## Sources
[URL or document references used to build this skill.]
```

---

## Adding a topic skill

1. Research the topic (internet research with user permission, or draw from existing KB/best-practices).
2. Write `.agents/skills/dev-library/<topic>/SKILL.md` following the format above.
3. Add a row to the table in this index.
4. Present sources to the user for approval before saving.
