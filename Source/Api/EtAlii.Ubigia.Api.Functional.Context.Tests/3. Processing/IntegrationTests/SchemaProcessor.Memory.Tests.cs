// We don't want these tests running on the build server.
#if (UBIGIA_IS_RUNNING_ON_BUILD_AGENT == false)

namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.Diagnostics;
    using JetBrains.dotMemoryUnit;
    using Xunit;
    using Xunit.Abstractions;

    public class SchemaProcessorMemoryTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private ITraversalScriptContext _scriptContext;
        private IGraphContext _context;
        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private IDiagnosticsConfiguration _diagnostics;
        private GraphContextConfiguration _configuration;

        public SchemaProcessorMemoryTests(QueryingUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;

            // We want to have dotmemory profiling output.
            DotMemoryUnitTestOutput.SetOutputMethod(_testOutputHelper.WriteLine);
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _diagnostics = _testContext.FunctionalTestContext.Diagnostics;
            _configuration = new GraphContextConfiguration()
                .UseLapaParser()
                .UseFunctionalGraphContextDiagnostics(_testContext.FunctionalTestContext.Diagnostics);
            await _testContext.FunctionalTestContext.ConfigureLogicalContextConfiguration(_configuration,true).ConfigureAwait(false);

            _scriptContext = new TraversalScriptContextFactory().Create(_configuration);
            _context = new GraphContextFactory().Create(_configuration);

            await _testContext.FunctionalTestContext.AddPeople(_scriptContext).ConfigureAwait(false);
            await _testContext.FunctionalTestContext.AddAddresses(_scriptContext).ConfigureAwait(false);

            _testOutputHelper.WriteLine("{1}.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphContext));
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            await _configuration.Connection.Close().ConfigureAwait(false);
            _configuration = null;
            _scriptContext = null;
            _context = null;

            _testOutputHelper.WriteLine("{1}.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphContext));
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
                var mutationResult = await processor.Process(mutationSchema).ConfigureAwait(false);
                await mutationResult.Completed().ConfigureAwait(false);
                var queryResult = await processor.Process(querySchema).ConfigureAwait(false);
                await queryResult.Completed().ConfigureAwait(false);

            });
            await isolator().ConfigureAwait(false);
            GC.Collect(); // Run explicit GC

            // Assert

            // We don't want any memory leaks.
            dotMemory.Check(memory => Assert.Equal(6, memory.GetObjects(where => where.LeakedOnEventHandler()).ObjectsCount));
        }
    }
}

#endif
