// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public class ProfilingLogicalContextTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private readonly LogicalUnitTestContext _testContext;

        public ProfilingLogicalContextTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            //_fabricContext.Dispose();
            //_fabricContext = null;
            return Task.CompletedTask;
        }

        [Fact]
        public async Task ProfilingLogicalContext_Create_01()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration()
                .UseLogicalDiagnostics(TestConfiguration.Root);
            await _testContext.FabricTestContext.ConfigureFabricContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            using var context = new LogicalContextFactory().CreateForProfiling(configuration, TestConfiguration.Root);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingLogicalContext_Create_02()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration()
                .UseLogicalDiagnostics(TestConfiguration.Root);
            await _testContext.FabricTestContext.ConfigureFabricContextConfiguration(configuration, true).ConfigureAwait(false);


            // Act.
            using var context = new LogicalContextFactory().CreateForProfiling(configuration, TestConfiguration.Root);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingLogicalContext_Create_03()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration()
                .UseLogicalDiagnostics(TestConfiguration.Root);
            await _testContext.FabricTestContext.ConfigureFabricContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            using var context = new LogicalContextFactory().CreateForProfiling(configuration, TestConfiguration.Root);

            // Assert.
            Assert.NotNull(context);
        }

    }
}
