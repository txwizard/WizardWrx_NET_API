@echo off
goto SKIPREM

::  ============================================================================
::
::  Name:               PromoteScripts2Production.CMD
::
::  Synopsis:           Promote new scripts and HTML pages from STAGING to
::                      PRODUCTION.
::
::  Arguments:          None
::
::  Usage Notes:        Search for " - Exit Code = " and compare the status code
::                      against the following list.
::
::                          1   = The STAGING file is OLDER than the prodcution
::                                file, probably because the production file was
::                                changed in place. Though it could replace the
::                                staging file with a copy of the production
::                                file, for now, we take the conservative
::                                approach, leaving the staging file unchanged.
::
::                          2   = The STAGING and production files are the SAME
::                                AGE, and are presumed to be identical. A more
::                                robust implementation, presently considered as
::                                overkill, whould leverage my message digest
::                                comparer to confirm that they are identical.
::
::                          3   = The STAGING file is NEWER than the production
::                                file. Hence, the Staging file will REPLACE the
::                                Production file.
::
::                          109 = The STAGING file is MISSING.
::
::                          110 = The PRODUCTION file is MISSING.
::
::  Dependencies:       ShowTime.exe        = This 64-bit C++ utility supersedes
::                                            the like-named shell script, but
::                                            it affords much greater control
::                                            over the display format.
::
::                      STT_VideoPlayer_Promoted_Fixup.exe = This C# character
::                                                           mode program fixes
::                                                           up script include
::                                                           script tags in the
::                                                           SalesTalk 2KnowWho
::                                                           video player.
::
::                      WWIsFileNewer.exe   = This 32-bit character mode Windows
::                                            program uses exit codes to tell a
::                                            shell script or other calling
::                                            process which of two files is
::                                            newer based on their last write
::                                            times.
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
::  Remarks:            Since a SubResource Integrity digest is always computed
::                      and there is currently no record of its previous value,
::                      every HTML document that references a JavaScript file or
::                      a Cascading Style Sheet file gets upgraded whenever this
::                      script executes.
::
::                      Though it could theoretically be implemented as a self
::                      contained PowerShell script, since I see little that it
::                      adds to my established collection of shell scripts and
::                      character mode programs that return exit codes to such,
::                      I have never expended the effort that would be required
::                      to learn enough PowerShell to do so.
::
::                      Meanwhile, this script is a testament to the power that
::                      old fashioned Windows NT command scripts afford to savvy
::                      programmers and administrators.
::
::  See Also:           PublishToPurlCommonAndMVCViews.CMD
::
::  Work Began:         Saturday, 24 December 2022
::
::  ----------------------------------------------------------------------------
::  Revision History
::  ----------------------------------------------------------------------------
::
::  Date       By Synopsis
::  ---------- -- -------------------------------------------------------------
::  2022/12/26 DG Initial version created and tested.
::  2022/12/27 DG Correct a logic error that surfaced in the first real use.
::  2023/01/08 DG Purge the files in the content staging directory.
::  2023/02/05 DG Add Webinar_Followup_Request_Form_DEV.js to the list.
::  2023/02/15 DG Add LeadLife_InputMask_Engine_DEV.js to the list, and put the
::                list in alphabetical order.
::  2023/03/27 DG Add Walking2Car_DEV.js to the list.
::  2023/04/02 DG Rename Walking2Car_DEV.js to Mobile_Index_DEV.js.
::  2023/06/29 DG Add Words2Actions_Recorder_Forms_DEV.js to the list.
::  2023/08/17 DG Add BootBoxtLaboratory.js as a commented-out item, and sort
::                the list alphabetically by production file name.
::  2023/08/18 DG 1) Uncomment BootBoxtLaboratory to restore it to the list,
::                   since there is already a file in the production directory.
::                2) Add Words2Actions_Form_TEMPLATE.HTML to the list of files.
::  2023/08/20 DG For files processed by STT_VideoPlayer_Promoted_Fixup.exe, set
::                a private environment variable to indicate that the program is
::                to be called, then do so only when the flag is set.
::                child scripts continue without interruption.
::  2023/09/07 DG Add LLCommon_DEV.js to the list of scripts.
::  2023/12/11 DG Account for relocation of STT_VideoPlayer.html to PURL COMMON.
::  2024/10/27 DG Impement SubResource Integrity (SRI) digest computation and
::                replace hard coded paths with paths that expand an environment
::                string that contains the absolute name of the solution root
::                directory, so that this script can be executed from the build
::                tools directory.
::  2024/10/30 DG Make the directory from which this script loaded the working
::                directory so that STT_VideoPlayer_Promoted_Fixup.exe can find
::                its dependent assemblies without recourse to the Windows PATH.
::  2024/11/10 DG Implement a command line argument that expects the name of a
::                file that was changed in place. When the parameter is present
::                and is the absolute (fully qualified) name of a JavaScript or
::                Cascading Style Sheets file, its SubResource Integrity digest
::                is computed, and STT_VideoPlayer_Promoted_Fixup.exe is invoked
::                to update relevant HTML pages.
::  2025/01/15 DG Change the program so that it ALWAYS computes SubResource
::                Integrity digests for ALL JavaScript and Cascading Style Sheet
::                files.
::  2025/03/13 DG Temporarily comment STT_VideoPlayer_Promoted_Fixup.exe out to
::                prevent execution until I finish debugging it.
::  2025/03/13 DG Restore STT_VideoPlayer_Promoted_Fixup.exe out to allow normal
::                execution until I finish debugging it.
::  2025/03/25 DG Add NotesSearch_Domain_Wide.HTML to the list of files.
::  2025/03/27 DG Adapt to support copying of missing files into the production
::                directory from the staging directory, so that new files "just
::                work" as you have every right to expect. While fixing this, I
::                discovered that the error messages for exit codes 109 and 110
::                were transposed.
::  2025/07/07 DG Add Agent_Recent_Phone_Calls.html and its code behind,
::                Agent_Recent_Phone_Calls.js, to the lists of files.
::  2025/10/02 DG Add CallMapGrid.js to the list of files.
::  2025/10/08 DG Make the file name expressions in subroutine IS_NEWER case
::                INsensitive and add an expression to look for the new
::                Agent_Recent_Phone_Calls.html HTML form.
::  ============================================================================

