namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.Servus.Api.Logical.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class DataContext_Nodes_Add_Tests
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
        public void Linq_Nodes_Select_Add_Cast_Single()
        {
            // Arrange.
            var items = _context.Nodes.Select(_monthPath);

            // Act.
            var single = items.Add("01").Cast<NamedObject>().Single();

            // Assert.
            Assert.AreEqual("01", single.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single()
        {
            // Arrange.
            var items = _context.Nodes.Select(_monthPath);

            // Act.
            dynamic single = items.Add("01").Single();

            // Assert.
            Assert.AreEqual("01", single.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Timed_01()
        {
            // Arrange.
            var start = Environment.TickCount;
            
            var delta = start;

            var items = _context.Nodes.Select(_monthPath);

            Console.WriteLine("context.Nodes.Select: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            // Act.
            dynamic single = items.Add("01").Single();

            Console.WriteLine("items.Add: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            // Assert.
            Assert.AreEqual("01", single.ToString());
            Assert.IsTrue(3000 > Environment.TickCount - start);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Timed_02()
        {
            // Arrange.
            var start = Environment.TickCount;

            var delta = start;

            var items = _context.Nodes.Select(_monthPath);

            Console.WriteLine("context.Nodes.Select: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            // Act.
            dynamic single = items.Add("01").Single();

            Console.WriteLine("items.Add: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            // Assert.
            Assert.AreEqual("01", single.ToString());
            Assert.IsTrue(3000 > Environment.TickCount - start);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Timed_03()
        {
            // Arrange.
            var start = Environment.TickCount;

            var delta = start;

            var items = _context.Nodes.Select(_monthPath);

            Console.WriteLine("context.Nodes.Select: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            // Act.
            dynamic single = items.Add("01").Single();

            Console.WriteLine("items.Add: {0}ms", Environment.TickCount - delta);
            delta = Environment.TickCount;

            // Assert.
            Assert.AreEqual("01", single.ToString());
            Assert.IsTrue(3000 > Environment.TickCount - start);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Add_Single_Three_Times()
        {
            var start = Environment.TickCount;

            // Arrange.
            var scope = new ExecutionScope(false);
            var items = _context.Nodes.Select(_monthPath);

            // Act.
            dynamic single = items.Add("01").Single();

            // Assert.
            Assert.AreEqual("01", single.ToString());
            var duration = Environment.TickCount - start;
            Assert.IsTrue(3000 > duration, "Execution took longer than: " + duration);

            TestCleanup();
            TestInitialize();

            // Arrange.
            start = Environment.TickCount;
            _logicalContext = await _testContext.CreateLogicalContext(true);
            var addResult = await _testContext.AddYearMonth(_logicalContext);
            _monthPath = addResult.Path;
            var configuration = new DataContextConfiguration()
                .Use(_diagnostics)
                .Use(_logicalContext);
            _context = new DataContextFactory().Create(configuration);
            items = _context.Nodes.Select(_monthPath);

            // Act.
            single = items.Add("01").Single();

            // Assert.
            Assert.AreEqual("01", single.ToString());
            duration = Environment.TickCount - start;
            Assert.IsTrue(3000 > duration, "Execution took longer than: " + duration);

            TestCleanup();
            TestInitialize();

            // Arrange.
            start = Environment.TickCount;
            _logicalContext = await _testContext.CreateLogicalContext(true);
            addResult = await _testContext.AddYearMonth(_logicalContext);
            _monthPath = addResult.Path;
            configuration = new DataContextConfiguration()
                .Use(_diagnostics)
                .Use(_logicalContext);
            _context = new DataContextFactory().Create(configuration);
            items = _context.Nodes.Select(_monthPath);

            // Act.
            single = items.Add("01").Single();

            // Assert.
            Assert.AreEqual("01", single.ToString());
            duration = Environment.TickCount - start;
            Assert.IsTrue(3000 > duration, "Execution took longer than: " + duration);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Invalid_Invalid_Character()
        {
            // Arrange.
            var items = _context.Nodes.Select(_monthPath);

            // Act.
            var act = new Action(() => items.Add("\"01").Single());

            // Assert.
            ExceptionAssert.Throws<NodeQueryingException>(act);
        }
    }
}
