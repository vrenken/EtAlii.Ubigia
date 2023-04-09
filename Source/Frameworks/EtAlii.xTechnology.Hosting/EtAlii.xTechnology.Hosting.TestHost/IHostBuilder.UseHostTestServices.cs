// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

public static partial class HostBuilderAddHostTestServicesExtensions
{
    public static IHostBuilder UseServicesFactoryOnTestHost<THostServicesFactory>(
        this IHostBuilder hostBuilder,
        IConfigurationRoot configurationRoot,
        out IService[] services)
        where THostServicesFactory : IHostServicesFactory, new()
    {
        var servicesFactory = new THostServicesFactory();
        services = servicesFactory.Create(configurationRoot);

        return hostBuilder.UseServicesOnTestHost(configurationRoot, services);
    }
}
