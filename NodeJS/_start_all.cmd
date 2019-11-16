@echo off
:: -----------------------------------------------------------------------
:: |  this file locates all of the PNG, GIF and ICO files in the folder  |
:: |  and runs (single-process) the BASE64-encoder for each file.        |
:: -----------------------------------------------------------------------
chcp 65001 2>nul >nul
pushd "%~dp0"

for %%e in (*.png *.gif *.ico *.mpg *.mpeg *.avi *.mp4 *.mp3 *.ogg *.ogv *.webm) do (
  echo %%~dpe%%~nxe
  call "%~sdp0index.cmd" "%%~sdpe%%~nxe"
)
pause
  ::start /MIN /ABOVENORMAL "cmd /c "call "%~sdp0index.cmd" "%%~sdpe%%~nxe"""
::%~sdp0
::  for /f %%a in ("") do ( set "FILENAME=%%~fsa" )
exit /b 0
