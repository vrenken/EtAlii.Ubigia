namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContentPart_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentPart_Create()
        {
            var contentPart = new ContentPart();
            Assert.IsNull(contentPart.Data);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentPart_Empty_Check()
        {
            Assert.IsNotNull(ContentPart.Empty);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentPart_Empty_Check_Data()
        {
            Assert.IsNotNull(ContentPart.Empty.Data);
            Assert.AreEqual(0, ContentPart.Empty.Data.Length);
        }
    }
}
