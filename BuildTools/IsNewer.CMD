@echo off
setlocal
goto SKIPREM

:: =======================================================================================
:: IsNewer.CMD
::
:: Purpose:
::     Compare two files using WWIsFileNewer.exe and conditionally copy
::     the newer file to the older location. Produces structured,
::     colorized diagnostics when VT mode is available.
::
:: Usage:
::     call IsNewer.cmd <DestinationFile> <SourceFile>
::
:: Exit Codes:
::     0 = No copy needed
::     1 = Copy performed successfully
::     2 = Unexpected condition or copy failure
::     (Raw XCOPY exit codes are preserved when copy fails)
::
:: Date       By Synopsis
:: ---------- -- -------------------------------------------------------------------------
:: 2026/02/28 DG Create script by extracting like-named local subroutine.
:: 2026/03/01 DG Add VT color coding of messages.
:: 2026/03/10 DG Correct oversignts in format of echo messages.
:: =======================================================================================

:SKIPREM

:: Arguments

	set "DEST=%~1"
	set "SRC=%~2"

:: Local flag controls whether a copy is required.

	set "_PerformCopy_=FALSE"

	echo %DL_INFO% Comparing:
	echo %DL_INFO%     Destination = %DEST%
	echo %DL_INFO%     Source      = %SRC%

	WWIsFileNewer.exe "%DEST%" "%SRC%"
	set "RC=%ERRORLEVEL%"

:: Dispatch on exact return code.
	if "%RC%"=="110" goto SRC_MISSING
	if "%RC%"=="109" goto DEST_MISSING
	if "%RC%"=="4"   goto UNEXPECTED
	if "%RC%"=="3"   goto SRC_NEWER
	if "%RC%"=="2"   goto SAME_AGE
	if "%RC%"=="1"   goto SRC_OLDER

	goto UNEXPECTED

:SRC_MISSING

	echo %DL_INFO% Source missing - status code [110] preserving destination
	endlocal & exit /b 3

:DEST_MISSING

	echo %DL_INFO% Destination missing - status code [109] copying source
	set "_PerformCopy_=TRUE"
	goto CHECK_COPY

:UNEXPECTED

	echo %DL_ERROR% WWIsFileNewer returned unexpected code [%RC%]
	endlocal & exit /b 2

:SRC_NEWER

	echo %DL_INFO% Source newer - status code [3] copying source
	set "_PerformCopy_=TRUE"
	goto CHECK_COPY

:SAME_AGE

	echo %DL_INFO% Files are same age - status code [2] no copying needed
	endlocal & exit /b 0

:SRC_OLDER

	echo %DL_INFO% Source is older - status code [1] no copying needed
	endlocal & exit /b 0

:CHECK_COPY

	if /I not "%_PerformCopy_%"=="TRUE" goto DONE

	echo %DL_INFO% File %SRC% will be copied to %DEST%
	echo %DL_INFO% Copying:

	call XCopyF "%SRC%" "%DEST%" /f /r /v /y
	set "XCERR=%ERRORLEVEL%"

	if not "%XCERR%"=="0" (
    	echo %DL_ERROR% Copy failed - XCOPY returned [%XCERR%]
    	endlocal & exit /b %XCERR%
	)

	echo %DL_OK% Copy completed successfully
	endlocal & exit /b 1

:DONE
	endlocal & exit /b 0