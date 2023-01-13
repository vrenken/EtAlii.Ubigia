// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.SignalRSystem;

using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

public class UserService : INetworkService
{
    public ServiceConfiguration Configuration { get; }

    public UserService(ServiceConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
    {
        application
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapHub<UserHub>(SignalRHub.User);
            });
    }

    public void ConfigureServices(IServiceCollection services, IServiceProvider globalServices)
    {
        services
            .AddCors()
            .AddSignalR(options =>
            {
                options.MaximumParallelInvocationsPerClient = 10;
                options.EnableDetailedErrors = Debugger.IsAttached;
            });
    }
}
