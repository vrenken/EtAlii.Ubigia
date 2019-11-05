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

    public class LinqQueryContextNodesAddAddTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private readonly LogicalUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private ILinqQueryContext _context;

        private string _countryPath;
        private LinqQueryContextConfiguration _configuration;

        public LinqQueryContextNodesAddAddTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _diagnostics = TestDiagnostics.Create();
            
            _configuration = new LinqQueryContextConfiguration()
                .UseFunctionalDiagnostics(_diagnostics);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(_configuration,true);

            _logicalContext = new LogicalContextFactory().Create(_configuration); // Hmz, I'm not so sure about this action.
            _context = new LinqQueryContextFactory().Create(_configuration);
                
            var addResult = await _testContext.LogicalTestContext.AddContinentCountry(_logicalContext);
            _countryPath = addResult.Path;

            Console.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            await _configuration.Connection.Close();
            _configuration = null;
            _countryPath = null;
            _context.Dispose();
            _context = null;
            _logicalContext.Dispose();
            _logicalContext = null;
            _diagnostics = null;

            Console.WriteLine("DataContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
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
