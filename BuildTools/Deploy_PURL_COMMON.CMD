@echo off
setlocal
goto SKIPREM
:CopyAndVerify

:: Arguments:
::     %1 = Source file (TSCLIENT path)
::     %2 = Destination file (server path)

    set /a COUNT+=1
    echo Show BEFOREe-copy stats for %1 and %2
    dirX.exe "%1" /f /h & dirX.exe "%2" /f /h

    echo Copying %1 to %2
    copy /y "%1" "%2"

    echo Show AFTER-copy stats for %1 and %2
    dirX.exe "%1" /f /h & dirX.exe "%2" /f /h

    echo Verifying copy of %2
    fc /b "%1" "%2"

    if errorlevel 2 (
        echo ERROR: File compare failed for %2
        set /a ERRCOUNT+=1
        goto :EOF
    )

    if errorlevel 1 (
        echo MISMATCH: %1 and %2 differ
        set /a MISCOUNT+=1
        goto :EOF
    )

    echo OK: %2 copied and verified
    set /a OKCOUNT+=1
    goto :EOF

:SKIPREM

    echo %0, version %~t0 Begin
    echo.
    ShowTime.exe

    call "%~dp0TSClientProbe.CMD" "%~dp0"
    if errorlevel 1 (
        echo TSCLIENT redirector unavailable ? aborting script
        WWPause.exe
        exit /b 1
    )

 :: Initialize counters
    set COUNT=0
    set OKCOUNT=0
    set MISCOUNT=0
    set ERRCOUNT=0

    :: Copying and verifying:
    ::   SRC  = \\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\STAGING\CallMapGrid_DEV.js
    ::   DEST = C:\inetpub\LeadLifeG2\Repository\_COMMON\Scripts\STAGING\CallMapGrid_DEV.js
    call :CopyAndVerify "\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\STAGING\CallMapGrid_DEV.js" "C:\inetpub\LeadLifeG2\Repository\_COMMON\Scripts\STAGING\CallMapGrid_DEV.js"

    :: Copying and verifying:
    ::   SRC  = \\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\CallMapGrid.js
    ::   DEST = C:\inetpub\LeadLifeG2\Repository\_COMMON\Scripts\CallMapGrid.js
    call :CopyAndVerify "\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\CallMapGrid.js" "C:\inetpub\LeadLifeG2\Repository\_COMMON\Scripts\CallMapGrid.js"

    :: Copying and verifying:
    ::   SRC  = \\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\STAGING\EmailFollowup_DEV.js
    ::   DEST = C:\inetpub\LeadLifeG2\Repository\_COMMON\Scripts\STAGING\EmailFollowup_DEV.js
    call :CopyAndVerify "\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\STAGING\EmailFollowup_DEV.js" "C:\inetpub\LeadLifeG2\Repository\_COMMON\Scripts\STAGING\EmailFollowup_DEV.js"

    :: Copying and verifying:
    ::   SRC  = \\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\EmailFollowup.js
    ::   DEST = C:\inetpub\LeadLifeG2\Repository\_COMMON\Scripts\EmailFollowup.js
    call :CopyAndVerify "\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\EmailFollowup.js" "C:\inetpub\LeadLifeG2\Repository\_COMMON\Scripts\EmailFollowup.js"

    :: Copying and verifying:
    ::   SRC  = \\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\STAGING\QuickResponse_DEV.js
    ::   DEST = C:\inetpub\LeadLifeG2\Repository\_COMMON\Scripts\STAGING\QuickResponse_DEV.js
    call :CopyAndVerify "\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\STAGING\QuickResponse_DEV.js" "C:\inetpub\LeadLifeG2\Repository\_COMMON\Scripts\STAGING\QuickResponse_DEV.js"

    :: Copying and verifying:
    ::   SRC  = \\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\QuickResponse.js
    ::   DEST = C:\inetpub\LeadLifeG2\Repository\_COMMON\Scripts\QuickResponse.js
    call :CopyAndVerify "\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Scripts\QuickResponse.js" "C:\inetpub\LeadLifeG2\Repository\_COMMON\Scripts\QuickResponse.js"

    :: Copying and verifying:
    ::   SRC  = \\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON\STAGING\Agent_Recent_Phone_Calls.html
    ::   DEST = C:\inetpub\LeadLifeG2\Repository\_COMMON\STAGING\Agent_Recent_Phone_Calls.html
    call :CopyAndVerify "\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON\STAGING\Agent_Recent_Phone_Calls.html" "C:\inetpub\LeadLifeG2\Repository\_COMMON\STAGING\Agent_Recent_Phone_Calls.html"

    :: Copying and verifying:
    ::   SRC  = \\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON\Agent_Recent_Phone_Calls.html
    ::   DEST = C:\inetpub\LeadLifeG2\Repository\_COMMON\Agent_Recent_Phone_Calls.html
    call :CopyAndVerify "\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON\Agent_Recent_Phone_Calls.html" "C:\inetpub\LeadLifeG2\Repository\_COMMON\Agent_Recent_Phone_Calls.html"

    :: Copying and verifying:
    ::   SRC  = \\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON\STAGING\NotesSearch_Domain_Wide.HTML
    ::   DEST = C:\inetpub\LeadLifeG2\Repository\_COMMON\STAGING\NotesSearch_Domain_Wide.HTML
    call :CopyAndVerify "\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON\STAGING\NotesSearch_Domain_Wide.HTML" "C:\inetpub\LeadLifeG2\Repository\_COMMON\STAGING\NotesSearch_Domain_Wide.HTML"

    :: Copying and verifying:
    ::   SRC  = \\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON\NotesSearch_Domain_Wide.HTML
    ::   DEST = C:\inetpub\LeadLifeG2\Repository\_COMMON\NotesSearch_Domain_Wide.HTML
    call :CopyAndVerify "\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON\NotesSearch_Domain_Wide.HTML" "C:\inetpub\LeadLifeG2\Repository\_COMMON\NotesSearch_Domain_Wide.HTML"

    :: Copying and verifying:
    ::   SRC  = \\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON\STAGING\Words2Actions_Form_TEMPLATE.HTML
    ::   DEST = C:\inetpub\LeadLifeG2\Repository\_COMMON\STAGING\Words2Actions_Form_TEMPLATE.HTML
    call :CopyAndVerify "\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON\STAGING\Words2Actions_Form_TEMPLATE.HTML" "C:\inetpub\LeadLifeG2\Repository\_COMMON\STAGING\Words2Actions_Form_TEMPLATE.HTML"

    :: Copying and verifying:
    ::   SRC  = \\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON\Words2Actions_Form_TEMPLATE.HTML
    ::   DEST = C:\inetpub\LeadLifeG2\Repository\_COMMON\Words2Actions_Form_TEMPLATE.HTML
    call :CopyAndVerify "\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\PURL_COMMON\Words2Actions_Form_TEMPLATE.HTML" "C:\inetpub\LeadLifeG2\Repository\_COMMON\Words2Actions_Form_TEMPLATE.HTML"

    echo Inventory of source files:
    dirX.exe "\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Views\Shared\_Layout_W2A_????.cshtml" /f /h
    echo Inventory of destination files BEFORE replacement:
    dirX.exe "C:\InetPub" /f /h /s /z
    echo Replacing layout files...
    replace "\\TSCLIENT\D\Source_Code\Visual_Studio\Projects\SalesTalk\Source\salestalk\LeadLife\LeadLife.Web\Views\Shared\_Layout_W2A_????.cshtml" C:\InetPub /s /r
    set REPLACE_RC=%ERRORLEVEL%
    echo replace.exe exit code: %REPLACE_RC%
    if %REPLACE_RC% NEQ 0 ( echo ERROR: replace.exe failed with exit code %REPLACE_RC%. Deployment aborted. & goto :EOF ) else ( echo replace.exe completed successfully. )
    echo Inventory of destination files AFTER replacement:
    dirX.exe "C:\InetPub" /f /h /s /z
    echo.
    :: Summary
    echo.
    echo ============================================
    echo Copy/Verification Summary
    echo ============================================
    echo Total files processed: %COUNT%
    echo Successful copies:     %OKCOUNT%
    echo Mismatches:            %MISCOUNT%
    echo Errors:                %ERRCOUNT%
    echo ============================================
    echo End of processing.
    echo.
    echo ============================================
    echo %~nx0 Done!
    echo ============================================
    echo.
    WWPause.exe
