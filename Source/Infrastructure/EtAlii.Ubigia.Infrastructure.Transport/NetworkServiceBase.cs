// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

    // TODO: These classes and dependencies should be moved to EtAlii.Ubigia.Infrastructure.Transport.Portal
    public abstract class NetworkServiceBase<TNetworkService> : INetworkService
        where TNetworkService : INetworkService
    {
        /// <inheritdoc />
        public ServiceConfiguration Configuration { get; }

        protected readonly ILogger Logger = Log.ForContext<TNetworkService>();

        protected NetworkServiceBase(ServiceConfiguration configuration)
        {
            Configuration = configuration;
            Logger.Information("Instantiated {ServiceName}", nameof(TNetworkService));
        }

        public abstract void ConfigureServices(IServiceCollection services, IServiceProvider globalServices);

        public abstract void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment);
    }
}
