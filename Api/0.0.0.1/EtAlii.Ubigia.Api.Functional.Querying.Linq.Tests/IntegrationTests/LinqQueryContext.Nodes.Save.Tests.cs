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

    public class LinqQueryContextNodesSaveTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private readonly LogicalUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private ILinqQueryContext _context;
        private string _countryPath;

        public LinqQueryContextNodesSaveTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
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

        public Task DisposeAsync()
        {
            var start = Environment.TickCount;

            _countryPath = null;
            _context.Dispose();
            _context = null;
            _logicalContext.Dispose();
            _logicalContext = null;
            _diagnostics = null;

            Console.WriteLine("LinqContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            return Task.CompletedTask;
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
            await _context.Nodes.Save(single);
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
            await _context.Nodes.Save(single);
            var newId = ((INode)single).Id;

            // Assert.
            Assert.NotEqual(originalId, newId);
        }
    }
}
