namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using GraphQL;
    using GraphQL.NewtonsoftJson;
    using Xunit;
    using Xunit.Abstractions;

    public class GraphQLQueryContextBasicTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private ITraversalContext _traversalContext;
        private IGraphQLQueryContext _queryContext;

        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IDocumentWriter _documentWriter;
        private GraphQLQueryContextConfiguration _configuration;

        public GraphQLQueryContextBasicTests(QueryingUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
            _documentWriter = new DocumentWriter(indent: false);

        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _configuration = new GraphQLQueryContextConfiguration()
                .UseTestTraversalParser()
                .UseFunctionalGraphQLDiagnostics(_testContext.FunctionalTestContext.Diagnostics);
            await _testContext.FunctionalTestContext.ConfigureLogicalContextConfiguration(_configuration,true).ConfigureAwait(false);

            _traversalContext = new TraversalContextFactory().Create(_configuration);
            _queryContext = new GraphQLQueryContextFactory().Create(_configuration);

            await _testContext.FunctionalTestContext.AddPeople(_traversalContext).ConfigureAwait(false);
            await _testContext.FunctionalTestContext.AddAddresses(_traversalContext).ConfigureAwait(false);

            _testOutputHelper.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            await _configuration.Connection.Close().ConfigureAwait(false);
            _configuration = null;
            _traversalContext = null;
            _queryContext = null;

            _testOutputHelper.WriteLine("DataContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Theory, ClassData(typeof(FileBasedGraphQLData))]
        public async Task GraphQL_Query_From_Files_Execute(string fileName, string title, string queryText)
        {
            // Arrange.

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            Assert.Empty(parseResult.Errors);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result);
            Assert.NotNull(title);
            Assert.NotNull(fileName);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Full_Local()
        {
            // Arrange.
            var queryText = @"query data { person @nodes(path:""person:Stark/Tony"") { firstname, lastname, nickname, birthdate, lives } }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Full_Relative()
        {
            // Arrange.
            var queryText = @"query data { person @nodes(path:""person:Stark/Tony"") { firstname @id(path:""""), lastname @id(path:""\\#FamilyName""), nickname, birthdate, lives } }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""firstname"": ""Tony"", ""lastname"": ""Stark"", ""nickname"": ""Iron Man"", ""birthdate"": ""1976-05-12"", ""lives"": 9 }}", result).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Relative_01()
        {
            // Arrange.
            var queryText = @"query data { person @nodes(path:""person:Stark/Tony"") { lastname @id(path:""\\#FamilyName"") } }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""lastname"": ""Stark"" }}", result).ConfigureAwait(false);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Relative_Twice()
        {
            // Arrange.
            var queryText = @"query data { person @nodes(path:""person:Stark/Tony"") { lastname @id(path:""\\#FamilyName"") } }";
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);

            // Act.
            var result1 = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);
            var result2 = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result1.Errors);
            Assert.Null(result2.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""lastname"": ""Stark"" }}", result1).ConfigureAwait(false);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""lastname"": ""Stark"" }}", result2).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Relative_02()
        {
            // Arrange.
            var queryText = @"query data { person @nodes(path:""person:Stark/Tony"") { firstname @id(path:"""") } }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""firstname"": ""Tony"" }}", result).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Local_1()
        {
            // Arrange.
            var queryText = @"query data { person @nodes(path:""person:Stark/Tony"") { firstname } }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result.Errors);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Local_2()
        {
            // Arrange.
            var queryText = @"query data { person @nodes(path:""person:Stark/Tony"") { nickname } }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""nickname"": ""Iron Man"" }}", result).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Local_3()
        {
            // Arrange.
            var queryText = @"query data { person @nodes(path:""person:Doe/John"") { nickname } }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""nickname"": ""Johnny"" }}", result).ConfigureAwait(false);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Relative()
        {
            // Arrange.
            var queryText = @"
                query data
                {
                    person @nodes(path:""person:Stark/Tony"")
                    {
                        firstname @id(path:"""")
                    }
                }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""firstname"": ""Tony"" }}", result).ConfigureAwait(false);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_Integer()
        {
            // Arrange.
            var queryText = @"query data { person @nodes(path:""person:Stark/Tony"") { lives } }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""lives"": 9 }}", result).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_01()
        {
            // Arrange.
            var queryText = @"query data { person @nodes(path:""person:Stark/Tony"") { nickname } }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""nickname"": ""Iron Man"" }}", result).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_04()
        {
            // Arrange.
            var queryText = @"query data
                          {
                            person @nodes(path:""person:Stark/Tony"")
                            {
                                nickname
                            }
                          }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""nickname"": ""Iron Man"" }}", result).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_And_Non_Argumented_Id()
        {
            // Arrange.
            var queryText = @"query data
                          {
                            person @nodes(path:""person:Stark/Tony"")
                            {
                                firstname @id
                                nickname
                            }
                          }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""firstname"": ""Tony"", ""nickname"": ""Iron Man"" }}", result).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_And_Argumented_Id()
        {
            // Arrange.
            var queryText = @"query data
                          {
                            person @nodes(path:""person:Stark/Tony"")
                            {
                                lastname @id(path:""\\#FamilyName"")
                                nickname
                            }
                          }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""lastname"": ""Stark"", ""nickname"": ""Iron Man"" }}", result).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_02()
        {
            // Arrange.
            var queryText = "query data { person\n@nodes(path:\"person:Stark/Tony\") { nickname } }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""nickname"": ""Iron Man"" }}", result).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_03()
        {
            // Arrange.
            var queryText = "query data\n{\nperson\n@nodes(path:\"person:Stark/Tony\")\n{\nnickname\n}\n}";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""nickname"": ""Iron Man"" }}", result).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Multiple_Starts_String()
        {
            // Arrange.
            var queryText = "query data\n{\nperson\n@nodes(path:\"person:Stark/Tony\")\n@nodes(path:\"person:Stark/Tony\")\n{\nnickname\n}\n}";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Droid()
        {
            // Arrange.
            var queryText = @"query data { droid(id: ""4"") { id, name } }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_Temp()
        {
            // Arrange.
            var queryText = @"query data { person @nodes(path:""person:Stark/Tony"") { id, nickname } }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.NotNull(result.Errors);
            await AssertQuery.ResultsAreNotEqual(_documentWriter,@"{ ""person"": { ""id"":""2"", ""nickname"": ""Iron Man"" }}", result).ConfigureAwait(false);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_Date()
        {
            // Arrange.
            var queryText = @"query data { person @nodes(path:""person:Stark/Tony"") { birthdate } }";

            // Act.
            var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
            var result = await _queryContext.Process(parseResult.Query).ConfigureAwait(false);

            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ ""person"": { ""birthdate"": ""1976-05-12"" }}", result).ConfigureAwait(false);
        }
    }
}
