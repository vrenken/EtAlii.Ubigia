﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Portal.Tests;

using System;
using System.Collections.Generic;
using EtAlii.Ubigia.Infrastructure.Fabric;
using EtAlii.Ubigia.Infrastructure.Fabric.InMemory;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.Ubigia.Infrastructure.Portal.Admin;
using EtAlii.Ubigia.Infrastructure.Portal.Setup;
using EtAlii.Ubigia.Infrastructure.Portal.User;
using EtAlii.Ubigia.Infrastructure.Transport;
using EtAlii.xTechnology.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

public class ServicesTestConfigurationBuilder
{
    private readonly List<IService> _services = new();
    private readonly IConfigurationRoot _configurationRoot;

    private readonly ILogger _logger = Log.ForContext<ServicesTestConfigurationBuilder>();

    private ISystemStatusChecker _alternativeSystemStatusChecker;

    public ServicesTestConfigurationBuilder()
    {
        _configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("HostSettings.json")
            .ExpandEnvironmentVariablesInJson()
            .Build();
    }

    public ServicesTestConfigurationBuilder AddStorage(out IStorageService service) => AddService("Storage", configuration => new StorageServiceFactory().Create(configuration), out service);

    public ServicesTestConfigurationBuilder AddInfrastructure(out IInfrastructureService service) => AddService("Infrastructure", configuration => new InfrastructureServiceFactory().Create(configuration), out service);

    public ServicesTestConfigurationBuilder AddAdminPortal(out INetworkService service) => AddService("Management-Portal", configuration => new AdminPortalService(configuration), out service);

    public ServicesTestConfigurationBuilder AddUserPortal(out INetworkService service) => AddService("User-Portal", configuration => new UserPortalService(configuration), out service);

    public ServicesTestConfigurationBuilder AddSetupPortal(out INetworkService service) => AddService("Setup-Portal", configuration => new SetupPortalService(configuration), out service);

    private ServicesTestConfigurationBuilder AddService<TService>(string sectionKey, Func<ServiceConfiguration, IService> serviceFactory, out TService service)
        where TService: IService
    {
        var configurationSection = _configurationRoot.GetSection(sectionKey);
        ServiceConfiguration.TryCreate(configurationSection, _configurationRoot, out var configuration);
        service = (TService)serviceFactory(configuration);
        _services.Add(service);

        _logger.Information("Added {ServiceName} service to ServiceInstancesBuilder", service.GetType().Name);

        return this;
    }

    public ServicesTestConfigurationBuilder SetSystemStatus(SystemStatus systemStatus)
    {
        _alternativeSystemStatusChecker = new TestSystemStatusChecker
        {
            SystemStatus = systemStatus
        };
        return this;
    }

    public ServicesTestConfiguration ToConfiguration()
    {
        return new ServicesTestConfiguration
        {
            Services = _services.ToArray(),
            ConfigurationRoot = _configurationRoot,
            AlternativeSystemStatusChecker = _alternativeSystemStatusChecker
        };
    }
}
