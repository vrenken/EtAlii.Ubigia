namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting.Tests;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    using Xunit.Abstractions;

    public class LinqQueryContextNodesAddTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private ILinqQueryContext _context;
        private string _countryPath;
        private readonly LogicalUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;

        public LinqQueryContextNodesAddTests(LogicalUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
        }

        public async Task InitializeAsync()
        {
            await TestInitialize();
        }

        public async Task DisposeAsync()
        {
            await TestCleanup();
        }

        private async Task TestInitialize()
        {
            var start = Environment.TickCount;

            _diagnostics = TestDiagnostics.Create();
            
            var configuration = new LinqQueryContextConfiguration()
                .UseFunctionalDiagnostics(_diagnostics);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration,true);

            _logicalContext = new LogicalContextFactory().Create(configuration); // Hmz, I'm not so sure about this action.
            _context = new LinqQueryContextFactory().Create(configuration);
            
            var addResult = await _testContext.LogicalTestContext.AddContinentCountry(_logicalContext);
            _countryPath = addResult.Path;

            Console.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        private Task TestCleanup()
        {
            var start = Environment.TickCount;

            _countryPath = null;
            _context.Dispose();
            _context = null;
            _logicalContext.Dispose();
            _logicalContext = null;
            _diagnostics = null;

            Console.WriteLine("DataContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return Task.CompletedTask;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Cast_Single()
        {
            // Arrange.
            var items = _context.Nodes.Select(_countryPath);

            // Act.
            var single = items.Add("Overijssel_01").Cast<NamedObject>().Single();

            // Assert.
            Assert.Equal("Overijssel_01", single.Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single()
        {
            // Arrange.
            var items = _context.Nodes.Select(_countryPath);

            // Act.
            dynamic single = items.Add("Overijssel_01").Single();

            // Assert.
            Assert.Equal("Overijssel_01", single.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Timed_01()
        {
            // Arrange.
            var start = Environment.TickCount;
            
            var delta = start;

            var items = _context.Nodes.Select(_countryPath);

            _testOutputHelper.WriteLine("context.Nodes.Select: {0}ms", TimeSpan.FromTicks(Environment.TickCount - delta).TotalMilliseconds);
            delta = Environment.TickCount;

            // Act.
            dynamic single = items.Add("Overijssel_01").Single();

            _testOutputHelper.WriteLine("items.Add: {0}ms", TimeSpan.FromTicks(Environment.TickCount - delta).TotalMilliseconds);

            // Assert.
            Assert.Equal("Overijssel_01", single.ToString());
            Assert.True(3000 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Timed_02()
        {
            // Arrange.
            var start = Environment.TickCount;

            var delta = start;

            var items = _context.Nodes.Select(_countryPath);

            _testOutputHelper.WriteLine("context.Nodes.Select: {0}ms", TimeSpan.FromTicks(Environment.TickCount - delta).TotalMilliseconds);
            delta = Environment.TickCount;

            // Act.
            dynamic single = items.Add("Overijssel_01").Single();

            _testOutputHelper.WriteLine("items.Add: {0}ms", TimeSpan.FromTicks(Environment.TickCount - delta).TotalMilliseconds);

            // Assert.
            Assert.Equal("Overijssel_01", single.ToString());
            Assert.True(3000 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Timed_03()
        {
            // Arrange.
            var start = Environment.TickCount;

            var delta = start;

            var items = _context.Nodes.Select(_countryPath);

            _testOutputHelper.WriteLine("context.Nodes.Select: {0}ms", TimeSpan.FromTicks(Environment.TickCount - delta).TotalMilliseconds);
            delta = Environment.TickCount;

            // Act.
            dynamic single = items.Add("Overijssel_01").Single();

            _testOutputHelper.WriteLine("items.Add: {0}ms", TimeSpan.FromTicks(Environment.TickCount - delta).TotalMilliseconds);

            // Assert.
            Assert.Equal("Overijssel_01", single.ToString());
            Assert.True(3000 > TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Add_Single_Three_Times()
        {
            var start = Environment.TickCount;

            // Arrange.
            //var scope = new ExecutionScope(false)
            var items = _context.Nodes.Select(_countryPath);

            // Act.
            dynamic single = items.Add("Overijssel_01").Single();

            // Assert.
            Assert.Equal("Overijssel_01", single.ToString());
            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            Assert.True(3000 > duration, "Execution took longer than: " + duration + "ms");

            await TestCleanup();
            await TestInitialize();

            // Arrange.
            start = Environment.TickCount;
                        
            var addResult = await _testContext.LogicalTestContext.AddContinentCountry(_logicalContext);
            _countryPath = addResult.Path;
            items = _context.Nodes.Select(_countryPath);

            // Act.
            single = items.Add("Overijssel_01").Single();

            // Assert.
            Assert.Equal("Overijssel_01", single.ToString());
            duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            Assert.True(3000 > duration, "Execution took longer than: " + duration + "ms");

            await TestCleanup();
            await TestInitialize();

            // Arrange.
            start = Environment.TickCount;

            addResult = await _testContext.LogicalTestContext.AddContinentCountry(_logicalContext);
            _countryPath = addResult.Path;
            items = _context.Nodes.Select(_countryPath);

            // Act.
            single = items.Add("Overijssel_01").Single();

            // Assert.
            Assert.Equal("Overijssel_01", single.ToString());
            duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            Assert.True(3000 > duration, "Execution took longer than: " + duration + "ms");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Invalid_Invalid_Character()
        {
            // Arrange.
            var items = _context.Nodes.Select(_countryPath);

            // Act.
            var act = new Action(() => items.Add("\"Overijssel_01").Single());

            // Assert.
            Assert.Throws<NodeQueryingException>(act);
        }
    }
}
