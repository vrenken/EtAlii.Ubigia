// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.Rest.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;

    public class TransportTestContext
    {
        public ITransportTestContext<InProcessInfrastructureHostTestContext> Create()
        {
            return new TransportTestContextFactory().Create<RestTransportTestContext>();
        }
    }
}