@echo off

:: ============================================================================
:: ConsoleLib.CMD — facade loader
:: ============================================================================

call "%~dp0ConsoleVT.CMD"
call "%~dp0ConsoleColors.CMD"
call "%~dp0ConsoleFormat.CMD"
call "%~dp0ConsoleDiag.CMD"