// We don't want these tests running on the build server.
#if (UBIGIA_IS_RUNNING_ON_BUILD_AGENT == false)

namespace EtAlii.Ubigia.Api.Functional.Querying.Tests 
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.xTechnology.Diagnostics;
    using JetBrains.dotMemoryUnit;
    using Xunit;
    using Xunit.Abstractions;

    public class SchemaProcessorMemoryTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private IGraphSLScriptContext _scriptContext;
        private IGraphTLContext _context;
        private readonly QueryingUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;
        private GraphTLQueryContextConfiguration _configuration;

        public SchemaProcessorMemoryTests(QueryingUnitTestContext testContext, ITestOutputHelper outputHelper)
        {
            // We want to have dotmemory profiling output.
            DotMemoryUnitTestOutput.SetOutputMethod(outputHelper.WriteLine);
            
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _diagnostics = _testContext.FunctionalTestContext.Diagnostics;
            _configuration = new GraphTLQueryContextConfiguration()
                .UseFunctionalGraphTLDiagnostics(_testContext.FunctionalTestContext.Diagnostics)
                .UseFunctionalGraphSLDiagnostics(_testContext.FunctionalTestContext.Diagnostics);
            await _testContext.FunctionalTestContext.ConfigureLogicalContextConfiguration(_configuration,true);
            
            _scriptContext = new GraphSLScriptContextFactory().Create(_configuration);
            _context = new GraphTLQueryContextFactory().Create(_configuration);
        
            await _testContext.FunctionalTestContext.AddPeople(_scriptContext);
            await _testContext.FunctionalTestContext.AddAddresses(_scriptContext); 

            Console.WriteLine("{1}.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphTLContext));
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            await _configuration.Connection.Close();
            _configuration = null;
            _scriptContext = null;
            _context = null;

            Console.WriteLine("{1}.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphTLContext));
        }

        [Fact]
        public async Task SchemaProcessor_Mutate_Person_01()
        {
            // Arrange.
            dotMemory.Check();

            // Act.
            var isolator = new Func<Task>(async () =>
            {
                var mutationText = 
                    @"Person @node(Person:Doe/John)
                    {
                        Weight <= 160.1,
                        NickName <= ""HeavyJohnny""
                    }";
                var mutationSchema = _context.Parse(mutationText).Schema;

                var queryText = 
                    @"Person @node(Person:Doe/John)
                    {
                        Weight,
                        NickName
                    }";
                var querySchema = _context.Parse(queryText).Schema;

                var scope = new SchemaScope();
                var configuration = new SchemaProcessorConfiguration()
                    .UseFunctionalDiagnostics(_diagnostics)
                    .Use(scope)
                    .Use(_scriptContext);
                var processor = new SchemaProcessorFactory().Create(configuration);

                // Act.
                var mutationResult = await processor.Process(mutationSchema);
                await mutationResult.Completed();
                var queryResult = await processor.Process(querySchema);
                await queryResult.Completed();

            });
            await isolator();
            GC.Collect(); // Run explicit GC

            // Assert

            // We don't want any memory leaks.
            dotMemory.Check(memory => Assert.Equal(6, memory.GetObjects(where => where.LeakedOnEventHandler()).ObjectsCount));            
        }
    }
}

#endif