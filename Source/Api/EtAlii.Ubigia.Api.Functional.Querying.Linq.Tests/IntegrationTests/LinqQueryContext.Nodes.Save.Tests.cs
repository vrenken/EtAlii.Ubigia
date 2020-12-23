namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    using Xunit.Abstractions;

    public class LinqQueryContextNodesSaveTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private ILinqQueryContext _context;
        private string _countryPath;
        private LinqQueryContextConfiguration _configuration;

        public LinqQueryContextNodesSaveTests(QueryingUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _diagnostics = DiagnosticsConfiguration.Default;

            _configuration = new LinqQueryContextConfiguration()
                .UseLapaParser()
                .UseFunctionalDiagnostics(_diagnostics);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(_configuration,true).ConfigureAwait(false);

            _logicalContext = new LogicalContextFactory().Create(_configuration); // Hmz, I'm not so sure about this action.
            _context = new LinqQueryContextFactory().Create(_configuration);

            var addResult = await _testContext.LogicalTestContext.AddContinentCountry(_logicalContext).ConfigureAwait(false);
            _countryPath = addResult.Path;

            _testOutputHelper.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task DisposeAsync()
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

            _testOutputHelper.WriteLine("LinqContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Add_Single_Save_Check_IsModified()
        {
            // Arrange.
            var items = _context.Nodes.Select(_countryPath);
            var value = new Random().Next();
            var single = items.Add("Overijssel_01").Cast<NamedObject>().Single();

            // Act.
            single.Value = value;
            var wasModified = ((INode)single).IsModified;
            await _context.Nodes.Save(single).ConfigureAwait(false);
            var isModified = ((INode)single).IsModified;

            // Assert.
            Assert.True(wasModified);
            Assert.False(isModified);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Add_Single_Save_Check_Id()
        {
            // Arrange.
            var items = _context.Nodes.Select(_countryPath);
            var value = new Random().Next();
            var single = items.Add("Overijssel_01").Cast<NamedObject>().Single();
            var originalId = ((INode)single).Id;

            // Act.
            single.Value = value;
            await _context.Nodes.Save(single).ConfigureAwait(false);
            var newId = ((INode)single).Id;

            // Assert.
            Assert.NotEqual(originalId, newId);
        }
    }
}
