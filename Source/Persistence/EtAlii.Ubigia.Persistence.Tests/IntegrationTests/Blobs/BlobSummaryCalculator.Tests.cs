// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class BlobSummaryCalculatorTests : IDisposable
    {
        private readonly StorageUnitTestContext _testContext;

        public BlobSummaryCalculatorTests()
        {
            _testContext = new StorageUnitTestContext();
        }

        public void Dispose()
        {
            _testContext.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobSummaryCalculator_Create()
        {
            // Arrange.

            // Act.
            var blobSummaryCalculator = new BlobSummaryCalculator(_testContext.Storage.PathBuilder, _testContext.Storage.FileManager);

            // Assert.
            Assert.NotNull(blobSummaryCalculator);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task BlobSummaryCalculator_Calculate()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var content = _testContext.TestContentFactory.Create();
            var blobStorer = new BlobStorer(_testContext.Storage.FolderManager, _testContext.Storage.PathBuilder);
            blobStorer.Store(containerId, content);
            var blobSummaryCalculator = new BlobSummaryCalculator(_testContext.Storage.PathBuilder, _testContext.Storage.FileManager);

            // Act.
            var summary = await blobSummaryCalculator.Calculate<Content>(containerId).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(summary);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task BlobSummaryCalculator_Calculate_With_All_Parts()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var content = _testContext.TestContentFactory.Create();
            var contentParts = _testContext.TestContentFactory.CreateParts(content.TotalParts);
            var blobStorer = new BlobStorer(_testContext.Storage.FolderManager, _testContext.Storage.PathBuilder);
            blobStorer.Store(containerId, content);
            var blobPartStorer = new BlobPartStorer(_testContext.Storage.FolderManager, _testContext.Storage.PathBuilder);
            foreach (var blobPart in contentParts)
            {
                blobPartStorer.Store(containerId, blobPart);
            }
            var blobSummaryCalculator = new BlobSummaryCalculator(_testContext.Storage.PathBuilder, _testContext.Storage.FileManager);

            // Act.
            var summary = await blobSummaryCalculator.Calculate<Content>(containerId).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(summary);
            Assert.True(summary.IsComplete);
            Assert.Equal(content.TotalParts, summary.TotalParts);
            Assert.Equal(content.TotalParts, (uint)summary.AvailableParts.Length);
            for (uint partId = 0; partId < content.TotalParts; partId++)
            {
                Assert.Contains(partId, summary.AvailableParts);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task BlobSummaryCalculator_Calculate_With_Some_Parts()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var content = _testContext.TestContentFactory.Create();
            var contentParts = _testContext.TestContentFactory.CreateParts(content.TotalParts);
            var blobStorer = new BlobStorer(_testContext.Storage.FolderManager, _testContext.Storage.PathBuilder);
            blobStorer.Store(containerId, content);
            contentParts.RemoveAt(4);
            contentParts.RemoveAt(2);
            var blobPartStorer = new BlobPartStorer(_testContext.Storage.FolderManager, _testContext.Storage.PathBuilder);
            foreach (var blobPart in contentParts)
            {
                blobPartStorer.Store(containerId, blobPart);
            }
            var blobSummaryCalculator = new BlobSummaryCalculator(_testContext.Storage.PathBuilder, _testContext.Storage.FileManager);

            // Act.
            var summary = await blobSummaryCalculator.Calculate<Content>(containerId).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(summary);
            Assert.False(summary.IsComplete);
            Assert.Equal(content.TotalParts, summary.TotalParts);
            Assert.Equal(content.TotalParts - 2, (uint)summary.AvailableParts.Length);
            for (uint partId = 0; partId < content.TotalParts; partId++)
            {
                if (partId == 2 || partId == 4)
                {
                    Assert.DoesNotContain(partId, summary.AvailableParts);
                }
                else
                {
                    Assert.Contains(partId, summary.AvailableParts);
                }
            }
        }
    }
}
