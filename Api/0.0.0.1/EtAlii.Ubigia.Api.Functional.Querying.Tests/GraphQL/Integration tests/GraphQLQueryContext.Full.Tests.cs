namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using GraphQL.Http;
    using Xunit;


    public class GraphQLQueryContextFullTests : IClassFixture<QueryingUnitTestContext>, IDisposable
    {
        private ILogicalContext _logicalContext;
        private IGraphSLScriptContext _scriptContext;
        private IGraphQLQueryContext _queryContext;
        
        private readonly QueryingUnitTestContext _testContext;
        private readonly IDocumentWriter _documentWriter;

        public GraphQLQueryContextFullTests(QueryingUnitTestContext testContext)
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
        public async Task GraphQL_Query_Traverse_Person_Single()
        {
            // Arrange.
            var query = @"
                query data @nodes(path:""/person/Stark/"")  
                { 
                    person @nodes(path:""Tony"")
                    { 
                        nickname 
                    }
                }";
            
            // Act.
            var result = await _queryContext.Execute(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ ""person"": { ""nickname"": ""Iron Man"" }}", result);
        }

        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Single_Nested_01()
        {
            // Arrange.
            var query = @"
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
            var result = await _queryContext.Execute(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ ""data2"": { ""person"": { ""nickname"": ""Iron Man"" }}}", result);
        }

        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Single_Nested_02()
        {
            // Arrange.
            var query = @"
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
            var result = await _queryContext.Execute(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ ""stark"": { ""tony"": { ""nickname"": ""Iron Man"" }}}", result);
        }

        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Multiple_Nested_01()
        {
            // Arrange.
            var query = @"
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
            var result = await _queryContext.Execute(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ ""data2"": { ""person1"": { ""nickname"": ""Iron Man"" }, ""person2"": { ""nickname"": ""Johnny"" }}}", result);
        }
    }
}
