@echo off
::                                                        Unicode support for console content and file-names.
chcp 65001 2>nul >nul

set "FILE_CONSOLE64=%~dp0ConsoleBase64.exe"

:LOOP
  set "sFILE_NAME_INPUT=%~1"
  set "sFILE_NAME_OUTPUT=%~1.txt"

  title sFILE_NAME_INPUT

  call "%FILE_CONSOLE64%" -encode -wrap -infile %sFILE_NAME_INPUT% >%sFILE_NAME_OUTPUT%


:NEXT
  shift
  goto LOOP


:END
  pause
  exit /b 0
  