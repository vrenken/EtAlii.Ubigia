namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ProfilingGraphSLScriptContextTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private readonly LogicalUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;

        public ProfilingGraphSLScriptContextTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public async Task InitializeAsync()
        {
            _diagnostics = TestDiagnostics.Create();
            _logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
        }

        public Task DisposeAsync()
        {
            _logicalContext.Dispose();
            _logicalContext = null;
            _diagnostics = null;
            return Task.CompletedTask;
        }

        [Fact]
        public void ProfilingGraphSLScriptContext_Create_01()
        {
            // Arrange.
            var configuration = new GraphSLScriptContextConfiguration()
                .Use(_logicalContext);

            // Act.
            var context = new GraphSLScriptContextFactory().CreateForProfiling(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public void ProfilingGraphSLScriptContext_Create_02()
        {
            // Arrange.
            var configuration = new GraphSLScriptContextConfiguration()
                .Use(_diagnostics)
                .Use(_logicalContext);

            // Act.
            var context = new GraphSLScriptContextFactory().CreateForProfiling(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public void ProfilingGraphSLScriptContext_Create_03()
        {
            // Arrange.
            var configuration = new GraphSLScriptContextConfiguration()
                .Use(_logicalContext)
                .Use(_diagnostics);

            // Act.
            var context = new GraphSLScriptContextFactory().CreateForProfiling(configuration);

            // Assert.
            Assert.NotNull(context);
        }
    }
}
