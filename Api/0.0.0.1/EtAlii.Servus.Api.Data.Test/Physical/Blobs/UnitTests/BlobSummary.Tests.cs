namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class BlobSummary_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
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
            Assert.AreEqual(isComplete, summary.IsComplete);
            Assert.AreEqual(availableParts, summary.AvailableParts);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
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
            Assert.AreEqual(isComplete, summary.IsComplete);
            Assert.AreEqual(parts, summary.TotalParts);
            Assert.AreEqual(availableParts, summary.AvailableParts);
        }
    }
}
