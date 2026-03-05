[CmdletBinding()]
param(
    [switch]$OpenOutput
)

$ErrorActionPreference = "Stop"

$packageRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$envPath = Join-Path $packageRoot ".env"
$artifactsRoot = Join-Path $packageRoot "artifacts"

function Read-DotEnv {
    param([string]$Path)
    $result = @{}
    if (-not (Test-Path $Path -PathType Leaf)) { return $result }

    foreach ($rawLine in Get-Content -Path $Path) {
        $line = $rawLine.Trim()
        if ([string]::IsNullOrWhiteSpace($line) -or $line.StartsWith("#")) { continue }

        $separator = $line.IndexOf("=")
        if ($separator -lt 1) { continue }

        $key = $line.Substring(0, $separator).Trim()
        $value = $line.Substring($separator + 1).Trim()
        if ([string]::IsNullOrWhiteSpace($key)) { continue }

        if (
            $value.Length -ge 2 -and (
                ($value.StartsWith('"') -and $value.EndsWith('"')) -or
                ($value.StartsWith("'") -and $value.EndsWith("'"))
            )
        ) {
            $value = $value.Substring(1, $value.Length - 2)
        }

        $result[$key] = $value
    }

    return $result
}

function Get-Setting {
    param(
        [hashtable]$Settings,
        [string]$Key,
        [string]$Default = ""
    )

    if (-not $Settings.ContainsKey($Key)) { return $Default }
    $value = $Settings[$Key]
    if ([string]::IsNullOrWhiteSpace($value)) { return $Default }
    return $value.Trim()
}

function Get-RequiredSetting {
    param(
        [hashtable]$Settings,
        [string]$Key
    )

    $value = Get-Setting -Settings $Settings -Key $Key
    if ([string]::IsNullOrWhiteSpace($value)) {
        throw "Missing required setting '$Key' in $envPath"
    }
    return $value
}

function Get-BoolSetting {
    param(
        [hashtable]$Settings,
        [string]$Key,
        [bool]$Default = $false
    )

    $value = Get-Setting -Settings $Settings -Key $Key
    if ([string]::IsNullOrWhiteSpace($value)) { return $Default }

    switch ($value.ToLowerInvariant()) {
        "1" { return $true }
        "true" { return $true }
        "yes" { return $true }
        "y" { return $true }
        "0" { return $false }
        "false" { return $false }
        "no" { return $false }
        "n" { return $false }
        default { return $Default }
    }
}

function Resolve-MxExe {
    param([hashtable]$Settings)

    $explicit = Get-Setting -Settings $Settings -Key "MENDIX_MX_EXE"
    if (-not [string]::IsNullOrWhiteSpace($explicit)) {
        if (-not (Test-Path $explicit -PathType Leaf)) {
            throw "MENDIX_MX_EXE does not exist: $explicit"
        }
        return (Resolve-Path $explicit).Path
    }

    $studioPath = Get-RequiredSetting -Settings $Settings -Key "MENDIX_STUDIO_PRO_PATH"
    $candidates = @(
        (Join-Path $studioPath "modeler\mx.exe"),
        (Join-Path $studioPath "mx.exe")
    )

    foreach ($candidate in $candidates) {
        if (Test-Path $candidate -PathType Leaf) {
            return (Resolve-Path $candidate).Path
        }
    }

    throw "Could not find mx.exe under MENDIX_STUDIO_PRO_PATH: $studioPath"
}

function Resolve-MprPath {
    param(
        [string]$AppPath,
        [string]$ExplicitMpr
    )

    if (-not [string]::IsNullOrWhiteSpace($ExplicitMpr)) {
        if (-not (Test-Path $ExplicitMpr -PathType Leaf)) {
            throw "MENDIX_MPR_PATH does not exist: $ExplicitMpr"
        }
        return (Resolve-Path $ExplicitMpr).Path
    }

    if (Test-Path $AppPath -PathType Leaf) {
        if ([IO.Path]::GetExtension($AppPath).ToLowerInvariant() -ne ".mpr") {
            throw "MENDIX_APP_PATH points to a file that is not .mpr: $AppPath"
        }
        return (Resolve-Path $AppPath).Path
    }

    if (-not (Test-Path $AppPath -PathType Container)) {
        throw "MENDIX_APP_PATH does not exist: $AppPath"
    }

    $mprFiles = Get-ChildItem -Path $AppPath -File -Filter *.mpr | Sort-Object Name
    if ($mprFiles.Count -eq 0) {
        throw "No .mpr file found in app folder: $AppPath"
    }
    if ($mprFiles.Count -gt 1) {
        $names = $mprFiles | ForEach-Object { $_.Name }
        throw "Multiple .mpr files found in ${AppPath}: $($names -join ', '). Set MENDIX_MPR_PATH in .env."
    }

    return $mprFiles[0].FullName
}

