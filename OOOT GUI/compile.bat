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
	call "%VSDEVCMD_PATH%"
	goto Build
) else (
	set /A i = %i% + 1
	goto FindVsFolder
)

:Build
C:
cd %USERPROFILE%\ooot
msbuild vs\oot.sln -p:Configuration=Release -p:Platform=Win32
exit

:Error
echo ERROR: Can't compile, "VsDevCmd.bat" not found!
pause
exit