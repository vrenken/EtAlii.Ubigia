namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using GraphQL;
    using GraphQL.Http;
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
            var query = @"query data @traverse(path:""person:Stark/Tony"") { person { firstname, lastname, nickname, birthdate, lives } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
             
            // Assert.
            AssertQueryResultsAreSame(@"{ ""person"": { ""firstname"": ""Tony"", ""lastname"": ""Stark"", ""nickname"": ""Iron Man"", ""birthdate"": ""1976-05-12"", ""lives"": 9 }}", result.Data);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Relative()
        {
            // Arrange.
            var query = @"query data @traverse(path:""person:Stark/Tony"") { person { lastname @traverse(path:""\\"") } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""person"": { ""lastname"": ""Stark"" }}", result.Data);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Local()
        {
            // Arrange.
            var query = @"query data @traverse(path:""person:Stark/Tony"") { person { firstname } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""person"": { ""firstname"": ""Tony"" }}", result.Data);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_Integer()
        {
            // Arrange.
            var query = @"query data @traverse(path:""person:Stark/Tony"") { person { lives } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""person"": { ""lives"": 9 }}", result.Data);
        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_01()
        {
            // Arrange.
            var query = @"query data @traverse(path:""person:Stark/Tony"") { person { nickname } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""person"": { ""nickname"": ""Iron Man"" }}", result.Data);
        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_02()
        {
            // Arrange.
            var query = "query data\n@traverse(path:\"person:Stark/Tony\") { person { nickname } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""person"": { ""nickname"": ""Iron Man"" }}", result.Data);
        }
                
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_03()
        {
            // Arrange.
            var query = "query data\n@traverse(path:\"person:Stark/Tony\")\n{\nperson\n{\nnickname\n}\n}";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""person"": { ""nickname"": ""Iron Man"" }}", result.Data);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Multiple_Starts_String()
        {
            // Arrange.
            var query = "query data\n@traverse(path:\"person:Stark/Tony\")\n@traverse(path:\"person:Stark/Tony\")\n{\nperson\n{\nnickname\n}\n}";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""person"": { ""nickname"": ""Iron Man"" }}", result.Data);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Droid()
        {
            // Arrange.
            var query = @"query data { droid(id: ""4"") { id, name } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{""droid"":{""id"":""4"",""name"":""C-3PO""}}", result.Data);
        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_Temp()
        {
            // Arrange.
            var query = @"query data @traverse(path:""person:Stark/Tony"") { person { id, nickname } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""person"": { ""id"":""2"", ""nickname"": ""Iron Man"" }}", result.Data);
        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_Date()
        {
            // Arrange.
            var query = @"query data @traverse(path:""person:Stark/Tony"") { person { birthdate } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""person"": { ""birthdate"": ""1976-05-12"" }}", result.Data);
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
