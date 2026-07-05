    @echo off

::  ============================================================================
::
::  Name:               RedirectionDrills.CMD
::
::  Synopsis:           Use RedirectedStreamTester.exe to exercise all possible
::                      standard stream redirection scenarios.
::
::  Arguments:          None
::
::  Environment:        This routine generates and uses several locally scoped
::                      environment variables.
::
::  Dependencies:       RedirectedStreamTester.exe = This is the standard stream
::                                                   detector.
::
::                      WWLOGGER.exe               = This is a 32 bit console
::                                                   mode (command line) program
::                                                   that records date stamps
::                                                   which are fully ISO 8601
::                                                   compliant.
::
::                                                   This program runs whenever
::                                                   this script runs.
::
::                      WWPause.exe                = This is a robust PAUSE
::                                                   command, which insists that
::                                                   you press the RETURN key to
::                                                   confirm that you had a
::                                                   chance to read the screen.
::                                                   In contrast, the built-in
::                                                   PAUSE command accepts ANY
::                                                   key.
::
::                                                   WWPause.exe is a 32 bit
::                                                   console mode version of
::                                                   P6PAUSE.COM, an Intel 8086
::                                                   Assembly language program,
::                                                   created by me in 1990.
::                                                   Though both programs run on
::                                                   Windows XP, due to a
::                                                   deficiency in the command
::                                                   parser, P6PAUSE.COM will
::                                                   not execute if the length
::                                                   of the working directory
::                                                   name exceeds about 56
::                                                   characters.
::
::                      RedirectedStreamTester.exe must be installed into one of
::                      the following two directories.
::
::                              C:\Users\DAVE\Documents\Visual Studio 2013\Projects\WizardWrx_Libs\DLLServices2\RedirectedStreamTester\bin\Release
::                              C:\Users\DAVE\Documents\Visual Studio 2013\Projects\WizardWrx_Libs\DLLServices2\RedirectedStreamTester\bin\Debug
::
::                      Of the two, the Release directory gets preference.
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
::  Created Date:       Wednesday, 24 June 2015
::
::  ----------------------------------------------------------------------------
::  Revision History
::  ----------------------------------------------------------------------------
::
::  Date       Author Version Synopsis
::  ---------- ------ ------- --------------------------------------------------
::  2015/06/24 DAG    1.00    Initial implementation.
::  2015/06/25 DAG    1.01    Preserve existing outputs created by the last run,
::                            and ensure that all tasks eventually call upon
::                            WWPause.exe to suspend the console window until a
::                            carbon unit acknowledges it.
::  2015/06/26 DAG    1.02    Correct an internal documentation error found when
::                            I derived StandardHandlesLabDrills.CMD from this
::                            script. The code is unaffected.
::  2015/09/23 DAG    1.03    Change the script to use the debuggable WWLogger,
::                            if it exists.
::  ============================================================================


:SKIPREM

    setlocal
    pushd "C:\Users\DAVE\Documents\Visual Studio 2013\Projects\WizardWrx_Libs\DLLServices2\Redirected_Streams"

    if exist C:\bin\WWLOGGERD.EXE (
        set LogPgm=C:\bin\WWLOGGERD.EXE
    ) else (
        set LogPgm=C:\bin\wwlogger.exe
    )

    %LogPgm% %~n0.LOG BOJ %~0, version 1.03

    if exist "C:\Users\DAVE\Documents\Visual Studio 2013\Projects\WizardWrx_Libs\DLLServices2\RedirectedStreamTester\bin\Release\RedirectedStreamTester.exe" (
        set PGMID="C:\Users\DAVE\Documents\Visual Studio 2013\Projects\WizardWrx_Libs\DLLServices2\RedirectedStreamTester\bin\Release\RedirectedStreamTester.exe"
    ) else (
        set PGMID="C:\Users\DAVE\Documents\Visual Studio 2013\Projects\WizardWrx_Libs\DLLServices2\RedirectedStreamTester\bin\Debug\RedirectedStreamTester.exe"
    )

    %LogPgm% %~n0.LOG Working Directory  = %CD%
    %LogPgm% %~n0.LOG Program File Name  = %PGMID%
    %LogPgm% %~n0.LOG Script Logger Name = %LogPgm%

    if "%~1" equ "NONE" goto RUN_NONE
    if "%~1" equ "IN"   goto RUN_IN
    if "%~1" equ "OUT"  goto RUN_OUT
    if "%~1" equ "ERR"  goto RUN_ERR

