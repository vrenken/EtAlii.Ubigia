namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using EtAlii.xTechnology.Diagnostics;

    public class NotStartedTransportUnitTestContext : IDisposable
    {
        public ITransportTestContext<InProcessInfrastructureHostTestContext> TransportTestContext { get; private set; }
        public IDiagnosticsConfiguration Diagnostics { get; private set; }

        public NotStartedTransportUnitTestContext()
        {
            Diagnostics = TestDiagnostics.Create();
            TransportTestContext = new TransportTestContext().Create();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            if (disposing)
            {
                TransportTestContext = null;
                Diagnostics = null;
            }
        }

        ~NotStartedTransportUnitTestContext()
        {
            Dispose(false);
        }
    }
}