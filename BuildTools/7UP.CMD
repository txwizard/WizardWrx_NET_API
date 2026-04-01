@echo off
goto SKIPREM

::  ============================================================================
::
::  Script Name:        7Up.CMD
::
::  Script Synopsis:    Make a clean backup of the SalesTalk Video Player source
::                      code repository.
::
::  Script Arguments:   This script has four arguments, as follows.
::
::                      %1 = Absolute (fully qualified) name of 7-Zip archive to
::                           create
::
::                      %2 = One of two values:
::
::                           1) Absolute name of directory to back up. Unless a
::                              third command line argument other than NO_PAUSE
::                              follows, all files in the specified directory go
::                              into the archive.
::
::                           2) Absolute filespec of files in a directory to go
::                              into the backup, in which case, the third
::                              argument, if present, MUST be NO_PAUSE.
::
::                      %3 = One of two values:
::
::                           1) NO_PAUSE causes the script to skip the WWPause
::                              call at the end of the script.
::
::                           2) Any other value is taken as the unqualified file
::                              name mask of the files in the directory given by
::                              the second argument to include in the archive
::                              given by the first argument.
::
::                      %4 = NO_PAUSE, as described above
::
::  Dependencies:       In addition to the local environment variable mentioned
::                      in the Remarks section that follows this section, this
::                      script has other dependenices, which must be stored in
::                      directories that are accessible via the PATH.
::
::                      NFiles.exe          This CPU-agnistic character-mode C#
::                                          utility reports the count of files
::                                          that match an absolute filespec.
::
::                      ShowTime.exe        This 64-bit C++ utility supersedes
::                                          the like-named shell script, but it
::                                          affords much greater control over the
::                                          display format.
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
::                      7z.exe              This is the command line interface
::                                          of the 7-zip archiver.
::
::  See Also:           7UP_Differential.CMD
::                      7Restore.CMD
::
::  Script Author:      David A. Gray
::
::  ----------------------------------------------------------------------------
::  Revision History
::  ----------------------------------------------------------------------------
::
::  Date       By  Synopsis
::  ---------- --- -------------------------------------------------------------
::  2021/01/17 DAG Create this script by adapting 7Restore and MakeZip_DAG.
::  2021/01/19 DAG Restore the step that clears the Archive flags in the source
::                 directory, and number the progress messages.
::  2021/01/26 DAG Hide the echo statements that list the input parameters.
::  2021/02/21 DAG Replace ShowTime.CMD with ShowTime.exe, a small 64-bit C++
::                 utility that affords much greater control over the format of
::                 the time display.
::  2022/04/06 DAG Add reference to derived script 7UP_Differential.CMD.
::  2022/05/29 DAG Eliminate listing the input directory.
::  2022/08/31 DAG Implement the new NO_PAUSE argument, along with suppression
::                 of output when there is nothing to back up.
::  2022/08/21 DAG Replace the arbitrary initialization of environment variable
::                 NO_PAUSE with a test for a value defined in the initial
::                 environment block.
::  2024/07/01 DAG Suppress the central directory listing at the end of the run.
::  2026/02/18 DAG Move the enumeration of command line parameters after SKIPREM
::                 so that it is executed on every run.
::  2026/02/28 DAG Adjust to look for 7z.exe in either of two locations.
::  ============================================================================

