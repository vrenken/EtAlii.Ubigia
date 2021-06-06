namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ProfilingTraversalContextTests : IClassFixture<TraversalUnitTestContext>, IAsyncLifetime
    {
        private readonly TraversalUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;
        //private ILogicalContext _logicalContext;

        public ProfilingTraversalContextTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public Task InitializeAsync()
        {
            _diagnostics = DiagnosticsConfiguration.Default;
            //_logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true).ConfigureAwait(false);
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
//            _logicalContext.Dispose();
//            _logicalContext = null;
            _diagnostics = null;
            return Task.CompletedTask;
        }

        [Fact]
        public async Task ProfilingTraversalContext_Create_01()
        {
            // Arrange.
            var configuration = new FunctionalContextConfiguration()
                .UseTestTraversalParser()
                .UseTraversalProfiling();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var context = new TraversalContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingTraversalContext_Create_02()
        {
            // Arrange.
            var configuration = new FunctionalContextConfiguration()
                .UseTestTraversalParser()
                .UseFunctionalTraversalDiagnostics(_diagnostics)
                .UseTraversalProfiling();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var context = new TraversalContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingTraversalContext_Create_03()
        {
            // Arrange.
            var configuration = new FunctionalContextConfiguration()
                .UseTestTraversalParser()
                .UseFunctionalTraversalDiagnostics(_diagnostics)
                .UseTraversalProfiling();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var context = new TraversalContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }
    }
}
