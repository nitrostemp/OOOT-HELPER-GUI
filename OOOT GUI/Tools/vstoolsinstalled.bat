@echo off
SET /A i = 0
goto FindVsFolder

:FindVsFolder
@if %i% == 0 ( 
	set "VS_PATH=C:\Program Files (x86)\Microsoft Visual Studio\2022"
) else if %i% == 1 (
	set "VS_PATH=C:\Program Files\Microsoft Visual Studio\2022"
) else if %i% == 2 (
	set "VS_PATH=C:\Program Files (x86)\Microsoft Visual Studio\2019"
) else if %i% == 3 (
	goto Error
)
goto FindVsDevCmd

:FindVsDevCmd
for /f "delims=" %%F in ('dir /b /s "%VS_PATH%\vsdevcmd.bat" 2^>nul') do set VSDEVCMD_PATH=%%F
@if exist "%VSDEVCMD_PATH%" (
	goto Build
) else (
	set /A i = %i% + 1
	goto FindVsFolder
)

:Build
echo %VSDEVCMD_PATH%
exit

:Error
echo fatal: Can't compile, "VsDevCmd.bat" not found!
exit