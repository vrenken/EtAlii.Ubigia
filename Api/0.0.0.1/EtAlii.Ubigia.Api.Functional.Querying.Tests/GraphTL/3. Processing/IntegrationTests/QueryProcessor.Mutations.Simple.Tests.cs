namespace EtAlii.Ubigia.Api.Functional.Tests 
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Querying;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

//using EtAlii.Ubigia.Api.Functional.Diagnostics.Querying;

    public class QueryProcessorMutationsSimpleTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private IGraphSLScriptContext _scriptContext;
        private IGraphTLQueryContext _queryContext;
        private readonly QueryingUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;

        public QueryProcessorMutationsSimpleTests(QueryingUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _diagnostics = _testContext.FunctionalTestContext.Diagnostics;
            var configuration = new GraphTLQueryContextConfiguration()
                .UseFunctionalGraphTLDiagnostics(_testContext.FunctionalTestContext.Diagnostics)
                .UseFunctionalGraphSLDiagnostics(_testContext.FunctionalTestContext.Diagnostics);
            await _testContext.FunctionalTestContext.ConfigureLogicalContextConfiguration(configuration,true);
            
            _scriptContext = new GraphSLScriptContextFactory().Create(configuration);
            _queryContext = new GraphTLQueryContextFactory().Create(configuration);
        
            await _testContext.FunctionalTestContext.AddPeople(_scriptContext);
            await _testContext.FunctionalTestContext.AddAddresses(_scriptContext); 

            Console.WriteLine("{1}.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphTLQueryContext));
        }

        public Task DisposeAsync()
        {
            var start = Environment.TickCount;

            _scriptContext = null;
            _queryContext = null;

            Console.WriteLine("{1}.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphTLQueryContext));
            return Task.CompletedTask;
        }

        [Fact]
        public async Task QueryProcessor_Mutate_Person_01()
        {
            // Arrange.
            var mutationText = @"Person @nodes(Person:Doe/John)
                               {
                                    Weight <= 160.1,
                                    NickName <= ""HeavyJohnny""
                               }";
            var mutation = _queryContext.Parse(mutationText).Query;

            var queryText = @"Person @nodes(Person:Doe/John)
                              {
                                    Weight,
                                    NickName
                              }";
            var query = _queryContext.Parse(queryText).Query;

            var scope = new QueryScope();
            var configuration = new QueryProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new QueryProcessorFactory().Create(configuration);

            // Act.
            var mutationResult = await processor.Process(mutation);
            await mutationResult.Output;
            var queryResult = await processor.Process(query);
            await queryResult.Output;

            // Assert.
            var mutationStructure = mutationResult.Structure.Single();
            Assert.NotNull(mutationStructure);
            var queryStructure = queryResult.Structure.Single();
            Assert.NotNull(queryStructure);
        }


        private void AssertValue(object expected, Structure structure, string valueName)
        {
            var value = structure.Values.SingleOrDefault(v => v.Name == valueName);
            Assert.NotNull(value);
            Assert.Equal(expected, value.Object);
            
        }
    }
}