@echo OFF
goto SKIPREM

::  ============================================================================
::
::  File Name:          C:\bin\XCOPYCHK.CMD
::
::  Synopsis:           Check the exit code returned by the XCOPY system utility
::                      and display or record the outcome.
::
::  Parameters:         %1 = REQUIRED: Exit code returned by XCOPY
::
::                      %2 = OPTIONAL: Log File to use for reporting
::
::  Dependencies:       WWLogger.exe    This Win32 character mode program
::                                      records messages in standard Windows
::                                      text files that can be viewed in a text
::                                      editor or imported into a data base or a
::                                      Microsoft Excel worksheet.
::
::                      WWPause.EXE     This robust replacement for the internal
::                                      PAUSE command is required if, and only
::                                      if, one or more of the commented-out
::                                      WWPause commands is made live. This is
::                                      needed only for debugging or observing
::                                      the script.
::
::  Remarks:            The exit code must be passed in as a parameter because
::                      the exit code returned by xcopy.exe doesn't survive the
::                      transition to this subsidiary script.
::
::                      This script evaluates the following exit codes returned
::                      by xcopy.exe.
::
::                      --------------------------------------------------------
::                      Exit code   Description
::                      ---------   --------------------------------------------
::                      0           Files were copied without error.
::                      1           No files were found to copy.
::                      2           The user pressed CTRL+C to terminate xcopy.
::                      4           Initialization error occurred. There is not
::                                  enough memory or disk space, or you entered
::                                  an invalid drive name or invalid syntax on
::                                  the command line.
::                      5           Disk write error occurred.
::                      --------------------------------------------------------
::
::  See Also:           XCOPYD.CMD and XCOPYF.CMD
::
::  Author:             David A. Gray
::
::  Copyright           Copyright 2014, 2026, David A. Gray.
::                      All rights reserved world wide.
::
::  Date Written:       Friday, 15 August 2014 and Saturday, 16 August 2014
::
::  ----------------------------------------------------------------------------
::  Revision Hiostory
::  ----------------------------------------------------------------------------
::
::  Date       Author Synopsis
::  ---------- ------ ----------------------------------------------------------
::  2014/08/16 DAG/WW Create and test on host ZAPHOD42, which runs Windows XP.
::  2026/10/02 DAG    Convert from Windows batch script to Windows NT command.
::  ============================================================================

:SKIPREM

    echo ------------------------------------------------------------------------
    echo %~0 version %~t0 Begin
    echo ------------------------------------------------------------------------
    echo.

    if %1 equ 5 goto COPYERR5
    if %1 equ 4 goto COPYERR4
    if %1 equ 3 goto COPYERR3
    if %1 equ 2 goto COPYERR2
    if %1 equ 1 goto COPYERR1

    goto COPY_OK

:COPYERR5

    if "%~2" == "" (
        echo ERROR: The xcopy.exe system utility program reported a disk write error.
    ) else (
        WWLogger.exe %2 ERROR: The xcopy.exe system utility program reported a disk write error.
    )

    goto FINISH

:COPYERR4

    if "%~2" == "" (
        echo ERROR: The xcopy.exe system utility program reported an initialization error.
    ) else (
        WWLogger.exe %2 ERROR: The xcopy.exe system utility program reported an initialization error.
    )

    goto FINISH

:COPYERR3

    if "%~2" == "" (
        echo ERROR: The xcopy.exe system utility program reported an unknown error.
    ) else (
        WWLogger.exe %2 ERROR: The xcopy.exe system utility program reported an unknown error.
    )

    goto FINISH

:COPYERR2

    if "%~2" == "" (
        echo ERROR: The xcopy.exe system utility program reported interruption by CTRL-C.
    ) else (
        WWLogger.exe %2 ERROR: The xcopy.exe system utility program reported interruption by CTRL-C.
    )

    goto FINISH

:COPYERR1

    if "%~2" == "" (
        echo Info: The xcopy.exe system utility program found no files to be copied.
    ) else (
        WWLogger.exe %2 Info: The xcopy.exe system utility program found no files to be copied.
    )

    goto FINISH

:COPY_OK

    if "%~2" == "" (
        echo Info: The xcopy.exe system utility program reported no errors.
    ) else (
        WWLogger.exe %2 Info: The xcopy.exe system utility program reported no errors.
    )

    goto FINISH

:FINISH

    echo.
    echo ------------------------------------------------------------------------
    echo Info: %~nx0 Done
    echo ------------------------------------------------------------------------
    echo.
    exit /b %1