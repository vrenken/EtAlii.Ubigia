namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using GraphQL; // TODO: These dependencies should be covered.
    using GraphQL.Http;
    using Newtonsoft.Json.Linq;
    using Xunit;


    public class GraphQLQueryContextMultipleTests : IClassFixture<QueryingUnitTestContext>, IDisposable
    {
        private IDataContext _dataContext;
        private IGraphQLQueryContext _context;
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

                _dataContext = await _testContext.FunctionalTestContext.CreateFunctionalContext(true);
                _context = new GraphQLQueryContextFactory().Create(_dataContext);
                
                await _testContext.FunctionalTestContext.AddPeople(_dataContext);
                await _testContext.FunctionalTestContext.AddAddresses(_dataContext);

                Console.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            });
            task.Wait();
        }

        private void TestCleanup()
        {
            var task = Task.Run(() =>
            {
                var start = Environment.TickCount;

                _context = null;

                Console.WriteLine("DataContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            });
            task.Wait();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Multiple_01()
        {
            // Arrange.
            var query = @"
                query data  
                { 
                    person @nodes(path:""/person/*/*"")
                    {
                        nickname 
                    }
                }";
            
            // Act.
            var result = await _context.Execute(query);
            
            // Assert.                           
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ 'person': [{ 'nickname': 'Johnny'} , { 'nickname': 'Janey'} , { 'nickname': 'Iron Man'}]}", result);
        }
        
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Multiple_With_Nested_Id()
        {
            // Arrange.
            var query = @"
                query data  
                { 
                    person @nodes(path:""/person/*/*"")
                    {
                        firstname @id
                        nickname 
                    }
                }";
            
            // Act.
            var result = await _context.Execute(query);
            
            // Assert.                           
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ 'person': [{ 'firstname': 'John', 'nickname': 'Johnny'} , { 'firstname': 'Jane', 'nickname': 'Janey'} , { 'firstname': 'Tony', 'nickname': 'Iron Man'}]}", result);
        }
        
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Multiple_02()
        {
            // Arrange.
            var query = @"
                query data  
                { 
                    person @nodes(path:""/person/Doe/*"")
                    {
                        nickname 
                    }
                }";
            
            // Act.
            var result = await _context.Execute(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ 'person': [{ 'nickname': 'Johnny'} , { 'nickname': 'Janey'} ]}", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Plural()
        {
            // Arrange.
            var query = @"
                query data @nodes(path:""person:Doe/*"") 
                { 
                    person 
                    { 
                        nickname 
                    } 
                }";
            
            // Act.
            var result = await _context.Execute(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ 'person': [{ 'nickname': 'Johnny'} , { 'nickname': 'Janey'} ]}", result);
        }
    }
}