:GET_SRI

    echo.
    echo Compute new SRI (SubResource Integrity) digest for file %~1
    setlocal

    if /I [%~x1] equ [.js] (
        set _SRI_Suffix_=_SRI_JS_SHA384
    ) else (
        if /I [%~x1] equ [.css] (
            set _SRI_Suffix_=_SRI_CSS_SHA384
        ) else (
            echo ERROR: File extension %~x1 is UNSUPPORTED.
            echo        File %~1 is being SKIPPED.
            goto :EOF
        )
    )

    set _SRI_Absolute_FileName_=%TEMP%\%~n1%_SRI_Suffix_%.TMP
    pushd %OpenSSL_Exe_Path%
    openssl dgst -sha384 -binary "%~1" | openssl base64 -A > %_SRI_Absolute_FileName_%
    popd
    echo.
    echo SHI digest file name = %_SRI_Absolute_FileName_%
    echo.
    echo SRI:
    echo.
    type %_SRI_Absolute_FileName_%

::  ----------------------------------------------------------------------------
::  This needs two echo commands, of which the first scrolls the console up
::  after the typed output, while the second renders the desired empty line.
::  ----------------------------------------------------------------------------

    echo.
    echo.

    goto :EOF


:IS_NEWER

::  ----------------------------------------------------------------------------
::  Though environment variable _Promoted_Fixup_ has script scope because the
::  main script declares and initializes it, _PerformCopy_ has subroutine scope
::  because it is declared and initialized immediately following the setlocal
::  statement that is the first executable statement in this subroutine.
::
::  HOWEVER, since SETLOCAL creates copies the environment block, a matching
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
::
::  When the value of script environment variable _Promoted_Fixup_ is set to Y,
::  file copying is omitted because the input file must be TRANSFORMED, which is
::  handled by calling STT_VideoPlayer_Promoted_Fixup.exe in the last task of
::  this shell script.
::  ----------------------------------------------------------------------------

    if /I "%_PerformCopy_%" equ "TRUE" (
        if /I "%~nx2" equ "STT_VideoPlayer.html" (
            echo Info: Since the STAGING version of STT_VideoPlayer.html is newer, enable the _Promoted_Fixup_ flag.
            set _Promoted_Fixup_=Y
        ) else (
            if /I "%~nx2" equ "Words2Actions_Form_TEMPLATE.HTML" (
                echo Info: Since the STAGING version of Words2Actions_Form_TEMPLATE.HTML is newer, enable the _Promoted_Fixup_ flag.
                set _Promoted_Fixup_=Y
            ) else (
                if /I "%~nx2" equ "NotesSearch_Domain_Wide.HTML" (
                    echo Info: Since the STAGING version of NotesSearch_Domain_Wide.HTML is newer, enable the _Promoted_Fixup_ flag.
                    set _Promoted_Fixup_=Y

                ) else (
                    if /I "%~nx2" equ "Agent_Recent_Phone_Calls.html" (
                        echo Info: Since the STAGING version of Agent_Recent_Phone_Calls.html is newer, enable the _Promoted_Fixup_ flag.
                        set _Promoted_Fixup_=Y
                    ) else (
                        if /I "%~x2" equ ".JS" (
                            echo Info: Since the STAGING version of this JavaScript is newer, compute its SubResource Integrity Digest and enable the _Promoted_Fixup_ flag.
                            set _Promoted_Fixup_=Y
                            echo.
                            echo Script Home Directory Name = %~dp0
                            echo.
                            call XCopyF %2 %1 /f /r /v /y
                            call :GET_SRI %~dp0%1
                        ) else (
                            if /I "%~x2" equ ".CSS" (
                                echo Info: Since the STAGING version of this Cascading Style Sheet is newer, compute its SubResource Integrity Digest and enable the _Promoted_Fixup_ flag.
                                set _Promoted_Fixup_=Y
                                call XCopyF %2 %1 /f /r /v /y
                                call :GET_SRI %~dp0%1
                            ) else (
                                echo Info: This file is presently exempt from SubResource Integrity.
                                call XCopyF %2 %1 /f /r /v /y
                            )
                        )
                    )
                )
            )
        )

        goto :EOF
    )

    if errorlevel   2 (
        echo Info: %2 is SAME AGE as %1 - Exit Code = 2.

        if /I "%~x2" equ ".JS" (
            echo Info: Though the source file is unchanged, a JavaScript file requires a SubResource Integrity digest.
            call :GET_SRI %~dp0%1
        ) else (
            if /I "%~x2" equ ".CSS" (
                echo Info: Though the source file is unchanged, a JavaScript file requires a SubResource Integrity digest.
                call :GET_SRI %~dp0%1
            ) else (
                echo Info: This file is presently exempt from SubResource Integrity.
            )
        )

        goto :EOF
    )

    if errorlevel   1 (
        echo Info: %2 is OLDER than %1 - Exit Code = 1.

        if /I "%~x2" equ ".JS" (
            echo Info: Though the source file is unchanged, a JavaScript file requires a SubResource Integrity digest.
            call :GET_SRI %~dp0%1
        ) else (
            if /I "%~x2" equ ".CSS" (
                echo Info: Though the source file is unchanged, a JavaScript file requires a SubResource Integrity digest.
                call :GET_SRI %~dp0%1
            ) else (
                echo Info: This file is presently exempt from SubResource Integrity.
            )
        )

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

    echo.
    echo Script Home Directory Name = %~dp0
    echo.
    echo ---------------------------------------------------------------------
    echo Info: Initialize script variablea and ensure that leftovers from
    echo       previous runs are removed.
    echo ---------------------------------------------------------------------
    echo.

    pushd "%~dp0"

    set _fCallFromScript_=Y
    set _NO_PAUSE_=NO_PAUSE
    set _Promoted_Fixup_=N

    if exist %TEMP%\*_SRI_*SHA???.TMP (
        echo Deleting computed SRI values found in user scratch directory.
        del %TEMP%\*_SRI_*SHA???.TMP
    ) else (
        echo The user scratch directory is devoid of computed SRI values.
    )

    if "%1" == "" (
        echo Performing the default tasks.
    ) else (
        if exist "%~1" (
            if /I [%~x1] equ [.js] (
                echo.
                echo ---------------------------------------------------------------------
                echo Generating SubResource Integrity string covering file %~1
                echo ---------------------------------------------------------------------
                echo.
                call :GET_SRI %~1
                set _Promoted_Fixup_=Y
                GOTO :PROMOTE
            ) else (
                if /I [%~x1] equ [.css] (
                    echo.
                    echo ---------------------------------------------------------------------
                    echo Generating SubResource Integrity string covering file %~1
                    echo ---------------------------------------------------------------------
                    echo.
                    call :GET_SRI %~1
                    set _Promoted_Fixup_=Y
                    GOTO :PROMOTE
                )
            )
        ) else (
            echo.
            echo ---------------------------------------------------------------------
            echo File cannot be found: %~1
            echo ---------------------------------------------------------------------
            echo.
            GOTO :DONE
        )
    )

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Update the JavaScript code-behind and libraries.
    echo ---------------------------------------------------------------------
    echo.

    call :IS_NEWER ..\LeadLife.Web\Scripts\Agent_Recent_Phone_Calls.js      ..\LeadLife.Web\Scripts\STAGING\Agent_Recent_Phone_Calls_DEV.js
    call :IS_NEWER ..\LeadLife.Web\Scripts\BootBoxtLaboratory.js            ..\LeadLife.Web\Scripts\STAGING\BootBoxtLaboratory_DEV.js
    call :IS_NEWER ..\LeadLife.Web\Scripts\CallMapGrid.js                   ..\LeadLife.Web\Scripts\STAGING\CallMapGrid_DEV.js
    call :IS_NEWER ..\LeadLife.Web\Scripts\KeywordHighlighter.js            ..\LeadLife.Web\Scripts\STAGING\KeywordHighlighter_DEV.js
    call :IS_NEWER ..\LeadLife.Web\Scripts\LeadLifeJSHelpersGlobals.js      ..\LeadLife.Web\Scripts\STAGING\LeadLifeJSHelpersGlobals_DEV.js
    call :IS_NEWER ..\LeadLife.Web\Scripts\LeadLifeJSHelpersLib.js          ..\LeadLife.Web\Scripts\STAGING\LeadLifeJSHelpersLib_DEV.js
    call :IS_NEWER ..\LeadLife.Web\Scripts\LeadLife_InputMask_Engine.js     ..\LeadLife.Web\Scripts\STAGING\LeadLife_InputMask_Engine_DEV.js
    call :IS_NEWER ..\LeadLife.Web\Scripts\LLCommon.js                      ..\LeadLife.Web\Scripts\STAGING\LLCommon_DEV.js
    call :IS_NEWER ..\LeadLife.Web\Scripts\Mobile_Index.js                  ..\LeadLife.Web\Scripts\STAGING\Mobile_Index_DEV.js
    call :IS_NEWER ..\LeadLife.Web\Scripts\STT_VideoPlayer.js               ..\LeadLife.Web\Scripts\STAGING\STT_VideoPlayer_DEV.js
    call :IS_NEWER ..\LeadLife.Web\Scripts\Webinar_Followup_Request_Form.js ..\LeadLife.Web\Scripts\STAGING\Webinar_Followup_Request_Form_DEV.js
    call :IS_NEWER ..\LeadLife.Web\Scripts\Words2Actions_Recorder_Forms.js  ..\LeadLife.Web\Scripts\STAGING\Words2Actions_Recorder_Forms_DEV.js

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Discard the editor backups in the staging directory.
    echo ---------------------------------------------------------------------
    echo.

    pushd ..\LeadLife.Web\Scripts\STAGING
    call ShredDocumentBackups
    popd

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Update the player/viewer web page.
    echo ---------------------------------------------------------------------
    echo.

    call :IS_NEWER ..\PURL_COMMON\STT_VideoPlayer.html ..\PURL_COMMON\STAGING\STT_VideoPlayer.html

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Discard the editor backups in the staging directory.
    echo ---------------------------------------------------------------------
    echo.

    pushd ..\PURL_COMMON\STAGING
    call ShredDocumentBackups
    popd

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Update Words2Actions_Form_TEMPLATE.HTML web page.
    echo ---------------------------------------------------------------------
    echo.

    call :IS_NEWER ..\PURL_COMMON\Words2Actions_Form_TEMPLATE.HTML ..\PURL_COMMON\STAGING\Words2Actions_Form_TEMPLATE.HTML

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Update NotesSearch_Domain_Wide.HTML web page.
    echo ---------------------------------------------------------------------
    echo.

    call :IS_NEWER ..\PURL_COMMON\NotesSearch_Domain_Wide.HTML ..\PURL_COMMON\STAGING\NotesSearch_Domain_Wide.HTML

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Update Agent_Recent_Phone_Calls.html web page.
    echo ---------------------------------------------------------------------
    echo.

    call :IS_NEWER ..\PURL_COMMON\Agent_Recent_Phone_Calls.html ..\PURL_COMMON\STAGING\Agent_Recent_Phone_Calls.html

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Discard the editor backups in the PURL COMMON directories.
    echo ---------------------------------------------------------------------
    echo.

    pushd ..\PURL_COMMON
    call ShredDocumentBackups ALL
    popd

:PROMOTE

    if "%_Promoted_Fixup_%" equ "Y" (
        pushd "%~dp0"
        STT_VideoPlayer_Promoted_Fixup.exe
        popd
    ) else (
        echo.
        echo ---------------------------------------------------------------------
        echo Info: SKIPPING STT_VideoPlayer_Promoted_Fixup.EXE because no HTML.
        echo ---------------------------------------------------------------------
        echo.
    )

:DONE

    echo.
    echo ---------------------------------------------------------------------
    echo %~nx0 Done!
    echo ---------------------------------------------------------------------
    echo.

    WWPause.exe