@echo OFF
goto SKIPREM

::  ============================================================================
::
::  Name:               XCOPYD.CMD
::
::  Synopsis:           Hardened, colorized, multi-file-aware XCOPY wrapper that
::                      uses the XCOPY system utility program to copy files into
::                      a directory named in its second argument.
::
::  Behavior:           * Answers XCOPY "file or directory?" prompt via
::                        XcopyDestIsFile.TXT
::                      * Detects XCOPY filename normalization (e.g.,
::                        foo.exe.config --> foo.config)
::                      * Handles wildcard source masks (multi?file)
::                      * Emits loud, colorized, structured diagnostics when a
::                        configuration file is silently renamed
::                      * Returns XCOPY’s exit code unchanged
::
::  Color:              * Optional VT variables: FG_YELLOW, FG_RED, FG_CYAN,
::                        ANSI_RESET
::                      * If undefined, output remains readable.
::
::  See Also:           XCOPYF.CMD            This is the companion script that
::                                            tells XCOPY that the destination
::                                            is a file.
::
::  Arguments:          Since this is intended as a drop-in replacement for
::                      XCOPY.EXE, the syntax is identical. Please see Remarks.
::
::  Environment:        Nothing special.
::
::  Dependencies:       XCOPY.EXE           = This is the Windows system utility
::                                            around which this shell script is
::                                            wrapped.
::
::                      XcopyDestIsDir.TXT  = This standard ASCII text file has
::                                            the one-character response, along
::                                            with a carriage return/line feed,
::                                            that answers the disambiguation
::                                            question displayed by XCOPY when a
::                                            command includes both source and
::                                            destination path names. Please see
::                                            Remarks.
::
::                                            XcopyDestIsDir.TXT and XCOPYD.CMD
::                                            must go into the same directory.
::
::                                            Experience gained from internal
::                                            production usage led me to add a
::                                            sanity check, which displays an
::                                            actionable error message when this
::                                            file isn't where it is supposed to
::                                            be.
::
::                                            This script returns an exit code
::                                            of 99 when the response file is
::                                            missing. I chose 99 because it is
::                                            way above the highest documented
::                                            exit code returned by xcopy.exe.
::
::  Remarks:            A missing response file elicits one of those technically
::                      correct, but useless error messages: "The system cannot
::                      find the file specified."
::
::                      After I spent about two hours resolving this issue, for
::                      the 3rd or 4th time in about that many weeks, I decided
::                      that a decent sanity check would be appreciated.
::
::                      Notwithstanding information in the XCOPY documentation
::                      that suggests otherwise, I haven't found a combination
::                      of switches that can be used to suppress this prompt by
::                      giving XCOPY the necessary information in its command
::                      line. After months of more and more frequent need for
::                      this sort of XCOPY command, I decided that it was time
::                      to condense it to a shell command that would work as a
::                      drop-in replacement for XCOPY.
::
::                      Saveral weeks ago, I decided to see whether the response
::                      could be piped in as part of the command. When that met
::                      with failure, the die was cast; I would make this pair
::                      of shell scripts.
::
::                      I debated about making this a regular .BAT file, but I
::                      decided against it because it uses a couple of shell
::                      tricks that work in both, but didn't come into being
::                      until around the time of Windows NT 4. By making this a
::                      Windows NT command script, I am putting you on notice
::                      that it stretches the limits of old-fashioned MS-DOS
::                      batch files. The most important of these features is the
::                      trick that enables me to separate the path from the name
::                      of the shell script, and use it to locate the response
::                      file, which must reside in the same directory.
::
::  Author:             David A. Gray
::
::  Created Date:       Friday, 25 April 2014
::
::  ----------------------------------------------------------------------------
::  Revision History
::  ----------------------------------------------------------------------------
::
::  Date       By  Version Synopsis
::  ---------- --- ------- -----------------------------------------------------
::  2014/04/25 DAG 1.00    Initial implementation.
::  2014/08/16 DAG 2.00    Return the exit code to the caller.
::  2014/11/23 DAG 3.00    Substitute a clear, actionable message for the bland
::                         message, "The system cannot find the file specified."
::  2026/03/20 DAG 4.00    Adapt to warn loudly when legacy xcopy file renaming
::                         causes the configuration file that sits alongside an
::                         EXE to be surreptitiously renamed.
::  2026/03/24 DAG 4.01    Implement VT colors by calling ConsoleLib.CMD and add
::                         an audible alarm.
::  ============================================================================

