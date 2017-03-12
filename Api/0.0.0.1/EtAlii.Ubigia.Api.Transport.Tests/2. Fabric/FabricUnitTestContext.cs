namespace EtAlii.Ubigia.Api.Diagnostics.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;

    public class FabricUnitTestContext : IDisposable
    {
        public IFabricTestContext FabricTestContext { get; private set; }
        public IDiagnosticsConfiguration DiagnosticsConfiguration { get; private set; }

        public FabricUnitTestContext()
        {
            var task = Task.Run(async () =>
            {
                DiagnosticsConfiguration = TestDiagnostics.Create();
                FabricTestContext = new FabricTestContextFactory().Create();
                await FabricTestContext.Start();
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(async () =>
            {
                await FabricTestContext.Stop();
                FabricTestContext = null;
                DiagnosticsConfiguration = null;
            });
            task.Wait();
        }
    }
}