@echo off
goto SKIPREM

::  ============================================================================
::
::  Name:               CaptureTestStandOutput_RETAIL.CMD
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
::  Remarks:            Since there is no compelling reason to return through
::                      this script, invocation of CaptureTestStandOutput.CMD
::                      dispenses with the CALL mechanism.
::
::                      This script has almost everything it needs to be a
::                      general purpose test platform.
::
::  ----------------------------------------------------------------------------
::  Revision History
::  ----------------------------------------------------------------------------
::
::  Date       By  Version Synopsis
::  ---------- --- ------- --------------------------------------------------
::  2016/04/03 DAG 4.00    Create this copy from the debug script.
::  2016/04/06 DAG 5.00    Differentiate release and debug outputs for the logs,
::                         and change the version string from 5.5 to 6.0.
::  2017/07/12 DAG         Implement my new self-maintaining version marking
::                         method and change the version string from 6.0 to 7.0.
::  2017/08/28 DAG         Adjust the script to derive its paths from its own
::                         location.
::  ============================================================================

:SKIPREM

    echo.
    echo BOJ %0, version %~t0
    echo.
    call ShowTime.CMD
    echo.
    echo ---------------------------------------------------------------------
    echo This script is about to launch DLLServices2TestStand.exe. At present,
    echo there are two silent prompts, both of which require you to press the
    echo ENTER key. Press ENTER now to launch DLLServices2TestStand.exe.
    echo ---------------------------------------------------------------------
    echo.
    setlocal
    set _Confiuration=Release
    set _ScriptDir=%~dp0
    CaptureTestStandOutput.CMD