:SKIPREM

	call "%~dp0ConsoleLib.CMD"

:: -----------------------------------------------------------------------------
:: Validate response file.
:: -----------------------------------------------------------------------------

    if not exist "%~dp0XcopyDestIsDir.TXT" (
        echo.
        echo -------------------------------------------------------------------
        echo %FG_RED%ERROR:%ANSI_RESET% Response file %~dp0XcopyDestIsDir.TXT is missing.
        echo A copy must always accompany this script file, %0.
        echo -------------------------------------------------------------------
        echo.
        WWPause
        exit /b 99
    )

:: -----------------------------------------------------------------------------
:: Prepare temp files.
:: -----------------------------------------------------------------------------

    set "_XC_BEFORE=%TEMP%\xcopy_before_%RANDOM%.lst"
    set "_XC_AFTER=%TEMP%\xcopy_after_%RANDOM%.lst"
    set "_XC_DELTA=%TEMP%\xcopy_delta_%RANDOM%.lst"
    set "_XC_SRCNAMES=%TEMP%\xcopy_srcnames_%RANDOM%.lst"

:: -----------------------------------------------------------------------------
:: Make both path strings short (8.3).
:: -----------------------------------------------------------------------------

    for %%F in ("%_XC_BEFORE%") do set "_XC_BEFORE_S=%%~sfF"
    for %%F in ("%_XC_AFTER%")  do set "_XC_AFTER_S=%%~sfF"

:: -----------------------------------------------------------------------------
:: Snapshot destination BEFORE copy.
:: -----------------------------------------------------------------------------

    dir /b "%~2" > "%_XC_BEFORE%" 2>NUL

:: -----------------------------------------------------------------------------
:: Snapshot source filenames matched by wildcard.
:: -----------------------------------------------------------------------------

    dir /b %1 > "%_XC_SRCNAMES%" 2>NUL

:: -----------------------------------------------------------------------------
:: Perform the XCOPY operation.
:: -----------------------------------------------------------------------------

    xcopy %1 %2 %3 %4 %5 %6 %7 %8 %9 < "%~dp0XcopyDestIsDir.TXT"
    set "_XCOPY_RC=%ERRORLEVEL%"

:: -----------------------------------------------------------------------------
:: Snapshot destination AFTER copy.
:: -----------------------------------------------------------------------------

    dir /b "%~2" > "%_XC_AFTER%" 2>NUL

:: -----------------------------------------------------------------------------
:: Compute delta: files created by XCOPY.
:: -----------------------------------------------------------------------------

    ListDelta "%_XC_BEFORE%" "%_XC_AFTER%" "%_XC_DELTA%"

:: -----------------------------------------------------------------------------
:: Compare delta filenames to source filenames.
:: -----------------------------------------------------------------------------

    for /f "usebackq delims=" %%D in ("%_XC_DELTA%") do call :XCOPYF_HandleDelta "%%D"

    goto :XCOPYF_AfterLoop

:XCOPYF_HandleDelta

    set "_XC_NEWFILE=%~1"

:: Check if this new file name appears in the source list

    findstr /i /x "%_XC_NEWFILE%" "%_XC_SRCNAMES%" >NUL
    if errorlevel 1 (
        echo.
        echo %FG_YELLOW%[XCopyF] WARNING:%ANSI_RESET% XCOPY filename normalization detected
        echo     %FG_CYAN%Source mask:%ANSI_RESET% %1
        echo     %FG_CYAN%Destination:%ANSI_RESET% %2
        echo     %FG_CYAN%Created:%ANSI_RESET%     %_XC_NEWFILE%
        echo     %FG_CYAN%Cause:%ANSI_RESET%       XCOPY wildcard normalization
        echo.
        echo     %FG_YELLOW%This does NOT stop the operation, but the destination%ANSI_RESET%
        echo     %FG_YELLOW%may not match the expected file layout.%ANSI_RESET%
        echo.
        BeepBeep
    )

    goto :eof

:XCOPYF_AfterLoop

:: -----------------------------------------------------------------------------
:: Pack out your trash.
:: -----------------------------------------------------------------------------

    del "%_XC_BEFORE%" 2>NUL
    del "%_XC_AFTER%" 2>NUL
    del "%_XC_DELTA%" 2>NUL
    del "%_XC_SRCNAMES%" 2>NUL

    exit /b %_XCOPY_RC%
