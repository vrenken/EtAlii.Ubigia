namespace EtAlii.Servus.Storage.Portable.Tests.IntegrationTests
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Storage.Portable.Tests;
    using EtAlii.Servus.Storage;
    using EtAlii.Servus.Storage.Tests;
    using EtAlii.Servus.Tests;
    using Xunit;
    using System;
    using TestAssembly = EtAlii.Servus.Storage.Tests.TestAssembly;

    
    public class ContentPart_Tests : PortableStorageTestBase
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
