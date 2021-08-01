// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class PropertiesStorageTests : IAsyncLifetime
    {
        private StorageUnitTestContext _testContext;

        public async Task InitializeAsync()
        {
            _testContext = new StorageUnitTestContext();
            await _testContext
                .InitializeAsync()
                .ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await _testContext
                .DisposeAsync()
                .ConfigureAwait(false);
            _testContext = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyStorage_Store_SimpleTestProperties()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var properties = _testContext.Properties.CreateSimple();

            // Act.
            _testContext.Storage.Properties.Store(containerId, properties);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyStorage_Store_1000_SimpleTestProperties()
        {
            // Arrange.
            var count = 1000;
            var containerIds = StorageTestHelper.CreateSimpleContainerIdentifiers(count);
            var properties = _testContext.Properties.CreateSimple(count);

            var now = DateTime.Now;

            for (var i = 0; i < count; i++)
            {
                // Act.
                _testContext.Storage.Properties.Store(containerIds[i], properties[i]);
            }

            // Assert.
            var delta = DateTime.Now - now;
            Assert.True(delta < TimeSpan.FromSeconds(80), $"delta={delta}");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyStorage_Store_Retrieve_SimpleTestProperties()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var properties = _testContext.Properties.CreateSimple();

            // Act.
            _testContext.Storage.Properties.Store(containerId, properties);
            var retrievedProperties = _testContext.Storage.Properties.Retrieve(containerId);

            // Assert.
            Assert.NotNull(retrievedProperties);
            Assert.Equal(properties["Name"], retrievedProperties["Name"]);
            Assert.Equal(properties["Value"], retrievedProperties["Value"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PropertyStorage_Store_Retrieve_1000_SimpleTestProperties()
        {
            // Arrange.
            var count = 1000;
            var containerIds = StorageTestHelper.CreateSimpleContainerIdentifiers(count);
            var properties = _testContext.Properties.CreateSimple(count);

            for (var i = 0; i < count; i++)
            {
                // Act.
                _testContext.Storage.Properties.Store(containerIds[i], properties[i]);
            }

            var now = DateTime.Now;

            for (var i = 0; i < count; i++)
            {
                // Act.
                var retrievedProperties = _testContext.Storage.Properties.Retrieve(containerIds[i]);

                // Assert.
                Assert.NotNull(retrievedProperties);
                Assert.Equal(properties[i]["Name"], retrievedProperties["Name"]);
                Assert.Equal(properties[i]["Value"], retrievedProperties["Value"]);
            }

            // Assert.
            var delta = DateTime.Now - now;
            Assert.True(delta < TimeSpan.FromSeconds(60), $"delta={delta}");
        }
    }
}
