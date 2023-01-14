// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport;

using System;
using EtAlii.Ubigia.Infrastructure.Functional;
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
    private bool _shouldActivate;

    protected NetworkServiceBase(ServiceConfiguration configuration)
    {
        Configuration = configuration;
        Logger.Information("Instantiated {ServiceName}", nameof(TNetworkService));
    }


    protected virtual bool ShouldActivate(IFunctionalContext functionalContext)
    {
        // Default implementation is that all network services get disabled when
        // the system is not yet in a good state.
        // The setup portal on the other hand will be activated only when the .

        // return true;
        return functionalContext.Status.SystemIsOperational;
    }

    protected abstract void ConfigureNetworkServices(IServiceCollection services, IServiceProvider globalServices, IFunctionalContext functionalContext);

    public void ConfigureServices(IServiceCollection services, IServiceProvider globalServices)
    {
        var functionalContext = globalServices.GetService<IInfrastructureService>().Functional;
        _shouldActivate = ShouldActivate(functionalContext);

        if (_shouldActivate)
        {
            ConfigureNetworkServices(services, globalServices, functionalContext);
        }
    }

    public void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
    {
        if (_shouldActivate)
        {
            ConfigureNetworkApplication(application, environment);
        }
    }

    protected abstract void ConfigureNetworkApplication(IApplicationBuilder application, IWebHostEnvironment environment);
}
