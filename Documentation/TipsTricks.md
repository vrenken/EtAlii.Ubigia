r# Tips and Tricks

##Testing for linux
Use the commandline below to run all the unit tests in a linux container:

```powershell
docker run `
    --rm `
    -v ${pwd}:/app `
    -w /app `
    mcr.microsoft.com/dotnet/sdk:6.0 `
    dotnet test ./EtAlii.Ubigia.sln --logger:trx --configuration:'Debug-Ubuntu' /p:UbigiaIsRunningOnBuildAgent=true
```

The most important tests that cover most - if not all - platform-specific runtime implementations are located in the technology specific projects.
These are mostly:
- transport test projects
- persistence test projects.

- To quickly only run those the command lines below are a good fix:

```powershell
docker run `
    --rm `
    -v ${pwd}:/app `
    -w /app `
    mcr.microsoft.com/dotnet/sdk:6.0 `
    dotnet test ./Api/EtAlii.Ubigia.Api.Transport.Grpc.Tests/EtAlii.Ubigia.Api.Transport.Grpc.Tests.csproj --logger:trx --results-directory:./Api/EtAlii.Ubigia.Api.Transport.Grpc.Tests/bin/TestResults --configuration:'Debug-Ubuntu' /p:UbigiaIsRunningOnBuildAgent=true

docker run `
    --rm `
    -v ${pwd}:/app `
    -w /app `
    mcr.microsoft.com/dotnet/sdk:6.0 `
    dotnet test ./Persistence/EtAlii.Ubigia.Persistence.Ntfs.Tests/EtAlii.Ubigia.Persistence.Ntfs.Tests.csproj --logger:trx --results-directory:./Persistence/EtAlii.Ubigia.Persistence.Ntfs.Tests/bin/TestResults --configuration:'Debug-Ubuntu' /p:UbigiaIsRunningOnBuildAgent=true
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

```powershell
dotnet publish .\Source\EtAlii.Ubigia.sln --configuration:'Release'
docker build `
    --tag ubigia/storage:preview `
    --file ./Source/Infrastructure/Hosting/Hosts/EtAlii.Ubigia.Infrastructure.Hosting.DockerHost/Dockerfile
    .
```

And testing:
```powershell
docker create `
    --name ubigia_test_storage `
    --env SERILOG_SERVER_URL=http://seq.avalon:5341 `
    --volume c:\temp\ubigia:/ubigia/data `
    --publish 64002:80 `
    ubigia/storage:preview
```

## Architecture Decision Log

- **[2021-10-08] Code quality is guaranteed using the default warnings and errors as provided by Sonarcloud.**

  The team behind SonarQube / SonarCloud has made it their day job to find out what the best code quality conventions are for many, many different languages. The default rules are already very powerful, and taking them as the rule of thumb makes any internal discussions about quality and convention obsolete.
Therefore all errors and warnings (including the security ones) should never be reconfigured in the SonarCloud portal, but rather corrected in the code. In special circumstances - and only when all other alternatives are not viable - a SonarCloud warning can be disabled using the #pragma warning disable S1234 concept, but in that case also a decent comment should be added to describe the reason for the exemption.
Having these in code instead of in the SonarCloud portal also allows for future corrections and/or reconsideration.


- **[< 2021] SOLID and DRY should be aimed for wherever pragmatically possible.**

  TODO: elaborate

- **[< 2021] Strong separation between logic and state.**

  The code base honors the differences between logic and data by ensuring that data (state) is modelled using classes/structs that reside in __Model_ folders.
  Additionally, most of the other classes do never contain 'state' (i.e. 'functional' data) beyond any configuration or options.

- **[< 2021] All unit tests will be structured according to the triple-A Arrange/Act/Assert pattern.**

  TODO: elaborate

