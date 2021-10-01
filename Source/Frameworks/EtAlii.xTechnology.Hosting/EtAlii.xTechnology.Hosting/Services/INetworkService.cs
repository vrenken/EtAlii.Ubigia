// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// This interface indicates that a service should be configured as a network service.
    /// It is mostly relevant for determining when and how a service should be configured, in case of a network service this means also that
    /// the service will be instantiated in an isolated manner. I.e. It's dependencies cannot be consumed by other services.
    /// </summary>
    public interface INetworkService : IService
    {
        void ConfigureServices(IServiceCollection services, IServiceProvider globalServices);

        void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment);
    }
}
