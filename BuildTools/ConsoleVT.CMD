@echo off
setlocal

:: ============================================================================
:: ConsoleVT.CMD — Robust VT (ANSI) enablement (caret-free version)
:: ============================================================================

set "VT_ENABLED=0"

VTProbe.exe
if %errorlevel%==1 (
    set "VT_ENABLED=0"
    echo VT Colorization is unavailable.
) else (
    set "VT_ENABLED=1"
    echo VT Colorization is available and enabled.
)

endlocal & (
    set "VT_ENABLED=%VT_ENABLED%"
)

:: ============================================================================
:: VT status evaluated; exit code no longer meaningful
:: ============================================================================

exit /b 0