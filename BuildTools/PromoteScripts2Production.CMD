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
::  2026/01/19 DG Make a backup of the SubResource Integrity digest string files
::                in a directory that is visible to both the BuildTools and
::                the Git repository, and is INCLUDED in Git commits.
::  2026/01/22 DG Move the backup of the SubResource Integrity digest files into
::                the block that calls STT_VideoPlayer_Promoted_Fixup.exe.
::  2026/01/30 DG Make the command that invokes 7UP.CMD a `CALL`, and replace
::                all instances of `pushd` with `cd /d`, followed by undecorated
::                CD commands to display the result.
::  2026/02/05 DG Add UploadFile.js to the list of JavaScript scripts.
::  2026/02/11 DG Add a `cd /d` back to the script diectory. Since we've changed
::                from pushd to cd, they are necessary becuse there is no longer
::                a stack from which to pop paths.
::  2026/03/30 DG Replace local subroutine IS_NEWER with independent tested
::                shell script IsNewer.CMD, and add dp0ConsoleLib.CMD to provide
::                VT color coding.
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
    cd /d %OpenSSL_Exe_Path%
    cd
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


:SKIPREM

    setlocal

    echo ---------------------------------------------------------------------
    echo %0, version %~t0 Begin
    echo.

    ShowTime.exe

    call "%~dp0ConsoleLib.CMD"

    echo.
    echo Script Home Directory Name = %~dp0
    echo.
    echo ---------------------------------------------------------------------
    echo Info: Initialize script variablea and ensure that leftovers from
    echo       previous runs are removed.
    echo ---------------------------------------------------------------------
    echo.

    cd /d "%~dp0"
    cd

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

    call "%~dp0IsNewer.CMD" ..\LeadLife.Web\Scripts\Agent_Recent_Phone_Calls.js      ..\LeadLife.Web\Scripts\STAGING\Agent_Recent_Phone_Calls_DEV.js
    call "%~dp0IsNewer.CMD" ..\LeadLife.Web\Scripts\BootBoxtLaboratory.js            ..\LeadLife.Web\Scripts\STAGING\BootBoxtLaboratory_DEV.js
    call "%~dp0IsNewer.CMD" ..\LeadLife.Web\Scripts\CallMapGrid.js                   ..\LeadLife.Web\Scripts\STAGING\CallMapGrid_DEV.js
    call "%~dp0IsNewer.CMD" ..\LeadLife.Web\Scripts\UploadFile.js                    ..\LeadLife.Web\Scripts\STAGING\UploadFile_DEV.js
    call "%~dp0IsNewer.CMD" ..\LeadLife.Web\Scripts\KeywordHighlighter.js            ..\LeadLife.Web\Scripts\STAGING\KeywordHighlighter_DEV.js
    call "%~dp0IsNewer.CMD" ..\LeadLife.Web\Scripts\LeadLifeJSHelpersGlobals.js      ..\LeadLife.Web\Scripts\STAGING\LeadLifeJSHelpersGlobals_DEV.js
    call "%~dp0IsNewer.CMD" ..\LeadLife.Web\Scripts\LeadLifeJSHelpersLib.js          ..\LeadLife.Web\Scripts\STAGING\LeadLifeJSHelpersLib_DEV.js
    call "%~dp0IsNewer.CMD" ..\LeadLife.Web\Scripts\LeadLife_InputMask_Engine.js     ..\LeadLife.Web\Scripts\STAGING\LeadLife_InputMask_Engine_DEV.js
    call "%~dp0IsNewer.CMD" ..\LeadLife.Web\Scripts\LLCommon.js                      ..\LeadLife.Web\Scripts\STAGING\LLCommon_DEV.js
    call "%~dp0IsNewer.CMD" ..\LeadLife.Web\Scripts\Mobile_Index.js                  ..\LeadLife.Web\Scripts\STAGING\Mobile_Index_DEV.js
    call "%~dp0IsNewer.CMD" ..\LeadLife.Web\Scripts\STT_VideoPlayer.js               ..\LeadLife.Web\Scripts\STAGING\STT_VideoPlayer_DEV.js
    call "%~dp0IsNewer.CMD" ..\LeadLife.Web\Scripts\Webinar_Followup_Request_Form.js ..\LeadLife.Web\Scripts\STAGING\Webinar_Followup_Request_Form_DEV.js
    call "%~dp0IsNewer.CMD" ..\LeadLife.Web\Scripts\Words2Actions_Recorder_Forms.js  ..\LeadLife.Web\Scripts\STAGING\Words2Actions_Recorder_Forms_DEV.js

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Discard the editor backups in the staging directory.
    echo ---------------------------------------------------------------------
    echo.

    cd /d ..\LeadLife.Web\Scripts\STAGING
    cd
    call ShredDocumentBackups

    cd /d "%~dp0"
    cd

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Update the player/viewer web page.
    echo ---------------------------------------------------------------------
    echo.

    call "%~dp0IsNewer.CMD" ..\PURL_COMMON\STT_VideoPlayer.html ..\PURL_COMMON\STAGING\STT_VideoPlayer.html

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Discard the editor backups in the staging directory.
    echo ---------------------------------------------------------------------
    echo.

    cd /d ..\PURL_COMMON\STAGING
    cd
    call ShredDocumentBackups

    cd /d "%~dp0"
    cd

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Update Words2Actions_Form_TEMPLATE.HTML web page.
    echo ---------------------------------------------------------------------
    echo.

    call "%~dp0IsNewer.CMD" ..\PURL_COMMON\Words2Actions_Form_TEMPLATE.HTML ..\PURL_COMMON\STAGING\Words2Actions_Form_TEMPLATE.HTML

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Update NotesSearch_Domain_Wide.HTML web page.
    echo ---------------------------------------------------------------------
    echo.

    call "%~dp0IsNewer.CMD" ..\PURL_COMMON\NotesSearch_Domain_Wide.HTML ..\PURL_COMMON\STAGING\NotesSearch_Domain_Wide.HTML

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Update Agent_Recent_Phone_Calls.html web page.
    echo ---------------------------------------------------------------------
    echo.

    call "%~dp0IsNewer.CMD" ..\PURL_COMMON\Agent_Recent_Phone_Calls.html ..\PURL_COMMON\STAGING\Agent_Recent_Phone_Calls.html

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Discard the editor backups in the PURL COMMON directories.
    echo ---------------------------------------------------------------------
    echo.

    cd /d ..\PURL_COMMON
    cd
    call ShredDocumentBackups ALL