:SKIPREM

    echo %~0, version %~t0 Begin
    showtime.exe
    setlocal

    echo.
    echo ---------------------------------------------------------------------
    echo Info List Input Parameters: Parameter 1 = %~1
    echo                             Parameter 2 = %~2
    echo                             Parameter 3 = %~3
    echo                             Parameter 4 = %~4
    echo ---------------------------------------------------------------------

    echo.
    echo ---------------------------------------------------------------------
    echo Step 1 of 5: Back up the source directory.
    echo ---------------------------------------------------------------------
    echo.

    if defined _NO_PAUSE_ (
        echo Info: _NO_PAUSE_ environment string found in environment block.
        echo Info: _NO_PAUSE_ = %_NO_PAUSE_%
    ) else (
        set _NO_PAUSE_=DO_PAUSE

        if "%4" equ "NO_PAUSE" (
            echo Info: _NO_PAUSE_ environment string extablished by fourth argument.
            set _NO_PAUSE_=NO_PAUSE
        ) else (
            if "%3" equ "NO_PAUSE" (
                echo Info: _NO_PAUSE_ environment string extablished by third argument.
                set _NO_PAUSE_=NO_PAUSE
                set _BKUP_SOURCE_=%~2
            ) else (
                if "%3" equ "" (
                    set _BKUP_SOURCE_=%~2
                ) else (
                    set _BKUP_SOURCE_=%~2\%~3
                )
            )
        )
    )

    set _LIST_ARCHIVE_=FALSE

    if "%4" equ "LIST_ARCHIVE" (
        echo Info: _LIST_ARCHIVE_ environment string extablished by fourth argument.
        set _LIST_ARCHIVE_=TRUE
    ) else (
        if "%3" equ "LIST_ARCHIVE" (
            echo Info: _LIST_ARCHIVE_ environment string extablished by third argument.
            set _LIST_ARCHIVE_=TRUE
        )
    )

    NFiles.exe -s "%_BKUP_SOURCE_%"

    if %ERRORLEVEL% gtr 10 (

        rem --------------------------------------------------------------------
        rem Though it is installed into a different location on host SLIDERULE,
        rem there is a symbolic link to 7z.exe in directory C:\bin, so that it
        rem is always accessible through that directory.
        rem --------------------------------------------------------------------

        echo Using 7-Zip at: %Z7EXE%
        C:\bin\7z.exe a -r -stl -p%MAKEZIP_DAG% -mmt=on -mx9 -mtm=on -snh -snl "%~d1%~p1%~n1_%COMPUTERNAME%%~x1" "%_BKUP_SOURCE_%"
        @echo off
        echo.
        echo ---------------------------------------------------------------------
        echo Step 2 of 5: Clear the A flags in the source directory.
        echo ---------------------------------------------------------------------
        echo.

        attrib "%_BKUP_SOURCE_%" -a /s

        IF %_LIST_ARCHIVE_% EQU "TRUE" (
            echo.
            echo ---------------------------------------------------------------------
            echo Step 3 of 5: List the contents of the archive.
            echo ---------------------------------------------------------------------
            echo.

            "C:\Program Files\7-Zip\7z.exe" l "%~d1%~p1%~n1_%COMPUTERNAME%%~x1"
        ) else (
            echo.
            echo ---------------------------------------------------------------------
            echo SKIPPING Step 3 of 5: List the contents of the archive.
            echo ---------------------------------------------------------------------
            echo.
        )

        echo.
        echo ---------------------------------------------------------------------
        echo Step 4 of 5: Write-protect and give an unique name to the archive.
        echo ---------------------------------------------------------------------
        echo.

        Date2FN -r "%~d1%~p1%~n1_%COMPUTERNAME%%~x1"
    ) else (
        if %ERRORLEVEL% equ 10 (
            echo.
            echo ---------------------------------------------------------------------
            echo Skipping steps 1 through 4 because there is nothing to back up.
            echo ---------------------------------------------------------------------
            echo.
        ) else (
            if %ERRORLEVEL% equ  9 (
                echo
                echo.
                echo ---------------------------------------------------------------------
                echo Skipping steps 1 through 4 because The directory specified in
                echo %_BKUP_SOURCE_% cannot be found.
                echo ---------------------------------------------------------------------
                echo.
            )
        )
    )

    echo.
    echo ---------------------------------------------------------------------
    echo Step 5 of 5: List the archives.
    echo ---------------------------------------------------------------------
    echo.

    call dirX "%~d1%~p1%~n1_%COMPUTERNAME%*%~x1"

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Now is your chance to capture the timestamps and logs.
    echo ---------------------------------------------------------------------
    echo.

    if "%_NO_PAUSE_%" equ "DO_PAUSE" (
        WWPause
    )