// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    public class ProfilingTraversalContextTests : IClassFixture<TraversalUnitTestContext>, IAsyncLifetime
    {
        private readonly TraversalUnitTestContext _testContext;

        public ProfilingTraversalContextTests(TraversalUnitTestContext testContext)
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
            var options = new FunctionalContextOptions()
                .UseTestTraversalParser()
                .UseTraversalProfiling();
            await _testContext.Logical.ConfigureLogicalContextConfiguration(options, true).ConfigureAwait(false);

            // Act.
            var context = new TraversalContextFactory().Create(options);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingTraversalContext_Create_02()
        {
            // Arrange.
            var options = new FunctionalContextOptions()
                .UseTestTraversalParser()
                .UseFunctionalTraversalDiagnostics(_testContext.ClientConfiguration)
                .UseTraversalProfiling();
            await _testContext.Logical.ConfigureLogicalContextConfiguration(options, true).ConfigureAwait(false);

            // Act.
            var context = new TraversalContextFactory().Create(options);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingTraversalContext_Create_03()
        {
            // Arrange.
            var options = new FunctionalContextOptions()
                .UseTestTraversalParser()
                .UseFunctionalTraversalDiagnostics(_testContext.ClientConfiguration)
                .UseTraversalProfiling();
            await _testContext.Logical.ConfigureLogicalContextConfiguration(options, true).ConfigureAwait(false);

            // Act.
            var context = new TraversalContextFactory().Create(options);

            // Assert.
            Assert.NotNull(context);
        }
    }
}
