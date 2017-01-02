namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    [TestClass]
    public partial class DataContext_Nodes_ToArray_Tests
    {
        private static IDiagnosticsConfiguration _diagnostics;
        private static ILogicalTestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var task = Task.Run(async () =>
            {
                _testContext = new LogicalTestContextFactory().Create();
                await _testContext.Start();
                _diagnostics = TestDiagnostics.Create();
            });
            task.Wait();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var task = Task.Run(async () =>
            {
                _diagnostics = null;
                await _testContext.Stop();
                _testContext = null;
            });
            task.Wait();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var task = Task.Run(() =>
            {
            });
            task.Wait();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var task = Task.Run(() =>
            {
            });
            task.Wait();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Cast_ToArray_With_Single_Item()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addResult = await _testContext.AddYearMonth(logicalContext);
            var monthPath = addResult.Path;
            var monthEntry = addResult.Entry;
            await _testContext.AddDays(logicalContext, monthEntry, 1);
            var path = String.Format("{0}/", monthPath);
            var configuration = new DataContextConfiguration()
                                .Use(logicalContext)
                                .Use(_diagnostics);
            var context = new DataContextFactory().Create(configuration);
            var items = context.Nodes.Select(path);

            // Act.
            var single = items.Cast<NamedObject>().ToArray();

            // Assert.
            Assert.AreEqual("01", single[0].Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Cast_ToArray_With_Multiple_Item()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addResult = await _testContext.AddYearMonth(logicalContext);
            var monthPath = addResult.Path;
            var monthEntry = addResult.Entry;
            await _testContext.AddDays(logicalContext, monthEntry, 2);
            var path = String.Format("{0}/", monthPath);
            var configuration = new DataContextConfiguration()
                .Use(_diagnostics)
                .Use(logicalContext);
            var context = new DataContextFactory().Create(configuration);
            var items = context.Nodes.Select(path);

            // Act.
            var single = items.Cast<NamedObject>().ToArray();

            // Assert.
            Assert.AreEqual("01", single[0].Type);
            Assert.AreEqual("02", single[1].Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_ToArray_With_Multiple_Item()
        {
            // Arrange.
            var logicalContext = await _testContext.CreateLogicalContext(true);
            var addResult = await _testContext.AddYearMonth(logicalContext);
            var monthPath = addResult.Path;
            var monthEntry = addResult.Entry;
            await _testContext.AddDays(logicalContext, monthEntry, 2);
            var path = String.Format("{0}/", monthPath);
            var configuration = new DataContextConfiguration()
                .Use(_diagnostics)
                .Use(logicalContext);
            var context = new DataContextFactory().Create(configuration);
            var items = context.Nodes.Select(path);

            // Act.
            dynamic single = items.ToArray();

            // Assert.
            Assert.AreEqual("01", single[0].ToString());
            Assert.AreEqual("02", single[1].ToString());
        }
    }
}
