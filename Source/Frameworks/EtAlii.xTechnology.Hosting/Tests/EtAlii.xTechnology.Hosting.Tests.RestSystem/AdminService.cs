// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.RestSystem
{
    using EtAlii.xTechnology.Hosting.Service.Rest;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminService : ServiceBase
    {
        public AdminService(IConfigurationSection configuration) : base(configuration)
        {
        }

        
        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<AdminController>()
                .AddControllers()
                .AddTypedControllers<AdminController>()
                .AddTypedControllers<RoutesController>();
        }
    }
}
