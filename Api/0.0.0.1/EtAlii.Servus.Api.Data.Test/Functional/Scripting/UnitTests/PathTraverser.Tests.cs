namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PathTraverser_Tests
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
        public void PathTraverser_New()
        {
            // Arrange.
            var scope = new ScriptScope();
            var connection = default(IDataConnection);
            var timeTraverser = new TimeTraverser(connection);

            // Act.
            var pathTraverser = new PathTraverser(connection, timeTraverser);

            // Assert.
        }

        //[TestMethod, TestCategory(TestAssembly.Category)]
        //public void PathTraverser_Traverse_Empty_Path()
        //{
        //    // Arrange.
        //    var scriptScopeHelper = new ScriptScopeHelper();
        //    var scope = new ScriptScope();
        //    var pathTraverser = new PathTraverser(scriptScopeHelper);

        //    // Act.
        //    pathTraverser.Traverse(new string[] { }, null, scope, null); 

        //    // Assert.
        //}
    }
}