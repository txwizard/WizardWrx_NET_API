@echo off
goto SKIPREM

::  ============================================================================
::
::  Name:               CaptureTestStandOutput.CMD
::
::  Synopsis:           Exercise DLLServices2TestStand.exe, and document the
::                      outcome.
::
::  Arguments:          This shell script has no arguments.
::
::  Dependencies:       DLLServices2TestStand.exe   = This is the program to run
::                                                    and document the outcome.
::
::                      Date2FN.exe                 = This 32-bit character mode
::                                                    Windows program appends a
::                                                    formatted string represen-
::                                                    ting the LastWriteTime of
::                                                    a file to its base name to
::                                                    create a name that is
::                                                    intended to be locally
::                                                    unique. The optional R
::                                                    switch instructs it to
::                                                    mark the file as read only
::                                                    by setting the read only
::                                                    attribute in the file
::                                                    system.
::
::                      ExportFromEventLogNDays.CMD = This shell script uses the
::                                                    SysInternals psloglist.exe
::                                                    utility to extract records
::                                                    from a Windows Event Log.
::                                                    The extracted records go
::                                                    into a tab delimited ASCII
::                                                    file that I import into a
::                                                    Microsoft Excel workbook.
::
::                      ShowTime.CMD                = This little shell script
::                                                    displays the current time
::                                                    per the machine clock.
::
::                      WWPause.exe                 = This 32-bit character mode
::                                                    Windows program replaces
::                                                    the lame intrinsic pause
::                                                    command implemented by the
::                                                    command processor. Unlike
::                                                    Pause, it insists on the
::                                                    RETURN key being pressed.
::
::                      DLLServices2TestStand.exe must be installed into the
::                      following directory, relative to the directory in which
::                      this script is installed.
::
::                          ..\DLLServices2TestStand\bin\%_Confiuration%\DLLServices2TestStand.exe
::
::                      All other dependent programs must be installed into a
::                      directory that is in the Windows PATH list.
::
::  Remarks:            This script does the work that was initially implemented
::                      redundantly in the two scripts that call it:
::
::                          1) CaptureTestStandOutput_DEBUG.CMD
::                          2) CaptureTestStandOutput_RETAIL.CMD
::
::                      There being no compelling reason to return, t
::
::  ----------------------------------------------------------------------------
::  Revision History
::  ----------------------------------------------------------------------------
::
::  Date       By  Synopsis
::  ---------- --- -------------------------------------------------------------
::  2017/08/28 DAG Create this script from CaptureTestStandOutput_DEBUG.CMD.
::  ============================================================================

:SKIPREM

    ECHO Entering script %0, version %~t0

    if "%_Confiuration%" == "" (
        echo.
        echo -------------------------------------------------------------------
        echo ERROR: This script must be launched by one of its parents!
        echo -------------------------------------------------------------------
    ) else (
        setlocal
        echo Script Directory Name = %_ScriptDir%
        echo.
        WWPause.exe
        echo.
        pushd %_ScriptDir%
        pushd ..\_NOTES

        ..\DLLServices2TestStand\bin\%_Confiuration%\DLLServices2TestStand.exe > DLLServices2TestStand_7_0_Output_%_Confiuration%.LOG
        echo.
        echo -------------------------------------------------------------------
        echo DLLServices2TestStand.exe exit code = %ERRORLEVEL%
        echo -------------------------------------------------------------------
        echo.

        Date2FN.exe -r DLLServices2TestStand_7_0_Output_%_Confiuration%.LOG

        if exist DLLServices2TestStand_7_0_%_Confiuration%_Trace.LOG (
            Date2FN.exe -r DLLServices2TestStand_7_0_%_Confiuration%_Trace.LOG
        ) else (
            echo.
            echo -------------------------------------------------------------------
            echo DLLServices2TestStand_7_0_%_Confiuration%_Trace.LOG does not exist.
            echo -------------------------------------------------------------------
            echo.
        )

        call ExportFromEventLogNDays.CMD Application DLLServices2TestStand_7_0_%_Confiuration%_ApplicationEvents.TXT
        Date2fn.exe -r DLLServices2TestStand_7_0_%_Confiuration%_ApplicationEvents.TXT

        echo.
        echo EOJ %~nx0 for %_Confiuration% configuration
    )

    echo.
    WWPause.exe