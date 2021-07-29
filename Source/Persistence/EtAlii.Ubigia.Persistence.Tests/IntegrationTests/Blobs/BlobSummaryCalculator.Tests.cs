// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    public class BlobSummaryCalculatorTests : StorageUnitTestContext
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobSummaryCalculator_Create()
        {
            // Arrange.

            // Act.
            var blobSummaryCalculator = new BlobSummaryCalculator(Storage.PathBuilder, Storage.FileManager);

            // Assert.
            Assert.NotNull(blobSummaryCalculator);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task BlobSummaryCalculator_Calculate()
        {
            // Arrange.
            var containerId = StorageTestHelper.CreateSimpleContainerIdentifier();
            var content = TestContentFactory.Create();
            var blobStorer = new BlobStorer(Storage.FolderManager, Storage.PathBuilder);
            blobStorer.Store(containerId, content);
            var blobSummaryCalculator = new BlobSummaryCalculator(Storage.PathBuilder, Storage.FileManager);

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
            var content = TestContentFactory.Create();
            var contentParts = TestContentFactory.CreateParts(content.TotalParts);
            var blobStorer = new BlobStorer(Storage.FolderManager, Storage.PathBuilder);
            blobStorer.Store(containerId, content);
            var blobPartStorer = new BlobPartStorer(Storage.FolderManager, Storage.PathBuilder);
            foreach (var blobPart in contentParts)
            {
                blobPartStorer.Store(containerId, blobPart);
            }
            var blobSummaryCalculator = new BlobSummaryCalculator(Storage.PathBuilder, Storage.FileManager);

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
            var content = TestContentFactory.Create();
            var contentParts = TestContentFactory.CreateParts(content.TotalParts);
            var blobStorer = new BlobStorer(Storage.FolderManager, Storage.PathBuilder);
            blobStorer.Store(containerId, content);
            contentParts.RemoveAt(4);
            contentParts.RemoveAt(2);
            var blobPartStorer = new BlobPartStorer(Storage.FolderManager, Storage.PathBuilder);
            foreach (var blobPart in contentParts)
            {
                blobPartStorer.Store(containerId, blobPart);
            }
            var blobSummaryCalculator = new BlobSummaryCalculator(Storage.PathBuilder, Storage.FileManager);

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
