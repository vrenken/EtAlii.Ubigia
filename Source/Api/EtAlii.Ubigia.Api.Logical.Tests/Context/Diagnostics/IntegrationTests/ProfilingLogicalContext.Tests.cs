namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ProfilingLogicalContextTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private readonly LogicalUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;

        public ProfilingLogicalContextTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public Task InitializeAsync()
        {
            _diagnostics = DiagnosticsConfiguration.Default;
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            //_fabricContext.Dispose();
            //_fabricContext = null;
            _diagnostics = null;
            return Task.CompletedTask;
        }

        [Fact]
        public async Task ProfilingLogicalContext_Create_01()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration();
            await _testContext.FabricTestContext.ConfigureFabricContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            using var context = new LogicalContextFactory().CreateForProfiling(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingLogicalContext_Create_02()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration()
                .UseLogicalDiagnostics(_diagnostics);
            await _testContext.FabricTestContext.ConfigureFabricContextConfiguration(configuration, true).ConfigureAwait(false);


            // Act.
            using var context = new LogicalContextFactory().CreateForProfiling(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingLogicalContext_Create_03()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration()
                .UseLogicalDiagnostics(_diagnostics);
            await _testContext.FabricTestContext.ConfigureFabricContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            using var context = new LogicalContextFactory().CreateForProfiling(configuration);

            // Assert.
            Assert.NotNull(context);
        }

    }
}
