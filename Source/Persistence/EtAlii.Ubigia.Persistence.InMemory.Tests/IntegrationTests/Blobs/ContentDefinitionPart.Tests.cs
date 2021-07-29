// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence.Tests;
    using Xunit;

    public class ContentDefinitionPartTests : IDisposable
    {
        private readonly InMemoryStorageUnitTestContext _testContext;
        public ContentDefinitionPartTests()
        {
            _testContext = new InMemoryStorageUnitTestContext();
        }

        public void Dispose()
        {
            _testContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void ContentDefinitionPart_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinitionPart = _testContext.TestContentFactory.CreatePart();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, contentDefinitionPart);

            // Assert.
        }

        [Fact]
        public async Task ContentDefinitionPart_Store_And_Retrieve_Check_Id()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart();

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
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart();

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
            var contentDefinitionPart = _testContext.TestContentDefinitionFactory.CreatePart();

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
            var first = _testContext.TestContentDefinitionFactory.CreatePart();
            var second = _testContext.TestContentDefinitionFactory.CreatePart();
            _testContext.Storage.Blobs.Store(containerId, first);

            // Act.
            _testContext.Storage.Blobs.Store(containerId, second);

            // Assert.
        }

        [Fact]
        public void ContentDefinitionPart_Store_Same()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var first = _testContext.TestContentDefinitionFactory.CreatePart();
            var second = _testContext.TestContentDefinitionFactory.CreatePart(first.Id);
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

            var contentDefinitionPart = await _testContext.Storage.Blobs.Retrieve<ContentDefinitionPart>(containerId, 1000).ConfigureAwait(false);
            Assert.Null(contentDefinitionPart);
        }
    }
}