:RUN_NONE

    call :THE_DRILL NONE "STDERR=D"             "STDIN=D"           "STDOUT=D"
    if errorlevel 1     goto RUN_DONE
    if "%~1" equ "NONE" goto RUN_DONE

:RUN_IN

    call :THE_DRILL IN   "STDERR=D"             "STDIN=R,STDIN.TXT" "STDOUT=D"
    if errorlevel 1     goto RUN_DONE
    if "%~1" equ "IN"   goto RUN_DONE

:RUN_OUT

    call :THE_DRILL OUT  "STDERR=D"             "STDIN=D"           "STDOUT=R,STDOUT.TXT"
    if errorlevel 1     goto RUN_DONE
    if "%~1" equ "OUT"  goto RUN_DONE

:RUN_ERR

    call :THE_DRILL ERR  "STDERR=R,STDERR.TXT"  "STDIN=D"           "STDOUT=D"
    if errorlevel 1     goto RUN_DONE
    if "%~1" equ "ERR"  goto RUN_DONE

    %LogPgm% %~n0.LOG Executing ExportFromEventLog1Source.CMD via shell call
    echo.
    call ..\scripts\ExportFromEventLog1Source.CMD
    echo.
    %LogPgm% %~n0.LOG ExportFromEventLog1Source.CMD done
    echo.
    %LogPgm% %~n0.LOG EOJ %~nx0

:RUN_DONE

    echo.
    WWPause.exe
    goto :EOF


::  ----------------------------------------------------------------------------
::  Subroutine Name:    SUBSTR
::
::  Synopsis:           Extract a substring from environment string RedirFN, and
::                      recycle the variable to return the substring.
::
::  Arguments:          %1  = (Required) String from which to extract characters
::
::                      %2  = (Required) Leading characters to skip
::
::                      %3  = (Optional) Number of characters to keep
::  ----------------------------------------------------------------------------

:SUBSTR

    set _WholeStr=%~1
    set _StartChar=%2

    if "%3" equ "" (
        call set RedirFN=%%_WholeStr:~%_StartChar%%%
    ) else (
        set _Length=%3
        call set RedirFN=%%_WholeStr:~%_StartChar%,%_Length%%%
    )

    goto :EOF


::  ----------------------------------------------------------------------------
::  Subroutine Name:    THE_DRILL
::
::  Synopsis:           Using the redirection parameters passed into its three
::                      required arguments, execute RedirectedStreamTester.exe.
::
::  Arguments:          %1  = Must be one of the following
::
::                              NONE    = None of the three standard streams is
::                                        redirected.
::
::                              IN      = Standard Input is redirected.
::
::                              OUT     = Standard Output is redirected.
::
::                              ERR     = Standard Error is redirected.
::
::                      %2  = Standard Input (STDIN) stream state.
::
::                            See Remarks for instructions about the formatting
::                            of this argument.
::
::                      %3  = Standard Output (STDOUT) stream state.
::
::                            See Remarks for instructions about the formatting
::                            of this argument.
::
::                      %4  = Standard Error (STDERR) stream state.
::
::                            See Remarks for instructions about the formatting
::                            of this argument.
::
::  Remarks:            The last three arguments follow a strict format, as
::                      follows.
::
::                          StreamID=S[,UQFN]
::
::                      where:  StreamID    = One of the following case
::                                            insensitive console stream
::                                            identities:
::
::                                              STDERR  = Standard Error
::                                              STDIN   = Standard Input
::                                              STDOUT  = Standard Output
::
::                              S           = One of the following stream state
::                                            flags:
::
::                                              D       = Default (to console)
::                                              R       = Redirected to a file
::
::                              UQFN        = Valid only when S = R, unqualified
::                                            file name, which either exists, if
::                                            StreamID = STDIN, or is created,
::                                            if StreamID = STDOUT or STDERR, in
::                                            the working directory.
::
::                      Following is a valid example, copied from my notes.
::
::                          STDERR=D STDIN=R,STDIN.TXT STDOUT=D
::
::                      All four arguments are required. Parsing the last three
::                      is left to RedirectedStreamTester.exe.
::  ----------------------------------------------------------------------------

:THE_DRILL

    %LogPgm% %~n0.LOG THE_DRILL: %1 %2 %3 %4

