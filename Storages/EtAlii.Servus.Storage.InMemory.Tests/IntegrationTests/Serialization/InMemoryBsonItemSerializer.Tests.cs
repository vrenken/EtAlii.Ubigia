namespace EtAlii.Servus.Storage.InMemory.Tests.IntegrationTests
{
    using System;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Storage.Tests;
    using Xunit;

    
    public class InMemoryBsonItemSerializer_Tests : InMemoryStorageTestBase
    {
        [Fact]
        public void InMemoryBsonItemSerializer_Create()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new InternalBsonItemSerializer(serializer);
            var propertiesSerializer = new InternalBsonPropertiesSerializer(serializer);
            var inMemoryItems = new InMemoryItems();
            var inMemoryItemsHelpers = new InMemoryItemsHelper(inMemoryItems);

            // Act.
            var storageSerializer = new InMemoryStorageSerializer(itemSerializer, propertiesSerializer, inMemoryItemsHelpers);

            // Assert.
        }

        [Fact]
        public void InMemoryBsonItemSerializer_Serialize_Item()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new InternalBsonItemSerializer(serializer);
            var propertiesSerializer = new InternalBsonPropertiesSerializer(serializer);
            var storageSerializer = new InMemoryStorageSerializer(itemSerializer, propertiesSerializer, Storage.InMemoryItemsHelper);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testItem = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            storageSerializer.Serialize(fileName, testItem);
            if (Storage.InMemoryItems.Exists(fileName))
            {
                Storage.InMemoryItems.Delete(fileName);
            }

            // Assert.
        }

        [Fact]
        public void InMemoryBsonItemSerializer_Deserialize_Item()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new InternalBsonItemSerializer(serializer);
            var propertiesSerializer = new InternalBsonPropertiesSerializer(serializer);
            var storageSerializer = new InMemoryStorageSerializer(itemSerializer, propertiesSerializer, Storage.InMemoryItemsHelper);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testItem = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            storageSerializer.Serialize(fileName, testItem);
            var retrievedTestItem = storageSerializer.Deserialize<SimpleTestItem>(fileName);
            if (Storage.InMemoryItems.Exists(fileName))
            {
                Storage.InMemoryItems.Delete(fileName);
            }

            // Assert.
            Assert.NotNull(retrievedTestItem);
            Assert.Equal(testItem.Name, retrievedTestItem.Name);
            Assert.Equal(testItem.Value, retrievedTestItem.Value);
        }



        [Fact]
        public void InMemoryBsonItemSerializer_Serialize_Properties()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new InternalBsonItemSerializer(serializer);
            var propertiesSerializer = new InternalBsonPropertiesSerializer(serializer);
            var storageSerializer = new InMemoryStorageSerializer(itemSerializer, propertiesSerializer, Storage.InMemoryItemsHelper);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testProperties = TestProperties.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, testProperties);
            if (Storage.InMemoryItems.Exists(fileName))
            {
                Storage.InMemoryItems.Delete(fileName);
            }

            // Assert.
        }

        [Fact]
        public void InMemoryBsonItemSerializer_Deserialize_Properties()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new InternalBsonItemSerializer(serializer);
            var propertiesSerializer = new InternalBsonPropertiesSerializer(serializer);
            var storageSerializer = new InMemoryStorageSerializer(itemSerializer, propertiesSerializer, Storage.InMemoryItemsHelper);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testProperties = TestProperties.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, testProperties);
            var retrievedTestProperties = storageSerializer.Deserialize(fileName);
            if (Storage.InMemoryItems.Exists(fileName))
            {
                Storage.InMemoryItems.Delete(fileName);
            }

            // Assert.
            Assert.NotNull(retrievedTestProperties);
            Assert.Equal(testProperties["Name"], retrievedTestProperties["Name"]);
            Assert.Equal(testProperties["Value"], retrievedTestProperties["Value"]);
        }
    }
}
