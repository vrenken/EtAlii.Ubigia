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
        private IGraphContext _context;
        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private FunctionalOptions _options;

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

            _options = await TraversalContextOptionsUseTestParsingExtension.UseTestParsing(new FunctionalOptions(_testContext.ClientConfiguration))
                .UseFunctionalDiagnostics()
                .UseDataConnectionToNewSpace(_testContext, true)
                .ConfigureAwait(false);

            var traversalContext = new TraversalContextFactory().Create(_options);
            _context = new GraphContextFactory().Create(_options);

            var scope = new ExecutionScope();
            await _testContext.Functional
                .AddPeople(traversalContext, scope)
                .ConfigureAwait(false);
            await _testContext.Functional
                .AddAddresses(traversalContext, scope)
                .ConfigureAwait(false);

            _testOutputHelper.WriteLine("{1}.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphContext));
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            await _options.Connection.Close().ConfigureAwait(false);
            _options = null;
            _context = null;

            _testOutputHelper.WriteLine("{1}.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphContext));
        }

        [Fact]
        public async Task SchemaProcessor_Mutate_Person_01()
        {
            // Arrange.
            dotMemory.Check();
            var scope = new ExecutionScope();

            // Act.
            var isolator = new Func<Task>(async () =>
            {
                var mutationText =
                    @"Person = @node(Person:Doe/John)
                    {
                        Weight = 160.1,
                        NickName = ""HeavyJohnny""
                    }";
                var mutationSchema = _context.Parse(mutationText, scope).Schema;

                var queryText =
                    @"Person = @node(Person:Doe/John)
                    {
                        Weight,
                        NickName
                    }";
                var querySchema = _context.Parse(queryText, scope).Schema;

                var options = _options.CreateScope();
                var processor = new TestSchemaProcessorFactory().Create(options);

                // Act.
                var mutationResults = await processor
                    .Process(mutationSchema, scope)
                    .ToArrayAsync()
                    .ConfigureAwait(false);
                Assert.NotEmpty(mutationResults);
                var queryResults = await processor
                    .Process(querySchema, scope)
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
