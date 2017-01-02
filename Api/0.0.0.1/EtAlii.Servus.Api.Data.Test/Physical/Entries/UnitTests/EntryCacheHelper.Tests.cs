namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EntryCacheHelper_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void EntryCacheHelper_Create()
        {
            // Arrange.
            var cacheProvider = new EntryCacheProvider();

            // Act.
            var helper = new EntryCacheHelper(cacheProvider);

            // Assert.
            Assert.IsNotNull(helper);
        }
    }
}
