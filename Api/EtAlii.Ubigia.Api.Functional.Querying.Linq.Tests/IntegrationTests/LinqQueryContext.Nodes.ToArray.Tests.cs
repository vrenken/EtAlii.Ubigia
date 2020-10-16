namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting.Tests;
    using EtAlii.Ubigia.Api.Logical;
    using Xunit;

    public class LinqQueryContextNodesToArrayTests : IClassFixture<QueryingUnitTestContext>, IDisposable
    {
        private readonly QueryingUnitTestContext _testContext;

        public LinqQueryContextNodesToArrayTests(QueryingUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public void Dispose()
        {
            // Dispose any relevant resources.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Cast_ToArray_With_Single_Item()
        {
            // Arrange.
            var configuration = new LinqQueryContextConfiguration()
                .UseFunctionalDiagnostics(_testContext.Diagnostics);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration,true);
            var logicalContext = new LogicalContextFactory().Create(configuration); // Hmz, I'm not so sure about this action.
            var context = new LinqQueryContextFactory().Create(configuration);

            var addResult = await _testContext.LogicalTestContext.AddContinentCountry(logicalContext);
            var countryPath = addResult.Path;
            var countryEntry = addResult.Entry;
            await _testContext.LogicalTestContext.AddRegions(logicalContext, countryEntry, 1);
            var path = $"{countryPath}/";

            var items = context.Nodes.Select(path);

            // Act.
            var single = items.Cast<NamedObject>().ToArray();

            // Assert.
            Assert.Equal("Overijssel_01", single[0].Type);
            
            // Assure.
            await configuration.Connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_Cast_ToArray_With_Multiple_Item()
        {
            // Arrange.
            var configuration = new LinqQueryContextConfiguration()
                .UseFunctionalDiagnostics(_testContext.Diagnostics);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration,true);
            var logicalContext = new LogicalContextFactory().Create(configuration); // Hmz, I'm not so sure about this action.
            var context = new LinqQueryContextFactory().Create(configuration);

            var addResult = await _testContext.LogicalTestContext.AddContinentCountry(logicalContext);
            var countryPath = addResult.Path;
            var countryEntry = addResult.Entry;
            await _testContext.LogicalTestContext.AddRegions(logicalContext, countryEntry, 2);
            var path = $"{countryPath}/";
            var items = context.Nodes.Select(path);

            // Act.
            var single = items.Cast<NamedObject>().ToArray();

            // Assert.
            Assert.Equal("Overijssel_01", single[0].Type);
            Assert.Equal("Overijssel_02", single[1].Type);
                        
            // Assure.
            await configuration.Connection.Close();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Linq_Nodes_Select_ToArray_With_Multiple_Item()
        {
            // Arrange.
            var configuration = new LinqQueryContextConfiguration()
                .UseFunctionalDiagnostics(_testContext.Diagnostics);
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration,true);
            var logicalContext = new LogicalContextFactory().Create(configuration); // Hmz, I'm not so sure about this action.
            var context = new LinqQueryContextFactory().Create(configuration);

            var addResult = await _testContext.LogicalTestContext.AddContinentCountry(logicalContext);
            var countryPath = addResult.Path;
            var countryEntry = addResult.Entry;
            await _testContext.LogicalTestContext.AddRegions(logicalContext, countryEntry, 2);
            var path = $"{countryPath}/";
            var items = context.Nodes.Select(path);

            // Act.
            dynamic single = items.ToArray();

            // Assert.
            Assert.Equal("Overijssel_01", single[0].ToString());
            Assert.Equal("Overijssel_02", single[1].ToString());
                        
            // Assure.
            await configuration.Connection.Close();
        }
    }
}
