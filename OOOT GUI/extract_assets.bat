@echo off
cd %USERPROFILE%\OOOT
if "%~1"=="EUR_MQD" (
    setup.py -b EUR_MQD -c
) else (
    setup.py -b PAL_1.0 -c
)