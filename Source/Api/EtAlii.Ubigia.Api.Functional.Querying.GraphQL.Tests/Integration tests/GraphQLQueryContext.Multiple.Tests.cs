namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using GraphQL;
    using GraphQL.NewtonsoftJson;
    using Xunit;
    using Xunit.Abstractions;

    public class GraphQLQueryContextMultipleTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private ITraversalScriptContext _scriptContext;
        private IGraphQLQueryContext _queryContext;

        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IDocumentWriter _documentWriter;
        private GraphQLQueryContextConfiguration _configuration;

        public GraphQLQueryContextMultipleTests(QueryingUnitTestContext testContext, ITestOutputHelper testOutputHelper)
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
        public async Task GraphQL_Query_Traverse_Person_Multiple_01()
        {
            // Arrange.
            var queryText = @"
                query data
                {
                    person @nodes(path:""/person/*/*"")
                    {
                        nickName
                    }
                }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"
                {
                    'person':
                    [
                        { 'nickName':'Johnny' },
                        { 'nickName':'Janey' },
                        { 'nickName':'Iron Man' },
                        { 'nickName':'Pete' },
                        { 'nickName':'LadyL' },
                        { 'nickName':'Bengel' },
                        { 'nickName':'Scheetje' }
                    ]
                }", result).ConfigureAwait(false);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Multiple_With_Nested_Id()
        {
            // Arrange.
            var queryText = @"
                query data
                {
                    person @nodes(path:""/person/*/*"")
                    {
                        firstname @id
                        nickName
                    }
                }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"
                {
                    'person':
                    [
                        { 'firstname':'John', 'nickName':'Johnny' },
                        { 'firstname':'Jane', 'nickName':'Janey' },
                        { 'firstname':'Tony', 'nickName':'Iron Man' },
                        { 'firstname':'Peter', 'nickName':'Pete' },
                        { 'firstname':'Tanja', 'nickName':'LadyL' },
                        { 'firstname':'Arjan', 'nickName':'Bengel' },
                        { 'firstname':'Ida', 'nickName':'Scheetje' }
                    ]
                }", result).ConfigureAwait(false);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Multiple_02()
        {
            // Arrange.
            var queryText = @"
                query data
                {
                    person @nodes(path:""/person/Doe/*"")
                    {
                        nickName
                    }
                }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ 'person': [{ 'nickName': 'Johnny'} , { 'nickName': 'Janey'} ]}", result).ConfigureAwait(false);
        }

        [Fact(Skip = "Root data queries are not yet supported"), Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Plural_01()
        {
            // Arrange.
            var queryText = @"
                query data @nodes(path:""person:Doe/*"")
                {
                    person
                    {
                        nickName
                    }
                }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ 'person': [{ 'nickName': 'Johnny'} , { 'nickName': 'Janey'} ]}", result).ConfigureAwait(false);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Multiple_Friends()
        {
            // Arrange.
            var queryText = @"
                query data
                {
                    person @nodes(path:""person:Stark/Tony"")
                    {
                        name @id,
                        friends @nodes(path:""/Friends/#FirstName"")
                        {
                            name @id
                            nickName
                            lastname @id(path:""\\#FamilyName"")
                    }
                    }
                }";

            // Act.
                var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"
                {
                    'person':
                    {
                        'name':'Tony',
                        'friends':
                        [
                            {'name':'John','nickName':'Johnny', 'lastname': 'Doe'},
                            {'name':'Jane','nickName':'Janey', 'lastname': 'Doe'},
                            {'name':'Peter','nickName':'Pete', 'lastname': 'Banner'}
                        ]
                    }
                }", result).ConfigureAwait(false);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Plural_02()
        {
            // Arrange.
            var queryText = @"
                query data
                {
                    #location @nodes(path:""location:DE/Berlin//"", mode: ""Intersect"")
                    #time @nodes(path:""time:2012//"")
                    person @nodes(path:""person:Banner/Peter"")
                    {
                        nickname
                        firstname @id
                        lastname @id(path:""\\#FamilyName"")
                    }
                }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ 'person': { 'nickname': 'Pete', 'firstname': 'Peter', 'lastname': 'Banner'} }", result).ConfigureAwait(false);

        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Plural_03()
        {
            // Arrange.
            var queryText = @"
                query data
                {
                    #location @nodes(path:""location:DE/Berlin//"", mode: ""Intersect"")
                    #time @nodes(path:""time:2012//"")
                    person @nodes(path:""person:Banner/*"")
                    {
                        nickName
                        firstname @id
                        lastname @id(path:""\\#FamilyName"")
                    }
                }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"
                {
                    'person':
                    [
                        {'nickName':'Pete','firstname':'Peter','lastname':'Banner'},
                        {'nickName':'LadyL','firstname':'Tanja','lastname':'Banner'},
                        {'nickName':'Bengel','firstname':'Arjan','lastname':'Banner'},
                        {'nickName':'Scheetje','firstname':'Ida','lastname':'Banner'}
                    ]
                }", result).ConfigureAwait(false);
        }
    }
}
