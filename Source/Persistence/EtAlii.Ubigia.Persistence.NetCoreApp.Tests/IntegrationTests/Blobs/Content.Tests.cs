﻿namespace EtAlii.Ubigia.Persistence.NetCoreApp.Tests
{
    using System;
    using EtAlii.Ubigia.Persistence.Tests;
    using Xunit;

    public class ContentTests : NetCoreAppStorageTestBase
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void Content_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var content = TestContentFactory.Create();

            // Act.
            Storage.Blobs.Store(containerId, content);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Content_Store_And_Retrieve_Check_Parts()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var content = TestContentFactory.Create();

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
            var first = TestContentFactory.Create();
            var second = TestContentFactory.Create();
            Storage.Blobs.Store(containerId, first);

            // Act.
            var act = new Action(() =>
            {
                Storage.Blobs.Store(containerId, second);
            });

            // Assert.
            Assert.Throws<BlobStorageException>(act);
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
