FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY Source/Infrastructure/Hosting/Hosts/EtAlii.Ubigia.Infrastructure.Hosting.DockerHost/bin/Release/net5.0/publish/ ubigia/
WORKDIR /ubigia

#Network related settings.
EXPOSE 64000/tcp
ENV UBIGIA_DATA_DATA_PORT_INTERFACE         = 'http://+'
ENV UBIGIA_DATA_DATA_PORT                   = 64000
EXPOSE 64001/tcp
ENV UBIGIA_DATA_MANAGEMENT_PORT_INTERFACE   = 'http://+'
ENV UBIGIA_DATA_MANAGEMENT_PORT             = 64001

# Storage related settings.
RUN mkdir /ubigia/data
VOLUME ["/ubigia/data"]
ENV UBIGIA_DATA_LOCATION = '/ubigia/data'

# Diagnostics related settings.
ENV SERILOG_SERVER_URL = 'http://seq.avalon:5341'

ENTRYPOINT ["dotnet", "EtAlii.Ubigia.Infrastructure.Hosting.DockerHost.dll"]
