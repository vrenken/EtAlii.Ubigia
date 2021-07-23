# Tips and Tricks

##Testing for linux
Use the commandline below to run the unit tests in a linux container:

```powershell
docker run --rm -v ${pwd}:/app -w /app mcr.microsoft.com/dotnet/sdk:5.0 dotnet test ./EtAlii.Ubigia.sln --logger:trx --configuration:'Debug-Ubuntu' /p:UbigiaIsRunningOnBuildAgent=true
```

Make sure to run the commandline is run in the same folder that the solution file resides in.
Currently this is ```./Source``` relative to the root of the repository
