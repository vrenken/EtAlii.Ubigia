namespace EtAlii.Servus.Storage.Portable.Tests.IntegrationTests
{
    using System;
    using System.IO;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Storage.Tests;
    using Xunit;
    using TestAssembly = EtAlii.Servus.Storage.Portable.Tests.TestAssembly;

    
    public class PortableJsonItemSerializer_Tests : PortableStorageTestBase
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
        public void PortableJsonItemSerializer_Deserialize_Item()
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
            var properties = TestProperties.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, properties);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Assert.
            Assert.NotEqual(true, properties.Stored);
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
            var properties = TestProperties.CreateSimple();
            storageSerializer.Serialize(fileName, properties);

            // Act.
            var retrievedProperties = storageSerializer.Deserialize(fileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Assert.
            AssertData.AreEqual(properties, retrievedProperties);
            Assert.NotEqual(true, retrievedProperties.Stored);
            Assert.NotEqual(true, properties.Stored);
        }
    }
}
