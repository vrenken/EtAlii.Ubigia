﻿namespace EtAlii.Ubigia.Persistence.InMemory.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence.Tests;
    using Xunit;

    public class ContentPartTests : InMemoryStorageTestBase
    {
        [Fact]
        public void ContentPart_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentPart = TestContentFactory.CreatePart();

            // Act.
            Storage.Blobs.Store(containerId, contentPart);

            // Assert.
        }

        [Fact]
        public async Task ContentPart_Store_And_Retrieve_Check_Data()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentPart = TestContentFactory.CreatePart();

            // Act.
            Storage.Blobs.Store(containerId, contentPart);
            var retrievedContentPart = await Storage.Blobs.Retrieve<ContentPart>(containerId, contentPart.Id);

            // Assert.
            AssertData.AreEqual(contentPart.Data, retrievedContentPart.Data);
        }

        [Fact]
        public void ContentPart_Store_Twice()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var first = TestContentFactory.CreatePart();
            var second = TestContentFactory.CreatePart();
            Storage.Blobs.Store(containerId, first);

            // Act.
            Storage.Blobs.Store(containerId, second);

            // Assert.
        }

        [Fact]
        public void ContentPart_Store_Same()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var first = TestContentFactory.CreatePart();
            var second = TestContentFactory.CreatePart(first.Data, first.Id);
            Storage.Blobs.Store(containerId, first);

            // Act.
            var act = new Action(() =>
            {
                Storage.Blobs.Store(containerId, second);
            });

            // Assert.
            Assert.Throws<BlobStorageException>(act);
        }

        [Fact]
        public async Task ContentPart_Retrieve_None_Existing()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var contentPart = await Storage.Blobs.Retrieve<ContentPart>(containerId, 1000);

            // Assert.
            Assert.Null(contentPart);
        }
    }
}
