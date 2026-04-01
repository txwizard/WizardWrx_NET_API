@echo off
setlocal

:: ============================================================================
:: ConsoleFormat.CMD — banners, sections, indentation
:: ============================================================================

set "CF_BANNER=%FG_CYAN%============================================================%ANSI_RESET%"
set "CF_SECTION=%FG_MAGENTA%---%ANSI_RESET%"

set "INDENT_1=    "
set "INDENT_2=        "
set "INDENT_3=            "

endlocal & (
    set "CF_BANNER=%CF_BANNER%"
    set "CF_SECTION=%CF_SECTION%"
    set "INDENT_1=%INDENT_1%"
    set "INDENT_2=%INDENT_2%"
    set "INDENT_3=%INDENT_3%"
)