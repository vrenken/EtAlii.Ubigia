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

    public class ProfilingDataContextTests : IClassFixture<LogicalUnitTestContext>, IDisposable
    {
        private IDiagnosticsConfiguration _diagnostics;
        private ILogicalContext _logicalContext;

        public ProfilingDataContextTests(LogicalUnitTestContext testContext)
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
        public void ProfilingDataContext_Create_01()
        {
            // Arrange.
            var configuration = new DataContextConfiguration()
                .Use(_logicalContext);

            // Act.
            var context = new DataContextFactory().CreateForProfiling(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public void ProfilingDataContext_Create_02()
        {
            // Arrange.
            var configuration = new DataContextConfiguration()
                .Use(_diagnostics)
                .Use(_logicalContext);

            // Act.
            var context = new DataContextFactory().CreateForProfiling(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact]
        public void ProfilingDataContext_Create_03()
        {
            // Arrange.
            var configuration = new DataContextConfiguration()
                .Use(_logicalContext)
                .Use(_diagnostics);

            // Act.
            var context = new DataContextFactory().CreateForProfiling(configuration);

            // Assert.
            Assert.NotNull(context);
        }
    }
}
