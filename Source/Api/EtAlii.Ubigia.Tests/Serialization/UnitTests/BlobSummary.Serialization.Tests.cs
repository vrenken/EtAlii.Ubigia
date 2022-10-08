namespace EtAlii.Ubigia.Tests
{
    using System.IO;
    using Xunit;

    public class BlobSummarySerializationTests
    {
        [Fact]
        public void BlobSummary_Serialize_Deserialize()
        {
            // Arrange.
            var blobSummary = BlobSummary.Create(true, new ulong[] { 1, 2, 3 }, 3);

            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream);
            using var reader = new BinaryReader(stream);

            // Act.
            writer.Write(blobSummary);
            stream.Position = 0;
            var blobSummary2 = reader.Read<BlobSummary>();

            // Assert.
            Assert.NotNull(blobSummary2);
            Assert.Equal(blobSummary.IsComplete, blobSummary.IsComplete);
            Assert.Equal(blobSummary.TotalParts, blobSummary2.TotalParts);
            Assert.Equal(blobSummary.AvailableParts, blobSummary2.AvailableParts);
        }
    }
}
