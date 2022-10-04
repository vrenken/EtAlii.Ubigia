// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Serialization;
    using EtAlii.Ubigia.Tests;
    using Xunit;
#if HAS_PHYSICAL_FILESYSTEM
    using System.IO;
#endif

    [CorrelateUnitTests]
    public class BsonItemSerializerTests : IAsyncLifetime
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

        [Fact]
        public void BsonItemSerializer_Create()
        {
            // Arrange.
            var itemSerializer = new BsonItemSerializer();
            var propertiesSerializer = new BsonPropertiesSerializer();

            // Act.
            var storageSerializer = _testContext.CreateSerializer(itemSerializer, propertiesSerializer);

            // Assert.
            Assert.NotNull(storageSerializer);
        }

        [Fact]
        public void BsonItemSerializer_Serialize_Item()
        {
            // Arrange.
            var itemSerializer = new BsonItemSerializer();
            var propertiesSerializer = new BsonPropertiesSerializer();
            var storageSerializer = _testContext.CreateSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
            _testContext.Storage.FolderManager.Create(folder);
            var fileName = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testItem = StorageTestHelper.CreateSimpleTestItem();

            // Act.
            storageSerializer.Serialize(fileName, testItem);

            // Assert.
#if HAS_PHYSICAL_FILESYSTEM
            Assert.True(File.Exists(fileName));
#else
            Assert.True(true);
#endif

            // Assure.
            _testContext.DeleteFileWhenNeeded(fileName);
        }

        [Fact]
        public async Task BsonItemSerializer_Deserialize_Item()
        {
            // Arrange.
            var itemSerializer = new BsonItemSerializer();
            var propertiesSerializer = new BsonPropertiesSerializer();
            var storageSerializer = _testContext.CreateSerializer(itemSerializer, propertiesSerializer);
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

            // Assert.
            Assert.NotNull(retrievedTestItem);
            Assert.Equal(testItem.Name, retrievedTestItem.Name);
            Assert.Equal(testItem.Value, retrievedTestItem.Value);

            // Assure.
            _testContext.DeleteFileWhenNeeded(fileName);
        }

        [Fact]
        public void BsonItemSerializer_Serialize_Properties()
        {
            // Arrange.
            var itemSerializer = new BsonItemSerializer();
            var propertiesSerializer = new BsonPropertiesSerializer();
            var storageSerializer = _testContext.CreateSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
            _testContext.Storage.FolderManager.Create(folder);
            var fileName = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testProperties = _testContext.Properties.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, testProperties);

            // Assert.
#if HAS_PHYSICAL_FILESYSTEM
            Assert.True(File.Exists(fileName));
#else
            Assert.True(true);
#endif

            // Assure.
            _testContext.DeleteFileWhenNeeded(fileName);
        }

        [Fact]
        public void BsonItemSerializer_Deserialize_Properties()
        {
            // Arrange.
            var itemSerializer = new BsonItemSerializer();
            var propertiesSerializer = new BsonPropertiesSerializer();
            var storageSerializer = _testContext.CreateSerializer(itemSerializer, propertiesSerializer);
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var folder = _testContext.Storage.PathBuilder.GetFolder(containerId);
            _testContext.Storage.FolderManager.Create(folder);
            var fileName = _testContext.Storage.PathBuilder.GetFileName(Guid.NewGuid().ToString(), containerId);
            var testProperties = _testContext.Properties.CreateSimple();

            // Act.
            storageSerializer.Serialize(fileName, testProperties);
            var retrievedTestProperties = storageSerializer.Deserialize(fileName);

            // Assert.
            Assert.NotNull(retrievedTestProperties);
            Assert.Equal(testProperties["Name"], retrievedTestProperties["Name"]);
            Assert.Equal(testProperties["Value"], retrievedTestProperties["Value"]);

            // Assure.
            _testContext.DeleteFileWhenNeeded(fileName);
        }
    }
}
