namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Tests;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;
    using TestAssembly = EtAlii.Ubigia.Api.Tests.TestAssembly;

    
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
            var addResult = await _testContext.LogicalTestContext.AddContinentCountry(logicalContext);
            var countryPath = addResult.Path;
            var countryEntry = addResult.Entry;
            await _testContext.LogicalTestContext.AddRegions(logicalContext, countryEntry, 1);
            var path = $"{countryPath}/";
            var configuration = new DataContextConfiguration()
                                .Use(logicalContext)
                                .Use(_testContext.Diagnostics);
            var context = new DataContextFactory().Create(configuration);
            var items = context.Nodes.Select(path);

            // Act.
            var single = items.Cast<NamedObject>().ToArray();

            // Assert.
            Assert.Equal("Overijssel_01", single[0].Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Cast_ToArray_With_Multiple_Item()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var addResult = await _testContext.LogicalTestContext.AddContinentCountry(logicalContext);
            var countryPath = addResult.Path;
            var countryEntry = addResult.Entry;
            await _testContext.LogicalTestContext.AddRegions(logicalContext, countryEntry, 2);
            var path = $"{countryPath}/";
            var configuration = new DataContextConfiguration()
                .Use(_testContext.Diagnostics)
                .Use(logicalContext);
            var context = new DataContextFactory().Create(configuration);
            var items = context.Nodes.Select(path);

            // Act.
            var single = items.Cast<NamedObject>().ToArray();

            // Assert.
            Assert.Equal("Overijssel_01", single[0].Type);
            Assert.Equal("Overijssel_02", single[1].Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_ToArray_With_Multiple_Item()
        {
            // Arrange.
            var logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
            var addResult = await _testContext.LogicalTestContext.AddContinentCountry(logicalContext);
            var countryPath = addResult.Path;
            var countryEntry = addResult.Entry;
            await _testContext.LogicalTestContext.AddRegions(logicalContext, countryEntry, 2);
            var path = $"{countryPath}/";
            var configuration = new DataContextConfiguration()
                .Use(_testContext.Diagnostics)
                .Use(logicalContext);
            var context = new DataContextFactory().Create(configuration);
            var items = context.Nodes.Select(path);

            // Act.
            dynamic single = items.ToArray();

            // Assert.
            Assert.Equal("Overijssel_01", single[0].ToString());
            Assert.Equal("Overijssel_02", single[1].ToString());
        }
    }
}
