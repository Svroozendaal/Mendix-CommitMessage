# ACM — Application Context (overview)

> Grounding for the `development-agent` and `parser-upgrader`. Read this first, then drill
> into `PRODUCT_PLAN.md`, `features/`, the canonical technical docs in
> `.app-info/docs/`, and the Mendix domain skills in `ACM/AI-tools/skills/`.

## What ACM is

`AutoCommitMessage` (ACM) inspects **uncommitted Mendix model changes**
(`.mpr` / `.mprops`), derives semantic diffs from `mx dump-mpr` output, and
presents/exports structured change data for commit-message tooling.
