@echo off
goto SKIPREM

::  ============================================================================
::
::  Script Name:        Application_Backup.CMD
::
::  Script Synopsis:    Copy all files that are new or changed in the the source
::                      directory to the destination directory.
::
::  Script Arguments:   %1              Absolute (fully qualified) name of the
::                                      source directory from which to copy
::                                      files.
::
::                      %2              Absolute (fully qualified) name of the
::                                      target directory to which to copy the
::                                      files.
::
::  Dependencies:       This script has other dependenices, which must be stored
::                      in directories that are accessible via the PATH.
::
::                      ShowTime.exe        This 64-bit C++ utility supersedes
::                                          the like-named shell script, but it
::                                          affords much greater control over the
::                                          display format.
::
::                      WWLogger.exe        This Win32 character mode program
::                                          records messages in standard Windows
::                                          text files that can be viewed in a
::                                          text editor or imported into a data
::                                          base or a Microsoft Excel worksheet.
::
::                      WWPause.exe         This Win32 character mode program
::                                          is my robust replacement for the
::                                          pause command that has been built
::                                          into every Windows and MS-DOS
::                                          command processor since DOS
::                                          version 1.0, and Windows version 1.
::                                          It accepts only two keyboard inputs,
::                                          RETURN and CTRL-C; this restriction
::                                          prevents it from responding to just
::                                          'any key," and causing the script to
::                                          exit before you are finished reading
::                                          or capturing its outputs.
::
::  Script Author:      David A. Gray, after Bud Pass
::
::  ----------------------------------------------------------------------------
::  Revision History
::  ----------------------------------------------------------------------------
::
::  Date       By  Synopsis
::  ---------- --- -------------------------------------------------------------
::  2026/10/02 DAG Initial implementation.
::  ============================================================================

:SKIPREM

    setlocal

    set "_SRC_=%~1"
    set "_DST_=%~2"

    set "_XCLUDE_=C:\bin\ExcludeFromBackup.LST"
    set "_COMMON_=C:\bin\ZZ_Backup_PURL_COMMON.CMD"

    set "_LOG_=C:\Temp\LOGS\%~n0.LOG"

    WWLogger.exe "%_LOG_%" Info: %~0, version %~t0 Begin
    WWLogger.exe "%_LOG_%" Info: Starting backup: _SRC_="%~1" DST="%~2"

    echo.
    showtime.exe
    echo.

    if "%~1"=="" (
        WWLogger.exe "%_LOG_%" ERROR: Missing source directory parameter.
        goto Done
    )

    if "%~2"=="" (
        WWLogger.exe "%_LOG_%" ERROR: Missing destination directory parameter.
        goto Done
    )

    ::  --------------------------------------------------------------------
    ::  Validate that source and destination directories exist
    ::  --------------------------------------------------------------------
    if exist "%_SRC_%\" (
        WWLogger.exe "%_LOG_%" Info: Source directory      = %_SRC_%
        echo.
    ) else (
        echo.
        WWLogger.exe "%_LOG_%" ERROR: Source directory "%_SRC_%" cannot be found.
        goto Done
    )

    if exist "%_DST_%\" (
        WWLogger.exe "%_LOG_%" Info: Destination directory = %_DST_%
        echo.
    ) else (
        echo.
        WWLogger.exe "%_LOG_%" ERROR: Destination directory "%_DST_%" cannot be found.
        goto Done
    )

    if exist "%_XCLUDE_%" (
        WWLogger.exe "%_LOG_%" Info: Exclusion List File   = %_XCLUDE_%
        echo.
        xcopy /EXCLUDE:%_XCLUDE_% /fshedcrky "%_SRC_%" "%_DST_%\"
        set "XCOPY_RC=%ERRORLEVEL%"
    ) else (
        WWLogger.exe "%_LOG_%" ERROR: Exclusion list "%_XCLUDE_%" is missing.
        goto Done
    )


::  ----------------------------------------------------------------------------
::	Prevent CMD.EXE from parsing this until it exists, which doesn't happen
::	until the TRUE block above it executes.
::  ----------------------------------------------------------------------------

    echo.
    call XCOPYCHK.CMD %XCOPY_RC% "%_LOG_%"
    echo.

    if %XCOPY_RC% GEQ 2 goto Done

    if exist "%_COMMON_%" (
        WWLogger.exe "%_LOG_%" Info: Calling COMMON backup script "%_COMMON_%"
        echo.
        call "%_COMMON_%"
    ) else (
        WWLogger.exe "%_LOG_%" WARNING: COMMON backup script "%_COMMON_%" is missing; skipping.
    )

:Done

    echo.
    echo %~n0 Done!
    echo.
    WWPause.exe
    goto :EOF
