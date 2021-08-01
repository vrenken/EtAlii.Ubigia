// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Serialization;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class JsonItemSerializerTests : IAsyncLifetime
    {
        private StorageUnitTestContext _testContext;

        public async Task InitializeAsync()
        {
            _testContext = new StorageUnitTestContext();
            await _testContext
                .InitializeAsync()
                .ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await _testContext
                .DisposeAsync()
                .ConfigureAwait(false);
            _testContext = null;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void JsonItemSerializer_Create()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new JsonItemSerializer(serializer);
            var propertiesSerializer = new JsonPropertiesSerializer(serializer);

            // Act.
            var storageSerializer = _testContext.CreateSerializer(itemSerializer, propertiesSerializer);

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
            var storageSerializer = _testContext.CreateSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
            _testContext.Storage.FolderManager.Create(folder);
            var fileName = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testItem = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            storageSerializer.Serialize(fileName, testItem);

            // Assert.

            // Assure.
            _testContext.DeleteFileWhenNeeded(fileName);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task JsonItemSerializer_Deserialize_Item()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new JsonItemSerializer(serializer);
            var propertiesSerializer = new JsonPropertiesSerializer(serializer);
            var storageSerializer = _testContext.CreateSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
            _testContext.Storage.FolderManager.Create(folder);
            var fileName = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testItem = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            storageSerializer.Serialize(fileName, testItem);
            var retrievedTestItem = await storageSerializer.Deserialize<SimpleTestItem>(fileName).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(retrievedTestItem);
            Assert.Equal(testItem.Name, retrievedTestItem.Name);
            Assert.Equal(testItem.Value, retrievedTestItem.Value);

            // Assure.
            _testContext.DeleteFileWhenNeeded(fileName);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void JsonItemSerializer_Serialize_Properties()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new JsonItemSerializer(serializer);
            var propertiesSerializer = new JsonPropertiesSerializer(serializer);
            var storageSerializer = _testContext.CreateSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
            _testContext.Storage.FolderManager.Create(folder);
            var fileName = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var properties = _testContext.Properties.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, properties);

            // Assert.

            // Assure.
            _testContext.DeleteFileWhenNeeded(fileName);
        }

        [Fact, Trait("Category", TestAssembly.Category)]//, ExpectedException(typeof(AssertFailedException), "JSON support should be disabled")]
        public void JsonItemSerializer_Deserialize_Properties()
        {
            // Arrange.
            var serializer = new SerializerFactory().Create();
            var itemSerializer = new JsonItemSerializer(serializer);
            var propertiesSerializer = new JsonPropertiesSerializer(serializer);
            var storageSerializer = _testContext.CreateSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
            _testContext.Storage.FolderManager.Create(folder);
            var fileName = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var properties = _testContext.Properties.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, properties);
            var retrievedProperties = storageSerializer.Deserialize(fileName);

            // Assert.
            AssertData.AreEqual(properties, retrievedProperties);
            Assert.False(retrievedProperties.Stored);
            Assert.False(properties.Stored);

            // Assure.
            _testContext.DeleteFileWhenNeeded(fileName);
        }
    }
}
