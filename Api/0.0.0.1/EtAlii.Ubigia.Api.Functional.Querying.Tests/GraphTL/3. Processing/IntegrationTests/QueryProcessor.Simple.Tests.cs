namespace EtAlii.Ubigia.Api.Functional.Tests 
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Querying;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.Diagnostics;
    using GraphQL;
    using Xunit;
//using EtAlii.Ubigia.Api.Functional.Diagnostics.Querying;

    public class QueryProcessorSimpleTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private IGraphSLScriptContext _scriptContext;
        private IGraphTLQueryContext _queryContext;
        private readonly QueryingUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;

        public QueryProcessorSimpleTests(QueryingUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _diagnostics = _testContext.FunctionalTestContext.Diagnostics;
            var configuration = new GraphTLQueryContextConfiguration()
                .UseFunctionalGraphTLDiagnostics(_testContext.FunctionalTestContext.Diagnostics)
                .UseFunctionalGraphSLDiagnostics(_testContext.FunctionalTestContext.Diagnostics);
            await _testContext.FunctionalTestContext.ConfigureLogicalContextConfiguration(configuration,true);
            
            _scriptContext = new GraphSLScriptContextFactory().Create(configuration);
            _queryContext = new GraphTLQueryContextFactory().Create(configuration);
        
            await _testContext.FunctionalTestContext.AddPeople(_scriptContext);
            await _testContext.FunctionalTestContext.AddAddresses(_scriptContext);

            Console.WriteLine("{1}.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphTLQueryContext));
        }

        public Task DisposeAsync()
        {
            var start = Environment.TickCount;

            _scriptContext = null;
            _queryContext = null;

            Console.WriteLine("{1}.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphTLQueryContext));
            return Task.CompletedTask;
        }

        
        [Fact]
        public Task QueryProcessor_Create()
        {
            // Arrange.
            var scope = new QueryScope();
            var configuration = new QueryProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);

            // Act.
            var processor = new QueryProcessorFactory().Create(configuration);

            // Assert.
            Assert.NotNull(processor);
            
            return Task.CompletedTask;
        }
        
        

        [Fact]
        public async Task QueryProcessor_Process_Time_Now()
        {
            // Arrange.
            var selectQueryText = @"Time @node(time:Now)
                               {
                                    Millisecond @value()
                               }";

            var selectQuery = _queryContext.Parse(selectQueryText).Query;

            var scope = new QueryScope();
            var configuration = new QueryProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new QueryProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectQuery);
            var lastResult = await result.Output.Cast<INode>().LastOrDefaultAsync();

            // Assert.
            Assert.NotNull(result.Output);
            Assert.NotNull(lastResult);
            var structure = result.Structure.SingleOrDefault();
            Assert.NotNull(structure);
            Assert.Same(structure, lastResult);
            var milliSecond = structure.Values.SingleOrDefault(v => v.Name == "Millisecond");
            Assert.NotNull(milliSecond);
            Assert.IsType<int>(milliSecond.Object);
        }

        [Fact]
        public async Task QueryProcessor_Process_Person()
        {
            // Arrange.
            var selectQueryText = @"Person @nodes(Person:Stark/Tony)
                               {
                                    FirstName @value()
                                    LastName @value(\)
                               }";

            var selectQuery = _queryContext.Parse(selectQueryText).Query;

            var scope = new QueryScope();
            var configuration = new QueryProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new QueryProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectQuery);
            var lastResult = await result.Output.Cast<Structure>().LastOrDefaultAsync();

            // Assert.
            Assert.NotNull(result.Output);
            Assert.NotNull(lastResult);
            var structure = result.Structure.SingleOrDefault();
            Assert.NotNull(structure);
            Assert.Same(structure, lastResult);
            var firstName = structure.Values.SingleOrDefault(v => v.Name == "FirstName");
            Assert.NotNull(firstName);
            Assert.Equal("Tony", firstName.Object);
            var lastName = structure.Values.SingleOrDefault(v => v.Name == "LastName");
            Assert.NotNull(lastName);
            Assert.Equal("Stark", lastName.Object);
        }

        [Fact]
        public async Task QueryProcessor_Process_Persons()
        {
            // Arrange.
            var selectQueryText = @"Person @nodes(Person:Doe/*)
                               {
                                    FirstName @value()
                                    LastName @value(\)
                               }";

            var selectQuery = _queryContext.Parse(selectQueryText).Query;

            var scope = new QueryScope();
            var configuration = new QueryProcessorConfiguration()
                .UseFunctionalDiagnostics(_diagnostics)
                .Use(scope)
                .Use(_scriptContext);
            var processor = new QueryProcessorFactory().Create(configuration);

            // Act.
            var result = await processor.Process(selectQuery);
            var lastResult = await result.Output.Cast<Structure>().LastOrDefaultAsync();

            // Assert.
            Assert.NotNull(result.Output);
            Assert.NotNull(lastResult);
            Assert.Equal(2, result.Structure.Count);
            
            var firstPerson = result.Structure[0];
            Assert.NotNull(firstPerson);
            Assert.Same(firstPerson, lastResult);
            var firstName = firstPerson.Values.SingleOrDefault(v => v.Name == "FirstName");
            Assert.NotNull(firstName);
            Assert.Equal("Doe", firstName.Object);
            var lastName = firstPerson.Values.SingleOrDefault(v => v.Name == "LastName");
            Assert.NotNull(lastName);
            Assert.Equal("John", lastName.Object);

            var secondPerson = result.Structure[1];
            Assert.NotNull(secondPerson);
            Assert.Same(secondPerson, lastResult);
            firstName = secondPerson.Values.SingleOrDefault(v => v.Name == "FirstName");
            Assert.NotNull(firstName);
            Assert.Equal("Doe", firstName.Object);
            lastName = secondPerson.Values.SingleOrDefault(v => v.Name == "LastName");
            Assert.NotNull(lastName);
            Assert.Equal("Jane", lastName.Object);

        }
    }
}