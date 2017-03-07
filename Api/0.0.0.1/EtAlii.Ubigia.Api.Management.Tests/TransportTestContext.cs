namespace EtAlii.Ubigia.Api.Management.IntegrationTests
{
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.SignalR.Tests;
    using EtAlii.Ubigia.Api.Transport.Tests;

    internal class TransportTestContext
    {
        public ITransportTestContext Create()
        {
            return new TransportTestContextFactory().Create<SignalRTransportTestContext>();
        }
    }
}