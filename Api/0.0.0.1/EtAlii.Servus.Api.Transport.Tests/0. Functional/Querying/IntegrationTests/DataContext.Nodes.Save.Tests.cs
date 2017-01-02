namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.Servus.Api.Logical.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    [TestClass]
    public partial class DataContext_Nodes_Save_Tests
    {
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private IDataContext _context;
        private string _monthPath;
        private static ILogicalTestContext _testContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var task = Task.Run(async () =>
            {
                _testContext = new LogicalTestContextFactory().Create();
                await _testContext.Start();
            });
            task.Wait();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var task = Task.Run(async () =>
            {
                await _testContext.Stop();
                _testContext = null;
            });
            task.Wait();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var task = Task.Run(async () =>
            {
                var start = Environment.TickCount;

                _diagnostics = TestDiagnostics.Create();
                _logicalContext = await _testContext.CreateLogicalContext(true);
                var configuration = new DataContextConfiguration()
                    .Use(_diagnostics)
                    .Use(_logicalContext);
                _context = new DataContextFactory().Create(configuration);
                var addResult = await _testContext.AddYearMonth(_logicalContext);
                _monthPath = addResult.Path;

                Console.WriteLine("DataContext_Nodes.Initialize: {0}ms", Environment.TickCount - start);
            });
            task.Wait();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var task = Task.Run(() =>
            {
                var start = Environment.TickCount;

                _monthPath = null;
                _context.Dispose();
                _context = null;
                _logicalContext.Dispose();
                _logicalContext = null;
                _diagnostics = null;

                Console.WriteLine("DataContext_Nodes.Cleanup: {0}ms", Environment.TickCount - start);
            });
            task.Wait();
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Save_Check_IsModified()
        {
            // Arrange.
            var items = _context.Nodes.Select(_monthPath);
            var value = new Random().Next();
            var single = items.Add("01").Cast<NamedObject>().Single();

            // Act.
            single.Value = value;
            var wasModified = ((INode)single).IsModified;
            _context.Nodes.Save(single);
            var isModified = ((INode)single).IsModified;

            // Assert.
            Assert.IsTrue(wasModified);
            Assert.IsFalse(isModified);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Save_Check_Id()
        {
            // Arrange.
            var items = _context.Nodes.Select(_monthPath);
            var value = new Random().Next();
            var single = items.Add("01").Cast<NamedObject>().Single();
            var originalId = ((INode)single).Id;

            // Act.
            single.Value = value;
            _context.Nodes.Save(single);
            var newId = ((INode)single).Id;

            // Assert.
            Assert.AreNotEqual(originalId, newId);
        }
    }
}
