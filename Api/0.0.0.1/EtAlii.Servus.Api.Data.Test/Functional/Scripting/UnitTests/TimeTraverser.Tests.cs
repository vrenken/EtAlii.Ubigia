namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TimeTraverser_Tests
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void TimeTraverser_New()
        {
            // Arrange.
            var scope = new ScriptScope();
            var connection = default(IDataConnection);

            // Act.
            var timeTraverser = new TimeTraverser(connection);

            // Assert.
        }

        //[TestMethod, TestCategory(TestAssembly.Category)]
        //public void TimeTraverser_Traverse_Empty_Path()
        //{
        //    // Arrange.
        //    var scriptScopeHelper = new ScriptScopeHelper();
        //    var scope = new ScriptScope();
        //    var timeTraverser = new TimeTraverser(connection);

        //    // Act.
        //    timeTraverser.Traverse(null); 

        //    // Assert.
        //}
    }
}