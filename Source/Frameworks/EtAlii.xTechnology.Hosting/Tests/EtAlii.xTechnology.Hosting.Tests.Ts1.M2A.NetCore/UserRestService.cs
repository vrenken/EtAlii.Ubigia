// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.NetCore
{
    using System;
    using System.Diagnostics;
    using EtAlii.xTechnology.Hosting.Service.Rest;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class UserRestService : INetworkService
    {
        /// <inheritdoc />
        public ServiceConfiguration Configuration { get; }

        public UserRestService(ServiceConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    if (Debugger.IsAttached)
                    {
                        endpoints.AddDebugRouter();
                    }
                    endpoints.MapControllers();
                });

        }

        public void ConfigureServices(IServiceCollection services, IServiceProvider globalServices)
        {
            services.AddSingleton<UserController>();
            services
                .AddControllers()
                .AddTypedControllers<UserController>();
        }
    }
}
