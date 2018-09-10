@echo off

::  ============================================================================
::
::  Name:               DLLServices2TestStand_Redirected_STDIN.CMD
::
::  Synopsis:           Use Test_Data\HereDoc.TEXT and DLLServices2TestStand.exe
::                      to test the standard input redirection file name getter.
::
::  Arguments:          %1 = [Debug|Release|D|R], default being R.
::
::  Environment:        This routine generates and uses several locally scoped
::                      environment variables.
::
::  Dependencies:       DLLServices2TestStand.exe = This is the test stand
::                                                  program for class library
::                                                  WizardWrx.DllServices2.dll.
::
::                      WWLOGGER.exe              = This is a 32 bit console
::                                                  mode (command line) program
::                                                  that records date stamps
::                                                  which are fully ISO 8601
::                                                  compliant.
::
::                                                  This program runs whenever
::                                                  this script runs.
::
::                      WWPause.exe               = This robust PAUSE command
::                                                  insists that you press the
::                                                  RETURN key to confirm that
::                                                  you had a chance to read the
::                                                  screen. In contrast, the
::                                                  built-in PAUSE command accepts
::                                                  ANY key.
::
::                                                  WWPause.exe is a 32 bit
::                                                  console mode version of
::                                                  P6PAUSE.COM, an Intel 8086
::                                                  Assembly language program,
::                                                  created by me in 1990.
::                                                  Though both programs run on
::                                                  Windows XP, due to a
::                                                  deficiency in the command
::                                                  parser, P6PAUSE.COM will not
::                                                  execute if the length of the
::                                                  working directory name
::                                                  exceeds about 56 characters.
::                                                  Moreover, since P6PAUSE.COM
::                                                  is a 16-bit MS-DOS program,
::                                                  it is incompatible with all
::                                                  64 bit operating systems.
::
::                      StandardHandlesLab.exe must be installed into one of
::                      the following two directories.
::
::                              C:\Users\DAVE\Documents\Visual Studio 2013\Projects\WizardWrx_Libs\DLLServices2\DLLServices2TestStand\bin\Debug
::                              C:\Users\DAVE\Documents\Visual Studio 2013\Projects\WizardWrx_Libs\DLLServices2\DLLServices2TestStand\bin\Release
::
::                      Of the two, the Debug directory gets preference,
::                      subject to override by a command line argument.
::
::                      ExportFromEventLog1Source.CMD must be installed into the
::                      following directory, making the test program accessed
::                      via a relative path.
::
::                          C:\Users\DAVE\Documents\Visual Studio 2013\Projects\WizardWrx_Libs\DLLServices2\scripts
::
::                      All other dependent programs must be installed into any
::                      directory named in the Windows PATH list.
::
::  Remarks:            I chose WWLOGGER.exe over its successor, ELALogger.exe,
::                      because the latter is not yet tested on Windows 7.
::
::  References:         Variables: Extract Part of a Variable (Substring)
::                      http://ss64.com/nt/syntax-substring.html
::
::  Author:             David A. Gray
::
::  Created Date:       Friday, 09 October 2015 and Saturday, 10 October 2015
::
::  ----------------------------------------------------------------------------
::  Revision History
::  ----------------------------------------------------------------------------
::
::  Date       Author Version Synopsis
::  ---------- ------ ------- --------------------------------------------------
::  2015/10/10 DAG    1.00    Adapt StandardHandlesLabDrills.CMD to create this
::                            script.
::  2015/10/28 DAG    1.01    Add my standard BOJ and EOJ echo messages.
::  ============================================================================


:SKIPREM

    setlocal
    echo BOJ %~0, version 1.01
    echo.
    pushd "%~dp0"

    WWLOGGER.EXE ..\Redirected_Streams\%~n0.LOG BOJ %~0, version 1.01

    if /i "%~1" equ "d" (
        set PGMID=..\DLLServices2TestStand\bin\Debug\DLLServices2TestStand.exe
    ) else (
        if /i "%~1" equ "r" (
            set PGMID=..\DLLServices2TestStand\bin\Release\DLLServices2TestStand.exe
        ) else (
            if /i "%~1" equ "debug" (
                set PGMID=..\DLLServices2TestStand\bin\Debug\DLLServices2TestStand.exe
            ) else (
                if /i "%~1" equ "Release" (
                    set PGMID=..\DLLServices2TestStand\bin\Release\DLLServices2TestStand.exe
                ) else (
                    set PGMID=..\DLLServices2TestStand\bin\Debug\DLLServices2TestStand.exe
                )
            )
        )
    )

    set InputFN=C:\Users\DAVE\Documents\Programming\Visual_Studio_6\EXE\Console\WWLOGGER\Test_Data\HereDoc.TEXT

    echo.                              >  %TEMP%\%~n0.TXT
    echo Working Directory = %CD%      >> %TEMP%\%~n0.TXT
    echo Program File Name = %PGMID%   >> %TEMP%\%~n0.TXT
    echo Input File Name   = %InputFN% >> %TEMP%\%~n0.TXT

    WWLOGGER.EXE ..\Redirected_Streams\%~n0.LOG -S Operating Parameters: < %TEMP%\%~n0.TXT

    echo.
    echo ------------------------------------------------------------------
    echo Verify the items listed above, then press Return to begin the test.
    echo ------------------------------------------------------------------
    echo.
    WWPause.exe

    %PGMID% RedirectionDetection < %InputFN%

    WWLOGGER.EXE ..\Redirected_Streams\%~n0.LOG EOJ %~nx0

    echo.
    echo EOJ %~nx0
    echo.
    WWPause.exe
