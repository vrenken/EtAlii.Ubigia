// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Serialization;
    using Xunit;

    public class BsonItemSerializerTests : StorageUnitTestContext
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void BsonItemSerializer_Create()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);

            // Act.
            var storageSerializer = CreateSerializer(itemSerializer, propertiesSerializer);

            // Assert.
            Assert.NotNull(storageSerializer);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BsonItemSerializer_Serialize_Item()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);
            var storageSerializer = CreateSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testItem = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            storageSerializer.Serialize(fileName, testItem);

            // Assert.

            // Assure.
            DeleteFileWhenNeeded(fileName);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task BsonItemSerializer_Deserialize_Item()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);
            var storageSerializer = CreateSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testItem = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            storageSerializer.Serialize(fileName, testItem);
            var retrievedTestItem = await storageSerializer
                .Deserialize<SimpleTestItem>(fileName)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(retrievedTestItem);
            Assert.Equal(testItem.Name, retrievedTestItem.Name);
            Assert.Equal(testItem.Value, retrievedTestItem.Value);

            // Assure.
            DeleteFileWhenNeeded(fileName);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BsonItemSerializer_Serialize_Properties()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);
            var storageSerializer = CreateSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testProperties = TestPropertiesFactory.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, testProperties);

            // Assert.

            // Assure.
            DeleteFileWhenNeeded(fileName);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BsonItemSerializer_Deserialize_Properties()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new BsonItemSerializer(serializer);
            var propertiesSerializer = new BsonPropertiesSerializer(serializer);
            var storageSerializer = CreateSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testProperties = TestPropertiesFactory.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, testProperties);
            var retrievedTestProperties = storageSerializer.Deserialize(fileName);

            // Assert.
            Assert.NotNull(retrievedTestProperties);
            Assert.Equal(testProperties["Name"], retrievedTestProperties["Name"]);
            Assert.Equal(testProperties["Value"], retrievedTestProperties["Value"]);

            // Assure.
            DeleteFileWhenNeeded(fileName);
        }
    }
}
