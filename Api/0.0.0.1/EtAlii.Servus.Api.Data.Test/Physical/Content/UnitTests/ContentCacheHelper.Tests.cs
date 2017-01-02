namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContentCacheHelper_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentCacheHelper_Create()
        {
            // Arrange.
            var cacheProvider = new ContentCacheProvider();

            // Act.
            var helper = new ContentCacheHelper(cacheProvider);

            // Assert.
            Assert.IsNotNull(helper);
        }
    }
}