function Sanitize-Token {
    param([string]$Value)

    if ([string]::IsNullOrWhiteSpace($Value)) { return "app" }

    $invalid = [IO.Path]::GetInvalidFileNameChars()
    $builder = New-Object System.Text.StringBuilder
    foreach ($char in $Value.ToCharArray()) {
        if ($invalid -contains $char) {
            [void]$builder.Append("_")
        } else {
            [void]$builder.Append($char)
        }
    }

    $token = $builder.ToString().Trim()
    if ([string]::IsNullOrWhiteSpace($token)) { return "app" }
    return $token
}

function Get-ModulesFromManifest {
    param([string]$ManifestPath)

    $manifest = Get-Content -Raw $ManifestPath | ConvertFrom-Json
    $modules = @()

    foreach ($artifact in @($manifest.artifacts)) {
        if ($artifact.type -ne "module-domain-model-json") { continue }
        if ($artifact.path -match "[\\/]modules(?:[\\/]marketplace)?[\\/]([^\\/]+)[\\/]domain-model\.json$") {
            $modules += $matches[1]
        }
    }

    return @($modules | Sort-Object -Unique)
}

function Apply-Template {
    param(
        [string]$TemplatePath,
        [string]$TargetPath,
        [hashtable]$Tokens
    )

    $content = Get-Content -Raw $TemplatePath
    foreach ($key in $Tokens.Keys) {
        $content = $content.Replace("{{${key}}}", [string]$Tokens[$key])
    }

    $directory = Split-Path -Parent $TargetPath
    if (-not (Test-Path $directory)) {
        New-Item -Path $directory -ItemType Directory -Force | Out-Null
    }

    Set-Content -Path $TargetPath -Value $content -Encoding UTF8
}

function Get-ModuleIndexRows {
    param([string[]]$Modules)

    if ($Modules.Count -eq 0) {
        return "| none | Unknown | none |"
    }

    $rows = @()
    foreach ($module in $Modules) {
        $rows += "| $module | Unknown | [README](modules/$module/README.md) |"
    }

    return ($rows -join "`n")
}

if (-not (Test-Path $envPath -PathType Leaf)) {
    throw "Missing .env file: $envPath"
}

$settings = Read-DotEnv -Path $envPath
$mxExe = Resolve-MxExe -Settings $settings
$appPath = Get-RequiredSetting -Settings $settings -Key "MENDIX_APP_PATH"
$explicitMprPath = Get-Setting -Settings $settings -Key "MENDIX_MPR_PATH"
$mprPath = Resolve-MprPath -AppPath $appPath -ExplicitMpr $explicitMprPath

$configuredDataRoot = Get-Setting -Settings $settings -Key "MENDIX_DATA_ROOT"
$dataRoot = if ([string]::IsNullOrWhiteSpace($configuredDataRoot)) {
    Join-Path (Split-Path -Parent $packageRoot) "mendix-data"
} else {
    $configuredDataRoot
}

$configuredAppName = Get-Setting -Settings $settings -Key "APP_NAME"
$appName = if ([string]::IsNullOrWhiteSpace($configuredAppName)) {
    [IO.Path]::GetFileNameWithoutExtension($mprPath)
} else {
    $configuredAppName
}

$moduleFilter = Get-Setting -Settings $settings -Key "MENDIX_MODULES" -Default "*"
$strictQuality = Get-BoolSetting -Settings $settings -Key "STRICT_QUALITY_GATE" -Default $false

$parserExe = Join-Path $packageRoot "Mendix-model-overview-parser\bin\win-x64\ModelOverviewCli.exe"
$parserSourceProject = Join-Path $packageRoot "Mendix-model-overview-parser\src\model-overview-cli\ModelOverviewCli.csproj"
$scaffoldScript = Join-Path $packageRoot "run-kb-scaffold.ps1"
$qualityGateScript = Join-Path $packageRoot "run-kb-quality-gate.ps1"

if (-not (Test-Path $scaffoldScript -PathType Leaf)) {
    throw "Missing scaffold script: $scaffoldScript"
}
if (-not (Test-Path $qualityGateScript -PathType Leaf)) {
    throw "Missing quality gate script: $qualityGateScript"
}

$dumpsRoot = Join-Path $dataRoot "dumps"
$appOverviewRoot = Join-Path $dataRoot "app-overview"
$knowledgeBaseRoot = Join-Path $dataRoot "knowledge-base"

