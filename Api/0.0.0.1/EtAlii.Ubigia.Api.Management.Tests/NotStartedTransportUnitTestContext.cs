namespace EtAlii.Ubigia.Api.Management.IntegrationTests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical.Tests;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.xTechnology.Diagnostics;

    public class NotStartedTransportUnitTestContext : IDisposable
    {
        public ITransportTestContext TransportTestContext { get; private set; }
        public IDiagnosticsConfiguration Diagnostics { get; private set; }

        public NotStartedTransportUnitTestContext()
        {
            Diagnostics = TestDiagnostics.Create();
            TransportTestContext = new TransportTestContext().Create();
        }

        public void Dispose()
        {
            TransportTestContext = null;
            Diagnostics = null;
        }
    }
}