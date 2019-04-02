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

    public class LinqQueryContextNodesAddAddTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private readonly LogicalUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private ILinqQueryContext _context;

        private string _countryPath;

        public LinqQueryContextNodesAddAddTests(LogicalUnitTestContext testContext)
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

            Console.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public Task DisposeAsync()
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

        [Fact(Skip="Not working yet"), Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_At()
        {
            // Arrange.
            var items = _context.Nodes.Select(_countryPath);

            // Act.
            dynamic single = items.Add("Overijssel_01").At(DateTime.Now).Single();

            // Assert.
            Assert.Equal("Overijssel_01", single.Label);
        }

        [Fact(Skip = "Not working yet"), Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_At_Cast()
        {
            // Arrange.
            var items = _context.Nodes.Select(_countryPath);

            // Act.
            var single = items.Add("Overijssel_01").At(DateTime.Now).Cast<NamedObject>().Single();

            // Assert.
            Assert.Equal("Overijssel_01", single.Type);
        }
    }
}
