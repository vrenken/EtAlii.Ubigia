namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Content = EtAlii.Servus.Api.Content;

    [TestClass]
    public class BlobHelper_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BlobHelper_GetName_ContentDefinition()
        {
            var blob = new ContentDefinition();
            var name = BlobHelper.GetName(blob);
            Assert.AreEqual(@"ContentDefinition", name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BlobHelper_GetName_ContentDefinition_Generic()
        {
            var name = BlobHelper.GetName<ContentDefinition>();
            Assert.AreEqual(@"ContentDefinition", name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BlobHelper_GetName_Content()
        {
            var blob = new Content();
            var name = BlobHelper.GetName(blob);
            Assert.AreEqual(@"Content", name);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void BlobHelper_GetName_Content_Generic()
        {
            var name = BlobHelper.GetName<Content>();
            Assert.AreEqual(@"Content", name);
        }
    }
}
