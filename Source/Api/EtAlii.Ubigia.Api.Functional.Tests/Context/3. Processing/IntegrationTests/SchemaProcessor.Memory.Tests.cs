// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if UBIGIA_IS_RUNNING_ON_BUILD_AGENT == false

namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Tests;
    using JetBrains.dotMemoryUnit;
    using Xunit;
    using Xunit.Abstractions;

    [CorrelateUnitTests]
    public class SchemaProcessorMemoryTests : IClassFixture<FunctionalUnitTestContext>, IAsyncLifetime
    {
        private IGraphContext _context;
        private readonly FunctionalUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private FunctionalOptions _options;
        private int _test;

        public SchemaProcessorMemoryTests(FunctionalUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;

            // We want to have dotmemory profiling output.
            DotMemoryUnitTestOutput.SetOutputMethod(_testOutputHelper.WriteLine);
        }

        public async Task InitializeAsync()
        {
            var initialize = Environment.TickCount;

            _options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseDiagnostics()
                .UseLogicalContext(_testContext, true)
                .ConfigureAwait(false);

            var (graphContext, traversalContext) = _testContext.CreateComponent<IGraphContext, ITraversalContext>(_options);
            _context = graphContext;

            var scope = new ExecutionScope();
            await _testContext.Functional
                .AddPeople(traversalContext, scope)
                .ConfigureAwait(false);
            await _testContext.Functional
                .AddAddresses(traversalContext, scope)
                .ConfigureAwait(false);

            _testOutputHelper.WriteLine($"Initialize: {TimeSpan.FromTicks(Environment.TickCount - initialize).TotalMilliseconds}ms");

            _test = Environment.TickCount;
        }

        public async Task DisposeAsync()
        {
            _testOutputHelper.WriteLine($"Test: {TimeSpan.FromTicks(Environment.TickCount - _test).TotalMilliseconds}ms");

            var dispose = Environment.TickCount;

            await _options.LogicalContext
                .DisposeAsync()
                .ConfigureAwait(false);
            _options = null;
            _context = null;

            _testOutputHelper.WriteLine($"Dispose: {TimeSpan.FromTicks(Environment.TickCount - dispose).TotalMilliseconds}ms");
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

                var processor = _testContext.CreateSchemaProcessor(_options);

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
            dotMemory.Check(memory => Assert.Equal(4, memory.GetObjects(where => where.LeakedOnEventHandler()).ObjectsCount));
        }
    }
}

#endif
