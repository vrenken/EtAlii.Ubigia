﻿dir "*.Tests.csproj" -Recurse | %{dotnet test $PSItem.FullName /p:CollectCoverage=true /p:SolutionDir='c:\Git\EtAlii\EtAlii.Ubigia\' --no-restore --no-build --collect:"XPlat Code Coverage" -nodeReuse:false -maxCpuCount:8 -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover} 