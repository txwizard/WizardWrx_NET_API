@echo off
setlocal

:: ============================================================================
:: ConsoleDiag.cmd — Structured diagnostic prefixes with safe timestamps
:: ============================================================================

:: Acquire a stable, locale-independent timestamp via WMIC.
:: WMIC returns: YYYYMMDDhhmmss.mmmmmmsUUU
for /f %%A in ('wmic os get LocalDateTime ^| find "."') do set "DTS=%%A"

:: Extract components safely.
set "YYYY=%DTS:~0,4%"
set "MM=%DTS:~4,2%"
set "DD=%DTS:~6,2%"
set "HH=%DTS:~8,2%"
set "MN=%DTS:~10,2%"
set "SS=%DTS:~12,2%"

:: Construct a safe ISO-8601-like timestamp.
set "TS=[%YYYY%/%MM%/%DD% %HH%:%MN%:%SS%]"

:: Diagnostic prefixes.
:: Colors come from ConsoleColors.cmd; if VT is disabled, they are empty strings.
set "DL_INFO=%FG_CYAN%[INFO]%ANSI_RESET% %TS% [%_CallingScriptName_%]"
set "DL_WARN=%FG_YELLOW%[WARN]%ANSI_RESET% %TS% [%_CallingScriptName_%]"
set "DL_ERROR=%FG_BRIGHT_WHITE%%BG_RED%[ERROR]%ANSI_RESET% %TS% [%_CallingScriptName_%]"
set "DL_OK=%FG_GREEN%[ OK ]%ANSI_RESET% %TS% [%_CallingScriptName_%]"

endlocal & (
    set "DL_INFO=%DL_INFO%"
    set "DL_WARN=%DL_WARN%"
    set "DL_ERROR=%DL_ERROR%"
    set "DL_OK=%DL_OK%"
)