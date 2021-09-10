namespace EtAlii.Ubigia.Tests
{
    using Xunit;

    public class BlobHelperTests
    {
        [Fact]
        public void BlobHelper_GetName_ContentDefinition()
        {
            var blob = new ContentDefinition();
            var name = Blob.GetName(blob);
            Assert.Equal(@"ContentDefinition", name);
        }

        [Fact]
        public void BlobHelper_GetName_ContentDefinition_Generic()
        {
            var name = Blob.GetName<ContentDefinition>();
            Assert.Equal(@"ContentDefinition", name);
        }

        [Fact]
        public void BlobHelper_GetName_Content()
        {
            var blob = new Content();
            var name = Blob.GetName(blob);
            Assert.Equal(@"Content", name);
        }

        [Fact]
        public void BlobHelper_GetName_Content_Generic()
        {
            var name = Blob.GetName<Content>();
            Assert.Equal(@"Content", name);
        }
    }
}
