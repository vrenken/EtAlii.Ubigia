namespace EtAlii.Servus.Api.Diagnostics.Tests
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