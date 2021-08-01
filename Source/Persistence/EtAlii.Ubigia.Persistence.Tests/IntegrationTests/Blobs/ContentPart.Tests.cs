// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class ContentPartTests : IAsyncLifetime
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
        public void ContentPart_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentPart = _testContext.Content.CreatePart();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, contentPart);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentPart_Store_And_Retrieve_Check_Data()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentPart = _testContext.Content.CreatePart();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, contentPart);
            var retrievedContentPart = await _testContext.Storage.Blobs.Retrieve<ContentPart>(containerId, contentPart.Id).ConfigureAwait(false);

            // Assert.
            AssertData.AreEqual(contentPart.Data, retrievedContentPart.Data);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPart_Store_Twice()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var first = _testContext.Content.CreatePart();
            var second = _testContext.Content.CreatePart();
            _testContext.Storage.Blobs.Store(containerId, first);

            // Act.
            _testContext.Storage.Blobs.Store(containerId, second);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPart_Store_Same()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var first = _testContext.Content.CreatePart();
            var second = _testContext.Content.CreatePart(first.Data, first.Id);
            _testContext.Storage.Blobs.Store(containerId, first);

            // Act.
            var act = new Action(() =>
            {
                _testContext.Storage.Blobs.Store(containerId, second);
            });

            // Assert.
            Assert.Throws<BlobStorageException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentPart_Retrieve_None_Existing()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var contentPart = await _testContext.Storage.Blobs.Retrieve<ContentPart>(containerId, 1000).ConfigureAwait(false);

            // Assert.
            Assert.Null(contentPart);
        }
    }
}
