<#
.SYNOPSIS
    Run semantic QA benchmark checks on a generated KB.

.DESCRIPTION
    Evaluates canonical, app-understanding questions against expected evidence
    locations and patterns in markdown output. Produces a scored report and
    fails when score/critical criteria are not met.
#>

param(
    [string]$OutputRoot = "mendix-data/knowledge-base",
    [Parameter(Mandatory = $true)]
    [string]$AppName,
    [int]$MinScore = 85
)

$ErrorActionPreference = "Stop"

function Test-EvidenceItem {
    param(
        [string]$KbRoot,
        [hashtable]$Item
    )

    $path = Join-Path $KbRoot $Item.File
    if (-not (Test-Path $path -PathType Leaf)) {
        return [pscustomobject]@{
            File = $Item.File
            Hit = $false
            Reason = "missing file"
        }
    }

    $text = Get-Content -Raw $path
    foreach ($pattern in @($Item.Patterns)) {
        if ($text -notmatch [regex]::Escape([string]$pattern)) {
            return [pscustomobject]@{
                File = $Item.File
                Hit = $false
                Reason = "pattern missing: $pattern"
            }
        }
    }

    return [pscustomobject]@{
        File = $Item.File
        Hit = $true
        Reason = "ok"
    }
}

$kbRoot = Join-Path $OutputRoot $AppName
if (-not (Test-Path $kbRoot -PathType Container)) {
    Write-Error "KB root does not exist: $kbRoot"
    exit 1
}

$checks = @(
    @{
        Id = "Q1"
        Weight = 12
        Critical = $true
        Question = "How is a transaction created and saved?"
        Evidence = @(
            @{ File = "modules/SmartExpenses/FLOWS.md"; Patterns = @("ACT_Transaction_Create", "ACT_Transaction_NewEdit_Save") },
            @{ File = "modules/SmartExpenses/PAGES.md"; Patterns = @("SmartExpenses.Transaction_New", "Page-Flow Links") }
        )
    },
    @{
        Id = "Q2"
        Weight = 12
        Critical = $true
        Question = "Which flows can change SmartExpenses.Transaction and under which role constraints?"
        Evidence = @(
            @{ File = "modules/SmartExpenses/DOMAIN.md"; Patterns = @("SmartExpenses.Transaction", "Entity Lifecycle Matrix") },
            @{ File = "modules/SmartExpenses/FLOWS.md"; Patterns = @("SUB_Transaction_setStatus", "Tier 1 Deep Narratives") },
            @{ File = "app/SECURITY.md"; Patterns = @("SmartExpenses.Transaction", "Role-to-Module-Role Matrix") }
        )
    },
    @{
        Id = "Q3"
        Weight = 10
        Critical = $true
        Question = "What does ImporterHelper call in SmartExpenses?"
        Evidence = @(
            @{ File = "routes/cross-module.md"; Patterns = @("ImporterHelper.ACT_ImportTransaction_AcceptTransactions", "SmartExpenses.SUB_Transaction_setStatus") },
            @{ File = "app/CALL_GRAPH.md"; Patterns = @("ImporterHelper", "SmartExpenses") }
        )
    },
    @{
        Id = "Q4"
        Weight = 9
        Critical = $false
        Question = "Which pages are shown during budget type management?"
        Evidence = @(
            @{ File = "modules/SmartExpenses/PAGES.md"; Patterns = @("BudgetType", "Page-Flow Links") },
            @{ File = "routes/by-page.md"; Patterns = @("SmartExpenses.BudgetType", "Shown by flows") }
        )
    },
    @{
        Id = "Q5"
        Weight = 9
        Critical = $false
        Question = "What entity lifecycle exists for BudgetTerm?"
        Evidence = @(
            @{ File = "modules/SmartExpenses/DOMAIN.md"; Patterns = @("SmartExpenses.BudgetTerm", "Entity Lifecycle Matrix") }
        )
    },
    @{
        Id = "Q6"
        Weight = 9
        Critical = $false
        Question = "Which user roles can access parent home flows/pages?"
        Evidence = @(
            @{ File = "app/SECURITY.md"; Patterns = @("Parent", "Role-to-Module-Role Matrix") },
            @{ File = "routes/by-page.md"; Patterns = @("SmartExpenses.Home_Parent", "SmartExpenses.Parent") }
        )
    },
    @{
        Id = "Q7"
        Weight = 10
        Critical = $true
        Question = "Where is transaction status determined?"
        Evidence = @(
            @{ File = "modules/SmartExpenses/FLOWS.md"; Patterns = @("SUB_Transaction_setStatus", "Tier 1 Deep Narratives") },
            @{ File = "routes/by-flow.md"; Patterns = @("SmartExpenses.SUB_Transaction_setStatus", "Touches Entities") }
        )
    },
    @{
        Id = "Q8"
        Weight = 8
        Critical = $false
        Question = "What scheduled/system automation affects custom modules?"
        Evidence = @(
            @{ File = "modules/SmartExpenses/RESOURCES.md"; Patterns = @("Scheduled Events") },
            @{ File = "routes/cross-module.md"; Patterns = @("Custom-boundary dependency lens") }
        )
    },
    @{
        Id = "Q9"
        Weight = 11
        Critical = $true
        Question = "Which module is the custom orchestration hub?"
        Evidence = @(
            @{ File = "app/CALL_GRAPH.md"; Patterns = @("Custom Module Boundary") },
            @{ File = "routes/cross-module.md"; Patterns = @("Hub/leaf module classification") }
        )
    },
    @{
        Id = "Q10"
        Weight = 10
        Critical = $false
        Question = "What is still unknown and why?"
        Evidence = @(
            @{ File = "modules/SmartExpenses/README.md"; Patterns = @("Top risks/unknowns in model understanding", "Unknown") },
            @{ File = "READER.md"; Patterns = @("Confidence levels", "Unknown") }
        )
    }
)

