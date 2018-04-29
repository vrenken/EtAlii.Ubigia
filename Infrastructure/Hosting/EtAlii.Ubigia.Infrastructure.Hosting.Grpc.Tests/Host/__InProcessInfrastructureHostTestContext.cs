namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc;
    using global::Grpc.Core;

    public class InProcessInfrastructureHostTestContext : HostTestContext<InProcessInfrastructureTestHost>
    {
        public Channel CreateGrpcInfrastructureChannel()
        {
            var port = Host.Server.Ports.First();
            return new Channel("localhost", port.BoundPort, ChannelCredentials.Insecure);
        }
        
//        public IInfrastructureClient CreateRestInfrastructureClient()
//        {
//            var httpClientFactory = new TestHttpClientFactory(Host.Server);
//            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory);
//            return infrastructureClient;
//        }

    }
}