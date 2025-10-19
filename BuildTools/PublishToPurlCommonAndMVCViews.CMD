@echo off
goto SKIPREM

::  ============================================================================
::
::  Name:               PublishToPurlCommonAndMVCViews.CMD
::
::  Synopsis:           Update the PURL COMMON, Views/Mobile, and Views/Shared
::                      directories from the local repository.
::
::  Arguments:          None
::
::  Dependencies:       ShowTime.exe        = This 64-bit C++ utility supersedes
::                                            the like-named shell script, but
::                                            it affords much greater control
::                                            over the display format.
::
::                      XCopyF.CMD          = This shell script handles files
::                                            present in both directories more
::                                            gracefully than does XCOPY.exe
::                                            when left to its own devices, so
::                                            that all prompts emitted by XCOPY
::                                            are answered correctly.
::
::                      WWIsFileNewer.exe   = This 32-bit character mode Windows
::                                            program returns status codes that
::                                            inform the calling script of the
::                                            relative ages of a pair of files,
::                                            including the case when one or
::                                            both are missing. In particular,
::                                            status code 3 indicates that the
::                                            second of the two is newer, so
::                                            that it can be replaced, while a
::                                            file that is unchanged remains so.
::
::                      WWPause.exe         = This 32-bit character mode Windows
::                                            program replaces the intrinsic
::                                            pause command implemented by the
::                                            command processor.
::
::                      All dependent programs are expected to live in the same
::                      directory as does this script OR in a directory that
::                      is in the system PATH list.
::
::  Remarks:            The choice of name for the template file is intended to
::                      force it to sort first in alphabetical file lists.
::
::  See Also:           PromoteScripts2Production.CMD
::
::  ----------------------------------------------------------------------------
::  Revision History
::  ----------------------------------------------------------------------------
::
::  Date       By  Synopsis
::  ---------- -- -------------------------------------------------------------
::  2024/11/29 DG Initial version created and tested.
::  2025/03/27 DG Make changes comparable to those in the IS_NEWER subroutine
::                of shell script PromoteScripts2Production.CMD in the
::                like-named subroutine of this shell script.
::  2025/07/07 DG Add Agent_Recent_Phone_Calls.html and its code behind,
::                Agent_Recent_Phone_Calls.js, to the lists of files.
::
::                Having discovered that this script is OK as written, I chose
::                to leave the typographical error correction.
::  ============================================================================

:IS_NEWER

::  ----------------------------------------------------------------------------
::  Though environment variable _Promoted_Fixup_ has script scope because the
::  main script declares and initializes it, _PerformCopy_ has subroutine scope
::  because it is declared and initialized immediately following the setlocal
::  statement that is the first executable statement in this subroutine.
::
::  HOWEVER, since SETLOCAL creates copies of the environment block, a matching
::  ENDLOCAL causes it to be destroyed, including changes applied to variables
::  that have script scope. Therefore, you cannot expect new values assigned to
::  script scoped variables to survive.
::
::  A corollary of this is that ALL environment variables declared and used in a
::  subroutine MUST be initialized to a known good initial value on EVERY call.
::  ----------------------------------------------------------------------------

    set _PerformCopy_=FALSE

    WWIsFileNewer.exe "%~1" "%~2"

    if errorlevel 110 (
        echo Info: File %2 is MISSING - Exit Code = 110.
        echo       File %1 will be preserved.
        goto :EOF
    )

    if errorlevel 109 (
        echo Info: File %1 is MISSING - Exit Code = 109.
        echo       File %2 will be copied to %1.
        set _PerformCopy_=TRUE
    )

    if errorlevel   4 (
        echo Info: WWIsFileNewer UNANTICIPATED status code = %errorlevel%
        goto :EOF
    )

    if errorlevel   3 (
        echo Info: %2 is NEWER than %1 - Exit Code = 3.
        echo       File %2 will be copied to %1.
        set _PerformCopy_=TRUE
    )

