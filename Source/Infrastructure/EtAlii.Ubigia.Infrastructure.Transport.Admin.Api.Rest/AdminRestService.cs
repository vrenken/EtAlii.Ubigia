﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest;

using System;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.Ubigia.Infrastructure.Transport.Rest;
using EtAlii.xTechnology.Hosting;
using EtAlii.xTechnology.Hosting.Service.Rest;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using EtAlii.xTechnology.Threading;
using Microsoft.AspNetCore.Hosting;

public class AdminRestService : NetworkServiceBase<AdminRestService>
{
    private IContextCorrelator _contextCorrelator;

    public AdminRestService(ServiceConfiguration configuration)
        : base(configuration)
    {
    }

    protected override void ConfigureNetworkServices(
        IServiceCollection services,
        IServiceProvider globalServices,
        IFunctionalContext functionalContext)
    {
        _contextCorrelator = functionalContext.ContextCorrelator;

        services
            .AddSingleton(functionalContext)
            .AddSingleton(functionalContext.Storages)
            .AddSingleton(functionalContext.Spaces)
            .AddSingleton(functionalContext.Accounts)

            .AddSingleton(functionalContext.Roots) // We want the management portal to manage the roots as well.

            .AddAttributeBasedInfrastructureAuthorization()
            .AddControllers()
            .AddInfrastructureSerialization()
            .AddMvcOptions(options =>
            {
                options.EnableEndpointRouting = false;
                options.InputFormatters.Clear();
                options.InputFormatters.Add(new PayloadMediaTypeInputFormatter());
                options.OutputFormatters.Clear();
                options.OutputFormatters.Add(new PayloadMediaTypeOutputFormatter());
            })
            .AddTypedControllers<RestController>();
    }

    protected override void ConfigureNetworkApplication(
        IApplicationBuilder application,
        IWebHostEnvironment environment)
    {
        application
            .UseRouting()
            .UseAuthorization()
            .UseCorrelationIdsFromHeaders(_contextCorrelator)
            .UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
