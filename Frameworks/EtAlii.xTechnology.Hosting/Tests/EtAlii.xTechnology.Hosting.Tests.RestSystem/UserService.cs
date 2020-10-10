﻿namespace EtAlii.xTechnology.Hosting.Tests.RestSystem
{
    using EtAlii.xTechnology.Hosting.Service.Rest;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class UserService : ServiceBase
    {
        public UserService(IConfigurationSection configuration) : base(configuration)
        {
        }

        
        
        protected override void ConfigureApplication(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<UserController>()
                .AddControllers()
                .AddTypedControllers<UserController>();
        }
    }
}
