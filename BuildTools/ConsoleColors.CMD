@echo off
setlocal

:: ============================================================================
:: ConsoleColors.cmd — Define ANSI color constants.
:: ============================================================================

:: Acquire ESC (ASCII 27) safely and portably.
for /f %%A in ('echo prompt $E^| cmd') do set "ESC=%%A"

if "%VT_ENABLED%"=="1" (
    rem Set foreground colors.
    set "FG_BLACK=%ESC%[30m"
    set "FG_RED=%ESC%[31m"
    set "FG_GREEN=%ESC%[32m"
    set "FG_YELLOW=%ESC%[33m"
    set "FG_BLUE=%ESC%[34m"
    set "FG_MAGENTA=%ESC%[35m"
    set "FG_CYAN=%ESC%[36m"
    set "FG_WHITE=%ESC%[37m"
    set "FG_BRIGHT_WHITE=%ESC%[97m"

    rem Set background colors.
    set "BG_BLACK=%ESC%[40m"
    set "BG_RED=%ESC%[41m"
    set "BG_GREEN=%ESC%[42m"
    set "BG_YELLOW=%ESC%[43m"
    set "BG_BLUE=%ESC%[44m"
    set "BG_MAGENTA=%ESC%[45m"
    set "BG_CYAN=%ESC%[46m"
    set "BG_WHITE=%ESC%[47m"

    rem Define reset sequences.
    set "ANSI_RESET=%ESC%[0m"
    set "FG_RESET=%ESC%[39m"
    set "BG_RESET=%ESC%[49m"
) else (
    rem VT is unavailable — define no-op fallbacks (variables must exist).
    set "FG_BLACK="
    set "FG_RED="
    set "FG_GREEN="
    set "FG_YELLOW="
    set "FG_BLUE="
    set "FG_MAGENTA="
    set "FG_CYAN="
    set "FG_WHITE="
    set "FG_BRIGHT_WHITE="

    set "BG_BLACK="
    set "BG_RED="
    set "BG_GREEN="
    set "BG_YELLOW="
    set "BG_BLUE="
    set "BG_MAGENTA="
    set "BG_CYAN="
    set "BG_WHITE="

    set "ANSI_RESET="
    set "FG_RESET="
    set "BG_RESET="
)

endlocal & (
    rem Explicit export — no enumeration, no warnings, no pre-expansion issues.
    set "FG_BLACK=%FG_BLACK%"
    set "FG_RED=%FG_RED%"
    set "FG_GREEN=%FG_GREEN%"
    set "FG_YELLOW=%FG_YELLOW%"
    set "FG_BLUE=%FG_BLUE%"
    set "FG_MAGENTA=%FG_MAGENTA%"
    set "FG_CYAN=%FG_CYAN%"
    set "FG_WHITE=%FG_WHITE%"
    set "FG_BRIGHT_WHITE=%FG_BRIGHT_WHITE%"

    set "BG_BLACK=%BG_BLACK%"
    set "BG_RED=%BG_RED%"
    set "BG_GREEN=%BG_GREEN%"
    set "BG_YELLOW=%BG_YELLOW%"
    set "BG_BLUE=%BG_BLUE%"
    set "BG_MAGENTA=%BG_MAGENTA%"
    set "BG_CYAN=%BG_CYAN%"
    set "BG_WHITE=%BG_WHITE%"

    set "ANSI_RESET=%ANSI_RESET%"
    set "FG_RESET=%FG_RESET%"
    set "BG_RESET=%BG_RESET%"
)