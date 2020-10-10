namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.xTechnology.Diagnostics;

    public class NotStartedTransportUnitTestContext : IDisposable
    {
        public ITransportTestContext<InProcessInfrastructureHostTestContext> TransportTestContext { get; private set; }
        public IDiagnosticsConfiguration Diagnostics { get; private set; }

        public NotStartedTransportUnitTestContext()
        {
            Diagnostics = UbigiaDiagnostics.DefaultConfiguration;
            TransportTestContext = new TransportTestContext().Create();
        }

        public void Dispose()
        {
            TransportTestContext = null;
            Diagnostics = null;
        }
    }
}