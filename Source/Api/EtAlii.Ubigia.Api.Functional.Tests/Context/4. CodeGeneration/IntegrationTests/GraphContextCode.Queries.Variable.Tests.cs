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
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class GraphContextCodeQueriesVariableTests : IClassFixture<QueryingUnitTestContext>, IAsyncLifetime
    {
        private ITraversalContext _traversalContext;
        private readonly QueryingUnitTestContext _testContext;
        private readonly ITestOutputHelper _testOutputHelper;
        private FunctionalOptions _options;

        public GraphContextCodeQueriesVariableTests(QueryingUnitTestContext testContext, ITestOutputHelper testOutputHelper)
        {
            _testContext = testContext;
            _testOutputHelper = testOutputHelper;
        }

        public async Task InitializeAsync()
        {
            var start = Environment.TickCount;

            _options = new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestTraversalParser()
                .UseTestContextParser()
                .UseFunctionalDiagnostics();
            await _testContext.Functional.ConfigureLogicalContextOptions(_options,true).ConfigureAwait(false);

            _traversalContext = new TraversalContextFactory().Create(_options);

            await _testContext.Functional.AddPeople(_traversalContext).ConfigureAwait(false);
            await _testContext.Functional.AddAddresses(_traversalContext).ConfigureAwait(false);

            _testOutputHelper.WriteLine("{1}.Initialize: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphContext));
        }

        public async Task DisposeAsync()
        {
            var start = Environment.TickCount;

            await _options.Connection.Close().ConfigureAwait(false);
            _options = null;
            _traversalContext = null;

            _testOutputHelper.WriteLine("{1}.Cleanup: {0}ms", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds, nameof(IGraphContext));
        }


        [Fact]
        public async Task GraphContextCodeQueries_Query_Person_By_Variables()
        {
            // Arrange.
            var processor = new TestSchemaProcessorFactory();
            var parser = new TestSchemaParserFactory();
            var context = new GraphContext(_options, processor, parser, _traversalContext);

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
