// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public static class HostBuilderAddHostServicesExtensions
    {
        public static IHostBuilder UseHostServices<THostServicesFactory>(
            this IHostBuilder hostBuilder,
            IConfigurationRoot configurationRoot)
            where THostServicesFactory : IHostServicesFactory, new()
        {
            var logger = Log.ForContext(typeof(HostBuilderAddHostServicesExtensions));

            var servicesFactory = new THostServicesFactory();
            var services = servicesFactory.Create(configurationRoot);

            logger.Information("Found {ServicesCount} services", services.Length);
            foreach (var service in services)
            {
                logger.Information("Found service: {ServiceId}", service.Configuration.Section.Key);
            }

            return hostBuilder
                .ConfigureServices((_, serviceCollection) => ConfigureBackgroundServices(hostBuilder, logger, services, serviceCollection))
                .ConfigureHostConfiguration(builder => ConfigureHostConfiguration(hostBuilder, builder, configurationRoot))
                .ConfigureWebHost(webHostBuilder =>
                {
                    webHostBuilder
                        .UseUrls() // No need to have ASP.NET Core try to use it's default urls.
                        .UseKestrel(kestrelOptions =>
                        {
                            var configurator = new KestrelConfigurator();
                            configurator.Configure(kestrelOptions, services, configurationRoot);
                        });
                    webHostBuilder.Configure((context, application) => ConfigureApplication(hostBuilder, logger, services, context, application));
                    //_configureHost?.Invoke(webHostBuilder);
                });
        }

        public static void ConfigureApplication(this IHostBuilder _, ILogger logger, IService[] services, WebHostBuilderContext context, IApplicationBuilder application)
        {
            // Each network service gets instantiated in its own isolated environment.
            // The only subsystems that services can share.
            foreach (var service in services.OfType<INetworkService>())
            {
                logger.Information("Configuring network service {ServiceName}", service.GetType().Name);

                application.IsolatedMapOnCondition(context.HostingEnvironment, service);

                var uriBuilder = new UriBuilder
                {
                    Host = service.Configuration.IpAddress,
                    Port = (int)service.Configuration.Port,
                    Path = service.Configuration.Path
                };

                logger.Information("Configured network service {ServiceName}: {Location}", service.GetType().Name, uriBuilder.ToString());

            }
        }
        public static void ConfigureHostConfiguration(this IHostBuilder _, IConfigurationBuilder builder, IConfigurationRoot configurationRoot)
        {
            builder.AddConfiguration(configurationRoot);
        }

        public static void ConfigureBackgroundServices(this IHostBuilder _, ILogger logger, IService[] services, IServiceCollection serviceCollection)
        {
            foreach (var service in services.OfType<IBackgroundService>())
            {
                logger.Information("Configuring background service {ServiceName}", service.GetType().Name);

                service.ConfigureServices(serviceCollection, services);

                logger.Information("Configured background service {ServiceName}", service.GetType().Name);
            }
        }
    }
}
