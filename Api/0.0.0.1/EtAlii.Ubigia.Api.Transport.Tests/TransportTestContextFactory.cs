namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;

    public class TransportTestContextFactory
    {
        public TTransportTestContext Create<TTransportTestContext>()
            where TTransportTestContext : TransportTestContextBase<InProcessInfrastructureHostTestContext>, new()
        {
            return new TTransportTestContext();
        }
    }
}
