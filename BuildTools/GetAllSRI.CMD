@echo off
goto SKIPREM

::  ============================================================================
::
::  Script Name:        GetAllSRI.CMD
::
::  Script Synopsis:    Use OpenSSL to create a new set of SubResource Integrity
::                      digest files.
::
::  Script Arguments:   This script has NO ARGUMENTS.
::
::  Dependencies:       All dependencies, listed below, must be stored in the
::                      same directory or in a directory that is accessible via
::                      the Windows PATH environment string.
::
::                      7UP.CMD             This Windows NT command script uses
::                                          the 7-Zip character-mode system tol
::                                          to make a backup of the SRI files.
::
::                      GET_SRI.CMD         This Windows NT command script uses
::                                          OpenSSL to create a SubResource
::                                          Integrity Digest file for each JS
::                                          file in the PURL COMMON directory.
::
::                      ShowTime.exe        This 64-bit C++ utility supersedes
::                                          the like-named shell script, but it
::                                          affords much greater control over the
::                                          display format.
::
::                      STT_VideoPlayer_Promoted_Fixup.exe  This character-mode
::                                                          C# program injects
::                                                          the SRI strings into
::                                                          the PURL COMMON HTML
::                                                          documents.
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
::
::  Script Author:      David A. Gray
::
::  ----------------------------------------------------------------------------
::  Revision History
::  ----------------------------------------------------------------------------
::
::  Date       By  Synopsis
::  ---------- --- -------------------------------------------------------------
::  2026/04/12 DAG Create this script from scratch by capturing commands entered
::                 by hand into a command prompt.
::  ============================================================================

:SKIPREM

    echo %~0, version %~t0 Begin
    showtime.exe

    if exist %TEMP%\*_SRI_*SHA???.TMP (
        echo Deleting computed SRI values found in user scratch directory.
        del %TEMP%\*_SRI_*SHA???.TMP
    ) else (
        echo The user scratch directory is devoid of computed SRI values.
    )

	cd /d "%~dp0"

	call GET_SRI.CMD D:\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\Words2Actions_Recorder_Forms.js
	call GET_SRI.CMD D:\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\CallMapGrid.js
	call GET_SRI.CMD D:\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\LLCommon.js
	call GET_SRI.CMD D:\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\Agent_Recent_Phone_Calls.js
	call GET_SRI.CMD D:\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\UploadFile.js
	call GET_SRI.CMD D:\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\LeadLifeJSHelpersLib.js
	call GET_SRI.CMD D:\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\STT_VideoPlayer.js
	call GET_SRI.CMD D:\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\LeadLifeJSHelpersGlobals.js
	call GET_SRI.CMD D:\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\KeywordHighlighter.js
	call GET_SRI.CMD D:\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\Mobile_Index.js
	call GET_SRI.CMD D:\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\LeadLife_InputMask_Engine.js

	call C:\bin\7UP.CMD D:\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON\SubResource_Integrity_String_Backups\SRI_Digests.7z %TEMP%\*_SRI_*SHA???.TMP

	STT_VideoPlayer_Promoted_Fixup.exe

	WWPause.exe