New-Item -ItemType Directory -Path $dumpsRoot -Force | Out-Null
New-Item -ItemType Directory -Path $appOverviewRoot -Force | Out-Null
New-Item -ItemType Directory -Path $knowledgeBaseRoot -Force | Out-Null

$timestamp = (Get-Date).ToUniversalTime().ToString("yyyy-MM-ddTHH-mm-ss.fffZ")
$appToken = Sanitize-Token -Value $appName
$dumpFolder = Join-Path $dumpsRoot "${timestamp}_$appToken"
$dumpPath = Join-Path $dumpFolder "working-dump.json"
$runFolder = Join-Path $appOverviewRoot "cli_$timestamp"

New-Item -ItemType Directory -Path $dumpFolder -Force | Out-Null

Write-Host ""
Write-Host "=== KnowledgeBase Creator ===" -ForegroundColor Cyan
Write-Host "mx.exe:   $mxExe"
Write-Host "mpr:      $mprPath"
Write-Host "dump:     $dumpPath"
Write-Host "overview: $runFolder"
Write-Host "kb root:  $knowledgeBaseRoot"

Write-Host ""
Write-Host "[1/5] Dumping .mpr..." -ForegroundColor Yellow
& $mxExe dump-mpr $mprPath --output-file $dumpPath
if ($LASTEXITCODE -ne 0) {
    throw "mx dump-mpr failed with exit code $LASTEXITCODE"
}

Write-Host "[2/5] Building app-overview export..." -ForegroundColor Yellow
if (Test-Path $parserExe -PathType Leaf) {
    $args = @("--dump", $dumpPath, "--output", $runFolder)
    if (-not [string]::IsNullOrWhiteSpace($moduleFilter) -and $moduleFilter -ne "*") {
        $args += @("--modules", $moduleFilter)
    }
    & $parserExe @args
    if ($LASTEXITCODE -ne 0) {
        throw "Parser executable failed with exit code $LASTEXITCODE"
    }
}
elseif (Test-Path $parserSourceProject -PathType Leaf) {
    $args = @("run", "--project", $parserSourceProject, "--configuration", "Release", "--", "--dump", $dumpPath, "--output", $runFolder)
    if (-not [string]::IsNullOrWhiteSpace($moduleFilter) -and $moduleFilter -ne "*") {
        $args += @("--modules", $moduleFilter)
    }
    & dotnet @args
    if ($LASTEXITCODE -ne 0) {
        throw "Parser fallback (dotnet run) failed with exit code $LASTEXITCODE"
    }
}
else {
    throw "No parser binary or source project found in package."
}

$manifestPath = Join-Path $runFolder "manifest.json"
if (-not (Test-Path $manifestPath -PathType Leaf)) {
    throw "Parser output manifest missing: $manifestPath"
}

$modules = Get-ModulesFromManifest -ManifestPath $manifestPath
$kbRoot = Join-Path $knowledgeBaseRoot $appName

Write-Host "[3/5] Scaffolding knowledge-base..." -ForegroundColor Yellow
& powershell -NoProfile -ExecutionPolicy Bypass -File $scaffoldScript -RunFolder $runFolder -OutputRoot $knowledgeBaseRoot -AppName $appName
if ($LASTEXITCODE -ne 0) {
    throw "run-kb-scaffold.ps1 failed with exit code $LASTEXITCODE"
}

Write-Host "[4/5] Seeding KB templates..." -ForegroundColor Yellow
$generatedAt = (Get-Date).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ")
$commonTokens = @{
    APP_NAME = $appName
    GENERATED_AT_UTC = $generatedAt
    RUN_FOLDER = $runFolder
    MODULE_COUNT = [string]$modules.Count
    MODULE_INDEX_ROWS = (Get-ModuleIndexRows -Modules $modules)
}

$artifactDrop = Join-Path $kbRoot "_artifacts"
New-Item -ItemType Directory -Path $artifactDrop -Force | Out-Null
Get-ChildItem -Path $artifactsRoot -File -Filter *.md | ForEach-Object {
    Copy-Item -Path $_.FullName -Destination (Join-Path $artifactDrop $_.Name) -Force
}

Apply-Template -TemplatePath (Join-Path $artifactsRoot "KNOWLEDGEBASE_READER.md") -TargetPath (Join-Path $kbRoot "READER.md") -Tokens $commonTokens
Apply-Template -TemplatePath (Join-Path $artifactsRoot "ROUTING_TEMPLATE.md") -TargetPath (Join-Path $kbRoot "ROUTING.md") -Tokens $commonTokens