:PROMOTE

    cd /d "%~dp0"
    cd

    echo.
    echo ---------------------------------------------------------------------
    echo Info: Create new versions as needed of the production HTML documents
    echo       from the STAGING versions.
    echo ---------------------------------------------------------------------
    echo.

    if "%_Promoted_Fixup_%" equ "Y" (
        cd /d "%~dp0"
        echo.
        echo ---------------------------------------------------------------------
        echo CURRENT DIRectory Name:
        cd
        echo ---------------------------------------------------------------------

        echo.
        echo ---------------------------------------------------------------------
        echo Info: Process the HTML document files.
        echo ---------------------------------------------------------------------
        echo.
        STT_VideoPlayer_Promoted_Fixup.exe

        echo.
        echo ---------------------------------------------------------------------
        echo CURRENT DIRectory Name after STT_VideoPlayer_Promoted_Fixup:
        cd
        echo ---------------------------------------------------------------------
        echo.
        echo.
        echo ---------------------------------------------------------------------
        echo Info: Make a backup of the SubResource Integrity digest files.
        echo ---------------------------------------------------------------------
        echo.
        cd /d "%~dp0"
        echo.
        echo ---------------------------------------------------------------------
        echo CURRENT DIRectory Name:
        cd
        echo ---------------------------------------------------------------------
        echo.

        call 7UP ..\PURL_COMMON\SubResource_Integrity_String_Backups\SRI_Digests.7z %TEMP%\*_SRI_*SHA???.TMP
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