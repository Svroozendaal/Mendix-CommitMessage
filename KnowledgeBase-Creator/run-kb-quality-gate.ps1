<#
.SYNOPSIS
    Validate Knowledge Base document quality and structure contracts.

.DESCRIPTION
    Complements run-kb-scaffold.ps1 by checking content quality, not only file existence.
    Fails when required headings, sections, or links are missing.

.PARAMETER OutputRoot
    Root folder for knowledge bases. Default: mendix-data/knowledge-base

.PARAMETER AppName
    Name of the KB to validate.

.EXAMPLE
    .\run-kb-quality-gate.ps1 -AppName SmartExpenses
#>

param(
    [string]$OutputRoot = "mendix-data/knowledge-base",
    [string]$AppName
)

$ErrorActionPreference = "Stop"

if (-not $AppName) {
    Write-Error "AppName is required."
    exit 1
}

$kbRoot = Join-Path $OutputRoot $AppName
if (-not (Test-Path $kbRoot)) {
    Write-Error "KB root does not exist: $kbRoot"
    exit 1
}

$issues = New-Object System.Collections.Generic.List[object]

function Add-Issue {
    param(
        [string]$Severity,
        [string]$File,
        [string]$Message
    )

    $issues.Add([pscustomobject]@{
        Severity = $Severity
        File = $File
        Message = $Message
    }) | Out-Null
}

function Assert-Headings {
    param(
        [string]$File,
        [string[]]$Headings
    )

    if (-not (Test-Path $File)) {
        Add-Issue -Severity "error" -File $File -Message "Missing file."
        return
    }

    $text = Get-Content -Raw $File
    foreach ($h in $Headings) {
        if ($text -notmatch [regex]::Escape($h)) {
            Add-Issue -Severity "error" -File $File -Message "Missing required heading: $h"
        }
    }
}

function Check-Links {
    param([string]$File)

    if (-not (Test-Path $File)) { return }
    $text = Get-Content -Raw $File
    $matches = [regex]::Matches($text, "\[[^\]]+\]\(([^)]+)\)")
    foreach ($m in $matches) {
        $target = $m.Groups[1].Value
        if ($target -match "^(https?|mailto):") { continue }
        if ($target.StartsWith("#")) { continue }

        $clean = $target.Split("#")[0]
        if ([string]::IsNullOrWhiteSpace($clean)) { continue }

        $resolved = Join-Path (Split-Path -Parent $File) $clean
        if (-not (Test-Path $resolved)) {
            Add-Issue -Severity "error" -File $File -Message "Broken relative link: $target"
        }
    }
}

# Root and app-level files
$readerFile = Join-Path $kbRoot "READER.md"
$routingFile = Join-Path $kbRoot "ROUTING.md"
$appFolder = Join-Path $kbRoot "app"

Assert-Headings -File $readerFile -Headings @(
    "# How to Read This Knowledge Base",
    "## What is this?",
    "## How to navigate",
    "## How to answer questions",
    "## Confidence levels",
    "## Source"
)

Assert-Headings -File $routingFile -Headings @(
    "# Knowledge Base Routing",
    "## Quick lookup",
    "## Module index",
    "## Completeness",
    "## Source"
)

if (Test-Path $routingFile) {
    $routing = Get-Content -Raw $routingFile
    $placeholderLinkMatches = [regex]::Matches($routing, "\[[^\]]+\]\((modules/(X|<[^>]+>)/[^)]+)\)")
    foreach ($pm in $placeholderLinkMatches) {
        Add-Issue -Severity "error" -File $routingFile -Message "Placeholder module link found: $($pm.Groups[1].Value)"
    }

    if ($routing -match "modules/(X|<[^>]+>)/") {
        Add-Issue -Severity "error" -File $routingFile -Message "Placeholder module path token found (X or <...>)."
    }
}

