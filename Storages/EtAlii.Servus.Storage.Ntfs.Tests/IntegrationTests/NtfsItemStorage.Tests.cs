namespace EtAlii.Servus.Storage.Ntfs.Tests.IntegrationTests
{
    using TestAssembly = EtAlii.Servus.Storage.Ntfs.Tests.TestAssembly;
    using EtAlii.Servus.Storage.Tests;
    using Xunit;
    using System;

    
    public class NtfsItemStorage_Tests : NtfsStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsItemStorage_Store_SimpleTestItem()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var id = Guid.NewGuid();
            var item = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            Storage.Items.Store(item, id, containerId);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsItemStorage_Store_1000_SimpleTestItem()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var count = 1000;
            var ids = StorageTestHelper.CreateIds(count);
            var items = StorageTestHelper.CreateSimpleTestItems(count);

            var now = DateTime.Now;

            for (int i = 0; i < count; i++)
            {
                // Act.
                Storage.Items.Store(items[i], ids[i], containerId);
            }

            // Assert.
            var delta = DateTime.Now - now;
            Assert.True(delta < TimeSpan.FromSeconds(20));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsItemStorage_Store_Retrieve_SimpleTestItem()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var id = Guid.NewGuid();
            var item = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            Storage.Items.Store(item, id, containerId);
            var retrievedItem = Storage.Items.Retrieve<SimpleTestItem>(id, containerId);

            // Assert.
            Assert.NotNull(retrievedItem);
            Assert.Equal(item.Name, retrievedItem.Name);
            Assert.Equal(item.Value, retrievedItem.Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsItemStorage_Store_Retrieve_1000_SimpleTestItems()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var count = 1000;
            var ids = StorageTestHelper.CreateIds(count);
            var items = StorageTestHelper.CreateSimpleTestItems(count);

            for (int i = 0; i < count; i++)
            {
                // Act.
                Storage.Items.Store(items[i], ids[i], containerId);
            }

            var now = DateTime.Now;

            for (int i = 0; i < count; i++)
            {
                // Act.
                var retrievedItem = Storage.Items.Retrieve<SimpleTestItem>(ids[i], containerId);

                // Assert.
                Assert.NotNull(retrievedItem);
                Assert.Equal(items[i].Name, retrievedItem.Name);
                Assert.Equal(items[i].Value, retrievedItem.Value);
            }

            // Assert.
            var delta = DateTime.Now - now;
            Assert.True(delta < TimeSpan.FromSeconds(20));
        }
    }
}
