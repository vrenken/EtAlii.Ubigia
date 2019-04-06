namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Fabric.Tests;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ProfilingLogicalContextTests : IClassFixture<FabricUnitTestContext>, IAsyncLifetime
    {
        private readonly FabricUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;
        private IFabricContext _fabricContext;

        public ProfilingLogicalContextTests(FabricUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            _diagnostics = TestDiagnostics.Create();
            _fabricContext = await _testContext.FabricTestContext.CreateFabricContext(true);
        }

        public Task DisposeAsync()
        {
            _fabricContext.Dispose();
            _fabricContext = null;
            _diagnostics = null;
            return Task.CompletedTask;
        }

        [Fact]
        public void ProfilingLogicalContext_Create_01()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration()
                .Use(_fabricContext);

            // Act.
            var context = new LogicalContextFactory().CreateForProfiling(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public void ProfilingLogicalContext_Create_02()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration()
                .Use(_diagnostics)
                .Use(_fabricContext);

            // Act.
            var context = new LogicalContextFactory().CreateForProfiling(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public void ProfilingLogicalContext_Create_03()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration()
                .Use(_fabricContext)
                .Use(_diagnostics);

            // Act.
            var context = new LogicalContextFactory().CreateForProfiling(configuration);

            // Assert.
            Assert.NotNull(context);
        }

    }
}
