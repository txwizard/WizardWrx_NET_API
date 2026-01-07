@echo off
goto SKIPREM

::    ==========================================================================
::
::    Name:             ShredDocumentBackups.CMD
::
::    Synopsis:         Use the SysInternals SDelete utility to destroy backups
::                      of documents in a directory and, optonally, its
::                      subdirectories.
::
::                      When done, wait for the user to review the results in
::                      the window.
::
::    Arguments:        None.
::
::    Dependencies:     WWPause     = This robust replacement for the anemic
::                                    internal PAUSE command (implemented by the
::                                    DOS shell, since the beginning), won't let
::                                    the script resume until you press the
::                                    ENTER key, or the CTRL-BREAK key
::                                    combination.
::
::                      del         = The internal DEL command supersedes the
::                                    SysInternals SDelete utility to save wear
::                                    and tear on the SSD.
::
::                      attrib.exe  = This Windows system utility is called when
::                                    requested to clear the RO attribute on all
::                                    files in the working directory, and its
::                                    subdirectories, when combined with the ALL
::                                    parameter.
::
::                      All dependent programs must be installed in a directory
::                      that is in the Windows PATH list.
::
::                      If _fCallFromScript_ is equal to Y, WWPause is skipped.
::
::    --------------------------------------------------------------------------
::    Revision History
::    --------------------------------------------------------------------------
::
::    Date       Author Version Synopsis
::    ---------- ------ ------- ------------------------------------------------
::    2013/03/17 DAG    1.00    Script created.
::    2017/10/20 DAG            Script modified so that it actually works as
::                              intended, and uses my modern version display.
::    2017/10/22 DAG            Correct an infinite loop when a degenerate case,
::                              a completely empty command line tail, is parsed.
::    2017/10/24 DAG            Correct an infinite loop when the most complex
::                              case, which specifies both arguments, is parsed.
::    2017/10/28 DAG            Manually wrap the informational message about
::                              the task to be performed.
::    2017/11/05 DAG            Align the comment sections at column 25, rather
::                              than 21, to conform to my current shell script
::                              coding convention.
::    2017/11/30 DAG            Skip WWPause if _fCallFromScript_ is Y.
::    2018/01/04 DAG            Adjust for apparently new behavior of pushd when
::                              fed a filtered argument, e. g., "%~1" instead of
::                              a bare string, e. g., %~1. Though this is new in
::                              Windows 10, since the filter is probably not
::                              technically necessary, it probably isn't treated
::                              as a breaking change.
::    2018/01/29 DAG            Add coverage for Microsoft Word backups, and for
::                              processing the directory in which a specified
::                              file lives. The first attempt, on 10 January, is
::                              a bust. Clarifying the debug messages today.
::    2021/02/21 DAG            Replace ShowTime.CMD with ShowTime.exe, a small
::                              64-bit C++ utility that affords much greater
::                              control over the format of the time display.
::    2023/06/28 DAG            Implement the CLEARRO parameter in such a way
::                              that it can be combined with the ALL parameter.
::    2023/08/21 DAG            Use the _NO_PAUSE_ environment string to provide
::                              an alternative to the established mechanism that
::                              bypasses WWPause.exe.
::    2024/01/07 DAG            Substitute the internal DEL command for SDelete.
::    ==========================================================================

::  ----------------------------------------------------------------------------
::  Since I like to keep the headnote with the routine that it deocuments, I put
::  it below the label that denotes its start, and immediately jump over it, to
::  keep the comments out of the execution path. This is the result of hard won
::  knowledtge about the strange things that can hapen when you have inline
::  comments and a stray character finds its way into the comment.
::  ----------------------------------------------------------------------------

:DO_WORK

    goto DO_WORK1

::  ----------------------------------------------------------------------------
::  Subroutine Name:    DO_WORK
::
::  Synopsis:           Delete files, if any, that match the Windows file glob
::                      passed in as its first (and only) argument.
::
::  Arguments:          %1 = Unqualified filespec to process
::
::  Remarks:            Local environment variable _fRecurse_ indicates whether
::                      the script should process subdirectories (if Y) or
::                      confine itself to the top level directory.
::
::                      Path strings fed in as parameters must be quoted because
::                      embedded parentheses confuse the parser, which also uses
::                      round brackets to delimit IF/ELSE blocks.
::
::                      Finally, :EOF is a reserved magic label that exits the
::                      current block. When the current block is a script, the
::                      entire script ends, but when the current block is a
::                      subroutine, the subroutine exits, and control returns to
::                      the calling script, whether that happens to be elsewhere
::                      in the same script file or in a different script file.
::  ----------------------------------------------------------------------------

