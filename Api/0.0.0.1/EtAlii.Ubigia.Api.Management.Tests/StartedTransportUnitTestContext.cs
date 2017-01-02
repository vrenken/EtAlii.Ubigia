namespace EtAlii.Ubigia.Api.Management.IntegrationTests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.xTechnology.Diagnostics;

    public class StartedTransportUnitTestContext : IDisposable
    {
        public ITransportTestContext TransportTestContext { get; private set; }
        public IDiagnosticsConfiguration Diagnostics { get; private set; }

        public StartedTransportUnitTestContext()
        {
            var task = Task.Run(async () =>
            {
                Diagnostics = TestDiagnostics.Create();
                TransportTestContext = new TransportTestContext().Create();
                await TransportTestContext.Start();
            });
            task.Wait();
        }

        public void Dispose()
        {
            var task = Task.Run(async () =>
            {
                await TransportTestContext.Stop();
                TransportTestContext = null;
                Diagnostics = null;
            });
            task.Wait();
        }
    }
}