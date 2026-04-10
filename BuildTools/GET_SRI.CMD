@echo off

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

goto :EOF