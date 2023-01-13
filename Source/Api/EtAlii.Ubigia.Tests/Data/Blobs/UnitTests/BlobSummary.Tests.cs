namespace EtAlii.Ubigia.Tests;

using System;
using Xunit;

public class BlobSummaryTests
{
    [Fact]
    public void BlobSummary_Create_From_Blob()
    {
        // Arrange.
        var random = new Random();
        var blob = new ContentDefinition();
        var availableParts = new[] { (ulong)random.Next(0, int.MaxValue) };
        var isComplete = availableParts.Length > 0;

        // Act.
        var summary = BlobSummary.Create(isComplete, availableParts, blob.TotalParts);

        // Assert.
        Assert.Equal(isComplete, summary.IsComplete);
        Assert.Equal(availableParts, summary.AvailableParts);
    }

    [Fact]
    public void BlobSummary_Create_From_TotalParts()
    {
        // Arrange.
        var random = new Random();
        var parts = (ulong)random.Next(0, int.MaxValue);
        var availableParts = new[] { (ulong)random.Next(0, int.MaxValue) };
        var isComplete = availableParts.Length > 0;

        // Act.
        var summary = BlobSummary.Create(isComplete, availableParts, parts);

        // Assert.
        Assert.Equal(isComplete, summary.IsComplete);
        Assert.Equal(parts, summary.TotalParts);
        Assert.Equal(availableParts, summary.AvailableParts);
    }
}
