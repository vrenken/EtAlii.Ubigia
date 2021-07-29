// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class ItemStorageTests : StorageUnitTestContext
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ItemStorage_Store_SimpleTestItem()
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
        public void ItemStorage_Store_SimpleTestItem_Has()
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
        public void ItemStorage_Store_SimpleTestItem_Get()
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
        public void ItemStorage_Store_SimpleTestItem_Remove()
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

        [Fact(Skip = "This test should succeed")]
        public void ItemStorage_Store_Null()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var id = Guid.NewGuid();

            // Act.
            var act = new Action(() => Storage.Items.Store((object)null, id, containerId));

            // Assert.
            Assert.Throws<StorageException>(act);

            // Assert.
        }

        [Fact(Skip = "This test should succeed")]
        public void ItemStorage_Store_SimpleTestItem_Invalid_ContainerIdentifier()
        {
            // Arrange.
            var containerId = ContainerIdentifier.Empty;
            var id = Guid.NewGuid();
            var item = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            var act = new Action(() => Storage.Items.Store(item, id, containerId));

            // Assert.
            Assert.Throws<StorageException>(act);
        }

        [Fact(Skip = "This test should succeed")]
        public void ItemStorage_Store_SimpleTestItem_Invalid_ContainerId()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var id = Guid.Empty;
            var item = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            var act = new Action(() => Storage.Items.Store(item, id, containerId));

            // Assert.
            Assert.Throws<StorageException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ItemStorage_Store_1000_SimpleTestItem()
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
            Assert.True(delta < TimeSpan.FromSeconds(25));
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ItemStorage_Store_Retrieve_SimpleTestItem()
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
        public async Task ItemStorage_Store_Retrieve_1000_SimpleTestItems()
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