::  ----------------------------------------------------------------------------
::  The file at the location specified by argument 2 is copied to the location
::  specified by argument 1 when the exit code returned by WWIsFileNewer.exe is
::  either 110, indicating that the location specified by argument 1 is vacant,
::  or 3, indicating that the file at the location specified by argument 1 is
::  older than the file at the location specified by argument 2.
::
::  Since Windows NT command scripts lack support for complex expressions, a new
::  locally scoped environment variable is initialized to FALSE on entry to this
::  subroutine, and is set to TRUE when either of the above-described conditions
::  is true. Testing the value of that environment variable substitutes for the
::  missing complex expression capability.
::  ----------------------------------------------------------------------------

    if "%_PerformCopy_%" equ "TRUE" (
        call XCopyF %2 %1 /f /r /v /y
        goto :EOF
    )

    if errorlevel   2 (
        echo Info: %2 is SAME AGE as %1 - Exit Code = 2.
        goto :EOF
    )

    if errorlevel   1 (
        echo Info: %2 is OLDER than %1 - Exit Code = 1.
        goto :EOF
    ) else (
        echo Info: WWIsFileNewer UNANTICIPATED status code = %errorlevel%
    )

    goto :EOF


:SKIPREM

    setlocal

    echo ---------------------------------------------------------------------
    echo %0, version %~t0 Begin
    echo.
    ShowTime.exe

    set _RackSpaceCommon_=C:\inetpub\LeadLifeG2\Repository\_COMMON
    set _SlideRuleCommon_=\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON
    set _RackSpaceInetPub_=C:\inetpub
    set _SlideRuleInetPub_=\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Script Directory   = %~dp0
    echo       Scratch Directory  = %TEMP%
    echo.
    echo       _RackSpaceCommon_  = %_RackSpaceCommon_%
    echo       _SlideRuleCommon_  = %_SlideRuleCommon_%
    echo.
    echo       _RackSpaceInetPub_ = %_RackSpaceInetPub_%
    echo       _SlideRuleInetPub_ = %_SlideRuleInetPub_%
    echo ---------------------------------------------------------------------
    echo.

    pushd %_RackSpaceInetPub_%

    dir %_SlideRuleCommon_%\*.HTML /b > %TEMP%\%~n0_Common.TMP
    dir %_RackSpaceCommon_%\Scripts\*.JS /b > %TEMP%\%~n0_Scripts.TMP
    dir /s /b | findstr Views\Mobile | findstr cshtml > %TEMP%\%~n0_MobileIndex.TMP
    dir _Layout_W2A_PROD.cshtml /s /b > %TEMP%\%~n0_Layout_W2A_PROD.TMP

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Recite relevant lists deposited in %TEMP%\%~n0_*.TMP
    echo ---------------------------------------------------------------------
    echo.

    dir %TEMP%\%~n0_*.TMP

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Update PURL COMMON HTML documents as needed
    echo ---------------------------------------------------------------------
    echo.

    for /f %%i in (%TEMP%\%~n0_Common.TMP) do call :IS_NEWER %_RackSpaceCommon_%\%%i %_SlideRuleCommon_%\%%i

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Update PURL COMMON Scripts documents as needed
    echo ---------------------------------------------------------------------
    echo.

    for /f %%i in (%TEMP%\%~n0_Scripts.TMP) do call :IS_NEWER %_RackSpaceCommon_%\Scripts\%%i %_SlideRuleInetPub_%\Scripts\%%i

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Update Mobile Index page document templates as needed
    echo ---------------------------------------------------------------------
    echo.

::  ----------------------------------------------------------------------------
::  The file names listed herein are absolute (fully qualified) names.
::  ----------------------------------------------------------------------------

    for /f %%i in (%TEMP%\%~n0_MobileIndex.TMP) do call :IS_NEWER %%i %_SlideRuleInetPub_%\Views\Mobile\Index.cshtml

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Update Mobile Index partial page documents as needed
    echo ---------------------------------------------------------------------
    echo.

    for /f %%i in (%TEMP%\%~n0_Layout_W2A_PROD.TMP) do call :IS_NEWER %%i %_SlideRuleInetPub_%\Views\Shared\_Layout_W2A_PROD.cshtml

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Remove the previous version saved by the HTML document editor
    echo ---------------------------------------------------------------------
    echo.

    del %_SlideRuleInetPub_%\Views\Shared\_Layout_W2A_PROD_*.cshtml

    echo.
    echo ---------------------------------------------------------------------
    echo %~nx0 Done!
    echo ---------------------------------------------------------------------
    echo.

    WWPause.exe