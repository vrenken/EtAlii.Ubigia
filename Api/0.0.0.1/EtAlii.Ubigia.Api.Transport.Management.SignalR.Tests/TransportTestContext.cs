// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.SignalR.Tests;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;

    internal class TransportTestContext
    {
        public ITransportTestContext<InProcessInfrastructureHostTestContext> Create()
        {
            return new TransportTestContextFactory().Create<SignalRTransportTestContext>();
        }
    }
}