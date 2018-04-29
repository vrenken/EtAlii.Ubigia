//namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
//{
//    using EtAlii.Ubigia.Api.Transport.Grpc;
//    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc;
//
//    public class InProcessInfrastructureHostTestContext : HostTestContext<InProcessInfrastructureTestHost>
//    {
//        public IInfrastructureClient CreateRestInfrastructureClient()
//        {
//            var httpClientFactory = new TestHttpClientFactory(Host.Server);
//            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory);
//            return infrastructureClient;
//        }
//
//    }
//}