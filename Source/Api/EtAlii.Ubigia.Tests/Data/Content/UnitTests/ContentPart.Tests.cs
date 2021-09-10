namespace EtAlii.Ubigia.Tests
{
    using Xunit;

    public class ContentPartTests
    {
        [Fact]
        public void ContentPart_Create()
        {
            var contentPart = new ContentPart();
            Assert.Null(contentPart.Data);
        }

        [Fact]
        public void ContentPart_Empty_Check()
        {
            Assert.NotNull(ContentPart.Empty);
        }

        [Fact]
        public void ContentPart_Empty_Check_Data()
        {
            Assert.NotNull(ContentPart.Empty.Data);
            Assert.Empty(ContentPart.Empty.Data);
        }
    }
}
