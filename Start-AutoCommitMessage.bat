@echo off
setlocal enabledelayedexpansion
cd /d "%~dp0"

set PORT=3109
set EXE=app\AutoCommitMessage.Standalone.exe

if not exist "%EXE%" (
    echo ERROR: %EXE% not found.
    echo Make sure you extracted the full artifact zip.
    pause
    exit /b 1
)

echo Starting AutoCommitMessage at http://localhost:%PORT% ...
start "" /B "%EXE%"

:: Wait for the server to become available (max 10 seconds)
set READY=0
for /L %%i in (1,1,20) do (
    if !READY!==0 (
        timeout /t 1 /nobreak >nul
        powershell -NoProfile -Command "try { Invoke-WebRequest http://localhost:%PORT% -UseBasicParsing -TimeoutSec 1 -EA Stop | Out-Null; exit 0 } catch { exit 1 }" >nul 2>&1
        if !errorlevel!==0 set READY=1
    )
)

if !READY!==1 (
    start http://localhost:%PORT%
    echo Browser opened. Server is running.
) else (
    echo Server may still be starting. Open http://localhost:%PORT% manually.
)

echo.
echo Press any key to stop the server and close.
pause >nul

taskkill /F /IM AutoCommitMessage.Standalone.exe >nul 2>&1
