# Semantic Benchmark Report

## Summary

- App: SmartExpenses
- KB Root: mendix-data\knowledge-base\SmartExpenses
- Minimum score: 85
- Final score: 100
- Critical checks passed: True
- Benchmark passed: True
- Generated at: 2026-03-05T07:13:46Z

## Scores

| Check | Critical | Evidence hits | Score | Passed |
|---|---|---|---|---|
| Q1 | True | 2/2 | 12/12 | True |
| Q2 | True | 3/3 | 12/12 | True |
| Q3 | True | 2/2 | 10/10 | True |
| Q4 | False | 2/2 | 9/9 | True |
| Q5 | False | 1/1 | 9/9 | True |
| Q6 | False | 2/2 | 9/9 | True |
| Q7 | True | 2/2 | 10/10 | True |
| Q8 | False | 2/2 | 8/8 | True |
| Q9 | True | 2/2 | 11/11 | True |
| Q10 | False | 2/2 | 10/10 | True |

## Evidence Details

| Check | Question | Evidence evaluation |
|---|---|---|
| Q1 | How is a transaction created and saved? | modules/SmartExpenses/FLOWS.md: ok ; modules/SmartExpenses/PAGES.md: ok |
| Q2 | Which flows can change SmartExpenses.Transaction and under which role constraints? | modules/SmartExpenses/DOMAIN.md: ok ; modules/SmartExpenses/FLOWS.md: ok ; app/SECURITY.md: ok |
| Q3 | What does ImporterHelper call in SmartExpenses? | routes/cross-module.md: ok ; app/CALL_GRAPH.md: ok |
| Q4 | Which pages are shown during budget type management? | modules/SmartExpenses/PAGES.md: ok ; routes/by-page.md: ok |
| Q5 | What entity lifecycle exists for BudgetTerm? | modules/SmartExpenses/DOMAIN.md: ok |
| Q6 | Which user roles can access parent home flows/pages? | app/SECURITY.md: ok ; routes/by-page.md: ok |
| Q7 | Where is transaction status determined? | modules/SmartExpenses/FLOWS.md: ok ; routes/by-flow.md: ok |
| Q8 | What scheduled/system automation affects custom modules? | modules/SmartExpenses/RESOURCES.md: ok ; routes/cross-module.md: ok |
| Q9 | Which module is the custom orchestration hub? | app/CALL_GRAPH.md: ok ; routes/cross-module.md: ok |
| Q10 | What is still unknown and why? | modules/SmartExpenses/README.md: ok ; READER.md: ok |