$results = New-Object System.Collections.Generic.List[object]
$criticalFailures = New-Object System.Collections.Generic.List[string]
$totalScore = 0.0

foreach ($check in $checks) {
    $evidenceResults = @()
    foreach ($item in @($check.Evidence)) {
        $evidenceResults += (Test-EvidenceItem -KbRoot $kbRoot -Item $item)
    }

    $hitCount = @($evidenceResults | Where-Object { $_.Hit }).Count
    $totalItems = @($evidenceResults).Count
    $checkScore = if ($totalItems -gt 0) { [math]::Round(($hitCount / $totalItems) * [double]$check.Weight, 2) } else { 0.0 }
    $passed = ($hitCount -eq $totalItems)

    if ($check.Critical -and -not $passed) {
        $criticalFailures.Add($check.Id) | Out-Null
    }

    $totalScore += $checkScore
    $results.Add([pscustomobject]@{
        Id = $check.Id
        Question = $check.Question
        Critical = [bool]$check.Critical
        HitCount = $hitCount
        EvidenceCount = $totalItems
        Score = $checkScore
        MaxScore = [double]$check.Weight
        Passed = $passed
        Details = ($evidenceResults | ForEach-Object { "$($_.File): $($_.Reason)" }) -join " ; "
    }) | Out-Null
}

$finalScore = [math]::Round($totalScore, 2)
$criticalOk = ($criticalFailures.Count -eq 0)
$scoreOk = ($finalScore -ge $MinScore)
$benchmarkPassed = $criticalOk -and $scoreOk

$reportDir = Join-Path $kbRoot "_reports"
if (-not (Test-Path $reportDir -PathType Container)) {
    New-Item -Path $reportDir -ItemType Directory -Force | Out-Null
}
$reportPath = Join-Path $reportDir "semantic-benchmark.md"

$resultRows = New-Object System.Collections.Generic.List[string]
foreach ($r in $results) {
    $resultRows.Add("| $($r.Id) | $($r.Critical) | $($r.HitCount)/$($r.EvidenceCount) | $($r.Score)/$($r.MaxScore) | $($r.Passed) |") | Out-Null
}

$detailRows = New-Object System.Collections.Generic.List[string]
foreach ($r in $results) {
    $detailRows.Add("| $($r.Id) | $($r.Question) | $($r.Details) |") | Out-Null
}

$report = @"
# Semantic Benchmark Report

## Summary

- App: $AppName
- KB Root: $kbRoot
- Minimum score: $MinScore
- Final score: $finalScore
- Critical checks passed: $criticalOk
- Benchmark passed: $benchmarkPassed
- Generated at: $((Get-Date).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"))

## Scores

| Check | Critical | Evidence hits | Score | Passed |
|---|---|---|---|---|
$($resultRows -join "`n")

## Evidence Details

| Check | Question | Evidence evaluation |
|---|---|---|
$($detailRows -join "`n")
"@

$utf8NoBom = New-Object System.Text.UTF8Encoding($false)
[System.IO.File]::WriteAllText($reportPath, $report.TrimEnd() + "`n", $utf8NoBom)

Write-Host ""
Write-Host "=== Semantic Benchmark: $AppName ==="
Write-Host "Score: $finalScore / 100 (min $MinScore)"
Write-Host "Critical failures: $($criticalFailures.Count)"
Write-Host "Report: $reportPath"

if (-not $benchmarkPassed) {
    if ($criticalFailures.Count -gt 0) {
        Write-Host "Failed critical checks: $($criticalFailures -join ', ')" -ForegroundColor Red
    }
    exit 1
}

Write-Host "Semantic benchmark passed." -ForegroundColor Green
exit 0
