namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Tests;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    using TestAssembly = EtAlii.Ubigia.Api.Tests.TestAssembly;

    
    public partial class DataContextNodesSaveTests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private IDataContext _context;
        private string _countryPath;
        private readonly LogicalUnitTestContext _testContext;

        public DataContextNodesSaveTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
            var task = Task.Run(async () =>
            {
                var start = Environment.TickCount;

                _diagnostics = TestDiagnostics.Create();
                _logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
                var configuration = new DataContextConfiguration()
                    .Use(_diagnostics)
                    .Use(_logicalContext);
                _context = new DataContextFactory().Create(configuration);
                var addResult = await _testContext.LogicalTestContext.AddContinentCountry(_logicalContext);
                _countryPath = addResult.Path;

                Console.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(() =>
            {
                var start = Environment.TickCount;

                _countryPath = null;
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
