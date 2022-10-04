// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class ContentDefinitionPartTests : IAsyncLifetime
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
        public void ContentDefinitionPart_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinitionPart = _testContext.ContentDefinitions.CreatePart();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, contentDefinitionPart);

            // Assert.
            Assert.True(contentDefinitionPart.Stored);
        }

        [Fact]
        public async Task ContentDefinitionPart_Store_And_Retrieve_Check_Id()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinitionPart = _testContext.ContentDefinitions.CreatePart();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, contentDefinitionPart);
            var retrievedContentDefinitionPart = await _testContext.Storage.Blobs.Retrieve<ContentDefinitionPart>(containerId, contentDefinitionPart.Id).ConfigureAwait(false);

            // Assert.
            Assert.Equal(contentDefinitionPart.Id, retrievedContentDefinitionPart.Id);
        }

        [Fact]
        public async Task ContentDefinitionPart_Store_And_Retrieve_Check_Size()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinitionPart = _testContext.ContentDefinitions.CreatePart();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, contentDefinitionPart);
            var retrievedContentDefinitionPart = await _testContext.Storage.Blobs.Retrieve<ContentDefinitionPart>(containerId, contentDefinitionPart.Id).ConfigureAwait(false);

            // Assert.
            Assert.Equal(contentDefinitionPart.Size, retrievedContentDefinitionPart.Size);
        }

        [Fact]
        public async Task ContentDefinitionPart_Store_And_Retrieve_Check_Checksum()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinitionPart = _testContext.ContentDefinitions.CreatePart();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, contentDefinitionPart);
            var retrievedContentDefinitionPart = await _testContext.Storage.Blobs.Retrieve<ContentDefinitionPart>(containerId, contentDefinitionPart.Id).ConfigureAwait(false);

            // Assert.
            Assert.Equal(contentDefinitionPart.Checksum, retrievedContentDefinitionPart.Checksum);
        }

        [Fact]
        public void ContentDefinitionPart_Store_Twice()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var first = _testContext.ContentDefinitions.CreatePart();
            var second = _testContext.ContentDefinitions.CreatePart();
            _testContext.Storage.Blobs.Store(containerId, first);

            // Act.
            _testContext.Storage.Blobs.Store(containerId, second);

            // Assert.
            Assert.True(second.Stored);
        }

        [Fact]
        public void ContentDefinitionPart_Store_Same()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var first = _testContext.ContentDefinitions.CreatePart();
            var second = _testContext.ContentDefinitions.CreatePart(first.Id);
            _testContext.Storage.Blobs.Store(containerId, first);

            // Act.
            var act = new Action(() =>
            {
                _testContext.Storage.Blobs.Store(containerId, second);
            });

            // Assert.
            Assert.Throws<BlobStorageException>(act);
        }

        [Fact]
        public async Task ContentDefinitionPart_Retrieve_None_Existing()
        {
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            var contentDefinitionPart = await _testContext.Storage.Blobs
                .Retrieve<ContentDefinitionPart>(containerId, 1000)
                .ConfigureAwait(false);
            Assert.Null(contentDefinitionPart);
        }
    }
}
