namespace EtAlii.Ubigia.Storage.Ntfs.Tests.IntegrationTests
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Storage.Ntfs.Tests;
    using EtAlii.Ubigia.Storage;
    using EtAlii.Ubigia.Storage.Tests;
    using EtAlii.Ubigia.Tests;
    using Xunit;
    using System;
    using TestAssembly = EtAlii.Ubigia.Storage.Tests.TestAssembly;

    
    public class ContentPart_Tests : NtfsStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPart_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentPart = TestContent.CreatePart();

            // Act.
            Storage.Blobs.Store(containerId, contentPart);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPart_Store_And_Retrieve_Check_Data()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentPart = TestContent.CreatePart();

            // Act.
            Storage.Blobs.Store(containerId, contentPart);
            var retrievedContentPart = Storage.Blobs.Retrieve<ContentPart>(containerId, contentPart.Id);

            // Assert.
            AssertData.AreEqual(contentPart.Data, retrievedContentPart.Data);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPart_Store_Twice()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var first = TestContent.CreatePart();
            var second = TestContent.CreatePart();
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
        public void ContentPart_Retrieve_None_Existing()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var contentPart = Storage.Blobs.Retrieve<ContentPart>(containerId, 1000);

            // Assert.
            Assert.Null(contentPart);
        }
    }
}
