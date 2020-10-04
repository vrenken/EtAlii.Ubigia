namespace EtAlii.Ubigia.Persistence.InMemory.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Persistence.Tests;
    using Xunit;

    public class ContentDefinitionTests : InMemoryStorageTestBase
    {
        [Fact]
        public void ContentDefinition_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinition = TestContentDefinitionFactory.Create();

            // Act.
            Storage.Blobs.Store(containerId, contentDefinition);

            // Assert.
        }

        [Fact]
        public void ContentDefinition_Store_And_Retrieve_Check_Size()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinition = TestContentDefinitionFactory.Create();

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
            var contentDefinition = TestContentDefinitionFactory.Create();

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
            var contentDefinition = TestContentDefinitionFactory.Create();

            // Act.
            Storage.Blobs.Store(containerId, contentDefinition);
            var retrievedContentDefinition = Storage.Blobs.Retrieve<ContentDefinition>(containerId);

            // Assert.
            Assert.Equal(contentDefinition.Parts.Count, retrievedContentDefinition.Parts.Count);
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
            var first = TestContentDefinitionFactory.Create();
            var second = TestContentDefinitionFactory.Create();
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
