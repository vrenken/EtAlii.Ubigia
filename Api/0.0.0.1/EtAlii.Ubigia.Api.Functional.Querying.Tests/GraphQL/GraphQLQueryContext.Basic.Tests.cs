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


    public class GraphQLQueryContextBasicTests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;
        private IDataContext _dataContext;
        private IGraphQLQueryContext _context;
        private string _countryPath;
        private readonly LogicalUnitTestContext _testContext;
        private readonly IDocumentWriter _documentWriter;

        public GraphQLQueryContextBasicTests(LogicalUnitTestContext testContext)
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

                _diagnostics = TestDiagnostics.Create();
                _logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
                var configuration = new DataContextConfiguration()
                    .Use(_diagnostics)
                    .Use(_logicalContext);
                _dataContext = new DataContextFactory().Create(configuration);
                _context = new GraphQLQueryContextFactory().Create(_dataContext);
                
                var addResult = await _testContext.LogicalTestContext.AddContinentCountry(_logicalContext);
                _countryPath = addResult.Path;

                Console.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            });
            task.Wait();
        }

        private void TestCleanup()
        {
            var task = Task.Run(() =>
            {
                var start = Environment.TickCount;

                _countryPath = null;
//                _context.Dispose();
                _context = null;
                _logicalContext.Dispose();
                _logicalContext = null;
                _diagnostics = null;

                Console.WriteLine("DataContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            });
            task.Wait();
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Select_Simple()
        {
            // Arrange.
            var query = @"query data @start(path:""person:Stark/Tony"") { hero { name } }";
            
            // Act.
            var result = await _context.Execute("Query", query, new Inputs());
            
            // Assert.
            AssertQueryResultsAreSame(@"{ ""hero"": { ""name"": ""R2-D2"" }}", result.Data);
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
