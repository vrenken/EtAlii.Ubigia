// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

public static partial class HostBuilderAddHostTestServicesExtensions
{
    public static IHostBuilder UseHostTestServices<THostServicesFactory>(
        this IHostBuilder hostBuilder,
        IConfigurationRoot configurationRoot,
        out IService[] services)
        where THostServicesFactory : IHostServicesFactory, new()
    {
        var logger = Log.ForContext(typeof(HostBuilderAddHostTestServicesExtensions));

        var servicesFactory = new THostServicesFactory();
        services = servicesFactory.Create(configurationRoot);

        logger.Information("Found {ServicesCount} services", services.Length);
        foreach (var service in services)
        {
            logger.Information("Found service: {ServiceId}", service.Configuration.Section.Key);
        }

        logger.Debug("Creating ASP.Net Core test host");

        var instantiatedServices = services; // We need to use a true local variable.
        return hostBuilder
            .ConfigureServices((_, serviceCollection) => hostBuilder.ConfigureBackgroundServices(logger, instantiatedServices, serviceCollection))
            .ConfigureHostConfiguration(builder => hostBuilder.ConfigureHostConfiguration(builder, configurationRoot))
            .ConfigureWebHost(webHostBuilder =>
            {
                webHostBuilder
                    .UseTestServer()
                    .Configure((context, application) => hostBuilder.ConfigureApplication(logger, instantiatedServices, context, application));
            });
    }
}
