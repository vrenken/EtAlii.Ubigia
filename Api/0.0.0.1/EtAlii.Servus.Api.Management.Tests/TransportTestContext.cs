namespace EtAlii.Servus.Api.Management.IntegrationTests
{
    using EtAlii.Servus.Api.Transport.Tests;

    internal class TransportTestContext
    {
        public ITransportTestContext Create()
        {
            return new TransportTestContextFactory().Create<SignalRTransportTestContext>();
        }
    }
}