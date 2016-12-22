namespace EtAlii.Servus.Storage.Ntfs.Tests.IntegrationTests
{
    using TestAssembly = EtAlii.Servus.Storage.Ntfs.Tests.TestAssembly;
    using EtAlii.Servus.Storage.Tests;
    using Xunit;
    using System;

    
    public class NtfsPropertiesStorage_Tests : NtfsStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsPropertyStorage_Store_SimpleTestProperties()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var properties = TestProperties.CreateSimple();

            // Act.
            Storage.Properties.Store(containerId, properties);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsPropertyStorage_Store_1000_SimpleTestProperties()
        {
            // Arrange.
            var count = 1000;
            var containerIds = StorageTestHelper.CreateSimpleContainerIdentifiers(count);
            var properties = TestProperties.CreateSimple(count);

            var now = DateTime.Now;

            for (int i = 0; i < count; i++)
            {
                // Act.
                Storage.Properties.Store(containerIds[i], properties[i]);
            }

            // Assert.
            var delta = DateTime.Now - now;
            Assert.True(delta < TimeSpan.FromSeconds(10));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsPropertyStorage_Store_Retrieve_SimpleTestProperties()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var properties = TestProperties.CreateSimple();

            // Act.
            Storage.Properties.Store(containerId, properties);
            var retrievedProperties = Storage.Properties.Retrieve(containerId);

            // Assert.
            Assert.NotNull(retrievedProperties);
            Assert.Equal(properties["Name"], retrievedProperties["Name"]);
            Assert.Equal(properties["Value"], retrievedProperties["Value"]);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsPropertyStorage_Store_Retrieve_1000_SimpleTestProperties()
        {
            // Arrange.
            var count = 1000;
            var containerIds = StorageTestHelper.CreateSimpleContainerIdentifiers(count);
            var properties = TestProperties.CreateSimple(count);

            for (int i = 0; i < count; i++)
            {
                // Act.
                Storage.Properties.Store(containerIds[i], properties[i]);
            }

            var now = DateTime.Now;

            for (int i = 0; i < count; i++)
            {
                // Act.
                var retrievedProperties = Storage.Properties.Retrieve(containerIds[i]);

                // Assert.
                Assert.NotNull(retrievedProperties);
                Assert.Equal(properties[i]["Name"], retrievedProperties["Name"]);
                Assert.Equal(properties[i]["Value"], retrievedProperties["Value"]);
            }

            // Assert.
            var delta = DateTime.Now - now;
            Assert.True(delta < TimeSpan.FromSeconds(10));
        }
    }
}
