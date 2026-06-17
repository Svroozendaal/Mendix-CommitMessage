---
name: code-review
description: Structured self-review before diff.md — quality checklist plus MUST FIX / SHOULD FIX / NICE TO HAVE severity classification and an explicit verdict.
status: full
---

# SKILL: code-review

## Purpose

Self-review procedure the development-agent runs on all changed code **before** writing `diff.md`. Combines a structural quality checklist with severity-classified findings and an explicit verdict, so the user reviews a diff that has already passed a defined quality bar. Also callable standalone to review an arbitrary set of changes.

## Input

- The set of changed/created files (from the work just performed, or named by the caller)
- *(if available)* `dev-library/security-checklist` — companion check for endpoints, handlers, data writes
- The surrounding module's code style (read neighbouring code before judging idiom)

## Output

A review verdict block, embedded in `diff.md` or reported in conversation when standalone:

```markdown
## Review verdict — <scope>

MUST FIX:
- [file] — [issue]

SHOULD FIX:
- [file] — [issue]

NICE TO HAVE:
- [file] — [issue]

Security check: PASS / FAIL / N/A
Verdict: CLEAN / FIXED INLINE / FINDINGS REMAIN
```

## Steps

1. **Scope.** List every changed or created file. Read each fully — no judging from diff hunks alone.
2. **Quality checklist.** For all changed code, verify:
   - **Single responsibility** — each function/class does one thing
   - **No unnecessary duplication** — shared logic extracted or reused, not copied
   - **Naming** — follows the conventions of the surrounding codebase
   - **Error paths explicit** — all failure modes handled; no silent failures
   - **No hardcoded values** — env-specific strings, magic numbers, credentials go through config/constants
   - **Testable in isolation** — minimal hidden dependencies
3. **Security companion.** For any endpoint, form handler, data write, or permission boundary: run `dev-library/security-checklist` if available, and record its PASS/FAIL.
4. **Classify findings.**
   - **MUST FIX** — security issues, data-loss risk, broken contracts, logic errors. Fix before presenting diff; never present a diff with known MUST FIX items unfixed.
   - **SHOULD FIX** — quality issues, missing edge cases, unclear naming. Fix now if cheap; otherwise list under `### Open points` in `diff.md`.
   - **NICE TO HAVE** — optional improvements. List only; do not expand scope.
5. **Reference exactly.** Every finding names the file and describes the issue concretely.
6. **Verdict.** `CLEAN` (nothing found), `FIXED INLINE` (findings found and resolved), or `FINDINGS REMAIN` (SHOULD FIX/NICE TO HAVE left for the user).

## Notes

- This is a self-review, not a replacement for the user's gate judgement — it raises the floor, the gate stays.
- Check findings against the best-practices library (`best-practices` skill) — a finding that contradicts a library rule is at least SHOULD FIX.
- A finding that *recurs across tasks* but isn't in the library yet is a best-practices candidate: note it in `diff.md` under Open points.
- Do not refactor unrelated code because the checklist inspired you; findings outside the change's scope go to Open points as observations.