::  ----------------------------------------------------------------------------
::  Verify that all four arguments are present.
::  ----------------------------------------------------------------------------

    if "%~4" equ ""     goto FEW_ARGS
    if "%~3" equ ""     goto FEW_ARGS
    if "%~2" equ ""     goto FEW_ARGS
    if "%~1" equ ""     goto FEW_ARGS

::  ----------------------------------------------------------------------------
::  The first argument determines which of four tasks is performed.
::
::  The remaiing three arguments are passed into RedirectedStreamTester.exe. for
::  parsing and use.
::  ----------------------------------------------------------------------------

    if "%~1" equ "NONE" goto DO_NONE
    if "%~1" equ "IN"   goto DO_IN
    if "%~1" equ "OUT"  goto DO_OUT
    if "%~1" equ "ERR"  goto DO_ERR
    goto :EOF

::  ----------------------------------------------------------------------------
::  This internal label executes task NONE.
::  ----------------------------------------------------------------------------

:DO_NONE

    %LogPgm% %~n0.LOG THE_DRILL: Task %1 - everything to console
    %PGMID% %2 %3 %4
    %LogPgm% %~n0.LOG THE_DRILL: Task %1 completed = exit code = %ERRORLEVEL%
    goto :EOF

::  ----------------------------------------------------------------------------
::  This internal label executes task IN.
::  ----------------------------------------------------------------------------

:DO_IN

    %LogPgm% %~n0.LOG THE_DRILL: Task %1 - STDIN redirected, everything else to console
    call :SUBSTR %3 8
    %LogPgm% %~n0.LOG THE_DRILL: Task %1 - STDIN redirected to %RedirFN%
    %PGMID% %2 %3 %4 <%RedirFN%
    %LogPgm% %~n0.LOG THE_DRILL: Task %1 completed = exit code = %ERRORLEVEL%
    goto :EOF

::  ----------------------------------------------------------------------------
::  This internal label executes task OUT.
::  ----------------------------------------------------------------------------

:DO_OUT

    %LogPgm% %~n0.LOG THE_DRILL: Task %1 - STDOUT redirected, everything else to console
    call :SUBSTR %4 9

    if exist %RedirFN% (
        Date2FN -r %RedirFN%

        if errorlevel 1 (
            %LogPgm% %~n0.LOG THE_DRILL: Task %1 - ABORTING: Date2FN exit code %ERRORLEVEL%
            goto :EOF
        ) else (
            %LogPgm% %~n0.LOG THE_DRILL: Task %1 - STDOUT redirected to a new %RedirFN% - existing report preserved
        )
    ) else (
        %LogPgm% %~n0.LOG THE_DRILL: Task %1 - STDOUT redirected to a new %RedirFN%
    )

    %PGMID% %2 %3 %4 >%RedirFN%
    %LogPgm% %~n0.LOG THE_DRILL: Task %1 completed = exit code = %ERRORLEVEL%
    goto :EOF

::  ----------------------------------------------------------------------------
::  This internal label executes task ERR.
::  ----------------------------------------------------------------------------

:DO_ERR

    %LogPgm% %~n0.LOG THE_DRILL: Task %1 - STDERR redirected, everything else to console
    call :SUBSTR %2 9

    if exist %RedirFN% (
        Date2FN -r %RedirFN%

        if errorlevel 1 (
            %LogPgm% %~n0.LOG THE_DRILL: Task %1 - ABORTING: Date2FN exit code %ERRORLEVEL%
            goto :EOF
        ) else (
            %LogPgm% %~n0.LOG THE_DRILL: Task %1 - STDERR redirected to a new %RedirFN% - existing report preserved
        )
    ) else (
        %LogPgm% %~n0.LOG THE_DRILL: Task %1 - STDERR redirected to a new %RedirFN%
    )

    %PGMID% %2 %3 %4 2>%RedirFN%
    %LogPgm% %~n0.LOG THE_DRILL: Task %1 completed = exit code = %ERRORLEVEL%
    goto :EOF

::  ----------------------------------------------------------------------------
::  This internal label only gets control when one or more arguments is missing.
::  ----------------------------------------------------------------------------

:FEW_ARGS

    echo.
    echo --------------------------------------------
    echo THE_DRILL: One or more arguments is missing.
    echo --------------------------------------------
    echo.
    exit /b 255