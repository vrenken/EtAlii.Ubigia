namespace EtAlii.Ubigia.Tests
{
    using Xunit;

    public class ContentPartTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPart_Create()
        {
            var contentPart = new ContentPart();
            Assert.Null(contentPart.Data);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPart_Empty_Check()
        {
            Assert.NotNull(ContentPart.Empty);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPart_Empty_Check_Data()
        {
            Assert.NotNull(ContentPart.Empty.Data);
            Assert.Empty(ContentPart.Empty.Data);
        }
    }
}
