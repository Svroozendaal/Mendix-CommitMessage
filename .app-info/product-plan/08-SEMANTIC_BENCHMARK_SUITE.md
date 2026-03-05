# Semantic Benchmark Suite

## Objective

Define canonical QA-style checks proving that generated KB output supports real application understanding.

## Scoring Model

1. Total score: `100`.
2. Scenario count: `10`.
3. Base weight: `10` each.
4. Scenario score = matched evidence ratio (`0..10`).

Pass criteria:

1. Total score `>= 85`.
2. No critical scenario failure.

## Critical Scenarios

Critical scenarios:

1. transaction create/save behaviour
2. transaction mutation + role constraints
3. ImporterHelper to SmartExpenses dependency.

## Scenario Set

1. How is a transaction created and saved?
   - Evidence: custom flow docs + relevant pages.
2. Which flows can change `SmartExpenses.Transaction` and under which roles?
   - Evidence: domain lifecycle + security + flow details.
3. What does `ImporterHelper` call in `SmartExpenses`?
   - Evidence: cross-module dependency docs.
4. Which pages are shown during budget type management?
   - Evidence: page index + page-flow links + flow details.
5. What entity lifecycle exists for `BudgetTerm`?
   - Evidence: custom domain lifecycle matrix.
6. Which user roles can access parent home paths?
   - Evidence: security role mapping + pages/flows.
7. Where is transaction status determined?
   - Evidence: flow detail for `SUB_Transaction_setStatus`.
8. What scheduled/system automation affects custom modules?
   - Evidence: resources docs + dependency notes.
9. Which module is the custom orchestration hub?
   - Evidence: call graph + cross-module hub analysis.
10. What is still unknown and why?
   - Evidence: explicit known-gaps sections.

## Evidence Rules

1. Each scenario has required files and regex evidence patterns.
2. Missing files score zero for corresponding checks.
3. Not-applicable scenarios (module absent) may be excluded from denominator if explicitly marked as `N/A`.

## Benchmark Output Contract

Benchmark output must include:

1. Scenario-by-scenario status (`PASS`, `FAIL`, `N/A`).
2. Matched evidence summary.
3. Total score and threshold.
4. Critical failure summary.
5. Final verdict.
