﻿REM "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\sn.exe" -d ServusCryptoContainer
REM if exist "$(TargetDir)ServusCryptoContainer.snk" del "$(TargetDir)ShellRegistration-public.snk"
REM "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\sn.exe" -Ra "$(TargetPath)" "$(ProjectDir)ShellRegistration.snk"
REM "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\sn.exe" -e "$(ProjectDir)ShellRegistration.snk" "$(TargetDir)ShellRegistration-public.snk"
REM "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\sn.exe" -i "$(ProjectDir)ShellRegistration.snk" ServusCryptoContainer