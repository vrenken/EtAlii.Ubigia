// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ContentDefinitionTests : IDisposable
    {
        private readonly StorageUnitTestContext _testContext;

        public ContentDefinitionTests()
        {
            _testContext = new StorageUnitTestContext();
        }

        public void Dispose()
        {
            _testContext.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentDefinition_Store()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, contentDefinition);

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDefinition_Store_And_Retrieve_Check_Size()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, contentDefinition);
            var retrievedContentDefinition = await _testContext.Storage.Blobs.Retrieve<ContentDefinition>(containerId).ConfigureAwait(false);

            // Assert.
            Assert.Equal(contentDefinition.Size, retrievedContentDefinition.Size);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDefinition_Store_And_Retrieve_Check_Checksum()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, contentDefinition);
            var retrievedContentDefinition = await _testContext.Storage.Blobs.Retrieve<ContentDefinition>(containerId).ConfigureAwait(false);

            // Assert.
            Assert.Equal(contentDefinition.Checksum, retrievedContentDefinition.Checksum);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDefinition_Store_And_Retrieve_Check_Parts()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var contentDefinition = _testContext.TestContentDefinitionFactory.Create();

            // Act.
            _testContext.Storage.Blobs.Store(containerId, contentDefinition);
            var retrievedContentDefinition = await _testContext.Storage.Blobs.Retrieve<ContentDefinition>(containerId).ConfigureAwait(false);

            // Assert.
            Assert.Equal(contentDefinition.Parts.Length, retrievedContentDefinition.Parts.Length);
            for (var i = 0; i < contentDefinition.Parts.Length; i++)
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
            var first = _testContext.TestContentDefinitionFactory.Create();
            var second = _testContext.TestContentDefinitionFactory.Create();
            _testContext.Storage.Blobs.Store(containerId, first);

            // Act.
            var act = new Action(() => _testContext.Storage.Blobs.Store(containerId, second));

            // Assert.
            Assert.Throws<BlobStorageException>(act);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task ContentDefinition_Retrieve_None_Existing()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();

            // Act.
            var contentDefinition = await _testContext.Storage.Blobs.Retrieve<ContentDefinition>(containerId).ConfigureAwait(false);

            // Assert.
            Assert.Null(contentDefinition);
        }
    }
}
