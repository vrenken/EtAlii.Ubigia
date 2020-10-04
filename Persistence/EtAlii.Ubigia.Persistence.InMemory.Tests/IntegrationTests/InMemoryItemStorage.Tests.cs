﻿namespace EtAlii.Ubigia.Persistence.InMemory.Tests
{
    using System;
    using EtAlii.Ubigia.Persistence.Tests;
    using Xunit;

    public class InMemoryItemStorageTests : InMemoryStorageTestBase
    {
        [Fact]
        public void InMemoryItemStorage_Store_SimpleTestItem()
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
        public void InMemoryItemStorage_Store_SimpleTestItem_Has()
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
        public void InMemoryItemStorage_Store_SimpleTestItem_Get()
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
        public void InMemoryItemStorage_Store_SimpleTestItem_Remove()
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
        public void InMemoryItemStorage_Store_Null()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var id = Guid.NewGuid();
            object item = null;

            // Act.
            var act = new Action(() => Storage.Items.Store(item, id, containerId));

            // Assert.
            Assert.Throws<StorageException>(act);

            // Assert.
        }

        [Fact(Skip = "This test should succeed")]
        public void InMemoryItemStorage_Store_SimpleTestItem_Invalid_ContainerIdentifier()
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
        public void InMemoryItemStorage_Store_SimpleTestItem_Invalid_ContainerId()
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

        [Fact]
        public void InMemoryItemStorage_Store_1000_SimpleTestItem()
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
            Assert.True(delta < TimeSpan.FromSeconds(20));
        }

        [Fact]
        public void InMemoryItemStorage_Store_Retrieve_SimpleTestItem()
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

        [Fact]
        public void InMemoryItemStorage_Store_Retrieve_1000_SimpleTestItems()
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
