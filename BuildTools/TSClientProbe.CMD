@echo off
goto SKIPREM

::  ============================================================================
::  TSClientProbe.CMD
::
::  Purpose:
::     Robust probe for working TSCLIENT network share to RDP guest machine.
::     Provides:
::         - VT (ANSI) enablement
::         - ANSI color constants
::         - Formatting helpers (banners, sections, indentation)
::         - Structured diagnostic prefixes with timestamps
::
::  Usage:
::     call "%~dp0TSClientProbe.CMD" "%_SlideRuleCommon_%"
::     if errorlevel 1 (
::         rem TSCLIENT unavailable — abort
::         echo %DL_ERROR% TSCLIENT redirector unavailable — aborting script
::         WWPause.exe
::         exit /b 1
::     )
::
::  Script Author:      David A. Gray
::
::  ----------------------------------------------------------------------------
::  Revision History
::  ----------------------------------------------------------------------------
::
::  Date       By  Synopsis
::  ---------- --- -------------------------------------------------------------
::  2026/02/21 DAG Create script with help from Microsoft CoPilot.
::  2026/02/27 DAG Re-save as plain ASCII. The original was created as UTF-16.
::  ============================================================================

:SKIPREM

    setlocal

:: Load console formatting library.

    call "%~dp0ConsoleLib.CMD"

    echo %DL_INFO% %~nx0 version %~t0 — Begin
    echo.

    ShowTime.exe

    set PROBE_DIR=%~1
    set PROBE_FILE=%PROBE_DIR%\tsclient_probe_%~n0.TMP

    echo %DL_INFO% Probing TSCLIENT redirector
    echo %DL_INFO% Directory   = %PROBE_DIR%
    echo %DL_INFO% Probe File  = %PROBE_FILE%
    echo %CF_SECTION% Step 1 of 2: Attempt to create probe file

    echo TSCLIENT probe > "%PROBE_FILE%" 2>nul

    if errorlevel 1 (
        rem Creation failed
        echo %DL_ERROR% Probe file creation FAILED — redirector dead
        exit /b 1
    )

    echo %DL_OK% Probe file creation succeeded

    echo %CF_SECTION% Step 2 of 2: Attempt to delete probe file
    del "%PROBE_FILE%" >nul 2>&1

    if errorlevel 1 (
        rem Deletion failed
        echo %DL_ERROR% Probe file deletion FAILED — redirector dead or path inaccessible
        exit /b 1
    )

    echo %DL_OK% Probe file deletion succeeded
    echo %DL_OK% TSCLIENT redirector is alive

    exit /b 0