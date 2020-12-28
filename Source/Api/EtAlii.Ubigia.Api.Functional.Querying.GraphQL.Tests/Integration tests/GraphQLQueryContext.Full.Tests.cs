namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using GraphQL;
    using GraphQL.NewtonsoftJson;
    using Xunit;
    using Xunit.Abstractions;

    public class GraphQLQueryContextFullTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private ITraversalScriptContext _scriptContext;
        private IGraphQLQueryContext _queryContext;

        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IDocumentWriter _documentWriter;
        private GraphQLQueryContextConfiguration _configuration;

        public GraphQLQueryContextFullTests(QueryingUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
            _documentWriter = new DocumentWriter(indent: false);
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _configuration = new GraphQLQueryContextConfiguration()
                .UseTestParser()
                .UseFunctionalGraphQLDiagnostics(_testContext.FunctionalTestContext.Diagnostics);
            await _testContext.FunctionalTestContext.ConfigureLogicalContextConfiguration(_configuration,true).ConfigureAwait(false);

            _scriptContext = new TraversalScriptContextFactory().Create(_configuration);
            _queryContext = new GraphQLQueryContextFactory().Create(_configuration);

            await _testContext.FunctionalTestContext.AddPeople(_scriptContext).ConfigureAwait(false);
            await _testContext.FunctionalTestContext.AddAddresses(_scriptContext).ConfigureAwait(false);

            _testOutputHelper.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            await _configuration.Connection.Close().ConfigureAwait(false);
            _configuration = null;
            _scriptContext = null;
            _queryContext = null;

            _testOutputHelper.WriteLine("DataContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Single()
        {
            // Arrange.
            var queryText = @"
                query data @nodes(path:""/person/Stark/"")
                {
                    person @nodes(path:""Tony"")
                    {
                        nickname
                    }
                }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ ""person"": { ""nickname"": ""Iron Man"" }}", result).ConfigureAwait(false);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Single_Nested_01()
        {
            // Arrange.
            var queryText = @"
                query data
                {
                    data2 @nodes(path:""/person/Stark/"")
                    {
                        person @nodes(path:""Tony"")
                        {
                            nickname
                        }
                    }

                }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ ""data2"": { ""person"": { ""nickname"": ""Iron Man"" }}}", result).ConfigureAwait(false);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Single_Nested_02()
        {
            // Arrange.
            var queryText = @"
                query data
                {
                    stark @nodes(path:""/person/Stark/"")
                    {
                        tony @nodes(path:""Tony"")
                        {
                            nickname
                        }
                    }

                }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ ""stark"": { ""tony"": { ""nickname"": ""Iron Man"" }}}", result).ConfigureAwait(false);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Multiple_Nested_01()
        {
            // Arrange.
            var queryText = @"
                query data
                {
                    data2 @nodes(path:""/person"")
                    {
                        person1 @nodes(path:""/Stark/Tony"")
                        {
                            nickname
                        }
                        person2 @nodes(path:""/Doe/John"")
                        {
                            nickname
                        }
                    }
                }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ ""data2"": { ""person1"": { ""nickname"": ""Iron Man"" }, ""person2"": { ""nickname"": ""Johnny"" }}}", result).ConfigureAwait(false);
        }
    }
}
