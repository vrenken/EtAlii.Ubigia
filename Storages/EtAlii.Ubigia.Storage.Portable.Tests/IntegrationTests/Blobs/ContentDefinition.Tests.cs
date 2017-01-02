namespace EtAlii.Ubigia.Storage.Portable.Tests.IntegrationTests
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Storage.Portable.Tests;
    using EtAlii.Ubigia.Storage;
    using EtAlii.Ubigia.Storage.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using System;
    using System.Linq;
    using TestAssembly = EtAlii.Ubigia.Storage.Tests.TestAssembly;

    
    public class ContentDefinition_Tests : PortableStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinition = TestContentDefinition.Create();

            // Act.
            Storage.Blobs.Store(containerId, contentDefinition);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
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

        [Fact, Trait("Category", TestAssembly.Category)]
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

        [Fact, Trait("Category", TestAssembly.Category)]
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

        [Fact, Trait("Category", TestAssembly.Category)]
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

        [Fact, Trait("Category", TestAssembly.Category)]
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
