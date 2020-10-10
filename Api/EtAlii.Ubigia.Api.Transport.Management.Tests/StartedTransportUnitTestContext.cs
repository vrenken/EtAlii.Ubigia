namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.xTechnology.Diagnostics;
    using Xunit;

    public class StartedTransportUnitTestContext : IAsyncLifetime
    {
        public ITransportTestContext<InProcessInfrastructureHostTestContext> TransportTestContext { get; private set; }
        public IDiagnosticsConfiguration Diagnostics { get; private set; }

        public async Task InitializeAsync()
        {
            Diagnostics = UbigiaDiagnostics.DefaultConfiguration;
            TransportTestContext = new TransportTestContext().Create();
            await TransportTestContext.Start();
        }

        public async Task DisposeAsync()
        {
            await TransportTestContext.Stop();
            TransportTestContext = null;
            Diagnostics = null;
        }
    }
}