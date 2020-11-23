﻿namespace EtAlii.Ubigia.Persistence.Portable.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence.Tests;
    using Xunit;

    public class PortableItemStorageTests : PortableStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableItemStorage_Store_SimpleTestItem()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var id = Guid.NewGuid();
            var item = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            Storage.Items.Store(item, id, containerId);

            // Assert.
        }

        [Fact]
        public void PortableItemStorage_Store_SimpleTestItem_Has()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var id = Guid.NewGuid();
            var item = StorageTestHelper.CreateSimpleTestItem();
            Storage.Items.Store(item, id, containerId);

            // Act.
            var hasItem = Storage.Items.Has(id, containerId);

            // Assert.
            Assert.True(hasItem);
        }

        [Fact]
        public void PortableItemStorage_Store_SimpleTestItem_Get()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var id = Guid.NewGuid();
            var item = StorageTestHelper.CreateSimpleTestItem();
            Storage.Items.Store(item, id, containerId);

            // Act.
            var ids = Storage.Items.Get(containerId);

            // Assert.
            Assert.Single(ids);
            Assert.Equal(id, ids[0]);
        }


        [Fact]
        public void PortableItemStorage_Store_SimpleTestItem_Remove()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var id = Guid.NewGuid();
            var item = StorageTestHelper.CreateSimpleTestItem();
            Storage.Items.Store(item, id, containerId);
            var hadItem = Storage.Items.Has(id, containerId);

            // Act.
            Storage.Items.Remove(id, containerId);

            // Assert.
            var hasItem = Storage.Items.Has(id, containerId);
            Assert.True(hadItem);
            Assert.False(hasItem);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableItemStorage_Store_1000_SimpleTestItem_Timed()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var count = 1000;
            var ids = StorageTestHelper.CreateIds(count);
            var items = StorageTestHelper.CreateSimpleTestItems(count);

            var now = DateTime.Now;

            for (var i = 0; i < count; i++)
            {
                // Act.
                Storage.Items.Store(items[i], ids[i], containerId);
            }

            // Assert.
            var delta = DateTime.Now - now;
            Assert.True(delta < TimeSpan.FromSeconds(60), $"delta={delta}"); // PV: Was originally 20 but we increased it due to parallel test taking longer.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task PortableItemStorage_Store_Retrieve_SimpleTestItem()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var id = Guid.NewGuid();
            var item = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            Storage.Items.Store(item, id, containerId);
            var retrievedItem = await Storage.Items.Retrieve<SimpleTestItem>(id, containerId).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(retrievedItem);
            Assert.Equal(item.Name, retrievedItem.Name);
            Assert.Equal(item.Value, retrievedItem.Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task PortableItemStorage_Store_Retrieve_1000_SimpleTestItems()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var count = 1000;
            var ids = StorageTestHelper.CreateIds(count);
            var items = StorageTestHelper.CreateSimpleTestItems(count);

            for (var i = 0; i < count; i++)
            {
                // Act.
                Storage.Items.Store(items[i], ids[i], containerId);
            }

            var now = DateTime.Now;

            for (var i = 0; i < count; i++)
            {
                // Act.
                var retrievedItem = await Storage.Items.Retrieve<SimpleTestItem>(ids[i], containerId).ConfigureAwait(false);

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
