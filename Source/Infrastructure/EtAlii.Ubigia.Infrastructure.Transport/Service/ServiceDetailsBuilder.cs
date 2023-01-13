// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.xTechnology.Hosting;

/// <summary>
/// The ServiceDetailsBuilder is able to make sense of all the services configured
/// and accessible through the infrastructure.
/// </summary>
public class ServiceDetailsBuilder : IServiceDetailsBuilder
{
    public ServiceDetails[] Build(INetworkService[] networkServices)
    {
        var managementGrpcService = networkServices.SingleOrDefault(ns => ns.Configuration.Section.Key == "Management-Api-Grpc");
        var dataGrpcService = networkServices.SingleOrDefault(ns => ns.Configuration.Section.Key == "User-Api-Grpc");

        var managementSignalRService = networkServices.SingleOrDefault(ns => ns.Configuration.Section.Key == "Management-Api-SignalR");
        var dataSignalRService = networkServices.SingleOrDefault(ns => ns.Configuration.Section.Key == "User-Api-SignalR");

        var managementRestService = networkServices.SingleOrDefault(ns => ns.Configuration.Section.Key == "Management-Api-Rest");
        var dataRestService = networkServices.SingleOrDefault(ns => ns.Configuration.Section.Key == "User-Api-Rest");

        var grpcServiceDetails = managementGrpcService != null && dataGrpcService != null
            ? ToServiceDetails(ServiceDetailsName.Grpc, managementGrpcService, dataGrpcService)
            : null;

        var signalRServiceDetails = managementSignalRService != null && dataSignalRService != null
            ? ToServiceDetails(ServiceDetailsName.SignalR, managementSignalRService, dataSignalRService)
            : null;

        var restServiceDetails = managementRestService != null && dataRestService != null
            ? ToServiceDetails(ServiceDetailsName.Rest, managementRestService, dataRestService)
            : null;

        return new []
            {
                grpcServiceDetails,
                signalRServiceDetails,
                restServiceDetails
            }.Where(sd => sd != null)
            .ToArray();
    }

    private ServiceDetails ToServiceDetails(string name, INetworkService managementService, INetworkService dataService)
    {
        var managementAddress = ToServiceAddress(managementService);
        var dataAddress = ToServiceAddress(dataService);
        var storageAddress = ToStorageAddress(dataService);
        return new ServiceDetails(name, managementAddress, dataAddress, storageAddress);

    }
    private Uri ToServiceAddress(INetworkService service)
    {
        var configuration = service.Configuration;
        var ipAddress = ConvertToDedicatedNetworkAddress(configuration.IpAddress);
        return new UriBuilder("https://", ipAddress, (int)configuration.Port, configuration.Path).Uri;
    }
    private Uri ToStorageAddress(INetworkService service)
    {
        var configuration = service.Configuration;
        var ipAddress = ConvertToDedicatedNetworkAddress(configuration.IpAddress);
        return new UriBuilder("https://", ipAddress).Uri;
    }

    [SuppressMessage(
        category: "Sonar Code Smell",
        checkId: "S1313:RSPEC-1313 - Using hardcoded IP addresses is security-sensitive",
        Justification = "Safe to do so here.")]
    private string ConvertToDedicatedNetworkAddress(string host)
    {
        // This is also ugly and way in the incorrect place.
        return host switch
        {
            "0.0.0.0" => "127.0.0.1",
            "255.255.255.255" => "127.0.0.1",
            _ => host
        };
    }
}
