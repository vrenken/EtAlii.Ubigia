﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc;

using System;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.Ubigia.Infrastructure.Transport.Grpc;
using EtAlii.xTechnology.Hosting;
using EtAlii.xTechnology.MicroContainer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using EtAlii.xTechnology.Threading;
using Microsoft.AspNetCore.Hosting;
using IServiceCollection = Microsoft.Extensions.DependencyInjection.IServiceCollection;

public class AdminGrpcService : NetworkServiceBase<AdminGrpcService>
{
    public AdminGrpcService(ServiceConfiguration configuration)
        : base(configuration)
    {
    }

    protected override void ConfigureNetworkServices(
        IServiceCollection services,
        IServiceProvider globalServices,
        IFunctionalContext functionalContext)
    {
        var container = new Container();
        new AdminApiScaffolding(functionalContext).Register(container);
        new AuthenticationScaffolding().Register(container);
        new SerializationScaffolding().Register(container);

        container.Register<IAdminAuthenticationService, AdminAuthenticationService>();
        container.Register<IAdminStorageService, AdminStorageService>();
        container.Register<IAdminAccountService, AdminAccountService>();
        container.Register<IAdminSpaceService, AdminSpaceService>();
        container.Register<IAdminInformationService, AdminInformationService>();

        services.AddSingleton(_ => container.GetInstance<ISimpleAuthenticationVerifier>());
        services.AddSingleton(_ => container.GetInstance<ISimpleAuthenticationTokenVerifier>());
        services.AddSingleton(_ => container.GetInstance<ISimpleAuthenticationBuilder>());

        services.AddSingleton(_ => (AdminAuthenticationService) container.GetInstance<IAdminAuthenticationService>());
        services.AddSingleton(_ => (AdminStorageService) container.GetInstance<IAdminStorageService>());
        services.AddSingleton(_ => (AdminAccountService) container.GetInstance<IAdminAccountService>());
        services.AddSingleton(_ => (AdminSpaceService) container.GetInstance<IAdminSpaceService>());
        services.AddSingleton(_ => (AdminInformationService) container.GetInstance<IAdminInformationService>());

        var authenticationTokenVerifier = container.GetInstance<ISimpleAuthenticationTokenVerifier>();
        var contextCorrelator = container.GetInstance<IContextCorrelator>();

        services
            .AddGrpc()
            .AddServiceOptions<AdminStorageService>(options =>
            {
                options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier);
                options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
            })
            .AddServiceOptions<AdminAccountService>(options =>
            {
                options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier);
                options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
            })
            .AddServiceOptions<AdminSpaceService>(options =>
            {
                options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier);
                options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
            })
            .AddServiceOptions<AdminInformationService>(options =>
            {
                options.Interceptors.Add<AccountAuthenticationInterceptor>(authenticationTokenVerifier);
                options.Interceptors.Add<CorrelationServiceInterceptor>(contextCorrelator);
            });
    }

    protected override void ConfigureNetworkApplication(
        IApplicationBuilder application,
        IWebHostEnvironment environment)
    {
        application
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<AdminAuthenticationService>();
                endpoints.MapGrpcService<AdminStorageService>();
                endpoints.MapGrpcService<AdminAccountService>();
                endpoints.MapGrpcService<AdminSpaceService>();
                endpoints.MapGrpcService<AdminInformationService>();
            });
    }
}
