[CmdletBinding()]
param(
    [string]$AppPath,
    [string]$StudioProPath
)

$ErrorActionPreference = 'Stop'

function Read-DotEnv {
    param([string]$Path)

    $result = @{}
    if (-not (Test-Path $Path -PathType Leaf)) {
        return $result
    }

    foreach ($rawLine in Get-Content -Path $Path) {
        $line = $rawLine.Trim()
        if ([string]::IsNullOrWhiteSpace($line) -or $line.StartsWith('#')) {
            continue
        }

        $separatorIndex = $line.IndexOf('=')
        if ($separatorIndex -lt 1) {
            continue
        }

        $key = $line.Substring(0, $separatorIndex).Trim()
        if ([string]::IsNullOrWhiteSpace($key)) {
            continue
        }

        $value = $line.Substring($separatorIndex + 1).Trim()
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

function Get-VersionFromPath {
    param([string]$Path)

    $match = [regex]::Match($Path, '\\(?<version>\d+\.\d+\.\d+\.\d+)\\')
    if ($match.Success) {
        try {
            return [version]$match.Groups['version'].Value
        } catch {
            return [version]'0.0.0.0'
        }
    }

    return [version]'0.0.0.0'
}

function Resolve-StudioProExe {
    param([string]$ConfiguredPath)

    if (-not [string]::IsNullOrWhiteSpace($ConfiguredPath)) {
        $resolvedConfiguredPath = [System.IO.Path]::GetFullPath($ConfiguredPath)
        if (-not (Test-Path $resolvedConfiguredPath -PathType Leaf)) {
            throw "Configured Studio Pro executable not found: $resolvedConfiguredPath"
        }

        return $resolvedConfiguredPath
    }

    $candidates = New-Object System.Collections.Generic.List[string]

    $studioProFromPath = Get-Command studiopro.exe -ErrorAction SilentlyContinue
    if ($null -ne $studioProFromPath -and -not [string]::IsNullOrWhiteSpace($studioProFromPath.Source)) {
        $candidates.Add($studioProFromPath.Source)
    }

    $mxFromPath = Get-Command mx.exe -ErrorAction SilentlyContinue
    if ($null -ne $mxFromPath -and -not [string]::IsNullOrWhiteSpace($mxFromPath.Source)) {
        $mxDirectory = Split-Path -Parent $mxFromPath.Source
        $candidates.Add((Join-Path $mxDirectory 'studiopro.exe'))
        $candidates.Add((Join-Path (Split-Path -Parent $mxDirectory) 'studiopro.exe'))
    }

    foreach ($hiveName in @('LocalMachine', 'CurrentUser')) {
        foreach ($viewName in @('Registry64', 'Registry32')) {
            try {
                $baseKey = [Microsoft.Win32.RegistryKey]::OpenBaseKey(
                    [Microsoft.Win32.RegistryHive]::$hiveName,
                    [Microsoft.Win32.RegistryView]::$viewName
                )
            } catch {
                continue
            }

            if ($null -eq $baseKey) {
                continue
            }

            try {
                $studioProRoot = $baseKey.OpenSubKey('SOFTWARE\Mendix\Studio Pro')
                if ($null -eq $studioProRoot) {
                    continue
                }

                foreach ($versionKeyName in $studioProRoot.GetSubKeyNames()) {
                    $versionKey = $studioProRoot.OpenSubKey($versionKeyName)
                    if ($null -eq $versionKey) {
                        continue
                    }

                    $installLocation = $versionKey.GetValue('InstallLocation')
                    if ($installLocation -is [string] -and -not [string]::IsNullOrWhiteSpace($installLocation)) {
                        $candidates.Add((Join-Path $installLocation 'studiopro.exe'))
                        $candidates.Add((Join-Path $installLocation 'modeler\studiopro.exe'))
                    }
                }
            } finally {
                $baseKey.Dispose()
            }
        }
    }

    foreach ($programFilesRoot in @(
        [Environment]::GetFolderPath([Environment+SpecialFolder]::ProgramFiles),
        [Environment]::GetFolderPath([Environment+SpecialFolder]::ProgramFilesX86)
    )) {
        if ([string]::IsNullOrWhiteSpace($programFilesRoot)) {
            continue
        }

        $mendixRoot = Join-Path $programFilesRoot 'Mendix'
        if (-not (Test-Path $mendixRoot -PathType Container)) {
            continue
        }

        foreach ($versionDirectory in Get-ChildItem -Path $mendixRoot -Directory -ErrorAction SilentlyContinue) {
            $candidates.Add((Join-Path $versionDirectory.FullName 'modeler\studiopro.exe'))
            $candidates.Add((Join-Path $versionDirectory.FullName 'studiopro.exe'))
        }
    }

    $resolvedCandidate = $candidates |
        Where-Object { -not [string]::IsNullOrWhiteSpace($_) } |
        ForEach-Object { [System.IO.Path]::GetFullPath($_) } |
        Where-Object { Test-Path $_ -PathType Leaf } |
        Select-Object -Unique |
        Sort-Object -Property @{ Expression = { Get-VersionFromPath -Path $_ }; Descending = $true } |
        Select-Object -First 1

    if ($null -eq $resolvedCandidate -or [string]::IsNullOrWhiteSpace($resolvedCandidate)) {
        throw "Could not locate studiopro.exe. Set MENDIX_STUDIOPRO_EXE in .env or pass -StudioProPath."
    }

    return $resolvedCandidate
}

$repoRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$dotEnv = Read-DotEnv -Path (Join-Path $repoRoot '.env')

if (-not $PSBoundParameters.ContainsKey('AppPath') -and $dotEnv.ContainsKey('MENDIX_APP_PATH')) {
    $AppPath = $dotEnv['MENDIX_APP_PATH']
}

if (-not $PSBoundParameters.ContainsKey('StudioProPath') -and $dotEnv.ContainsKey('MENDIX_STUDIOPRO_EXE')) {
    $StudioProPath = $dotEnv['MENDIX_STUDIOPRO_EXE']
}

if ([string]::IsNullOrWhiteSpace($AppPath)) {
    $AppPath = 'C:\MendixWorkers\Smart Expenses app-main'
}

$AppPath = [System.IO.Path]::GetFullPath($AppPath)

if (-not (Test-Path $AppPath -PathType Container)) {
    throw "Mendix app path not found: $AppPath"
}

$mprFile = Get-ChildItem -Path $AppPath -Filter '*.mpr' -File | Select-Object -First 1
if ($null -eq $mprFile) {
    throw "No .mpr file found in app path: $AppPath"
}

$studioProExe = Resolve-StudioProExe -ConfiguredPath $StudioProPath
$escapedMprPath = $mprFile.FullName.Replace('"', '""')
$arguments = "--enable-extension-development `"$escapedMprPath`""

Start-Process -FilePath $studioProExe -ArgumentList $arguments -WorkingDirectory $AppPath

Write-Host "Opened Mendix app with extension development enabled: $($mprFile.FullName)" -ForegroundColor Green
Write-Host "Studio Pro: $studioProExe"
