namespace EtAlii.Ubigia.Storage.InMemory.Tests.IntegrationTests
{
    using System;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    
    public class InMemoryJsonItemSerializer_Tests : InMemoryStorageTestBase
    {
        [Fact]
        public void InMemoryJsonItemSerializer_Create()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
            var inMemoryItems = new InMemoryItems();
            var inMemoryItemsHelpers = new InMemoryItemsHelper(inMemoryItems);

            // Act.
            // ReSharper disable once UnusedVariable
            var storageSerializer = new InMemoryStorageSerializer(itemSerializer, propertiesSerializer, inMemoryItemsHelpers);

            // Assert.
        }

        [Fact]
        public void InMemoryJsonItemSerializer_Serialize_Item()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
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
        public void InMemoryJsonItemSerializer_Deserialize_Item()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
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

        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void InMemoryJsonItemSerializer_Serialize_Properties()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
            var storageSerializer = new InMemoryStorageSerializer(itemSerializer, propertiesSerializer, Storage.InMemoryItemsHelper);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var properties = TestProperties.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, properties);
            if (Storage.InMemoryItems.Exists(fileName))
            {
                Storage.InMemoryItems.Delete(fileName);
            }

            // Assert.
            Assert.NotEqual(true, properties.Stored);
        }

        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void InMemoryJsonItemSerializer_Deserialize_Properties()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
            var storageSerializer = new InMemoryStorageSerializer(itemSerializer, propertiesSerializer, Storage.InMemoryItemsHelper);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var properties = TestProperties.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, properties);
            var retrievedProperties = storageSerializer.Deserialize(fileName);
            if (Storage.InMemoryItems.Exists(fileName))
            {
                Storage.InMemoryItems.Delete(fileName);
            }

            // Assert.
            AssertData.AreEqual(properties, retrievedProperties);
            Assert.NotEqual(true, retrievedProperties.Stored);
            Assert.NotEqual(true, properties.Stored);
        }
    }
}
