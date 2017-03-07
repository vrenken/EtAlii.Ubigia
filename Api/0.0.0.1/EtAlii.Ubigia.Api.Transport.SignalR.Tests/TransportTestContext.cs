// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.SignalR.Tests;

    internal class TransportTestContext
    {
        public ITransportTestContext Create()
        {
            return new TransportTestContextFactory().Create<SignalRTransportTestContext>();
        }
    }
}