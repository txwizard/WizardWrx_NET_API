@echo off
goto SKIPREM

::  ============================================================================
::
::  Name:               Web_Site_Wake_Ups.CMD
::
::  Synopsis:           Execute the Wake-up task against every Web site on the
::                      RackSpace server.
::
::  Arguments:          None
::
::  Dependencies:       MVC_REST4Phones_Tests.exe   = This character-mode C#
::                                                    program executes tasks
::                                                    that run against IIS to
::                                                    cause an application to
::                                                    wake up.
::
::                      ShowTime.exe                = This 64-bit C++ utility
::                                                    supersedes the like-named
::                                                    shell script, but affords
::                                                    much greater control over
::                                                    the display format.
::
::                      WWLOGGER.EXE                = Make a permanent record of
::                                                    all invocations, recorded
::                                                    in a log file that sits
::                                                    alongside the script, in
::                                                    the same directory, same
::                                                    base name, but different
::                                                    extension.
::
::                      WWSleep.exe                 = This is a 32 bit console
::                                                    mode (command line)
::                                                    program which pauses a
::                                                    batch script for up to a
::                                                    specified number of
::                                                    seconds. The time to pause
::                                                    is specified by preceding
::                                                    the integer representing
::                                                    the number of seconds to
::                                                    pause preceded by a hyphen
::                                                    (minus sign), as is common
::                                                    for Unix utilities.
::
::  Remarks:            On the machine on which this script is intended to run,
::                      MVC_REST4Phones_Tests.exe lives in local directory
::                      C:\Scheduled-Tasks\Operations, as do the custom C# DLLs
::                      upon which it relies.
::
::                      Except as noted, all dependent programs are expected to
::                      live in the same directory as does this script OR in a
::                      directory that is in the system PATH list.
::
::  ----------------------------------------------------------------------------
::  Revision History
::  ----------------------------------------------------------------------------
::
::  Date       By  Synopsis
::  ---------- --- -------------------------------------------------------------
::  2025/08/13 DAG Initial version created and tested.
::  2025/09/08 DAG Implement the HARDER parameter that forces Touch1File.exe to
::                 insert or update a time stamp in each web.config file that it
::                 touches.
::  2025/09/13 DAG Implement the new application pool recycler, written in C#
::                 with a generous helping of PowerShell. The name of the new
::                 program is ServerRestartApplicationRecoveryManager.
::  ============================================================================

::  +--------------------------------------------------------------------------+
::  |                    B e g i n   S u b r o u t i n e s                     |
::  +--------------------------------------------------------------------------+

::  ----------------------------------------------------------------------------
::  Since I like to keep the headnote with the routine that it deocuments, I put
::  it below the label that denotes its start, and immediately jump over it, to
::  keep the comments, themselves, out of the execution path. This is the result
::  of hard won knowledtge about the strange things that can hapen when you have
::  inline comments, when a stray character finds its way into the comment.
::
::  Including the comments after the label ensures that if you copy a subroutine
::  into another shell script, the headnotes arrive with it.
::  ----------------------------------------------------------------------------


:PingIt

    goto PingIt1

::  ----------------------------------------------------------------------------
::  Subroutine Name:    PingIt
::
::  Synopsis:           Invoke the MVC_REST4Phones program to execute the task
::                      specified in the argument. See the Remarks for details.
::
::  Arguments:          %1 = Name of task to have MVC_REST4Phones_Tests.exe do.
::
::  Remarks:            MVC_REST4Phones_Tests.exe is a character-mode program,
::                      written in Microsoft Visual C#, that lives in directory
::                      C:\Scheduled-Tasks\Operations on the machine on which
::                      this script is expected to run.
::  ----------------------------------------------------------------------------

:PingIt1

    echo.
    WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" Executing Task %1
    echo.

    MVC_REST4Phones_Tests.exe "TestCaseFile=%~dp0WebHooks4Phone_TestCases.TXT" TestCaseName=%1 "FlatFileLogFileName=%~dpn0.LOG"

    if ERRORLEVEL 1 (
        WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" Ping Task, MVC_REST4Phones_Tests, reported an Exception with Test Case = %1
    )

    echo.
    WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" Finished with Task %1 and pausing for 2 seconds
    echo.

    WWSleep -2

    echo.
    WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" Leaving subroutine PingIt Task %1
    echo.

    goto :EOF


:TouchIt

    goto TouchIt1

