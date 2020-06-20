namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;

    public class ServiceDetailsBuilder : IServiceDetailsBuilder
    {
        public ServiceDetails[] Build(IConfigurationDetails configurationDetails)
        {
            // TODO: 1. Ugly. This needs to change and not be needed at all.
            // TODO: 2. This needs to incorporate the Grpc services as well.
            // TODO: 3. What about website hosting? How will we cope with that?

            var serviceDetails = new List<ServiceDetails>();

            if (configurationDetails.Paths.ContainsKey("UserApiRest"))
            {
                var restServiceDetails = BuildWebApiServiceDetails(configurationDetails);
                serviceDetails.Add(restServiceDetails);
            }
            if (configurationDetails.Paths.ContainsKey("UserApiSignalR"))
            {
                var signalRServiceDetails = BuildSignalRServiceDetails(configurationDetails);
                serviceDetails.Add(signalRServiceDetails);
            }

            // Again ugly, but for now we need to maek this builder compatible with both the REST/SignalR AND Grpc services.
            // For this we need to be somewhat sneaky on how to build our details.
            // In a future refactoring all configurations will be merged into one single big one. 
            if (!configurationDetails.Paths.ContainsKey("SignalRApiRest") && !configurationDetails.Paths.ContainsKey("UserApiRest"))
            {
                // If we don't have any SignalR nor any REST details we should build and add Grpc service details.   
                var grpcServiceDetails = BuildGrpcServiceDetails(configurationDetails);
                serviceDetails.Add(grpcServiceDetails);
            }

            if (serviceDetails.Count(sd => sd.IsSystemService) == 0)
            {
                throw new NotSupportedException("Unable to classify one of the services as the system service");
            }

            if (serviceDetails.Count(sd => sd.IsSystemService) > 1)
            {
                throw new NotSupportedException("Unable to classify one single service as the system service");
            }
            
            return serviceDetails.ToArray();
        }

        private ServiceDetails BuildSignalRServiceDetails(IConfigurationDetails configurationDetails)
        {
            var userHost = configurationDetails.Hosts["UserHost"];
            userHost = ConvertToDedicatedNetworkAddress(userHost);
            var adminHost = configurationDetails.Hosts["AdminHost"];
            adminHost = ConvertToDedicatedNetworkAddress(adminHost);

            var dataAddressBuilder = new StringBuilder();
            dataAddressBuilder.Append($"http://{userHost}:{configurationDetails.Ports["UserPort"]}");
            dataAddressBuilder.Append(configurationDetails.Paths["UserApi"]);
            dataAddressBuilder.Append(configurationDetails.Paths["UserApiSignalR"]);
            var dataAddress = dataAddressBuilder.ToString();

            var managementAddressBuilder = new StringBuilder();
            managementAddressBuilder.Append($"http://{adminHost}:{configurationDetails.Ports["AdminPort"]}");
            managementAddressBuilder.Append(configurationDetails.Paths["AdminApi"]);
            managementAddressBuilder.Append(configurationDetails.Paths["AdminApiSignalR"]);
            var managementAddress = managementAddressBuilder.ToString();

            if (dataAddress == null)
            {
                throw new InvalidOperationException($"Unable to start SignalR service {nameof(InfrastructureService)}: {nameof(dataAddress)} cannot be build from configuration.");
            }
            if (!Uri.IsWellFormedUriString(dataAddress, UriKind.Absolute))
            {
                throw new InvalidOperationException($"Unable to start SignalR service {nameof(InfrastructureService)}: no valid {nameof(dataAddress)} can be build from configuration.");
            }

            if (managementAddress == null)
            {
                throw new InvalidOperationException($"Unable to start SignalR service {nameof(InfrastructureService)}: {nameof(managementAddress)} cannot be build from configuration.");
            }
            if (!Uri.IsWellFormedUriString(managementAddress, UriKind.Absolute))
            {
                throw new InvalidOperationException($"Unable to start SignalR service {nameof(InfrastructureService)}: no valid {nameof(managementAddress)} can be build from configuration.");
            }

            return new ServiceDetails("SignalR", new Uri(managementAddress, UriKind.Absolute), new Uri(dataAddress, UriKind.Absolute), false);
        }
        private ServiceDetails BuildWebApiServiceDetails(IConfigurationDetails configurationDetails)
        {
            var userHost = configurationDetails.Hosts["UserHost"];
            userHost = ConvertToDedicatedNetworkAddress(userHost);
            var adminHost = configurationDetails.Hosts["AdminHost"];
            adminHost = ConvertToDedicatedNetworkAddress(adminHost);

            var dataAddressBuilder = new StringBuilder();
            dataAddressBuilder.Append($"http://{userHost}:{configurationDetails.Ports["UserPort"]}");
            dataAddressBuilder.Append(configurationDetails.Paths["UserApi"]);
            dataAddressBuilder.Append(configurationDetails.Paths["UserApiRest"]);
            var dataAddress = dataAddressBuilder.ToString();

            var managementAddressBuilder = new StringBuilder();
            managementAddressBuilder.Append($"http://{adminHost}:{configurationDetails.Ports["AdminPort"]}");
            managementAddressBuilder.Append(configurationDetails.Paths["AdminApi"]);
            managementAddressBuilder.Append(configurationDetails.Paths["AdminApiRest"]);
            var managementAddress = managementAddressBuilder.ToString();

            if (dataAddress == null)
            {
                throw new InvalidOperationException($"Unable to start WebApi service {nameof(InfrastructureService)}: {nameof(dataAddress)} cannot be build from configuration.");
            }
            if (!Uri.IsWellFormedUriString(dataAddress, UriKind.Absolute))
            {
                throw new InvalidOperationException($"Unable to start WebApi service {nameof(InfrastructureService)}: no valid {nameof(dataAddress)} can be build from configuration.");
            }

            if (managementAddress == null)
            {
                throw new InvalidOperationException($"Unable to start WebApi service {nameof(InfrastructureService)}: {nameof(managementAddress)} cannot be build from configuration.");
            }
            if (!Uri.IsWellFormedUriString(managementAddress, UriKind.Absolute))
            {
                throw new InvalidOperationException($"Unable to start WebApi service {nameof(InfrastructureService)}: no valid {nameof(managementAddress)} can be build from configuration.");
            }

            return new ServiceDetails("WebApi", new Uri(managementAddress, UriKind.Absolute), new Uri(dataAddress, UriKind.Absolute), true);
        }
        private ServiceDetails BuildGrpcServiceDetails(IConfigurationDetails configurationDetails)
        {
            var userHost = configurationDetails.Hosts["UserHost"];
            userHost = ConvertToDedicatedNetworkAddress(userHost);
            var adminHost = configurationDetails.Hosts["AdminHost"];
            adminHost = ConvertToDedicatedNetworkAddress(adminHost);

            var dataAddressBuilder = new StringBuilder();
            dataAddressBuilder.Append($"http://{userHost}:{configurationDetails.Ports["UserPort"]}");
            dataAddressBuilder.Append(configurationDetails.Paths["UserApi"]);
            var dataAddress = dataAddressBuilder.ToString();

            var managementAddressBuilder = new StringBuilder();
            managementAddressBuilder.Append($"http://{adminHost}:{configurationDetails.Ports["AdminPort"]}");
            managementAddressBuilder.Append(configurationDetails.Paths["AdminApi"]);
            var managementAddress = managementAddressBuilder.ToString();

            if (dataAddress == null)
            {
                throw new InvalidOperationException($"Unable to start Grpc service {nameof(InfrastructureService)}: {nameof(dataAddress)} cannot be build from configuration.");
            }
            if (!Uri.IsWellFormedUriString(dataAddress, UriKind.Absolute))
            {
                throw new InvalidOperationException($"Unable to start Grpc service {nameof(InfrastructureService)}: no valid {nameof(dataAddress)} can be build from configuration.");
            }

            if (managementAddress == null)
            {
                throw new InvalidOperationException($"Unable to start Grpc service {nameof(InfrastructureService)}: {nameof(managementAddress)} cannot be build from configuration.");
            }
            if (!Uri.IsWellFormedUriString(managementAddress, UriKind.Absolute))
            {
                throw new InvalidOperationException($"Unable to start Grpc service {nameof(InfrastructureService)}: no valid {nameof(managementAddress)} can be build from configuration.");
            }
            
            return new ServiceDetails("Grpc", new Uri(managementAddress, UriKind.Absolute), new Uri(dataAddress, UriKind.Absolute), true);
        }

        private string ConvertToDedicatedNetworkAddress(string host)
        {
            // TODO. this is ugly and way in the incorrect place.
            return host switch
            {
                "0.0.0.0" => "127.0.0.1",
                "255.255.255.255" => "127.0.0.1",
                _ => host
            };
        }
    }
}