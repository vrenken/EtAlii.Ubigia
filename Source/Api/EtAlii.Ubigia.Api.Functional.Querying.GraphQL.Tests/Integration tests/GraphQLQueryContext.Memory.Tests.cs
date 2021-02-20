// We don't want these tests running on the build server.
#if UBIGIA_IS_RUNNING_ON_BUILD_AGENT == false

namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using JetBrains.dotMemoryUnit;
    using Xunit;
    using Xunit.Abstractions;

    public class GraphQLQueryContextMemoryTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private ITraversalContext _traversalContext;
        private IGraphQLQueryContext _queryContext;

        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private GraphQLQueryContextConfiguration _configuration;

        public GraphQLQueryContextMemoryTests(QueryingUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            // We want to have dotmemory profiling output.
            DotMemoryUnitTestOutput.SetOutputMethod(testOutputHelper.WriteLine);

            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
            _testOutputHelper = testOutputHelper;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _configuration = new GraphQLQueryContextConfiguration()
                .UseTestTraversalParser()
                .UseFunctionalGraphQLDiagnostics(_testContext.FunctionalTestContext.Diagnostics);
            await _testContext.FunctionalTestContext.ConfigureLogicalContextConfiguration(_configuration,true).ConfigureAwait(false);

            _traversalContext = new TraversalContextFactory().Create(_configuration);
            _queryContext = new GraphQLQueryContextFactory().Create(_configuration);

            await _testContext.FunctionalTestContext.AddPeople(_traversalContext).ConfigureAwait(false);
            await _testContext.FunctionalTestContext.AddAddresses(_traversalContext).ConfigureAwait(false);

            _testOutputHelper.WriteLine("DataContext_Nodes.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            await _configuration.Connection.Close().ConfigureAwait(false);
            _configuration = null;
            _traversalContext = null;
            _queryContext = null;

            _testOutputHelper.WriteLine("DataContext_Nodes.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task GraphQL_Query_Traverse_Person_Multiple_Nested_01()
        {
            // Arrange.
            dotMemory.Check();

            // Act.
            var isolator = new Func<Task>(async() =>
            {
                var queryText = @"
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

                var parseResult = await _queryContext.Parse(queryText).ConfigureAwait(false);
                await _queryContext.Process(parseResult.Query).ConfigureAwait(false);
            });
            await isolator().ConfigureAwait(false);
            GC.Collect(); // Run explicit GC

            // Assert.

            // We don't want any memory leaks.
            dotMemory.Check(memory => Assert.Equal(6, memory.GetObjects(where => where.LeakedOnEventHandler()).ObjectsCount));
        }
    }
}

#endif
