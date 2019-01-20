namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using GraphQL.Http;
    using Xunit;


    public class GraphQLQueryContextMultipleTests : IClassFixture<QueryingUnitTestContext>, IDisposable
    {
        private ILogicalContext  _logicalContext;
        private IGraphSLScriptContext _scriptContext;
        private IGraphQLQueryContext _queryContext;
        
        private readonly QueryingUnitTestContext _testContext;
        private readonly IDocumentWriter _documentWriter;

        public GraphQLQueryContextMultipleTests(QueryingUnitTestContext testContext)
        {
            _testContext = testContext;
            _documentWriter = new DocumentWriter(indent: false);
                
            TestInitialize();
        }

        public void Dispose()
        {
            TestCleanup();
        }

        private void TestInitialize()
        {
            var task = Task.Run(async () =>
            {
                var start = Environment.TickCount;

                _logicalContext = await _testContext.FunctionalTestContext.CreateLogicalContext(true);
                _queryContext = _testContext.FunctionalTestContext.CreateGraphQLQueryContext(_logicalContext);
                _scriptContext = _testContext.FunctionalTestContext.CreateGraphSLScriptContext(_logicalContext);
        
                await _testContext.FunctionalTestContext.AddPeople(_scriptContext);
                await _testContext.FunctionalTestContext.AddAddresses(_scriptContext);

                Console.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            });
            task.Wait();
        }

        private void TestCleanup()
        {
            var task = Task.Run(() =>
            {
                var start = Environment.TickCount;

                _logicalContext = null;
                _scriptContext = null;
                _queryContext = null;

                Console.WriteLine("DataContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            });
            task.Wait();
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
                        nickname 
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
                        { 'nickname':'Johnny' },
                        { 'nickname':'Janey' },
                        { 'nickname':'Iron Man' },
                        { 'nickname':'Pete' },
                        { 'nickname':'LadyL' },
                        { 'nickname':'Bengel' },
                        { 'nickname':'Scheetje' }
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
                        nickname 
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
                        { 'firstname':'John', 'nickname':'Johnny' },
                        { 'firstname':'Jane', 'nickname':'Janey' },
                        { 'firstname':'Tony', 'nickname':'Iron Man' },
                        { 'firstname':'Peter', 'nickname':'Pete' },
                        { 'firstname':'Tanja', 'nickname':'LadyL' },
                        { 'firstname':'Arjan', 'nickname':'Bengel' },
                        { 'firstname':'Ida', 'nickname':'Scheetje' }
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
                        nickname 
                    }
                }";
            
            // Act.
            var parseResult = await _queryContext.Parse(queryText);
            var result = await _queryContext.Process(parseResult.Query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ 'person': [{ 'nickname': 'Johnny'} , { 'nickname': 'Janey'} ]}", result);
        }

        [Fact(Skip = "Blocking the build server"), Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Plural_01()
        {
            // Arrange.
            var queryText = @"
                query data @nodes(path:""person:Doe/*"") 
                { 
                    person 
                    { 
                        nickname 
                    } 
                }";
            
            // Act.
            var parseResult = await _queryContext.Parse(queryText);
            var result = await _queryContext.Process(parseResult.Query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ 'person': [{ 'nickname': 'Johnny'} , { 'nickname': 'Janey'} ]}", result);
        }
        
        
        [Fact(Skip = "Blocking the build server"), Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Multiple_Friends()
        {
            // Arrange.
            var queryText = @"
                query data  
                { 
                    person @nodes(path:""person:Stark/Tony"")
                    {
                        name @id,
                        friends @nodes(path:""/Friends/"") 
                        { 
                            name @id
                            nickname
                        } 
                    }
                }";
                    
        // Act.
            var parseResult = await _queryContext.Parse(queryText);
        var result = await _queryContext.Process(parseResult.Query);
            
        // Assert.
        Assert.Null(result.Errors);
//      var actual = await _documentWriter.WriteToStringAsync(result);
        await AssertQuery.ResultsAreEqual(_documentWriter, @"
            {
                'person':
                {
                    'name':'Tony',
                    'friends':
                    [
                        {'name':'John','nickname':'Johnny'},
                        {'name':'Jane','nickname':'Janey'},
                        {'name':'Peter','nickname':'Pete'}
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
                        lastname @id(path:""\\.Type='FamilyName'"") 
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
                        nickname
                        firstname @id
                        lastname @id(path:""\\.Type='FamilyName'"") 
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
                        {'nickname':'Pete','firstname':'Peter','lastname':'Vrenken'},
                        {'nickname':'LadyL','firstname':'Tanja','lastname':'Vrenken'},
                        {'nickname':'Bengel','firstname':'Arjan','lastname':'Vrenken'},
                        {'nickname':'Scheetje','firstname':'Ida','lastname':'Vrenken'}
                    ]
                }", result);
            
        }
    }
}
