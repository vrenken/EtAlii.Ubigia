namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Diagnostics;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ProfilingGraphSLScriptContextTests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;

        public ProfilingGraphSLScriptContextTests(LogicalUnitTestContext testContext)
        {
            var task = Task.Run(async () =>
            {
                _diagnostics = TestDiagnostics.Create();
                _logicalContext = await testContext.LogicalTestContext.CreateLogicalContext(true);
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(() =>
            {
                _logicalContext.Dispose();
                _logicalContext = null;
                _diagnostics = null;
            });
            task.Wait();
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
