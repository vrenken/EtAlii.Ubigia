namespace EtAlii.Servus.Api.Fabric.Tests
{
    using EtAlii.Servus.Api.Tests;
    using Xunit;

    
    public class BlobHelper_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobHelper_GetName_ContentDefinition()
        {
            var blob = new ContentDefinition();
            var name = BlobHelper.GetName(blob);
            Assert.Equal(@"ContentDefinition", name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobHelper_GetName_ContentDefinition_Generic()
        {
            var name = BlobHelper.GetName<ContentDefinition>();
            Assert.Equal(@"ContentDefinition", name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobHelper_GetName_Content()
        {
            var blob = new Content();
            var name = BlobHelper.GetName(blob);
            Assert.Equal(@"Content", name);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void BlobHelper_GetName_Content_Generic()
        {
            var name = BlobHelper.GetName<Content>();
            Assert.Equal(@"Content", name);
        }
    }
}
