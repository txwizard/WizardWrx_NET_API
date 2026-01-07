@echo OFF
::  echo on
goto SKIPREM

::  ============================================================================
::
::  Name:               XCOPYF.CMD
::
::  Synopsis:           Use the XCOPY system utility program to copy a file into
::                      a directory, giving it a new name.
::
::  See Also:           XCOPYD.CMD            This is the companion script that
::                                            tells XCOPY that the destination
::                                            is a directory.
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
::                      XcopyDestIsFile.TXT = This standard ASCII text file has
::                                            the one-character response, along
::                                            with a carriage return/line feed,
::                                            that answers the disambiguation
::                                            question displayed by XCOPY when a
::                                            command includes both source and
::                                            destination file names. Please see
::                                            Remarks.
::
::                                            XcopyDestIsFile.TXT and XCOPYD.CMD
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
::  ============================================================================

:SKIPREM

    if exist "%~dp0XcopyDestIsFile.TXT" (
        xcopy %1 %2 %3 %4 %5 %6 %7 %8 %9 < "%~dp0XcopyDestIsFile.TXT"
        exit /b %ERRORLEVEL%
    ) else (
        echo.
        echo -------------------------------------------------------------------
        echo ERROR: Response file %~dp0XcopyDestIsFile.TXT is missing.
        echo.
        echo A copy must always accompany this script file,
        echo %0.
        echo -------------------------------------------------------------------
        echo.

        WWPause
        exit /b 99
    )