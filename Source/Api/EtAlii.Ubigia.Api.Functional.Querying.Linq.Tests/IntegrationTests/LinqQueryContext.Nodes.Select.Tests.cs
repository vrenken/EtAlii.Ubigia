namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    using Xunit.Abstractions;

    public class LinqQueryContextNodesSelectTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private ILinqQueryContext _context;
        private string _countryPath;
        private IEditableEntry _countryEntry;
        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private LinqQueryContextConfiguration _configuration;

        public LinqQueryContextNodesSelectTests(QueryingUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _diagnostics = DiagnosticsConfiguration.Default;

            _configuration = new LinqQueryContextConfiguration()
                .UseTestTraversalParser()
                .UseFunctionalDiagnostics(_diagnostics);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(_configuration,true).ConfigureAwait(false);

            _logicalContext = new LogicalContextFactory().Create(_configuration); // Hmz, I'm not so sure about this action.
            _context = new LinqQueryContextFactory().Create(_configuration);

            var addResult = await _testContext.LogicalTestContext.AddContinentCountry(_logicalContext).ConfigureAwait(false);
            _countryPath = addResult.Path;
            _countryEntry = addResult.Entry;

            _testOutputHelper.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            await _configuration.Connection.Close().ConfigureAwait(false);
            _configuration = null;
            _countryEntry = null;
            _countryPath = null;
            _context.Dispose();
            _context = null;
            _logicalContext.Dispose();
            _logicalContext = null;
            _diagnostics = null;

            _testOutputHelper.WriteLine("LinqContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select()
        {
            // Arrange.

            // Act.
            var vacation = _context.Nodes.Select("/Documents/Vacation/");

            // Assert.
            Assert.NotNull(vacation);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Cast_Single_With_Single_Item()
        {
            // Arrange.
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, 1).ConfigureAwait(false);
            var path = $"{_countryPath}/";
            var items = _context.Nodes.Select(path);

            // Act.
            var single = items.Cast<NamedObject>().Single();

            // Assert.
            Assert.Equal("Overijssel_01", single.Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Cast_With_Single_Item()
        {
            // Arrange.
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, 1).ConfigureAwait(false);
            var path = $"{_countryPath}/";
            var items = _context.Nodes.Select(path);

            // Act.
            dynamic single = items.Single();

            // Assert.
            Assert.Equal("Overijssel_01", single.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Any_With_Multiple_Items()
        {
            // Arrange.
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, 2).ConfigureAwait(false);
            var path = $"{_countryPath}/";
            var items = _context.Nodes.Select(path);

            // Act.
            var any = items.Any();

            // Assert.
            Assert.True(any);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Any_With_Multiple_Items_Fail()
        {
            // Arrange.
            var path = $"{_countryPath}/";
            var items = _context.Nodes.Select(path);

            // Act.
            var any = items.Any();

            // Assert.
            Assert.False(any);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Any_With_Single_Item()
        {
            // Arrange.
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, 1).ConfigureAwait(false);
            var path = $"{_countryPath}/Overijssel_01";
            var items = _context.Nodes.Select(path);

            // Act.
            var any = items.Any();

            // Assert.
            Assert.True(any);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Any_With_Single_Item_Fail()
        {
            // Arrange.
            var path = $"{_countryPath}/Overijssel_01";
            var items = _context.Nodes.Select(path);

            // Act.
            var any = items.Any();

            // Assert.
            Assert.False(any);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Count_With_Single_Item()
        {
            // Arrange.
            const int regions = 1;
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, regions).ConfigureAwait(false);
            var path = $"{_countryPath}/";
            var items = _context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.Equal(regions, count);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Count_With_Multiple_Items_2()
        {
            // Arrange.
            const int regions = 2;
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, regions).ConfigureAwait(false);
            var path = $"{_countryPath}/";
            var items = _context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.Equal(regions, count);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Count_With_Multiple_Items_5()
        {
            // Arrange.
            const int regions = 5;
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, regions).ConfigureAwait(false);
            var path = $"{_countryPath}/";
            var items = _context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.Equal(regions, count);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Count_With_Multiple_Items_20()
        {
            // Arrange.
            const int regions = 20;
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, regions).ConfigureAwait(false);
            var path = $"{_countryPath}/";
            var items = _context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.Equal(regions, count);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Count_With_Multiple_Items_50()
        {
            // Arrange.
            const int regions = 50;
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, regions).ConfigureAwait(false);
            var path = $"{_countryPath}/";
            var items = _context.Nodes.Select(path);

            // Act.
            var count = items.Count();

            // Assert.
            Assert.Equal(regions, count);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Count_With_Multiple_Items_50_Multiple_requests_20()
        {
            // Arrange.
            const int regions = 50;
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, regions).ConfigureAwait(false);
            var path = $"{_countryPath}/";
            var items = _context.Nodes.Select(path);
            const int iterations = 20;
            var counts = new int[iterations];

            // Act.
            for (var i = 0; i < iterations; i++)
            {
                counts[i] = items.Count();
            }

            // Assert.
            for (var i = 0; i < iterations; i++)
            {
                Assert.Equal(regions, counts[i]);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Any()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var locationRoot = await _logicalContext.Roots.Get("Location").ConfigureAwait(false);
            var locationEntry = await _logicalContext.Nodes.Select(GraphPath.Create(locationRoot.Identifier), executionScope).ConfigureAwait(false);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)locationEntry, "Europe", "NL", "Overijssel").ConfigureAwait(false);
            var path = $"/Location/Europe/NL/Overijssel";
            var items = _context.Nodes.Select(path);

            // Act.
            var any = items.Any();

            // Assert.
            Assert.True(any);
        }
    }
}
