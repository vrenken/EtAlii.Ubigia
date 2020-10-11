namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.Grpc.Tests;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;

    internal class TransportTestContext
    {
        public ITransportTestContext<InProcessInfrastructureHostTestContext> Create()
        {
            return new TransportTestContextFactory().Create<GrpcTransportTestContext>();
        }
    }
}