:DO_WORK1

    if [!_fRecurse_!] equ [Y] (
        if exist "%~1" (
            dir  "%~1"    /s
            del  "%~1" /f /s
        ) else (
            echo.
            echo -------------------------------------------------------
            echo Info: No files found matching spec "%~1" in parent or child directories
            echo -------------------------------------------------------
            echo.
        )

        if [%2] equ [ClearRO] (
            echo.
            echo -------------------------------------------------------
            echo Info: CLEARing RO flags in top directory and its children
            echo -------------------------------------------------------
            echo.
            attrib * -r /s
        ) else (
            echo.
            echo -------------------------------------------------------
            echo Info: RO flags PRESERVED
            echo -------------------------------------------------------
            echo.
        )
    ) else (
        if exist "%~1" (
            dir  "%~1"
            del  "%~1" /f
        ) else (
            echo.
            echo -------------------------------------------------------
            echo Info: No files found matching spec "%~1" in parent directory
            echo -------------------------------------------------------
            echo.
        )

        if [%2] equ [ClearRO] (
            echo.
            echo CLEARing RO flags in top directory only
            echo.
            attrib * -r
        ) else (
            if [%2] neq [DEFER] (
                echo.
                echo -------------------------------------------------------
                echo Info: RO flags PRESERVED
                echo -------------------------------------------------------
                echo.
            )
        )
    )

    goto :EOF


::  +--------------------------------------------------------------------------+
::  |                   B e g i n   M a i n   R o u t i n e                    |
::  +--------------------------------------------------------------------------+

:SKIPREM

    echo -------------------------------------------------------
    echo Info: %~0, version %~t0
    echo -------------------------------------------------------

    showtime.exe

    setlocal EnableDelayedExpansion

    set _scriptNX_=%~nx0
    set _fRecurse_=N
    set _fClearRO_=N

:NEXT

    if [%1] equ []       goto ARBEITE

    if /i "%~1" equ "ALL" (
        set _fRecurse_=Y
        shift
        echo.
        echo -------------------------------------------------------
        echo Info: Recursing subdirectories
        echo -------------------------------------------------------
        echo.
        goto NEXT
    )

    if /i "%~1" equ "CLEARRO" (
        set _fClearRO_=Y
        shift
        echo.
        echo -------------------------------------------------------
        echo Info: Clearning RO flags
        echo -------------------------------------------------------
        echo.
        goto NEXT
    )

    if "%CD%" neq "%~1" (
        echo.
        echo Info: Argument 1, Complete             = %1
        echo Info: Argument 1, Drive and Directory  = %~dp1
        echo Info: Argument 1, Name, sans Extension = %~n1
        pushd %~dp1
        shift
        echo.
        echo -------------------------------------------------------
        echo Info: CWD changed to %1 by way of PushD, which ignores
        echo       its instructions unless given a bare argument.
        echo.
        echo       This is new behavior in Win10.
        echo -------------------------------------------------------
        echo.
        goto NEXT
    )

:ARBEITE

    echo.
    echo -------------------------------------------------------
    echo Info: Processing directory %CD%
    echo       and its subdirectories.
    echo -------------------------------------------------------
    echo.

    call :DO_WORK *.backup DEFER
    call :DO_WORK *.wbk    DEFER
    call :DO_WORK *.bak    DEFER
    call :DO_WORK *(?).*   DEFER

    if [!_fClearRO_!] equ [Y] (
        call :DO_WORK "*(??).*" ClearRO
    ) else (
        call :DO_WORK "*(??).*" IGNORE
    )

:ALL_DONE

    echo.
    echo -------------------------------------------------------
    echo Info: %_scriptNX_% done!
    echo -------------------------------------------------------

:CARBON

    echo.

    if "%_NO_PAUSE_%" equ "DO_PAUSE" (
        WWPause
    )

    if "%_fCallFromScript_%" neq "Y" WWPause