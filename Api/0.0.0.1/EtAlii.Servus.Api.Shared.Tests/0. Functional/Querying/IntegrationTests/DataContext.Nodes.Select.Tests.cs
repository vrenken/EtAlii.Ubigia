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
    public partial class DataContext_Nodes_Select_Tests
    {
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private IDataContext _context;
        private string _monthPath;
        private IEditableEntry _monthEntry;
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
                _monthEntry = addResult.Entry;

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

                _monthEntry = null;
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
        public void Linq_Nodes_Select()
        {
            // Arrange.

            // Act.
            var vacation = _context.Nodes.Select("/Documents/Vacation/");

            // Assert.
            Assert.IsNotNull(vacation);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Cast_Single_With_Single_Item()
        {
            // Arrange.
            await _testContext.AddDays(_logicalContext, _monthEntry, 1);
            var path = String.Format("{0}/", _monthPath);
            var items = _context.Nodes.Select(path);

            // Act.
            var single = items.Cast<NamedObject>().Single();

            // Assert.
            Assert.AreEqual("01", single.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Cast_With_Single_Item()
        {
            // Arrange.
            await _testContext.AddDays(_logicalContext, _monthEntry, 1);
            var path = String.Format("{0}/", _monthPath);
            var items = _context.Nodes.Select(path);

            // Act.
            dynamic single = items.Single();

            // Assert.
            Assert.AreEqual("01", single.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Any_With_Multiple_Items()
        {
            // Arrange.
            await _testContext.AddDays(_logicalContext, _monthEntry, 2);
            var path = String.Format("{0}/", _monthPath);
            var items = _context.Nodes.Select(path);

            // Act.
            var any = items.Any();

            // Assert.
            Assert.IsTrue(any);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Any_With_Multiple_Items_Fail()
        {
            // Arrange.
            var path = String.Format("{0}/", _monthPath);
            var items = _context.Nodes.Select(path);

            // Act.
            var any = items.Any();

            // Assert.
            Assert.IsFalse(any);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Any_With_Single_Item()
        {
            // Arrange.
            await _testContext.AddDays(_logicalContext, _monthEntry, 1);
            var path = String.Format("{0}/01", _monthPath);
            var items = _context.Nodes.Select(path);

            // Act.
            var any = items.Any();

            // Assert.
            Assert.IsTrue(any);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void Linq_Nodes_Select_Any_With_Single_Item_Fail()
        {
            // Arrange.
            var path = String.Format("{0}/01", _monthPath);
            var items = _context.Nodes.Select(path);
            var any = false;

            // Act.
            var act = new Action(() =>
            {
                any = items.Any();
            });

            // Assert.
            Assert.IsFalse(any);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Count_With_Single_Item()
        {
            // Arrange.
            const int days = 1;
            await _testContext.AddDays(_logicalContext, _monthEntry, days);
            var path = String.Format("{0}/", _monthPath);
            var items = _context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.AreEqual(days, count);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Count_With_Multiple_Items_2()
        {
            // Arrange.
            const int days = 2;
            await _testContext.AddDays(_logicalContext, _monthEntry, days);
            var path = String.Format("{0}/", _monthPath);
            var items = _context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.AreEqual(days, count);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Count_With_Multiple_Items_5()
        {
            // Arrange.
            const int days = 5;
            await _testContext.AddDays(_logicalContext, _monthEntry, days);
            var path = String.Format("{0}/", _monthPath);
            var items = _context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.AreEqual(days, count);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Count_With_Multiple_Items_20()
        {
            // Arrange.
            const int days = 20;
            await _testContext.AddDays(_logicalContext, _monthEntry, days);
            var path = String.Format("{0}/", _monthPath);
            var items = _context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.AreEqual(days, count);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Count_With_Multiple_Items_50()
        {
            // Arrange.
            const int days = 50;
            await _testContext.AddDays(_logicalContext, _monthEntry, days);
            var path = String.Format("{0}/", _monthPath);
            var items = _context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.AreEqual(days, count);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Count_With_Multiple_Items_50_Multiple_requests_20()
        {
            // Arrange.
            const int days = 50;
            await _testContext.AddDays(_logicalContext, _monthEntry, days);
            var path = String.Format("{0}/", _monthPath);
            var items = _context.Nodes.Select(path);
            const int iterations = 20;
            var counts = new int[iterations];

            // Act.
            for (int i = 0; i < iterations; i++)
            {
                counts[i] = items.Count();
            }

            // Assert.
            for (int i = 0; i < iterations; i++)
            {
                Assert.AreEqual(days, counts[i]);
            }
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Any()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var timeRoot = await _logicalContext.Roots.Get("Time");
            var time = await _logicalContext.Nodes.Select(GraphPath.Create(timeRoot.Identifier), executionScope);
            var now = DateTime.Now;
            var year = now.ToString("yyyy");
            var month = now.ToString("MM");
            var day = now.ToString("dd");
            await _testContext.CreateHierarchy(_logicalContext, (IEditableEntry)time, year, month, day);
            var path = String.Format("/Time/{0}/{1}/{2}", year, month, day);
            var items = _context.Nodes.Select(path);

            // Act.
            var any = items.Any();

            // Assert.
            Assert.IsTrue(any);
        }
    }
}
