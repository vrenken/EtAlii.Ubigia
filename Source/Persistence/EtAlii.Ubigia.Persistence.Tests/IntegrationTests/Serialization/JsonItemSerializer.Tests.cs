// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Serialization;
    using Xunit;

    public class JsonItemSerializerTests : StorageUnitTestContext
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void JsonItemSerializer_Create()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new JsonItemSerializer(serializer);
            var propertiesSerializer = new JsonPropertiesSerializer(serializer);

            // Act.
            var storageSerializer = CreateSerializer(itemSerializer, propertiesSerializer);

            // Assert.
            Assert.NotNull(storageSerializer);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void JsonItemSerializer_Serialize_Item()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new JsonItemSerializer(serializer);
            var propertiesSerializer = new JsonPropertiesSerializer(serializer);
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
        public async Task JsonItemSerializer_Deserialize_Item()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new JsonItemSerializer(serializer);
            var propertiesSerializer = new JsonPropertiesSerializer(serializer);
            var storageSerializer = CreateSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testItem = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            storageSerializer.Serialize(fileName, testItem);
            var retrievedTestItem = await storageSerializer.Deserialize<SimpleTestItem>(fileName).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(retrievedTestItem);
            Assert.Equal(testItem.Name, retrievedTestItem.Name);
            Assert.Equal(testItem.Value, retrievedTestItem.Value);

            // Assure.
            DeleteFileWhenNeeded(fileName);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void JsonItemSerializer_Serialize_Properties()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new JsonItemSerializer(serializer);
            var propertiesSerializer = new JsonPropertiesSerializer(serializer);
            var storageSerializer = CreateSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var properties = TestPropertiesFactory.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, properties);

            // Assert.

            // Assure.
            DeleteFileWhenNeeded(fileName);
        }

        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void JsonItemSerializer_Deserialize_Properties()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new JsonItemSerializer(serializer);
            var propertiesSerializer = new JsonPropertiesSerializer(serializer);
            var storageSerializer = CreateSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var properties = TestPropertiesFactory.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, properties);
            var retrievedProperties = storageSerializer.Deserialize(fileName);

            // Assert.
            AssertData.AreEqual(properties, retrievedProperties);
            Assert.False(retrievedProperties.Stored);
            Assert.False(properties.Stored);

            // Assure.
            DeleteFileWhenNeeded(fileName);
        }
    }
}
