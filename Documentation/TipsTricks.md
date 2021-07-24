# Tips and Tricks

##Testing for linux
Use the commandline below to run all the unit tests in a linux container:

```powershell
docker run --rm -v ${pwd}:/app -w /app mcr.microsoft.com/dotnet/sdk:5.0 dotnet test ./EtAlii.Ubigia.sln --logger:trx --configuration:'Debug-Ubuntu' /p:UbigiaIsRunningOnBuildAgent=true
```

The most important tests that cover most - if not all - platform-specific runtime implementations are located in the transport test projects.
To quickly only run those the commandline below is a good fix:

```powershell
docker run --rm -v ${pwd}:/app -w /app mcr.microsoft.com/dotnet/sdk:5.0 dotnet test ./Api/EtAlii.Ubigia.Api.Transport.Grpc.Tests/EtAlii.Ubigia.Api.Transport.Grpc.Tests.csproj --logger:trx --configuration:'Debug-Ubuntu' /p:UbigiaIsRunningOnBuildAgent=true
```

Make sure to run the commandline is run in the same folder that the solution file resides in.
Currently this is ```./Source``` relative to the root of the repository
