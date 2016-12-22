namespace EtAlii.Servus.Storage.Ntfs.Tests.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage.Ntfs.Tests;
    using EtAlii.Servus.Storage;
    using EtAlii.Servus.Storage.Tests;
    using EtAlii.Servus.Tests;
    using Xunit;
    using System;
    using TestAssembly = EtAlii.Servus.Storage.Tests.TestAssembly;

    
    public class ContentDefinitionPart_Tests : NtfsStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinitionPart_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinitionPart = TestContent.CreatePart();

            // Act.
            Storage.Blobs.Store(containerId, contentDefinitionPart);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
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

        [Fact, Trait("Category", TestAssembly.Category)]
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

        [Fact, Trait("Category", TestAssembly.Category)]
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

        [Fact, Trait("Category", TestAssembly.Category)]
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

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinitionPart_Retrieve_None_Existing()
        {
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            var contentDefinitionPart = Storage.Blobs.Retrieve<ContentDefinitionPart>(containerId, 1000);
            Assert.Null(contentDefinitionPart);
        }
    }
}
