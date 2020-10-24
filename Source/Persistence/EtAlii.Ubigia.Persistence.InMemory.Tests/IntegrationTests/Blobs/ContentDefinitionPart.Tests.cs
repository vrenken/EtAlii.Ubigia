﻿namespace EtAlii.Ubigia.Persistence.InMemory.Tests
{
    using System;
    using EtAlii.Ubigia.Persistence.Tests;
    using Xunit;

    public class ContentDefinitionPartTests : InMemoryStorageTestBase
    {
        [Fact]
        public void ContentDefinitionPart_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinitionPart = TestContentFactory.CreatePart();

            // Act.
            Storage.Blobs.Store(containerId, contentDefinitionPart);

            // Assert.
        }

        [Fact]
        public void ContentDefinitionPart_Store_And_Retrieve_Check_Id()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinitionPart = TestContentDefinitionFactory.CreatePart();

            // Act.
            Storage.Blobs.Store(containerId, contentDefinitionPart);
            var retrievedContentDefinitionPart = Storage.Blobs.Retrieve<ContentDefinitionPart>(containerId, contentDefinitionPart.Id);

            // Assert.
            Assert.Equal(contentDefinitionPart.Id, retrievedContentDefinitionPart.Id);
        }

        [Fact]
        public void ContentDefinitionPart_Store_And_Retrieve_Check_Size()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinitionPart = TestContentDefinitionFactory.CreatePart();

            // Act.
            Storage.Blobs.Store(containerId, contentDefinitionPart);
            var retrievedContentDefinitionPart = Storage.Blobs.Retrieve<ContentDefinitionPart>(containerId, contentDefinitionPart.Id);

            // Assert.
            Assert.Equal(contentDefinitionPart.Size, retrievedContentDefinitionPart.Size);
        }

        [Fact]
        public void ContentDefinitionPart_Store_And_Retrieve_Check_Checksum()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinitionPart = TestContentDefinitionFactory.CreatePart();

            // Act.
            Storage.Blobs.Store(containerId, contentDefinitionPart);
            var retrievedContentDefinitionPart = Storage.Blobs.Retrieve<ContentDefinitionPart>(containerId, contentDefinitionPart.Id);

            // Assert.
            Assert.Equal(contentDefinitionPart.Checksum, retrievedContentDefinitionPart.Checksum);
        }

        [Fact]
        public void ContentDefinitionPart_Store_Twice()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var first = TestContentDefinitionFactory.CreatePart();
            var second = TestContentDefinitionFactory.CreatePart();
            Storage.Blobs.Store(containerId, first);

            // Act.
            Storage.Blobs.Store(containerId, second);

            // Assert.
        }

        [Fact]
        public void ContentDefinitionPart_Store_Same()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var first = TestContentDefinitionFactory.CreatePart();
            var second = TestContentDefinitionFactory.CreatePart(first.Id);
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
        public void ContentDefinitionPart_Retrieve_None_Existing()
        {
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            var contentDefinitionPart = Storage.Blobs.Retrieve<ContentDefinitionPart>(containerId, 1000);
            Assert.Null(contentDefinitionPart);
        }
    }
}