// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric.Ntfs.Tests;

using System;
using System.Threading;
using System.Threading.Tasks;
using EtAlii.xTechnology.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class StorageServiceFactoryTests
{
    [Fact]
    public void StorageServiceFactory_Ctor()
    {
        // Arrange.

        // Act.
        var factory = new StorageServiceFactory();

        // Assert.
        Assert.NotNull(factory);
    }

    [Fact]
    public void StorageServiceFactory_Create()
    {
        // Arrange.
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("HostSettings.json")
            .ExpandEnvironmentVariablesInJson()
            .Build();

        var configurationSection = configurationRoot.GetSection("Storage");
        ServiceConfiguration.TryCreate(configurationSection, configurationRoot, out var configuration);
        var factory = new StorageServiceFactory();

        // Act.
        var storageService = factory.Create(configuration);

        // Assert.
        Assert.NotNull(storageService);
    }

    [Fact]
    public async Task StorageServiceFactory_ConfigureServices()
    {
        // Arrange.
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("HostSettings.json")
            .ExpandEnvironmentVariablesInJson()
            .Build();

        var configurationSection = configurationRoot.GetSection("Storage");
        ServiceConfiguration.TryCreate(configurationSection, configurationRoot, out var configuration);
        var factory = new StorageServiceFactory();
        var storageService = (IStorageService)factory.Create(configuration);
        var serviceCollection = new ServiceCollection();
        var services = Array.Empty<IService>();

        // Act.
        await storageService.StartAsync(CancellationToken.None).ConfigureAwait(false);
        storageService.ConfigureServices(serviceCollection, services);
        await storageService.StopAsync(CancellationToken.None).ConfigureAwait(false);

        // Assert.
        Assert.Equal(2, serviceCollection.Count);
        Assert.NotNull(storageService.Storage);
    }
}
