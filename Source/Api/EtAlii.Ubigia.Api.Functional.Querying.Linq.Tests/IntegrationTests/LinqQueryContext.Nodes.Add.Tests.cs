namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    using Xunit.Abstractions;

    public class LinqQueryContextNodesAddTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private ILinqQueryContext _context;
        private string _countryPath;
        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private LinqQueryContextConfiguration _configuration;

        public LinqQueryContextNodesAddTests(QueryingUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
        }

        public async Task InitializeAsync()
        {
            await TestInitialize().ConfigureAwait(false);
        }

        public async Task DisposeAsync()
        {
            await TestCleanup().ConfigureAwait(false);
        }

        private async Task TestInitialize()
        {
            var start = Environment.TickCount;

            _diagnostics = DiagnosticsConfiguration.Default;

            _configuration = new LinqQueryContextConfiguration()
                .UseFunctionalDiagnostics(_diagnostics);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(_configuration,true).ConfigureAwait(false);

            _logicalContext = new LogicalContextFactory().Create(_configuration); // Hmz, I'm not so sure about this action.
            _context = new LinqQueryContextFactory().Create(_configuration);

            var addResult = await _testContext.LogicalTestContext.AddContinentCountry(_logicalContext).ConfigureAwait(false);
            _countryPath = addResult.Path;

            _testOutputHelper.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        private async Task TestCleanup()
        {
            var start = Environment.TickCount;

            await _configuration.Connection.Close().ConfigureAwait(false);
            _configuration = null;
            _countryPath = null;
            _context.Dispose();
            _context = null;
            _logicalContext.Dispose();
            _logicalContext = null;
            _diagnostics = null;

            _testOutputHelper.WriteLine("DataContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
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

            await TestCleanup().ConfigureAwait(false);
            await TestInitialize().ConfigureAwait(false);

            // Arrange.
            start = Environment.TickCount;

            var addResult = await _testContext.LogicalTestContext.AddContinentCountry(_logicalContext).ConfigureAwait(false);
            _countryPath = addResult.Path;
            items = _context.Nodes.Select(_countryPath);

            // Act.
            single = items.Add("Overijssel_01").Single();

            // Assert.
            Assert.Equal("Overijssel_01", single.ToString());
            duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            Assert.True(3000 > duration, "Execution took longer than: " + duration + "ms");

            await TestCleanup().ConfigureAwait(false);
            await TestInitialize().ConfigureAwait(false);

            // Arrange.
            start = Environment.TickCount;

            addResult = await _testContext.LogicalTestContext.AddContinentCountry(_logicalContext).ConfigureAwait(false);
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
            INode result = null;

            // Act.
            var act = new Action(() => result = items.Add("\"Overijssel_01").Single());

            // Assert.
            Assert.Throws<NodeQueryingException>(act);
            Assert.Null(result);
        }
    }
}
