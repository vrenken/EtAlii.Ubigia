namespace EtAlii.Ubigia.Infrastructure.Hosting.Grpc.Tests
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc;
    using global::Grpc.Core;

    public class InProcessInfrastructureHostTestContext : HostTestContext<InProcessInfrastructureTestHost>
    {
        public string HostIdentifier { get; }

        public InProcessInfrastructureHostTestContext()
        {
            var bytes = new byte[64];
            var rnd = new Random();
            rnd.NextBytes(bytes);
            HostIdentifier = Convert.ToBase64String(bytes);
        }

        public Channel CreateAdminGrpcInfrastructureChannel()
        {
            return new Channel("localhost", Host.AdminModule.Port, ChannelCredentials.Insecure);
        }
        public Channel CreateUserGrpcInfrastructureChannel()
        {
            return new Channel("localhost", Host.UserModule.Port, ChannelCredentials.Insecure);
        }
        
//        public IInfrastructureClient CreateRestInfrastructureClient()
//        {
//            var httpClientFactory = new TestHttpClientFactory(Host.Server);
//            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory);
//            return infrastructureClient;
//        }

    }
}