Apply-Template -TemplatePath (Join-Path $artifactsRoot "APP_OVERVIEW_TEMPLATE.md") -TargetPath (Join-Path $kbRoot "app/APP_OVERVIEW.md") -Tokens $commonTokens
Apply-Template -TemplatePath (Join-Path $artifactsRoot "MODULE_LANDSCAPE_TEMPLATE.md") -TargetPath (Join-Path $kbRoot "app/MODULE_LANDSCAPE.md") -Tokens $commonTokens
Apply-Template -TemplatePath (Join-Path $artifactsRoot "SECURITY_TEMPLATE.md") -TargetPath (Join-Path $kbRoot "app/SECURITY.md") -Tokens $commonTokens
Apply-Template -TemplatePath (Join-Path $artifactsRoot "CALL_GRAPH_TEMPLATE.md") -TargetPath (Join-Path $kbRoot "app/CALL_GRAPH.md") -Tokens $commonTokens

Apply-Template -TemplatePath (Join-Path $artifactsRoot "ROUTE_BY_ENTITY_TEMPLATE.md") -TargetPath (Join-Path $kbRoot "routes/by-entity.md") -Tokens $commonTokens
Apply-Template -TemplatePath (Join-Path $artifactsRoot "ROUTE_BY_PAGE_TEMPLATE.md") -TargetPath (Join-Path $kbRoot "routes/by-page.md") -Tokens $commonTokens
Apply-Template -TemplatePath (Join-Path $artifactsRoot "ROUTE_BY_FLOW_TEMPLATE.md") -TargetPath (Join-Path $kbRoot "routes/by-flow.md") -Tokens $commonTokens
Apply-Template -TemplatePath (Join-Path $artifactsRoot "ROUTE_CROSS_MODULE_TEMPLATE.md") -TargetPath (Join-Path $kbRoot "routes/cross-module.md") -Tokens $commonTokens

foreach ($module in $modules) {
    $moduleTokens = @{}
    foreach ($k in $commonTokens.Keys) { $moduleTokens[$k] = $commonTokens[$k] }
    $moduleTokens["MODULE_NAME"] = $module

    $moduleDir = Join-Path $kbRoot "modules/$module"
    New-Item -ItemType Directory -Path $moduleDir -Force | Out-Null

    Apply-Template -TemplatePath (Join-Path $artifactsRoot "MODULE_README_TEMPLATE.md") -TargetPath (Join-Path $moduleDir "README.md") -Tokens $moduleTokens
    Apply-Template -TemplatePath (Join-Path $artifactsRoot "MODULE_DOMAIN_TEMPLATE.md") -TargetPath (Join-Path $moduleDir "DOMAIN.md") -Tokens $moduleTokens
    Apply-Template -TemplatePath (Join-Path $artifactsRoot "MODULE_FLOWS_TEMPLATE.md") -TargetPath (Join-Path $moduleDir "FLOWS.md") -Tokens $moduleTokens
    Apply-Template -TemplatePath (Join-Path $artifactsRoot "MODULE_PAGES_TEMPLATE.md") -TargetPath (Join-Path $moduleDir "PAGES.md") -Tokens $moduleTokens
    Apply-Template -TemplatePath (Join-Path $artifactsRoot "MODULE_RESOURCES_TEMPLATE.md") -TargetPath (Join-Path $moduleDir "RESOURCES.md") -Tokens $moduleTokens
}

Write-Host "[5/5] Running validation..." -ForegroundColor Yellow
& powershell -NoProfile -ExecutionPolicy Bypass -File $scaffoldScript -Validate -OutputRoot $knowledgeBaseRoot -AppName $appName
if ($LASTEXITCODE -ne 0) {
    throw "Scaffold validation failed with exit code $LASTEXITCODE"
}

& powershell -NoProfile -ExecutionPolicy Bypass -File $qualityGateScript -OutputRoot $knowledgeBaseRoot -AppName $appName
if ($LASTEXITCODE -ne 0) {
    if ($strictQuality) {
        throw "Quality gate failed with exit code $LASTEXITCODE"
    }
    Write-Warning "Quality gate failed. Set STRICT_QUALITY_GATE=true in .env to fail hard."
}

Write-Host ""
Write-Host "Completed." -ForegroundColor Green
Write-Host "Overview run: $runFolder"
Write-Host "KB folder:    $kbRoot"
Write-Host "Start AI with: agents.md"

if ($OpenOutput -and (Test-Path $kbRoot -PathType Container)) {
    explorer.exe $kbRoot
}
