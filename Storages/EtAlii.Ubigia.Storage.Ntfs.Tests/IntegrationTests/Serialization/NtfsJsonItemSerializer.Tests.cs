namespace EtAlii.Ubigia.Storage.Ntfs.Tests.IntegrationTests
{
    using System;
    using System.IO;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Storage.Tests;
    using Xunit;

    
    public class NtfsJsonItemSerializer_Tests : NtfsStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsJsonItemSerializer_Create()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);

            // Act.
            // ReSharper disable once UnusedVariable
            var storageSerializer = new NtfsStorageSerializer(itemSerializer, propertiesSerializer); 

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void NtfsJsonItemSerializer_Serialize_Item()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
            var storageSerializer = new NtfsStorageSerializer(itemSerializer, propertiesSerializer);
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
        public void NtfsJsonItemSerializer_Deserialize_Item()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
            var storageSerializer = new NtfsStorageSerializer(itemSerializer, propertiesSerializer);
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
        public void NtfsJsonItemSerializer_Serialize_Properties()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
            var storageSerializer = new NtfsStorageSerializer(itemSerializer, propertiesSerializer);
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

        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void NtfsJsonItemSerializer_Deserialize_Properties()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new InternalJsonItemSerializer(serializer);
            var propertiesSerializer = new InternalJsonPropertiesSerializer(serializer);
            var storageSerializer = new NtfsStorageSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = Storage.PathBuilder.GetFolder(containerId);
            Storage.FolderManager.Create(folder);
            var fileName = Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var properties = TestProperties.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, properties);
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
