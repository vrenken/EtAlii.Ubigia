FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY Source/Infrastructure/Hosting/Hosts/EtAlii.Ubigia.Infrastructure.Hosting.DockerHost/bin/Release-Ubuntu/net5.0/publish/ ubigia/
WORKDIR /ubigia

#Network related settings.
ENV UBIGIA_CERTIFICATE_FILE             '/ubigia/system/certificate.pfx'
ENV UBIGIA_CERTIFICATE_PASSWORD         'MY_SILLY_PASSWORD'
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
VOLUME ["/ubigia/system"]
ENV UBIGIA_SYSTEM_FOLDER                '/ubigia/system'

# Diagnostics related settings.
ENV SERILOG_SERVER_URL                  'http://seq.avalon:5341'

# And finally the entry point.
ENTRYPOINT ["dotnet", "EtAlii.Ubigia.Infrastructure.Hosting.DockerHost.dll"]
