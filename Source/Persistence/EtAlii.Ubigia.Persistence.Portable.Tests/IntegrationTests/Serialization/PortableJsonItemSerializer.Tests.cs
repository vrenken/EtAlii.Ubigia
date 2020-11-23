﻿namespace EtAlii.Ubigia.Persistence.Portable.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence.Tests;
    using Xunit;

    public class PortableJsonItemSerializerTests : PortableStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableJsonItemSerializer_Create()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);

            // Act.
            var storageSerializer = new PortableStorageSerializer(itemSerializer, propertiesSerializer, StorageFolder);

            // Assert.
            Assert.NotNull(storageSerializer);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableJsonItemSerializer_Serialize_Item()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
            var storageSerializer = new PortableStorageSerializer(itemSerializer, propertiesSerializer, StorageFolder);
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
        public async Task PortableJsonItemSerializer_Deserialize_Item()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
            var storageSerializer = new PortableStorageSerializer(itemSerializer, propertiesSerializer, StorageFolder);
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

        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void PortableJsonItemSerializer_Serialize_Properties()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
            var storageSerializer = new PortableStorageSerializer(itemSerializer, propertiesSerializer, StorageFolder);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var properties = TestPropertiesFactory.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, properties);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Assert.
            Assert.False(properties.Stored);
        }

        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void PortableJsonItemSerializer_Deserialize_Properties()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
            var storageSerializer = new PortableStorageSerializer(itemSerializer, propertiesSerializer, StorageFolder);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var properties = TestPropertiesFactory.CreateSimple();
            storageSerializer.Serialize(fileName, properties);

            // Act.
            var retrievedProperties = storageSerializer.Deserialize(fileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Assert.
            AssertData.AreEqual(properties, retrievedProperties);
            Assert.False(retrievedProperties.Stored);
            Assert.False(properties.Stored);
        }
    }
}
