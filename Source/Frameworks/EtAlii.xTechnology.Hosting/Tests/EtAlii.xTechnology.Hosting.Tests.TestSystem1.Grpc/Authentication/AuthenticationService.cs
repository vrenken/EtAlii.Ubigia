// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc;

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

public class AuthenticationService : INetworkService
{
    public ServiceConfiguration Configuration { get; }

    public AuthenticationService(ServiceConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services, IServiceProvider globalServices)
    {
        // For testing we don't have anything related to the services to configure.
    }

    public void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
    {
        // For testing we don't have anything related to the application to configure.
    }
}
