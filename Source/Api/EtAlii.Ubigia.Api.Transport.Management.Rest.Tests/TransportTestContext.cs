namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Api.Transport.Rest.Tests;

    internal class TransportTestContext
    {
        public ITransportTestContext Create()
        {
            return new TransportTestContextFactory().Create<RestTransportTestContext>();
        }
    }
}