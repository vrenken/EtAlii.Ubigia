﻿namespace EtAlii.Ubigia.Persistence.NetCoreApp.Tests
{
    using System;
    using System.IO;
    using EtAlii.Ubigia.Persistence.Tests;
    using Xunit;

    public class NetCoreAppJsonItemSerializerTests : NetCoreAppStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NetCoreAppJsonItemSerializer_Create()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);

            // Act.
            var storageSerializer = new NetCoreAppStorageSerializer(itemSerializer, propertiesSerializer);

            // Assert.
            Assert.NotNull(storageSerializer);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NetCoreAppJsonItemSerializer_Serialize_Item()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
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
        public void NetCoreAppJsonItemSerializer_Deserialize_Item()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
            var storageSerializer = new NetCoreAppStorageSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testItem = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            storageSerializer.Serialize(fileName, testItem);
            var retrievedTestItem = storageSerializer.Deserialize<SimpleTestItem>(fileName);
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
        public void NetCoreAppJsonItemSerializer_Serialize_Properties()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
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

        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void NetCoreAppJsonItemSerializer_Deserialize_Properties()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
            var storageSerializer = new NetCoreAppStorageSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var properties = TestPropertiesFactory.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, properties);
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
