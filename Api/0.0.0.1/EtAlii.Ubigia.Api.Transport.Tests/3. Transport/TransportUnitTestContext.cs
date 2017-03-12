namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.xTechnology.Diagnostics;

    public class TransportUnitTestContext : IDisposable
    {
        public ITransportTestContext TransportTestContext { get; private set; }
        public IDiagnosticsConfiguration DiagnosticsConfiguration { get; private set; }

        public TransportUnitTestContext()
        {
            var task = Task.Run(async () =>
            {
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
            });
            task.Wait();
        }
    }
}