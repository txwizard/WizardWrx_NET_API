@echo off
WWLogger.exe "%_LOG_%" Info: %~0, version %~t0 Begin
echo.
showtime.exe
echo.
xcopy.exe /fshedcrky C:\inetpub\LeadLifeG2\Repository\_COMMON c:\temp\PURL_COMMON_Backup\
set "XCOPY_RC=%ERRORLEVEL%"
call XCOPYCHK.CMD %XCOPY_RC% "%_LOG_%"
exit /b %XCOPY_RC%
