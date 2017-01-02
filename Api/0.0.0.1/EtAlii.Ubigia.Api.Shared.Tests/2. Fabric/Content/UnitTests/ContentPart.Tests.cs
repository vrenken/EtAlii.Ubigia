namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;

    
    public class ContentPart_Tests
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
            Assert.Equal(0, ContentPart.Empty.Data.Length);
        }
    }
}
