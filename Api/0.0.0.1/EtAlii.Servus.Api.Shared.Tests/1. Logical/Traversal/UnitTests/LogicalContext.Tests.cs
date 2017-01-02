namespace EtAlii.Servus.Api.UnitTests
{
    using EtAlii.Servus.Api.Logical;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LogicalContext_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void LogicalContext_Create()
        {
            // Arrange.

            // Act.
            var context = new LogicalContextFactory().Create();

            // Assert.
            Assert.IsNotNull(context);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void LogicalContext_Dispose()
        {
            // Arrange.

            // Act.
            using (var context = new LogicalContextFactory().Create())
            {
            }

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void LogicalContext_Create_Check_Components()
        {
            // Arrange.

            // Act.
            var context = new LogicalContextFactory().Create();

            // Assert.
            Assert.IsNotNull(context);
            Assert.IsNotNull(context.PathBuilder);
            Assert.IsNotNull(context.PathTraverser);
        }
    }
}
