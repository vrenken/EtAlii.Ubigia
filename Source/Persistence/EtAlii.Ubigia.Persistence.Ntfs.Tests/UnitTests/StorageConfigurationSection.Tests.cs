// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Persistence.Ntfs;
    using EtAlii.Ubigia.Serialization;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;

    public class StorageConfigurationSectionTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void StorageConfigurationSection_Create()
        {
            // Arrange.

            // Act.
            var storageConfigurationSection = new StorageConfigurationSection();

            // Assert.
            Assert.NotNull(storageConfigurationSection);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void StorageConfigurationSection_ToStorageConfiguration()
        {
            // Arrange.
            var storageConfigurationSection = new StorageConfigurationSection
            {
                Name = "TestName", BaseFolder = "TestFolder"
            };

            // Act.
            var storageConfiguration = storageConfigurationSection.ToStorageConfiguration();

            // Assert.
            Assert.NotNull(storageConfiguration);
            Assert.Equal("TestName", storageConfiguration.Name);
            Assert.Single(storageConfiguration.Extensions.OfType<NtfsStorageExtension>());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void StorageConfigurationSection_Get_BaseFolder()
        {
            // Arrange.
            var storageConfigurationSection = new StorageConfigurationSection
            {
                Name = "TestName", BaseFolder = "TestFolder"
            };
            var storageConfiguration = storageConfigurationSection.ToStorageConfiguration();
            var extension = storageConfiguration.Extensions.OfType<NtfsStorageExtension>().Single();

            var container = new Container();
            container.Register<ISerializer, Serializer>();
            container.Register(() => storageConfiguration);

            // Act.
            extension.Initialize(container);

            // Assert.
            var pathBuilder = container.GetInstance<IPathBuilder>();
            Assert.Equal(@"TestFolder\TestName", pathBuilder.BaseFolder);
        }

    }
}
