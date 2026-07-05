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
::
::  2025/03/27 DG Make changes comparable to those in the IS_NEWER subroutine
::                of shell script PromoteScripts2Production.CMD in the
::                like-named subroutine of this shell script.
::
::  2025/07/07 DG Add Agent_Recent_Phone_Calls.html and its code behind,
::                Agent_Recent_Phone_Calls.js, to the lists of files.
::
::                Having discovered that this script is OK as written, I chose
::                to leave the typographical error correction.
::
::  2026/02/28 DG Move the IS_NEWER subroutine into its own shell script and add
::                VT colorized text.
::  ============================================================================


:SKIPREM

    setlocal

:: Load console formatting library.

    call "%~dp0ConsoleLib.CMD"

    echo %CF_BANNER%
    echo %DL_INFO% %~nx0 version %~t0 — Begin
    echo.

    ShowTime.exe

    set _RackSpaceCommon_=C:\inetpub\LeadLifeG2\Repository\_COMMON
    set _SlideRuleCommon_=\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON
    set _RackSpaceInetPub_=C:\inetpub
    set _SlideRuleInetPub_=\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web

    echo %CF_BANNER%
    echo %DL_INFO% Script Directory   = %~dp0
    echo %DL_INFO% Scratch Directory  = %TEMP%
    echo.
    echo %DL_INFO% _RackSpaceCommon_  = %_RackSpaceCommon_%
    echo %DL_INFO% _SlideRuleCommon_  = %_SlideRuleCommon_%
    echo.
    echo %DL_INFO% _RackSpaceInetPub_ = %_RackSpaceInetPub_%
    echo %DL_INFO% _SlideRuleInetPub_ = %_SlideRuleInetPub_%
    echo %CF_BANNER%
    echo.

:: ------------------------------------------------------------
:: TSCLIENT Redirector Probe
:: ------------------------------------------------------------

    echo %DL_INFO% Probing TSCLIENT redirector before proceeding...
    call "%~dp0TSClientProbe.CMD" "%_SlideRuleCommon_%"

if errorlevel 1 (
    rem TSCLIENT unavailable — abort
    echo %DL_ERROR% TSCLIENT redirector unavailable — aborting script
    WWPause.exe
    exit /b 1
)

echo %DL_OK% TSCLIENT redirector healthy — continuing
echo.

pushd %_RackSpaceInetPub_%

dir %_SlideRuleCommon_%\*.HTML /b > %TEMP%\%~n0_Common.TMP
dir %_RackSpaceCommon_%\Scripts\*.JS /b > %TEMP%\%~n0_Scripts.TMP
dir /s /b | findstr Views\Mobile | findstr cshtml > %TEMP%\%~n0_MobileIndex.TMP
dir _Layout_W2A_PROD.cshtml /s /b > %TEMP%\%~n0_Layout_W2A_PROD.TMP

echo %CF_BANNER%
echo %DL_INFO% Reciting relevant lists deposited in %TEMP%\%~n0_*.TMP
echo %CF_BANNER%
echo.

dir %TEMP%\%~n0_*.TMP
echo.

echo %CF_BANNER%
echo %DL_INFO% Update PURL COMMON HTML documents as needed
echo %CF_BANNER%
echo.

for /f %%i in (%TEMP%\%~n0_Common.TMP) do call IsNewer %_RackSpaceCommon_%\%%i %_SlideRuleCommon_%\%%i

echo.
echo %CF_BANNER%
echo %DL_INFO% Update PURL COMMON Scripts documents as needed
echo %CF_BANNER%
echo.

for /f %%i in (%TEMP%\%~n0_Scripts.TMP) do call IsNewer %_RackSpaceCommon_%\Scripts\%%i %_SlideRuleInetPub_%\Scripts\%%i

echo.
echo %CF_BANNER%
echo %DL_INFO% Update Mobile Index page document templates as needed
echo %CF_BANNER%
echo.

for /f %%i in (%TEMP%\%~n0_MobileIndex.TMP) do call IsNewer %%i %_SlideRuleInetPub_%\Views\Mobile\Index.cshtml

echo.
echo %CF_BANNER%
echo %DL_INFO% Update Mobile Index partial page documents as needed
echo %CF_BANNER%
echo.

for /f %%i in (%TEMP%\%~n0_Layout_W2A_PROD.TMP) do call IsNewer %%i %_SlideRuleInetPub_%\Views\Shared\_Layout_W2A_PROD.cshtml

echo.
echo %CF_BANNER%
echo %DL_INFO% Remove previous version saved by HTML editor
echo %CF_BANNER%
echo.

del %_SlideRuleInetPub_%\Views\Shared\_Layout_W2A_PROD_*.cshtml

echo.
echo %CF_BANNER%
echo %DL_OK% %~nx0 Done!
echo %CF_BANNER%
echo.

WWPause.exe

exit /b 0