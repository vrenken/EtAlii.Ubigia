// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Portal.Tests;

using System;
using System.Collections.Generic;
using EtAlii.Ubigia.Infrastructure.Fabric;
using EtAlii.Ubigia.Infrastructure.Fabric.InMemory;
using EtAlii.Ubigia.Infrastructure.Portal.Admin;
using EtAlii.Ubigia.Infrastructure.Portal.User;
using EtAlii.Ubigia.Infrastructure.Transport;
using EtAlii.xTechnology.Hosting;
using Microsoft.Extensions.Configuration;

public class ServiceInstancesBuilder
{
    private readonly List<IService> _services = new();
    private readonly IConfigurationRoot _configurationRoot;

    public ServiceInstancesBuilder()
    {
        _configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("HostSettings.json")
            .ExpandEnvironmentVariablesInJson()
            .Build();
    }

    public ServiceInstancesBuilder AddStorage(out IStorageService service) => AddService("Storage", configuration => new StorageServiceFactory().Create(configuration), out service);

    public ServiceInstancesBuilder AddInfrastructure(out IInfrastructureService service) => AddService("Infrastructure", configuration => new InfrastructureServiceFactory().Create(configuration), out service);

    public ServiceInstancesBuilder AddAdminPortal(out INetworkService service) => AddService("Management-Portal", configuration => new AdminPortalService(configuration), out service);

    public ServiceInstancesBuilder AddUserPortal(out INetworkService service) => AddService("User-Portal", configuration => new UserPortalService(configuration), out service);

    private ServiceInstancesBuilder AddService<TService>(string sectionKey, Func<ServiceConfiguration, IService> serviceFactory, out TService service)
        where TService: IService
    {
        var configurationSection = _configurationRoot.GetSection(sectionKey);
        ServiceConfiguration.TryCreate(configurationSection, _configurationRoot, out var configuration);
        service = (TService)serviceFactory(configuration);
        _services.Add(service);
        return this;
    }

    public IService[] ToServices() => _services.ToArray();
}
