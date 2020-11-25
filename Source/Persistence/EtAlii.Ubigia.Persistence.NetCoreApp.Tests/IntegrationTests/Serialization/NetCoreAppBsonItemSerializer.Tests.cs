﻿namespace EtAlii.Ubigia.Persistence.NetCoreApp.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence.Tests;
    using EtAlii.Ubigia.Serialization;
    using Xunit;

    public class NetCoreAppBsonItemSerializerTests : NetCoreAppStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NetCoreAppBsonItemSerializer_Create()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);

            // Act.
            var storageSerializer = new NetCoreAppStorageSerializer(itemSerializer, propertiesSerializer);

            // Assert.
            Assert.NotNull(storageSerializer);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NetCoreAppBsonItemSerializer_Serialize_Item()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);
            var storageSerializer = new NetCoreAppStorageSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testItem = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            storageSerializer.Serialize(fileName, testItem);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task NetCoreAppBsonItemSerializer_Deserialize_Item()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);
            var storageSerializer = new NetCoreAppStorageSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testItem = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            storageSerializer.Serialize(fileName, testItem);
            var retrievedTestItem = await storageSerializer.Deserialize<SimpleTestItem>(fileName).ConfigureAwait(false);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Assert.
            Assert.NotNull(retrievedTestItem);
            Assert.Equal(testItem.Name, retrievedTestItem.Name);
            Assert.Equal(testItem.Value, retrievedTestItem.Value);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NetCoreAppBsonItemSerializer_Serialize_Properties()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);
            var storageSerializer = new NetCoreAppStorageSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testProperties = TestPropertiesFactory.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, testProperties);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NetCoreAppBsonItemSerializer_Deserialize_Properties()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);
            var storageSerializer = new NetCoreAppStorageSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testProperties = TestPropertiesFactory.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, testProperties);
            var retrievedTestProperties = storageSerializer.Deserialize(fileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Assert.
            Assert.NotNull(retrievedTestProperties);
            Assert.Equal(testProperties["Name"], retrievedTestProperties["Name"]);
            Assert.Equal(testProperties["Value"], retrievedTestProperties["Value"]);
        }
    }
}
