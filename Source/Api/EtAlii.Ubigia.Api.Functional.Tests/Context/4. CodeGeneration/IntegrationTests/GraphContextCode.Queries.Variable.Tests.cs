// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;
    using Xunit.Abstractions;
    using EtAlii.Ubigia.Api.Functional.Context.Tests.Model;
    using EtAlii.Ubigia.Api.Functional.Tests;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class GraphContextCodeQueriesVariableTests : IClassFixture<FunctionalUnitTestContext>, IAsyncLifetime
    {
        private readonly FunctionalUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private FunctionalOptions _options;
        private int _test;

        public GraphContextCodeQueriesVariableTests(FunctionalUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
        }

        public async Task InitializeAsync()
        {
            var initialize = Environment.TickCount;

            _options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseDiagnostics()
                .UseLogicalContext(_testContext, true)
                .ConfigureAwait(false);

            var traversalContext = _testContext.CreateComponent<ITraversalContext>(_options);

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

            _testOutputHelper.WriteLine($"Dispose: {TimeSpan.FromTicks(Environment.TickCount - dispose).TotalMilliseconds}ms");
        }


        [Fact]
        public async Task GraphContextCodeQueries_Query_Person_By_Variables()
        {
            // Arrange.
            var processor = _testContext.CreateSchemaProcessor(_options);
            var parser = _testContext.CreateSchemaParser();
            var context = new GraphContext(processor, parser);

            // Act.
            var person = await context
                .ProcessPersonByFirstNameLastName("Stark", "Tony")
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(person);

            Assert.Equal("Tony", person.FirstName);
            Assert.Equal("Stark", person.LastName);
        }
    }
}
