namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Fabric.Tests;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class ProfilingLogicalContextTests : IClassFixture<FabricUnitTestContext>, IDisposable
    {
        private IDiagnosticsConfiguration _diagnostics;
        private IFabricContext _fabricContext;
        private readonly FabricUnitTestContext _testContext;

        public ProfilingLogicalContextTests(FabricUnitTestContext testContext)
        {
            _testContext = testContext;

            var task = Task.Run(async () =>
            {
                _diagnostics = TestDiagnostics.Create();
                _fabricContext = await _testContext.FabricTestContext.CreateFabricContext(true);
            });
            task.Wait();

        }

        public void Dispose()
        {
            var task = Task.Run(() =>
            {
                _fabricContext.Dispose();
                _fabricContext = null;
                _diagnostics = null;
            });
            task.Wait();
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
