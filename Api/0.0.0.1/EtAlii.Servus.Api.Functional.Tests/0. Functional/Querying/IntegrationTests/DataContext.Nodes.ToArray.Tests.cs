namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Diagnostics.Tests;
    using EtAlii.Servus.Api.Logical.Tests;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    using TestAssembly = EtAlii.Servus.Api.Tests.TestAssembly;

    
    public partial class DataContext_Nodes_ToArray_Tests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private readonly LogicalUnitTestContext _testContext;

        public DataContext_Nodes_ToArray_Tests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public void Dispose()
        {
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Cast_ToArray_With_Single_Item()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var addResult = await _testContext.LogicalTestContext.AddYearMonth(logicalContext);
            var monthPath = addResult.Path;
            var monthEntry = addResult.Entry;
            await _testContext.LogicalTestContext.AddDays(logicalContext, monthEntry, 1);
            var path = String.Format("{0}/", monthPath);
            var configuration = new DataContextConfiguration()
                                .Use(logicalContext)
                                .Use(_testContext.Diagnostics);
            var context = new DataContextFactory().Create(configuration);
            var items = context.Nodes.Select(path);

            // Act.
            var single = items.Cast<NamedObject>().ToArray();

            // Assert.
            Assert.Equal("01", single[0].Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Cast_ToArray_With_Multiple_Item()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var addResult = await _testContext.LogicalTestContext.AddYearMonth(logicalContext);
            var monthPath = addResult.Path;
            var monthEntry = addResult.Entry;
            await _testContext.LogicalTestContext.AddDays(logicalContext, monthEntry, 2);
            var path = String.Format("{0}/", monthPath);
            var configuration = new DataContextConfiguration()
                .Use(_testContext.Diagnostics)
                .Use(logicalContext);
            var context = new DataContextFactory().Create(configuration);
            var items = context.Nodes.Select(path);

            // Act.
            var single = items.Cast<NamedObject>().ToArray();

            // Assert.
            Assert.Equal("01", single[0].Type);
            Assert.Equal("02", single[1].Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_ToArray_With_Multiple_Item()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var addResult = await _testContext.LogicalTestContext.AddYearMonth(logicalContext);
            var monthPath = addResult.Path;
            var monthEntry = addResult.Entry;
            await _testContext.LogicalTestContext.AddDays(logicalContext, monthEntry, 2);
            var path = String.Format("{0}/", monthPath);
            var configuration = new DataContextConfiguration()
                .Use(_testContext.Diagnostics)
                .Use(logicalContext);
            var context = new DataContextFactory().Create(configuration);
            var items = context.Nodes.Select(path);

            // Act.
            dynamic single = items.ToArray();

            // Assert.
            Assert.Equal("01", single[0].ToString());
            Assert.Equal("02", single[1].ToString());
        }
    }
}
