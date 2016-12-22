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

    
    public class Content_Tests : NtfsStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void Content_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var content = TestContent.Create();

            // Act.
            Storage.Blobs.Store(containerId, content);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Content_Store_And_Retrieve_Check_Parts()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var content = TestContent.Create();

            // Act.
            Storage.Blobs.Store(containerId, content);
            var retrievedContent = Storage.Blobs.Retrieve<Content>(containerId);

            // Assert.
            AssertData.AreEqual(content, retrievedContent, false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Content_Store_Twice()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var first = TestContent.Create();
            var second = TestContent.Create();
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
        public void Content_Retrieve_None_Existing()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var content = Storage.Blobs.Retrieve<Content>(containerId);

            // Assert.
            Assert.Null(content);
        }
    }
}
