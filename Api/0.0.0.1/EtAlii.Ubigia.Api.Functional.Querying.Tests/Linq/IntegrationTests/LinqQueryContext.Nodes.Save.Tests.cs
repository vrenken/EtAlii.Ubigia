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
    using Xunit.Abstractions;


    public class LinqQueryContextNodesSaveTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime 
    {
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private ILinqQueryContext _context;
        private string _countryPath;
        private readonly ITestOutputHelper _output;
        private readonly LogicalUnitTestContext _testContext;

        public LinqQueryContextNodesSaveTests(ITestOutputHelper output, LogicalUnitTestContext testContext)
        {
            _output = output;
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

            _output.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            _countryPath = null;
            _context.Dispose();
            _context = null;
            _logicalContext.Dispose();
            _logicalContext = null;
            _diagnostics = null;

            _output.WriteLine("LinqContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);

            await Task.CompletedTask;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Save_Check_IsModified()
        {
            // Arrange.
            var items = _context.Nodes.Select(_countryPath);
            var value = new Random().Next();
            var single = items.Add("Overijssel_01").Cast<NamedObject>().Single();

            // Act.
            single.Value = value;
            var wasModified = ((INode)single).IsModified;
            _context.Nodes.Save(single);
            var isModified = ((INode)single).IsModified;

            // Assert.
            Assert.True(wasModified);
            Assert.False(isModified);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Linq_Nodes_Select_Add_Single_Save_Check_Id()
        {
            // Arrange.
            var items = _context.Nodes.Select(_countryPath);
            var value = new Random().Next();
            var single = items.Add("Overijssel_01").Cast<NamedObject>().Single();
            var originalId = ((INode)single).Id;

            // Act.
            single.Value = value;
            _context.Nodes.Save(single);
            var newId = ((INode)single).Id;

            // Assert.
            Assert.NotEqual(originalId, newId);
        }
    }
}
