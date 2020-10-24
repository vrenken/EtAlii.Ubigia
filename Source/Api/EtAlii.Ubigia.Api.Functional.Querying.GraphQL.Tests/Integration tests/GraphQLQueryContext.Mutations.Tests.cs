﻿namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using GraphQL.Http;
    using Xunit;
    using Xunit.Abstractions;

    public class GraphQLQueryContextMutationsTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private IGraphSLScriptContext _scriptContext;
        private IGraphQLQueryContext _queryContext;
        
        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IDocumentWriter _documentWriter;
        private GraphQLQueryContextConfiguration _configuration;

        public GraphQLQueryContextMutationsTests(QueryingUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
            _documentWriter = new DocumentWriter(indent: false);
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _configuration = new GraphQLQueryContextConfiguration()
                .UseFunctionalGraphQLDiagnostics(_testContext.FunctionalTestContext.Diagnostics)
                .UseFunctionalGraphSLDiagnostics(_testContext.FunctionalTestContext.Diagnostics);
            await _testContext.FunctionalTestContext.ConfigureLogicalContextConfiguration(_configuration,true);
            
            _scriptContext = new GraphSLScriptContextFactory().Create(_configuration);
            _queryContext = new GraphQLQueryContextFactory().Create(_configuration);
        
            await _testContext.FunctionalTestContext.AddPeople(_scriptContext);
            await _testContext.FunctionalTestContext.AddAddresses(_scriptContext);

            _testOutputHelper.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            await _configuration.Connection.Close();
            _configuration = null;
            _scriptContext = null;
            _queryContext = null;

            _testOutputHelper.WriteLine("DataContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact(Skip = "Development of mutations is pending"), Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Mutation_Single()
        {
            // Arrange.
            var queryText = @"
                mutation data 
                { 
                    person @node(path:""person:Stark/ += Tony"")
                    { 
                        nickname 
                    }
                }";
            
            // Act.
            var parseResult = await _queryContext.Parse(queryText);
            var result = await _queryContext.Process(parseResult.Query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ ""person"": { ""nickname"": ""Iron Man"" }}", result);
        }
    }
}