::  ----------------------------------------------------------------------------
::  Subroutine Name:    TouchIt
::
::  Synopsis:           Invoke the Touch1File system utility against web.config
::                      in the directory specified in the argument. See the
::                      Remarks for details.
::
::  Arguments:          %1 = Absolute (fully qualified) name of directory where
::                           the target web.config is located.
::
::                      %2 = HARDER operates as an override to cause the program
::                           to physically alter the file by adding or updating
::                           a string that is embedded just below the XML prolog
::                           tag that MUST be the first line of a web.config
::                           file. Among other things, the line records the UTC
::                           time when the update occurred.
::
::                           To avoid disrupting operations, the line is
::                           formatted as an XML comment.
::
::  Remarks:            Touch1File.exe is a character-mode program, written in
::                      Microsoft Visual C#, that lives in directory C:\bin on
::                      the machine on which this script is expected to run that
::                      "touches" a file, updating its LastWriteTime in the file
::                      catalog. A command line argument tells it to continue
::                      (resume) script execution after it touches the file.
::  ----------------------------------------------------------------------------

:TouchIt1

    echo.
    WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" Executing Touch Task, Target = %1
    echo.

    if "%2" equ "HARDER" (
        C:\bin\Touch1File.exe "%1\Web.config" how2wait=cont touchmethod=AppendOrUpdateToken
    ) else (
        C:\bin\Touch1File.exe "%1\Web.config" how2wait=cont
    )

    if ERRORLEVEL 1 (
        WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" Touch Task reported an Exception against Target = %1\Web.config
    )

    echo.
    WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" Finished with Target %1 and returning from subroutine TouchIt
    echo.

    goto :EOF

::  +--------------------------------------------------------------------------+
::  |                   B e g i n   M a i n   R o u t i n e                    |
::  +--------------------------------------------------------------------------+

:SKIPREM

    goto SKIPRM1

::  ----------------------------------------------------------------------------
::  Display a startup message, then verify that command extensions are enabled.
::
::  NOTE:   On Wednesday, 05 February 2014, I discovered that the test below is
::          broken, but the outcome is more or less acceptable, because it kills
::          the script immediately.
::
::  Moreover, there are circumstances in which %~t0 is not interpolated, either,
::  but it is replaced by the modified date of the executing script under most
::  circumstances, even when the script is called from a secondary shell, even
::  when that shell happens to be COMMAND.COM.
::  ----------------------------------------------------------------------------

:SKIPRM1

    echo.
    WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" %0 version %~t0 Begin
    echo.
    ShowTime.exe

    if CMDEXTVERSION 1  goto ARBEITE

    echo.
    echo ----------------------------------------------------
    echo     This script requires command extensions to be enabled. Since they
    echo     are enabled, by default, they have been disabled, directly or by GPO.

    goto ERR_DONE

::  ----------------------------------------------------------------------------
::  Though it is technically legal to allow the command processor to parse these
::  comments, which it should skip, I have learned the hard way that doing so is
::  unwise, because it takes only one illegal character such as an angle bracket
::  in just the right spot to make a mess of your careful work, with output that
::  makes the cause hard to pin down.
::  ----------------------------------------------------------------------------

:ARBEITE

    setlocal
    goto ARBEITE1

::  --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
::  Sample log entry:
::
::    call :LOG_ITEM "%~dpn0.LOG" "%~dpn0_OUTPUT.LOG" "Step 1 of 4: Copy C:\Users\DAVE\Documents\Programming\Visual_Studio_6\DLL\ProcessInfo\ProcessInfo32\P6CutilLib1_Source_Repository\* to C:\Users\DAVE\Documents\Programming\Visual_Studio_6\DLL\P6CUtilLib1"
::  --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

:ARBEITE1

    if /I "%1" equ "Delay" (
        echo.
        WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" Delaying main task for 120 seconds, two minutes, IOW
        echo.
        WWSleep -120
        echo.
        WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" Delay done
        echo.
        for /f "tokens=1 delims=" %%i in (%~dp0Web_Site_Wake_Ups_Touches.TXT) do call :TouchIt %%i HARDER
    ) else (
        echo.
        WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" Starting the main task immediately
        echo.
    )

    WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" Absolute Task Definition File Name = %~dp0WebHooks4Phone_TestCases.TXT
    echo.
    WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" "Absolute Task List File Name       = %~dpn0.LOG"
    echo.
    WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" Make the current working directory = C:\Scheduled-Tasks\Operations
    echo.

    pushd C:\Scheduled-Tasks\Operations

    WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" Review the status of all IIS application pools, and recycle the relevant application pools.

    ServerRestartApplicationRecoveryManager runmode=Production cwd=C:\Scheduled-Tasks\Operations

    WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" Execute the tasks in order, allowing a brief delay between each

    for /f "tokens=1 delims=" %%i in (%~dp0Web_Site_Wake_Ups_Pings.TXT) do call :PingIt %%i

    WWLOGGER.EXE "%~dpn0.LOG" "-f^YYYY/^MM/^DD ^hh:^mm:^ss.^ttt" All tasks are done

    echo.
    echo ---------------------------------------------------------------------
    echo %~nx0 Done!
    echo ---------------------------------------------------------------------
    echo.
