namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Diagnostics.Tests;
    using EtAlii.Servus.Api.Logical;
    using EtAlii.Servus.Api.Logical.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;


    public partial class DataContext_Nodes_Add_Tests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private IDataContext _context;
        private string _monthPath;
        private readonly LogicalUnitTestContext _testContext;

        public DataContext_Nodes_Add_Tests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;

            TestInitialize();
        }

        public void Dispose()
        {
            TestCleanup();
        }

        private void TestInitialize()
        {
            var task = Task.Run(async () =>
            {
                var start = Environment.TickCount;

                _diagnostics = TestDiagnostics.Create();
                _logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
                var configuration = new DataContextConfiguration()
                    .Use(_diagnostics)
                    .Use(_logicalContext);
                _context = new DataContextFactory().Create(configuration);
                var addResult = await _testContext.LogicalTestContext.AddYearMonth(_logicalContext);
                _monthPath = addResult.Path;

                Console.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            });
            task.Wait();
        }

        private void TestCleanup()
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

                Console.WriteLine("DataContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            });
            task.Wait();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Cast_Single()
        {
            // Arrange.
            var items = _context.Nodes.Select(_monthPath);

            // Act.
            var single = items.Add("01").Cast<NamedObject>().Single();

            // Assert.
            Assert.Equal("01", single.Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single()
        {
            // Arrange.
            var items = _context.Nodes.Select(_monthPath);

            // Act.
            dynamic single = items.Add("01").Single();

            // Assert.
            Assert.Equal("01", single.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Timed_01()
        {
            // Arrange.
            var start = Environment.TickCount;
            
            var delta = start;

            var items = _context.Nodes.Select(_monthPath);

            Console.WriteLine("context.Nodes.Select: {0}ms", TimeSpan.FromTicks(Environment.TickCount - delta).TotalMilliseconds);
            delta = Environment.TickCount;

            // Act.
            dynamic single = items.Add("01").Single();

            Console.WriteLine("items.Add: {0}ms", TimeSpan.FromTicks(Environment.TickCount - delta).TotalMilliseconds);
            delta = Environment.TickCount;

            // Assert.
            Assert.Equal("01", single.ToString());
            Assert.True(3000 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Timed_02()
        {
            // Arrange.
            var start = Environment.TickCount;

            var delta = start;

            var items = _context.Nodes.Select(_monthPath);

            Console.WriteLine("context.Nodes.Select: {0}ms", TimeSpan.FromTicks(Environment.TickCount - delta).TotalMilliseconds);
            delta = Environment.TickCount;

            // Act.
            dynamic single = items.Add("01").Single();

            Console.WriteLine("items.Add: {0}ms", TimeSpan.FromTicks(Environment.TickCount - delta).TotalMilliseconds);
            delta = Environment.TickCount;

            // Assert.
            Assert.Equal("01", single.ToString());
            Assert.True(3000 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Timed_03()
        {
            // Arrange.
            var start = Environment.TickCount;

            var delta = start;

            var items = _context.Nodes.Select(_monthPath);

            Console.WriteLine("context.Nodes.Select: {0}ms", TimeSpan.FromTicks(Environment.TickCount - delta).TotalMilliseconds);
            delta = Environment.TickCount;

            // Act.
            dynamic single = items.Add("01").Single();

            Console.WriteLine("items.Add: {0}ms", TimeSpan.FromTicks(Environment.TickCount - delta).TotalMilliseconds);
            delta = Environment.TickCount;

            // Assert.
            Assert.Equal("01", single.ToString());
            Assert.True(3000 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Add_Single_Three_Times()
        {
            var start = Environment.TickCount;

            // Arrange.
            var scope = new ExecutionScope(false);
            var items = _context.Nodes.Select(_monthPath);

            // Act.
            dynamic single = items.Add("01").Single();

            // Assert.
            Assert.Equal("01", single.ToString());
            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            Assert.True(3000 > duration, "Execution took longer than: " + duration + "ms");

            TestCleanup();
            TestInitialize();

            // Arrange.
            start = Environment.TickCount;
            _logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var addResult = await _testContext.LogicalTestContext.AddYearMonth(_logicalContext);
            _monthPath = addResult.Path;
            var configuration = new DataContextConfiguration()
                .Use(_diagnostics)
                .Use(_logicalContext);
            _context = new DataContextFactory().Create(configuration);
            items = _context.Nodes.Select(_monthPath);

            // Act.
            single = items.Add("01").Single();

            // Assert.
            Assert.Equal("01", single.ToString());
            duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            Assert.True(3000 > duration, "Execution took longer than: " + duration + "ms");

            TestCleanup();
            TestInitialize();

            // Arrange.
            start = Environment.TickCount;
            _logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            addResult = await _testContext.LogicalTestContext.AddYearMonth(_logicalContext);
            _monthPath = addResult.Path;
            configuration = new DataContextConfiguration()
                .Use(_diagnostics)
                .Use(_logicalContext);
            _context = new DataContextFactory().Create(configuration);
            items = _context.Nodes.Select(_monthPath);

            // Act.
            single = items.Add("01").Single();

            // Assert.
            Assert.Equal("01", single.ToString());
            duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            Assert.True(3000 > duration, "Execution took longer than: " + duration + "ms");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
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
