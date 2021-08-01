// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if UBIGIA_IS_RUNNING_ON_BUILD_AGENT == false

namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Tests;
    using JetBrains.dotMemoryUnit;
    using Xunit;
    using Xunit.Abstractions;

    [CorrelateUnitTests]
    public class SchemaProcessorMemoryTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private ITraversalContext _traversalContext;
        private IGraphContext _context;
        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private FunctionalContextConfiguration _configuration;

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

            _configuration = new FunctionalContextConfiguration()
                .UseTestTraversalParser()
                .UseTestContextParser()
                .UseFunctionalGraphContextDiagnostics(_testContext.ClientConfiguration);
            await _testContext.Functional.ConfigureLogicalContextConfiguration(_configuration,true).ConfigureAwait(false);

            _traversalContext = new TraversalContextFactory().Create(_configuration);
            _context = new GraphContextFactory().Create(_configuration);

            await _testContext.Functional.AddPeople(_traversalContext).ConfigureAwait(false);
            await _testContext.Functional.AddAddresses(_traversalContext).ConfigureAwait(false);

            _testOutputHelper.WriteLine("{1}.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphContext));
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            await _configuration.Connection.Close().ConfigureAwait(false);
            _configuration = null;
            _traversalContext = null;
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
                    @"Person = @node(Person:Doe/John)
                    {
                        Weight = 160.1,
                        NickName = ""HeavyJohnny""
                    }";
                var mutationSchema = _context.Parse(mutationText).Schema;

                var queryText =
                    @"Person = @node(Person:Doe/John)
                    {
                        Weight,
                        NickName
                    }";
                var querySchema = _context.Parse(queryText).Schema;

                var scope = new SchemaScope();
                var configuration = new SchemaProcessorConfiguration()
                    .UseFunctionalDiagnostics(_testContext.ClientConfiguration)
                    .Use(scope)
                    .Use(_traversalContext);
                var processor = new LapaSchemaProcessorFactory().Create(configuration);

                // Act.
                var mutationResults = await processor
                    .Process(mutationSchema)
                    .ToArrayAsync()
                    .ConfigureAwait(false);
                Assert.NotEmpty(mutationResults);
                var queryResults = await processor
                    .Process(querySchema)
                    .ToArrayAsync()
                    .ConfigureAwait(false);
                Assert.NotEmpty(queryResults);
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
