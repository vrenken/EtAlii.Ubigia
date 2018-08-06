namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using GraphQL;
    using GraphQL.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Xunit;


    public class GraphQLQueryContextBasicTests : IClassFixture<QueryingUnitTestContext>, IDisposable
    {
        private IDataContext _dataContext;
        private IGraphQLQueryContext _context;
        private readonly QueryingUnitTestContext _testContext;
        private readonly IDocumentWriter _documentWriter;

        public GraphQLQueryContextBasicTests(QueryingUnitTestContext testContext)
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
                
                await _testContext.FunctionalTestContext.AddJohnDoe(_dataContext);
                await _testContext.FunctionalTestContext.AddTonyStark(_dataContext);

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
        public async Task GraphQL_Query_Select_Simple_Full()
        {
            // Arrange.
            var query = @"query data @start(path:""person:Stark/Tony"") { hero { Firstname, Lastname, Nickname, Birthdate, Lives } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""hero"": { ""Firstname"": ""Tony"", ""Lastname"": ""Stark"", ""Birthdate"": ""1978-07-28"", ""Nickname"": ""Iron Man"", ""Lives"": 9 }}", result.Data);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Relative()
        {
            // Arrange.
            var query = @"query data @start(path:""person:Stark/Tony"") { hero { Lastname } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""hero"": { ""Lastname"": ""Stark"" }}", result.Data);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Local()
        {
            // Arrange.
            var query = @"query data @start(path:""person:Stark/Tony"") { hero { Firstname } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""hero"": { ""Firstname"": ""Tony"" }}", result.Data);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_Integer()
        {
            // Arrange.
            var query = @"query data @start(path:""person:Stark/Tony"") { hero { Lives } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""hero"": { ""Lives"": 9 }}", result.Data);
        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String()
        {
            // Arrange.
            var query = @"query data @start(path:""person:Stark/Tony"") { hero { Nickname } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""hero"": { ""Nickname"": ""Iron Man"" }}", result.Data);
        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_Date()
        {
            // Arrange.
            var query = @"query data @start(path:""person:Stark/Tony"") { hero { Birthdate } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""hero"": { ""Birthdate"": ""1978-07-28"" }}", result.Data);
        }
        
        private void AssertQueryResultsAreSame(string expected, object actual)
        {
            var expectedDocument = JObject.Parse(expected);
            var expectedString = _documentWriter.Write(expectedDocument);
            var actualString = _documentWriter.Write(actual);
            Assert.Equal(expectedString, actualString);
        }
    }
}
