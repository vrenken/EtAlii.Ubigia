namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore.Tests;
    using EtAlii.xTechnology.Diagnostics;

    public class StartedTransportUnitTestContext : IDisposable
    {
        public ITransportTestContext<InProcessInfrastructureHostTestContext> TransportTestContext { get; private set; }
        public IDiagnosticsConfiguration Diagnostics { get; private set; }

        public StartedTransportUnitTestContext()
        {
            var task = Task.Run(async () =>
            {
                Diagnostics = TestDiagnostics.Create();
                TransportTestContext = new TransportTestContext().Create();
                await TransportTestContext.Start();
            });
            //task = task.ContinueWith(t => throw new InvalidOperationException("Unable to start the TransportUnitTest", t.Exception), TaskContinuationOptions.OnlyOnFaulted);
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