namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.WebApi.Tests;
    using EtAlii.Ubigia.Api.Transport.Tests;

    internal class TransportTestContext
    {
        public ITransportTestContext Create()
        {
            return new TransportTestContextFactory().Create<WebApiTransportTestContext>();
        }
    }
}