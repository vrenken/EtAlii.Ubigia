FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY Source/Infrastructure/Hosting/Hosts/EtAlii.Ubigia.Infrastructure.Hosting.DockerHost/bin/Release-Ubuntu/net5.0/publish/ ubigia/
WORKDIR /ubigia

#Network related settings.
EXPOSE 64000/tcp
ENV UBIGIA_USER_API_INTERFACE           '0.0.0.0'
ENV UBIGIA_USER_API_PORT                '64000'
EXPOSE 64001/tcp
ENV UBIGIA_MANAGEMENT_API_INTERFACE     '0.0.0.0'
ENV UBIGIA_MANAGEMENT_API_PORT          '64001'
EXPOSE 64002/tcp
ENV UBIGIA_MANAGEMENT_PORTAL_INTERFACE  '0.0.0.0'
ENV UBIGIA_MANAGEMENT_PORTAL_PORT       '64002'

# Storage related settings.
VOLUME ["/ubigia/data"]
ENV UBIGIA_DATA_FOLDER                  '/ubigia/data'

# Diagnostics related settings.
ENV SERILOG_SERVER_URL                  'http://seq.avalon:5341'
ENTRYPOINT ["dotnet", "EtAlii.Ubigia.Infrastructure.Hosting.DockerHost.dll"]
