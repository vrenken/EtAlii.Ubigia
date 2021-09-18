// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.User.Api.NetCore
{
    using System.Diagnostics;
    using EtAlii.xTechnology.Hosting.Service.Rest;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class UserRestService : ServiceBase
    {
        public UserRestService(IConfigurationSection configuration) : base(configuration)
        {
        }


        protected override void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
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

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UserController>();
            services
                .AddControllers()
                .AddTypedControllers<UserController>();
        }
    }
}
