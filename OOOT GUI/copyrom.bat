@echo off
if "%~1"=="EUR_MQD" (
    copy %2 %USERPROFILE%\ooot\roms\EUR_MQD\baserom_original.n64
) else (
    copy %2 %USERPROFILE%\ooot\roms\PAL_1.0\baserom_original.n64
)