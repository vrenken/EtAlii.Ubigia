namespace EtAlii.Servus.Api.Fabric.Tests
{
    using EtAlii.Servus.Infrastructure.Hosting.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    [TestClass]
    public class ClasssName_Tests
    {
        private static TestHosting _testHosting;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _testHosting = new TestHosting();
            _testHosting.Start(TestAssembly.StorageName);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _testHosting.Stop();
            _testHosting = null;
        }

        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ClasssName_Construct()
        {
            // Arrange.

            // Act.

            // Assert.
        }

    }
}
