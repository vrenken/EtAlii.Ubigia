namespace EtAlii.Ubigia.Storage.InMemory.Tests.IntegrationTests
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Storage;
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using System;

    
    public class ContentDefinitionPart_Tests : InMemoryStorageTestBase
    {
        [Fact]
        public void ContentDefinitionPart_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinitionPart = TestContent.CreatePart();

            // Act.
            Storage.Blobs.Store(containerId, contentDefinitionPart);

            // Assert.
        }

        [Fact]
        public void ContentDefinitionPart_Store_And_Retrieve_Check_Id()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinitionPart = TestContentDefinition.CreatePart();

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
            var contentDefinitionPart = TestContentDefinition.CreatePart();

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
            var contentDefinitionPart = TestContentDefinition.CreatePart();

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
            var first = TestContentDefinition.CreatePart();
            var second = TestContentDefinition.CreatePart();
            Storage.Blobs.Store(containerId, first);

            // Act.
            var act = new Action(() =>
            {
                Storage.Blobs.Store(containerId, second);
            });

            // Assert.
            ExceptionAssert.Throws<BlobStorageException>(act);
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
