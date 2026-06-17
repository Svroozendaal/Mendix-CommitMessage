# open-browser-app.ps1
# Builds and starts the AutoCommitMessage standalone web version, then opens the browser.
# For developers working from source. End users should run the published
# AutoCommitMessage.Standalone.exe (see ACM/applicatiefolder/dist-README.txt).
#
# Usage: .\open-browser-app.ps1 [--path "C:\Projects\MyMendixApp"] [--port 3109]

param(
    [string]$Path = "",
    [int]$Port = 3109
)

$ErrorActionPreference = "Stop"
$url = "http://localhost:$Port"
$appDir = Join-Path $PSScriptRoot "ACM/applicatiefolder"

Write-Host "Building standalone..."
dotnet build "$appDir" -c Release --nologo -v quiet
if ($LASTEXITCODE -ne 0) {
    Write-Error "Build failed. Check errors above."
    exit 1
}

Write-Host "Starting AutoCommitMessage at $url ..."

# --no-browser: the exe can open a browser itself, but this script opens it below
# (after polling for readiness), so suppress the duplicate tab.
$dotnetArgs = @("run", "--project", $appDir, "--no-build", "-c", "Release", "--", "--no-browser")
if ($Port -ne 3109) { $dotnetArgs += "--port"; $dotnetArgs += "$Port" }
if ($Path -ne "") { $dotnetArgs += "--path"; $dotnetArgs += $Path }

$proc = Start-Process -FilePath "dotnet" `
    -ArgumentList $dotnetArgs `
    -WorkingDirectory $PSScriptRoot `
    -PassThru -NoNewWindow

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
