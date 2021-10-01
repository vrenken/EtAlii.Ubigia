// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// This interface indicates that a service should be configured as a background service.
    /// It is mostly relevant for determining when and how a service should be configured, in case of a background service this means that
    /// the service will be instantiated before the network services and not isolated. In other words it can be accessed
    /// as a dependency in network services.
    /// </summary>
    public interface IBackgroundService : IService, IHostedService
    {
        void ConfigureServices(IServiceCollection services);
    }
}
