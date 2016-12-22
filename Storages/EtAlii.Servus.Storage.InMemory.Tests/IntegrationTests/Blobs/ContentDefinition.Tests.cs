namespace EtAlii.Servus.Storage.InMemory.Tests.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Storage;
    using EtAlii.Servus.Storage.Tests;
    using EtAlii.Servus.Tests;
    using Xunit;
    using System;
    using System.Linq;

    
    public class ContentDefinition_Tests : InMemoryStorageTestBase
    {
        [Fact]
        public void ContentDefinition_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinition = TestContentDefinition.Create();

            // Act.
            Storage.Blobs.Store(containerId, contentDefinition);

            // Assert.
        }

        [Fact]
        public void ContentDefinition_Store_And_Retrieve_Check_Size()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinition = TestContentDefinition.Create();

            // Act.
            Storage.Blobs.Store(containerId, contentDefinition);
            var retrievedContentDefinition = Storage.Blobs.Retrieve<ContentDefinition>(containerId);

            // Assert.
            Assert.Equal(contentDefinition.Size, retrievedContentDefinition.Size);
        }

        [Fact]
        public void ContentDefinition_Store_And_Retrieve_Check_Checksum()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinition = TestContentDefinition.Create();

            // Act.
            Storage.Blobs.Store(containerId, contentDefinition);
            var retrievedContentDefinition = Storage.Blobs.Retrieve<ContentDefinition>(containerId);

            // Assert.
            Assert.Equal(contentDefinition.Checksum, retrievedContentDefinition.Checksum);
        }
         
        [Fact]
        public void ContentDefinition_Store_And_Retrieve_Check_Parts()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinition = TestContentDefinition.Create();

            // Act.
            Storage.Blobs.Store(containerId, contentDefinition);
            var retrievedContentDefinition = Storage.Blobs.Retrieve<ContentDefinition>(containerId);

            // Assert.
            Assert.Equal(contentDefinition.Parts.Count, retrievedContentDefinition.Parts.Count());
            for (int i = 0; i < contentDefinition.Parts.Count; i++)
            {
                Assert.Equal(contentDefinition.Parts[i].Checksum, retrievedContentDefinition.Parts.ElementAt(i).Checksum);
                Assert.Equal(contentDefinition.Parts[i].Size, retrievedContentDefinition.Parts.ElementAt(i).Size);
            }
        }

        [Fact]
        public void ContentDefinition_Store_Twice()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var first = TestContentDefinition.Create();
            var second = TestContentDefinition.Create();
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
        public void ContentDefinition_Retrieve_None_Existing()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var contentDefinition = Storage.Blobs.Retrieve<ContentDefinition>(containerId);

            // Assert.
            Assert.Null(contentDefinition);
        }
    }
}
