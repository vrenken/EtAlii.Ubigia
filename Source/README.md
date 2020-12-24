# Source code
This is the core source repository for the Ubigia project.

Check out [this](../Readme.md) page to for more reading materials.

## Development

The Ubigia project has a long history, but the latest solution are primarily build using JetBrains Rider.
Probably Visual Studio can be used as IDE as well, and a commandline dotnet build should work also as this is how the code is being build on the server,
but the additional files specific to Rider are optimized to early signal warnings and code smells.

## Build

Parameters relevant to the build are:

| Parameter         | Description                                                                                   |
| ---               | ---                                                                                           |
| NUGET_FEED_TOKEN  | The private token of the nuget feed to publish to.                                            |
| NUGET_FEED_SOURCE | The source NuGet feed. For normal use this should be "EtAlii.Ubigia".                         |
| SONARQUBE_TOKEN   | The private token to access the SonarQube instance with.                                      |
| SONARQUBE_PROJECT | The SonarQube project to publish analytics to. For normal use this should be "EtAlii.Ubigia". |

Most of these are stored and set in the server part of the build, but for testing purposes these can be set on the commandline as well.

Below are some of the more important command lines below needed to execute, test or manage the build.

Trigger a complete build.
```powershell
nuke --nuget-feed-token [NUGET_FEED_TOKEN] --nuget-feed-source [NUGET_FEED_SOURCE] --sonar-qube-server-token [SONARQUBE_TOKEN] --sonar-qube-server-url [SONARQUBE_URL] --sonar-qube-project-key [SONARQUBE_PROJECT]
```

Trigger the build compile step.
This is very useful to validate that the server and client builds are the same.
```powershell
nuke --nuget-feed-token [NUGET_FEED_TOKEN] --nuget-feed-source [NUGET_FEED_SOURCE] --sonar-qube-server-token [SONARQUBE_TOKEN] --sonar-qube-server-url [SONARQUBE_URL] --sonar-qube-project-key EtAlii.Ubigia -target RunCompile
```

Generate a HTML document which visualizes the individual build steps and their dependencies.
```powershell
 dotnet --plan
```

Update the build YAML file and show information about it.
```powershell
 dotnet --info
```

Update the nuke build packages.
```powershell
 dotnet tool update Nuke.GlobalTool --global --interactive
```
