namespace EtAlii.Ubigia.Api.Diagnostics.Tests
{
    using EtAlii.Ubigia.Api.Transport.Tests;

    internal class TransportTestContext
    {
        public ITransportTestContext Create()
        {
            return new TransportTestContextFactory().Create<SignalRTransportTestContext>();
        }
    }
}