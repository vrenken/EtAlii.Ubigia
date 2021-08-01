// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class ContentTests : IAsyncLifetime
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
        public void Content_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var content = _testContext.Content.Create();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, content);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Content_Store_And_Retrieve_Check_Parts()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var content = _testContext.Content.Create();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, content);
            var retrievedContent = await _testContext.Storage.Blobs.Retrieve<Content>(containerId).ConfigureAwait(false);

            // Assert.
            AssertData.AreEqual(content, retrievedContent, false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Content_Store_Twice()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var first = _testContext.Content.Create();
            var second = _testContext.Content.Create();
            _testContext.Storage.Blobs.Store(containerId, first);

            // Act.
            var act = new Action(() => _testContext.Storage.Blobs.Store(containerId, second));

            // Assert.
            Assert.Throws<BlobStorageException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
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
