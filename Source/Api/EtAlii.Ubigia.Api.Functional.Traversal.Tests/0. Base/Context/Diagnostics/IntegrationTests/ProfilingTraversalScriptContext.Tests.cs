namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ProfilingTraversalScriptContextTests : IClassFixture<ScriptingUnitTestContext>, IAsyncLifetime
    {
        private readonly ScriptingUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;
        //private ILogicalContext _logicalContext;

        public ProfilingTraversalScriptContextTests(ScriptingUnitTestContext testContext)
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
        public async Task ProfilingTraversalScriptContext_Create_01()
        {
            // Arrange.
            var configuration = new TraversalScriptContextConfiguration()
                .UseTestParser()
                .UseTraversalProfiling();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var context = new TraversalScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingTraversalScriptContext_Create_02()
        {
            // Arrange.
            var configuration = new TraversalScriptContextConfiguration()
                .UseTestParser()
                .UseFunctionalTraversalDiagnostics(_diagnostics)
                .UseTraversalProfiling();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var context = new TraversalScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingTraversalScriptContext_Create_03()
        {
            // Arrange.
            var configuration = new TraversalScriptContextConfiguration()
                .UseTestParser()
                .UseFunctionalTraversalDiagnostics(_diagnostics)
                .UseTraversalProfiling();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true).ConfigureAwait(false);

            // Act.
            var context = new TraversalScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }
    }
}
