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

        public GraphContextCodeQueriesVariableTests(FunctionalUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _options = await new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestParsing()
                .UseFunctionalDiagnostics()
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

            _testOutputHelper.WriteLine("{1}.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphContext));
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            await _options.LogicalContext.Options.Connection
                .Close()
                .ConfigureAwait(false);
            _options = null;

            _testOutputHelper.WriteLine("{1}.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphContext));
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
