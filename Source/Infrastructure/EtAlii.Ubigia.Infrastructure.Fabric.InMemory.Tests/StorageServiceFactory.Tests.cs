// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric.InMemory.Tests
{
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;
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
    }
}
