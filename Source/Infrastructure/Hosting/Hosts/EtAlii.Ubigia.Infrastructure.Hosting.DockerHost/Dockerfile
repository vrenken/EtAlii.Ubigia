FROM mcr.microsoft.com/dotnet/core/sdk:5.0 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet publish Infrastructure/Hosting/Hosts/EtAlii.Ubigia.Infrastructure.Hosting.DockerHost -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/Infrastructure/Hosting/Hosts/EtAlii.Ubigia.Infrastructure.Hosting.DockerHost/out .

ENTRYPOINT ["dotnet", "EtAlii.Ubigia.Infrastructure.Hosting.DockerHost.dll"]
