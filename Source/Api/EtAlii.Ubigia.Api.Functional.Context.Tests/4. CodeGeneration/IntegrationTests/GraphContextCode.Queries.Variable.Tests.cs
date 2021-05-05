namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;
    using Xunit.Abstractions;
    using EtAlii.Ubigia.Api.Functional.Context.Tests.Model;

    public class GraphContextCodeQueriesVariableTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private ITraversalContext _traversalContext;
        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private GraphContextConfiguration _configuration;

        public GraphContextCodeQueriesVariableTests(QueryingUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _configuration = new GraphContextConfiguration()
                .UseTestTraversalParser()
                .UseTestContextParser()
                .UseFunctionalGraphContextDiagnostics(_testContext.FunctionalTestContext.Diagnostics);
            await _testContext.FunctionalTestContext.ConfigureLogicalContextConfiguration(_configuration,true).ConfigureAwait(false);

            _traversalContext = new TraversalContextFactory().Create(_configuration);

            await _testContext.FunctionalTestContext.AddPeople(_traversalContext).ConfigureAwait(false);
            await _testContext.FunctionalTestContext.AddAddresses(_traversalContext).ConfigureAwait(false);

            _testOutputHelper.WriteLine("{1}.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphContext));
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            await _configuration.Connection.Close().ConfigureAwait(false);
            _configuration = null;
            _traversalContext = null;

            _testOutputHelper.WriteLine("{1}.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphContext));
        }


        [Fact]
        public async Task GraphContextCodeQueries_Query_Person_By_Variables()
        {
            // Arrange.
            var processor = new TestSchemaProcessorFactory();
            var parser = new TestSchemaParserFactory();
            var context = new GraphContext(processor, parser, _traversalContext);

            // Act.
            var person = await context
                .ProcessPersonByFirstNameLastName("Stark", "Tony")
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(person);

            Assert.Equal("Tony", person.FirstName);
            Assert.Equal("Stark", person.LastName);
        }
    }
}
