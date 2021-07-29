// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence.Tests;
    using EtAlii.Ubigia.Serialization;
    using Xunit;

    public class InMemoryBsonItemSerializerTests : IDisposable
    {
        private readonly InMemoryStorageUnitTestContext _testContext;
        public InMemoryBsonItemSerializerTests()
        {
            _testContext = new InMemoryStorageUnitTestContext();
        }

        public void Dispose()
        {
            _testContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void InMemoryBsonItemSerializer_Create()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);
            var inMemoryItems = new InMemoryItems();
            var inMemoryItemsHelpers = new InMemoryItemsHelper(inMemoryItems);

            // Act.
            var storageSerializer = new InMemoryStorageSerializer(itemSerializer, propertiesSerializer, inMemoryItemsHelpers);

            // Assert.
            Assert.NotNull(storageSerializer);
        }

        [Fact]
        public void InMemoryBsonItemSerializer_Serialize_Item()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);
            var storageSerializer = new InMemoryStorageSerializer(itemSerializer, propertiesSerializer, _testContext.Storage.InMemoryItemsHelper);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
            _testContext.Storage.FolderManager.Create(folder);
            var fileName = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testItem = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            storageSerializer.Serialize(fileName, testItem);
            if (_testContext.Storage.InMemoryItems.Exists(fileName))
            {
                _testContext.Storage.InMemoryItems.Delete(fileName);
            }

            // Assert.
        }

        [Fact]
        public async Task InMemoryBsonItemSerializer_Deserialize_Item()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);
            var storageSerializer = new InMemoryStorageSerializer(itemSerializer, propertiesSerializer, _testContext.Storage.InMemoryItemsHelper);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
            _testContext.Storage.FolderManager.Create(folder);
            var fileName = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testItem = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            storageSerializer.Serialize(fileName, testItem);
            var retrievedTestItem = await storageSerializer
                .Deserialize<SimpleTestItem>(fileName)
                .ConfigureAwait(false);
            if (_testContext.Storage.InMemoryItems.Exists(fileName))
            {
                _testContext.Storage.InMemoryItems.Delete(fileName);
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
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);
            var storageSerializer = new InMemoryStorageSerializer(itemSerializer, propertiesSerializer, _testContext.Storage.InMemoryItemsHelper);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
            _testContext.Storage.FolderManager.Create(folder);
            var fileName = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testProperties = _testContext.TestPropertiesFactory.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, testProperties);
            if (_testContext.Storage.InMemoryItems.Exists(fileName))
            {
                _testContext.Storage.InMemoryItems.Delete(fileName);
            }

            // Assert.
        }

        [Fact]
        public void InMemoryBsonItemSerializer_Deserialize_Properties()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);
            var storageSerializer = new InMemoryStorageSerializer(itemSerializer, propertiesSerializer, _testContext.Storage.InMemoryItemsHelper);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
            _testContext.Storage.FolderManager.Create(folder);
            var fileName = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testProperties = _testContext.TestPropertiesFactory.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, testProperties);
            var retrievedTestProperties = storageSerializer.Deserialize(fileName);
            if (_testContext.Storage.InMemoryItems.Exists(fileName))
            {
                _testContext.Storage.InMemoryItems.Delete(fileName);
            }

            // Assert.
            Assert.NotNull(retrievedTestProperties);
            Assert.Equal(testProperties["Name"], retrievedTestProperties["Name"]);
            Assert.Equal(testProperties["Value"], retrievedTestProperties["Value"]);
        }
    }
}
