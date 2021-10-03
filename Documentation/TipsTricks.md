# Tips and Tricks

##Testing for linux
Use the commandline below to run all the unit tests in a linux container:

```powershell
docker run --rm -v ${pwd}:/app -w /app mcr.microsoft.com/dotnet/sdk:5.0 dotnet test ./EtAlii.Ubigia.sln --logger:trx --configuration:'Debug-Ubuntu' /p:UbigiaIsRunningOnBuildAgent=true
```

The most important tests that cover most - if not all - platform-specific runtime implementations are located in the technology specific projects.
These are mostly:
- transport test projects
- persistence test projects.

- To quickly only run those the command lines below are a good fix:

```powershell
docker run --rm -v ${pwd}:/app -w /app mcr.microsoft.com/dotnet/sdk:5.0 dotnet test ./Api/EtAlii.Ubigia.Api.Transport.Grpc.Tests/EtAlii.Ubigia.Api.Transport.Grpc.Tests.csproj --logger:trx --results-directory:./Api/EtAlii.Ubigia.Api.Transport.Grpc.Tests/bin/TestResults --configuration:'Debug-Ubuntu' /p:UbigiaIsRunningOnBuildAgent=true

docker run --rm -v ${pwd}:/app -w /app mcr.microsoft.com/dotnet/sdk:5.0 dotnet test ./Persistence/EtAlii.Ubigia.Persistence.Ntfs.Tests/EtAlii.Ubigia.Persistence.Ntfs.Tests.csproj --logger:trx --results-directory:./Persistence/EtAlii.Ubigia.Persistence.Ntfs.Tests/bin/TestResults --configuration:'Debug-Ubuntu' /p:UbigiaIsRunningOnBuildAgent=true
```
Make sure to run the commandline is run in the same folder that the solution file resides in.
Currently this is ```./Source``` relative to the root of the repository

In Rider these settings are also available as Run/Debug configurations. A future improvement is to allow these to debug the tests as well.

## Updating towards a new version.
The Ubigia project uses Nerdbank GitVersion for its versioning.
TODO
```powershell
nbgv prepare-release
```


## Docker image creation

dotnet publish .\Source\EtAlii.Ubigia.sln
docker build -t ubigia/storage:preview -f ./Source/Infrastructure/Hosting/Hosts/EtAlii.Ubigia.Infrastructure.Hosting.DockerHost/Dockerfile .

## Architecture Decision Log

- **[< 2021] SOLID and DRY should be aimed for wherever pragmatically possible.**

- **[< 2021] Strong separation between logic and state.**

  TODO: Talk about the _Model folders.


- **[< 2021] All unit tests will be structured according to the triple-A Arrange/Act/Assert pattern.**

  TODO: elaborate

