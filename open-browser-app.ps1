# open-browser-app.ps1
# Starts the AutoCommitMessage standalone web version and opens the browser.
# Works in two modes:
#   - Artifact mode: runs the pre-built exe from .\app\
#   - Source mode:   builds and runs from source (requires .NET SDK)
#
# Usage: .\open-browser-app.ps1 [--path "C:\Projects\MyMendixApp"] [--port 3109]

param(
    [string]$Path = "",
    [int]$Port = 3109
)

$ErrorActionPreference = "Stop"
$url = "http://localhost:$Port"
$appDir = Join-Path $PSScriptRoot "app"
$standaloneDir = Join-Path $PSScriptRoot "standalone"
$exe = Join-Path $appDir "AutoCommitMessage.Standalone.exe"

# Build extra args shared between both modes
$extraArgs = @()
if ($Port -ne 3109)  { $extraArgs += "--port"; $extraArgs += "$Port" }
if ($Path -ne "")    { $extraArgs += "--path"; $extraArgs += $Path }

if (Test-Path $exe) {
    # --- Artifact mode: run pre-built binary ---
    Write-Host "Starting AutoCommitMessage (artifact) at $url ..."
    $proc = Start-Process -FilePath $exe `
        -ArgumentList $extraArgs `
        -WorkingDirectory $PSScriptRoot `
        -PassThru -NoNewWindow
} else {
    # --- Source mode: build then run ---
    Write-Host "Building standalone..."
    dotnet build "$standaloneDir" -c Release --nologo -v quiet
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Build failed. Check errors above."
        exit 1
    }

    Write-Host "Starting AutoCommitMessage (source) at $url ..."
    $dotnetArgs = @("run", "--project", $standaloneDir, "--no-build", "-c", "Release", "--") + $extraArgs
    $proc = Start-Process -FilePath "dotnet" `
        -ArgumentList $dotnetArgs `
        -WorkingDirectory $PSScriptRoot `
        -PassThru -NoNewWindow
}

# Wait for the server to become ready
$ready = $false
for ($i = 0; $i -lt 20; $i++) {
    Start-Sleep -Milliseconds 500
    try {
        $null = Invoke-WebRequest -Uri $url -UseBasicParsing -TimeoutSec 1 -ErrorAction Stop
        $ready = $true
        break
    } catch { }
}

if ($ready) {
    Start-Process $url
    Write-Host "Browser opened. Server running (PID $($proc.Id))."
} else {
    Write-Host "Server may still be starting. Open $url manually."
}

Write-Host "Press Ctrl+C or close this window to stop the server."
$proc.WaitForExit()
