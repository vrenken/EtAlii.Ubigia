namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;
    using GraphQL.Http;
    using Xunit;


    public class GraphQLQueryContextBasicTests : IClassFixture<QueryingUnitTestContext>, IDisposable
    {
        private ILogicalContext _logicalContext;
        private IGraphSLScriptContext _scriptContext;
        private IGraphQLQueryContext _queryContext;
        
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

                _scriptContext = null;
                _queryContext = null;

                Console.WriteLine("DataContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            });
            task.Wait();
        }

        [Theory, ClassData(typeof(FileBasedScriptData))]
        public async Task GraphQL_Query_From_Files_Execute(string fileName, string title, string query)
        {
            // Arrange.
#pragma warning disable 1717
            title = title;
            fileName = fileName;
#pragma warning restore 1717
            
            // Act.
            var result = await _queryContext.Process(query);
             
            // Assert.
            Assert.NotNull(result);
        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Full_Local()
        {
            // Arrange.
            var query = @"query data { person @nodes(path:""person:Stark/Tony"") { firstname, lastname, nickname, birthdate, lives } }";
            
            // Act.
            var result = await _queryContext.Process(query);
             
            // Assert.
            Assert.NotNull(result.Errors);
        }
        
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Full_Relative()
        {
            // Arrange.
            var query = @"query data { person @nodes(path:""person:Stark/Tony"") { firstname @id(path:""""), lastname @id(path:""\\""), nickname, birthdate, lives } }";
            
            // Act.
            var result = await _queryContext.Process(query);
             
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""firstname"": ""Tony"", ""lastname"": ""Stark"", ""nickname"": ""Iron Man"", ""birthdate"": ""1976-05-12"", ""lives"": 9 }}", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Relative_01()
        {
            // Arrange.
            var query = @"query data { person @nodes(path:""person:Stark/Tony"") { lastname @id(path:""\\"") } }";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""lastname"": ""Stark"" }}", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Relative_02()
        {
            // Arrange.
            var query = @"query data { person @nodes(path:""person:Stark/Tony"") { firstname @id(path:"""") } }";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""firstname"": ""Tony"" }}", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Local()
        {
            // Arrange.
            var query = @"query data { person @nodes(path:""person:Stark/Tony"") { firstname } }";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.NotNull(result.Errors);
        }

        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Path_Relative()
        {
            // Arrange.
            var query = @"
                query data 
                { 
                    person @nodes(path:""person:Stark/Tony"") 
                    { 
                        firstname @id(path:"""") 
                    } 
                }";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""firstname"": ""Tony"" }}", result);
        }

        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_Integer()
        {
            // Arrange.
            var query = @"query data { person @nodes(path:""person:Stark/Tony"") { lives } }";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""lives"": 9 }}", result);
        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_01()
        {
            // Arrange.
            var query = @"query data { person @nodes(path:""person:Stark/Tony"") { nickname } }";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""nickname"": ""Iron Man"" }}", result);
        }
                
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_04()
        {
            // Arrange.
            var query = @"query data 
                          { 
                            person @nodes(path:""person:Stark/Tony"") 
                            { 
                                nickname 
                            } 
                          }";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""nickname"": ""Iron Man"" }}", result);
        }
                
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_And_Non_Argumented_Id()
        {
            // Arrange.
            var query = @"query data 
                          { 
                            person @nodes(path:""person:Stark/Tony"") 
                            {
                                firstname @id 
                                nickname 
                            } 
                          }";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""firstname"": ""Tony"", ""nickname"": ""Iron Man"" }}", result);
        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_And_Argumented_Id()
        {
            // Arrange.
            var query = @"query data 
                          { 
                            person @nodes(path:""person:Stark/Tony"") 
                            {
                                lastname @id(path:""\\"")
                                nickname 
                            } 
                          }";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""lastname"": ""Stark"", ""nickname"": ""Iron Man"" }}", result);
        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_02()
        {
            // Arrange.
            var query = "query data { person\n@nodes(path:\"person:Stark/Tony\") { nickname } }";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""nickname"": ""Iron Man"" }}", result);
        }
                
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_03()
        {
            // Arrange.
            var query = "query data\n{\nperson\n@nodes(path:\"person:Stark/Tony\")\n{\nnickname\n}\n}";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter,@"{ ""person"": { ""nickname"": ""Iron Man"" }}", result);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Multiple_Starts_String()
        {
            // Arrange.
            var query = "query data\n{\nperson\n@nodes(path:\"person:Stark/Tony\")\n@nodes(path:\"person:Stark/Tony\")\n{\nnickname\n}\n}";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.NotNull(result.Errors);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Droid()
        {
            // Arrange.
            var query = @"query data { droid(id: ""4"") { id, name } }";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.NotNull(result.Errors);
        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_String_Temp()
        {
            // Arrange.
            var query = @"query data { person @nodes(path:""person:Stark/Tony"") { id, nickname } }";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.NotNull(result.Errors);
            await AssertQuery.ResultsAreNotEqual(_documentWriter,@"{ ""person"": { ""id"":""2"", ""nickname"": ""Iron Man"" }}", result);
        }
        
        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple_Property_Date()
        {
            // Arrange.
            var query = @"query data { person @nodes(path:""person:Stark/Tony"") { birthdate } }";
            
            // Act.
            var result = await _queryContext.Process(query);
            
            // Assert.
            Assert.Null(result.Errors);
            await AssertQuery.ResultsAreEqual(_documentWriter, @"{ ""person"": { ""birthdate"": ""1976-05-12"" }}", result);
        }
    }
}
