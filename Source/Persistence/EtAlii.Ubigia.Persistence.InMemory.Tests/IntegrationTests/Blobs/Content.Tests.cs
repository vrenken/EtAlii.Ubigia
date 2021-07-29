// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence.Tests;
    using Xunit;

    public sealed class ContentTests : IDisposable
    {
        private readonly InMemoryStorageUnitTestContext _testContext;
        public ContentTests()
        {
            _testContext = new InMemoryStorageUnitTestContext();
        }

        public void Dispose() => _testContext?.Dispose();

        [Fact]
        public void Content_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var content = _testContext.TestContentFactory.Create();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, content);

            // Assert.
        }

        [Fact]
        public async Task Content_Store_And_Retrieve_Check_Parts()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var content = _testContext.TestContentFactory.Create();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, content);
            var retrievedContent = await _testContext.Storage.Blobs.Retrieve<Content>(containerId).ConfigureAwait(false);

            // Assert.
            AssertData.AreEqual(content, retrievedContent, false);
        }

        [Fact]
        public void Content_Store_Twice()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var first = _testContext.TestContentFactory.Create();
            var second = _testContext.TestContentFactory.Create();
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
        public async Task Content_Retrieve_None_Existing()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var content = await _testContext.Storage.Blobs.Retrieve<Content>(containerId).ConfigureAwait(false);

            // Assert.
            Assert.Null(content);
        }
    }
}
