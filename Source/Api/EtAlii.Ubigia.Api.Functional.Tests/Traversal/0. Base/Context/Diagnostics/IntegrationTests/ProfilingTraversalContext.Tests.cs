// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#if USE_LAPA_PARSING_IN_TESTS

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    public class ProfilingTraversalContextTests : IClassFixture<FunctionalUnitTestContext>, IAsyncLifetime
    {
        private readonly FunctionalUnitTestContext _testContext;

        public ProfilingTraversalContextTests(FunctionalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact]
        public async Task ProfilingTraversalContext_Create_01()
        {
            // Arrange.
            var options = new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestTraversalParser()
                .UseTraversalProfiling();
            await _testContext.Logical
                .ConfigureLogicalContextOptions(options, true)
                .ConfigureAwait(false);

            // Act.
            var context = new TraversalContextFactory().Create(options);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingTraversalContext_Create_02()
        {
            // Arrange.
            var options = new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestTraversalParser()
                .UseTraversalProfiling()
                .UseFunctionalDiagnostics();
            await _testContext.Logical.ConfigureLogicalContextOptions(options, true).ConfigureAwait(false);

            // Act.
            var context = new TraversalContextFactory().Create(options);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingTraversalContext_Create_03()
        {
            // Arrange.
            var options = new FunctionalOptions(_testContext.ClientConfiguration)
                .UseTestTraversalParser()
                .UseTraversalProfiling()
                .UseFunctionalDiagnostics();
            await _testContext.Logical.ConfigureLogicalContextOptions(options, true).ConfigureAwait(false);

            // Act.
            var context = new TraversalContextFactory().Create(options);

            // Assert.
            Assert.NotNull(context);
        }
    }
}

#endif
