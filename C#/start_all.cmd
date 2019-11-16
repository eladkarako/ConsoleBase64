@echo off
::                                                        Unicode support for console content and file-names.
chcp 65001 2>nul >nul

echo I am the runner

for %%e in (*.*) do (

     if NOT "%%e"=="ConsoleBase64.exe" (

        if NOT "%%e"=="file_encoder.cmd" (

            if NOT "%%e"=="start_all.cmd" (

                ::parallel diffrent process. and continue.
                start /low "cmd /c "call file_encoder.cmd "%%e"""

            )

        )

     )
)
