namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    

    
    public class LinqQueryContextNodesSelectTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private ILinqQueryContext _context;
        private string _countryPath;
        private IEditableEntry _countryEntry;
        private readonly LogicalUnitTestContext _testContext;

        public LinqQueryContextNodesSelectTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _diagnostics = TestDiagnostics.Create();
            _logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var configuration = new LinqQueryContextConfiguration()
                .Use(_diagnostics)
                .Use(_logicalContext);
            _context = new LinqQueryContextFactory().Create(configuration);
            var addResult = await _testContext.LogicalTestContext.AddContinentCountry(_logicalContext);
            _countryPath = addResult.Path;
            _countryEntry = addResult.Entry;

            Console.WriteLine("LinqContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            _countryEntry = null;
            _countryPath = null;
            _context.Dispose();
            _context = null;
            _logicalContext.Dispose();
            _logicalContext = null;
            _diagnostics = null;

            Console.WriteLine("LinqContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);

            await Task.CompletedTask;
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
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, 1);
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
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, 1);
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
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, 2);
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
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, 1);
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
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, regions);
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
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, regions);
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
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, regions);
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
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, regions);
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
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, regions);
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
            await _testContext.LogicalTestContext.AddRegions(_logicalContext, _countryEntry, regions);
            var path = $"{_countryPath}/";
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
                Assert.Equal(regions, counts[i]);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Any()
        {
            // Arrange.
            var executionScope = new ExecutionScope(false);
            var locationRoot = await _logicalContext.Roots.Get("Location");
            var locationEntry = await _logicalContext.Nodes.Select(GraphPath.Create(locationRoot.Identifier), executionScope);
            await _testContext.LogicalTestContext.CreateHierarchy(_logicalContext, (IEditableEntry)locationEntry, "Europe", "NL", "Overijssel");
            var path = $"/Location/Europe/NL/Overijssel";
            var items = _context.Nodes.Select(path);

            // Act.
            var any = items.Any();

            // Assert.
            Assert.True(any);
        }
    }
}
