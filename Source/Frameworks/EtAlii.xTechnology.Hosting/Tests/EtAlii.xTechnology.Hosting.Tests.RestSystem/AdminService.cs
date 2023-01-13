// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.RestSystem;

using System;
using EtAlii.xTechnology.Hosting.Service.Rest;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

public class AdminService : INetworkService
{
    public ServiceConfiguration Configuration { get; }

    public AdminService(ServiceConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
    {
        application
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
    }

    public void ConfigureServices(IServiceCollection services, IServiceProvider globalServices)
    {
        services
            .AddSingleton<AdminController>()
            .AddControllers()
            .AddTypedControllers<AdminController>()
            .AddTypedControllers<RoutesController>();
    }
}
