namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;

    
    public class BlobSummary_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobSummary_Create_From_Blob()
        {
            // Arrange.
            var random = new Random();
            var blob = new ContentDefinition();
            var isComplete = true;
            var availableParts = new ulong[] { (ulong)random.Next(0, int.MaxValue) };

            // Act.
            var summary = BlobSummary.Create(isComplete, blob, availableParts);

            // Assert.
            Assert.Equal(isComplete, summary.IsComplete);
            Assert.Equal(availableParts, summary.AvailableParts);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobSummary_Create_From_TotalParts()
        {
            // Arrange.
            var random = new Random();
            var parts = (ulong)random.Next(0, int.MaxValue);
            var isComplete = true;
            var availableParts = new ulong[] { (ulong)random.Next(0, int.MaxValue) };

            // Act.
            var summary = BlobSummary.Create(isComplete, parts, availableParts);

            // Assert.
            Assert.Equal(isComplete, summary.IsComplete);
            Assert.Equal(parts, summary.TotalParts);
            Assert.Equal(availableParts, summary.AvailableParts);
        }
    }
}
