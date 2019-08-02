namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using GraphQL.Http;
    using Xunit;

    public class GraphQLQueryContextMultipleTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private IGraphSLScriptContext _scriptContext;
        private IGraphQLQueryContext _queryContext;
        
        private readonly QueryingUnitTestContext _testContext;
        private readonly IDocumentWriter _documentWriter;

        public GraphQLQueryContextMultipleTests(QueryingUnitTestContext testContext)
        {
            _testContext = testContext;
            _documentWriter = new DocumentWriter(indent: false);
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            var configuration = new GraphQLQueryContextConfiguration()
                .UseFunctionalGraphQLDiagnostics(_testContext.FunctionalTestContext.Diagnostics)
                .UseFunctionalGraphSLDiagnostics(_testContext.FunctionalTestContext.Diagnostics);
            await _testContext.FunctionalTestContext.ConfigureLogicalContextConfiguration(configuration,true);
            
            _scriptContext = new GraphSLScriptContextFactory().Create(configuration);
            _queryContext = new GraphQLQueryContextFactory().Create(configuration);

            await _testContext.FunctionalTestContext.AddPeople(_scriptContext);
            await _testContext.FunctionalTestContext.AddAddresses(_scriptContext);

            Console.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public Task DisposeAsync()
        {
            var start = Environment.TickCount;

            _scriptContext = null;
            _queryContext = null;

            Console.WriteLine("DataContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);

            return Task.CompletedTask;
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
            var parseResult = await _queryContext.Parse(queryText);
            var result = await _queryContext.Process(parseResult.Query);
            
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
                }", result);
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
            var parseResult = await _queryContext.Parse(queryText);
            var result = await _queryContext.Process(parseResult.Query);
            
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
                }", result);
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
            var parseResult = await _queryContext.Parse(queryText);
            var result = await _queryContext.Process(parseResult.Query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ 'person': [{ 'nickName': 'Johnny'} , { 'nickName': 'Janey'} ]}", result);
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
            var parseResult = await _queryContext.Parse(queryText);
            var result = await _queryContext.Process(parseResult.Query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ 'person': [{ 'nickName': 'Johnny'} , { 'nickName': 'Janey'} ]}", result);
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
                var parseResult = await _queryContext.Parse(queryText);
            var result = await _queryContext.Process(parseResult.Query);
                
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
                            {'name':'Peter','nickName':'Pete', 'lastname': 'Vrenken'}
                        ]
                    }
                }", result);
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
                    person @nodes(path:""person:Vrenken/Peter"")
                    { 
                        nickname
                        firstname @id
                        lastname @id(path:""\\#FamilyName"") 
                    }
                }";
            
            // Act.
            var parseResult = await _queryContext.Parse(queryText);
            var result = await _queryContext.Process(parseResult.Query);
            
            // Assert.    
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ 'person': { 'nickname': 'Pete', 'firstname': 'Peter', 'lastname': 'Vrenken'} }", result);
            
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
                    person @nodes(path:""person:Vrenken/*"")
                    { 
                        nickName
                        firstname @id
                        lastname @id(path:""\\#FamilyName"") 
                    }
                }";
            
            // Act.
            var parseResult = await _queryContext.Parse(queryText);
            var result = await _queryContext.Process(parseResult.Query);
            
            // Assert.    
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"
                {
                    'person':
                    [
                        {'nickName':'Pete','firstname':'Peter','lastname':'Vrenken'},
                        {'nickName':'LadyL','firstname':'Tanja','lastname':'Vrenken'},
                        {'nickName':'Bengel','firstname':'Arjan','lastname':'Vrenken'},
                        {'nickName':'Scheetje','firstname':'Ida','lastname':'Vrenken'}
                    ]
                }", result);
            
        }
    }
}
