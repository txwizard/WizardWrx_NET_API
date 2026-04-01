@echo off
setlocal

:: ============================================================================
:: ConsoleVT.CMD — Robust VT (ANSI) enablement (caret-free version)
:: ============================================================================

set "VT_ENABLED=0"

VTProbe.exe
if %errorlevel%==1 (
    set "VT_ENABLED=0"
) else (
    set "VT_ENABLED=1"
)

endlocal & (
    set "VT_ENABLED=%VT_ENABLED%"
)