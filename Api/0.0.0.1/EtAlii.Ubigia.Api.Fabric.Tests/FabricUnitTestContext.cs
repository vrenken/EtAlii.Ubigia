namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class FabricUnitTestContext : IAsyncLifetime
    {
        public IFabricTestContext FabricTestContext { get; private set; }
        public IDiagnosticsConfiguration DiagnosticsConfiguration { get; private set; }

        public FabricUnitTestContext()
        {
        }

        public async Task InitializeAsync()
        {
            DiagnosticsConfiguration = TestDiagnostics.Create();
            FabricTestContext = new FabricTestContextFactory().Create();
            await FabricTestContext.Start();
        }

        public async Task DisposeAsync()
        {
            await FabricTestContext.Stop();
            FabricTestContext = null;
            DiagnosticsConfiguration = null;
        }
    }
}