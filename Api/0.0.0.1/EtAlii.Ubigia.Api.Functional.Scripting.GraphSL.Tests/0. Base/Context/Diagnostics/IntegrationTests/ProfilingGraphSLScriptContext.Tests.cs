namespace EtAlii.Ubigia.Api.Functional.Scripting.GraphSL.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ProfilingGraphSLScriptContextTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
    {
        private readonly LogicalUnitTestContext _testContext;
        private IDiagnosticsConfiguration _diagnostics;
        //private ILogicalContext _logicalContext;

        public ProfilingGraphSLScriptContextTests(LogicalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        public Task InitializeAsync()
        {
            _diagnostics = TestDiagnostics.Create();
            //_logicalContext = await _testContext.LogicalTestContext.CreateLogicalContext(true);
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
        public async Task ProfilingGraphSLScriptContext_Create_01()
        {
            // Arrange.
            var configuration = new GraphSLScriptContextConfiguration()
                .UseGraphSLProfiling();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true);

            // Act.
            var context = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingGraphSLScriptContext_Create_02()
        {
            // Arrange.
            var configuration = new GraphSLScriptContextConfiguration()
                .UseFunctionalGraphSLDiagnostics(_diagnostics)
                .UseGraphSLProfiling();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true);

            // Act.
            var context = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public async Task ProfilingGraphSLScriptContext_Create_03()
        {
            // Arrange.
            var configuration = new GraphSLScriptContextConfiguration()
                .UseFunctionalGraphSLDiagnostics(_diagnostics)
                .UseGraphSLProfiling();
            await _testContext.LogicalTestContext.ConfigureLogicalContextConfiguration(configuration, true);

            // Act.
            var context = new GraphSLScriptContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }
    }
}
