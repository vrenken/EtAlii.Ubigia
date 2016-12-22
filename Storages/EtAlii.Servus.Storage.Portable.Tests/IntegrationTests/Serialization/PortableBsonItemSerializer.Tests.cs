namespace EtAlii.Servus.Storage.Portable.Tests.IntegrationTests
{
    using System;
    using System.IO;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Storage.Tests;
    using Xunit;
    using TestAssembly = EtAlii.Servus.Storage.Portable.Tests.TestAssembly;

    
    public class PortableBsonItemSerializer_Tests : PortableStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableBsonItemSerializer_Create()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new InternalBsonItemSerializer(serializer);
            var propertiesSerializer = new InternalBsonPropertiesSerializer(serializer);

            // Act.
            var storageSerializer = new PortableStorageSerializer(itemSerializer, propertiesSerializer, StorageFolder);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableBsonItemSerializer_Serialize_Item()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new InternalBsonItemSerializer(serializer);
            var propertiesSerializer = new InternalBsonPropertiesSerializer(serializer);
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
        public void PortableBsonItemSerializer_Deserialize_Item()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new InternalBsonItemSerializer(serializer);
            var propertiesSerializer = new InternalBsonPropertiesSerializer(serializer);
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

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableBsonItemSerializer_Serialize_Properties()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new InternalBsonItemSerializer(serializer);
            var propertiesSerializer = new InternalBsonPropertiesSerializer(serializer);
            var storageSerializer = new PortableStorageSerializer(itemSerializer, propertiesSerializer, StorageFolder);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testProperties = TestProperties.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, testProperties);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void PortableBsonItemSerializer_Deserialize_Properties()
        {
            // Arrange.
            var serializer = new Serializer();
            var itemSerializer = new InternalBsonItemSerializer(serializer);
            var propertiesSerializer = new InternalBsonPropertiesSerializer(serializer);
            var storageSerializer = new PortableStorageSerializer(itemSerializer, propertiesSerializer, StorageFolder);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testProperties = TestProperties.CreateSimple();

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