$appFiles = @(
    (Join-Path $appFolder "APP_OVERVIEW.md"),
    (Join-Path $appFolder "MODULE_LANDSCAPE.md"),
    (Join-Path $appFolder "SECURITY.md"),
    (Join-Path $appFolder "CALL_GRAPH.md")
)

foreach ($af in $appFiles) {
    if (-not (Test-Path $af)) {
        Add-Issue -Severity "error" -File $af -Message "Missing app-level file."
        continue
    }

    $t = Get-Content -Raw $af
    if ($t -notmatch "Export-backed|Inferred|Unknown") {
        Add-Issue -Severity "error" -File $af -Message "Missing confidence markers (Export-backed/Inferred/Unknown)."
    }
}

# Module-level contracts
$modulesDir = Join-Path $kbRoot "modules"
if (-not (Test-Path $modulesDir)) {
    Add-Issue -Severity "error" -File $modulesDir -Message "Missing modules directory."
}
else {
    $moduleDirs = Get-ChildItem $modulesDir -Directory | Sort-Object Name
    foreach ($mod in $moduleDirs) {
        $readme = Join-Path $mod.FullName "README.md"
        $domain = Join-Path $mod.FullName "DOMAIN.md"
        $flows = Join-Path $mod.FullName "FLOWS.md"
        $pages = Join-Path $mod.FullName "PAGES.md"
        $resources = Join-Path $mod.FullName "RESOURCES.md"

        Assert-Headings -File $readme -Headings @(
            "## Summary",
            "## Purpose",
            "## Navigation",
            "## Cross-Module Dependencies",
            "## Source"
        )

        if (Test-Path $readme) {
            $rt = Get-Content -Raw $readme
            if ($rt -notmatch "Shared entities via associations:") {
                Add-Issue -Severity "error" -File $readme -Message "Missing shared-entities dependency line."
            }
        }

        Assert-Headings -File $domain -Headings @(
            "## Entities",
            "## Associations",
            "## Enumerations"
        )

        Assert-Headings -File $flows -Headings @(
            "## Flow Catalogue",
            "### Action Flows (ACT_*)",
            "### Data Sources (DS_*)",
            "### Validation Flows (VAL_*)",
            "### Other Flows",
            "## Cross-Module Calls",
            "## Flow Details"
        )

        Assert-Headings -File $pages -Headings @(
            "## Page Inventory",
            "## Page-Flow Links",
            "## Snippets"
        )

        Assert-Headings -File $resources -Headings @(
            "## Constants",
            "## Scheduled Events",
            "## Other Resources"
        )
    }
}

# Routing index contracts
$routesDir = Join-Path $kbRoot "routes"
Assert-Headings -File (Join-Path $routesDir "by-entity.md") -Headings @("# Entity Index")
Assert-Headings -File (Join-Path $routesDir "by-page.md") -Headings @("# Page Index")
Assert-Headings -File (Join-Path $routesDir "by-flow.md") -Headings @("# Flow Index")
Assert-Headings -File (Join-Path $routesDir "cross-module.md") -Headings @("# Cross-Module Dependencies")

# Link validation on all markdown files
$allMd = Get-ChildItem -Recurse -File $kbRoot -Filter *.md | Where-Object { $_.FullName -notmatch "[\\/]_artifacts[\\/]" }
foreach ($f in $allMd) {
    Check-Links -File $f.FullName
}

Write-Host ""
Write-Host "=== KB Quality Gate: $AppName ==="
Write-Host "Root: $kbRoot"
Write-Host "Files checked: $($allMd.Count)"
Write-Host "Issues: $($issues.Count)"

if ($issues.Count -gt 0) {
    Write-Host ""
    Write-Host "QUALITY ISSUES:" -ForegroundColor Red
    foreach ($i in $issues) {
        Write-Host "[$($i.Severity)] $($i.File) :: $($i.Message)" -ForegroundColor Red
    }
    exit 1
}
else {
    Write-Host ""
    Write-Host "Quality gate passed." -ForegroundColor Green
    exit 0
}
