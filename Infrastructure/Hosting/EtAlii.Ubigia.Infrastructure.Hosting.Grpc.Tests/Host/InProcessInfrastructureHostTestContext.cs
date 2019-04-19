namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc;
    using global::Grpc.Core;
    using global::Grpc.Core.Logging;

    public class InProcessInfrastructureHostTestContext : HostTestContext<InProcessInfrastructureTestHost>
    {
        public string HostIdentifier { get; }

        public InProcessInfrastructureHostTestContext()
        {
            if(Debugger.IsAttached)
            {
                // Server Startup
                GrpcEnvironment.SetLogger(new LogLevelFilterLogger(new ConsoleLogger(), LogLevel.Debug) ); // show inner log
            }
            
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
        public Channel CreateGrpcInfrastructureChannel(Uri address)
        {
            return new Channel(address.DnsSafeHost, address.Port, ChannelCredentials.Insecure);
        }
        
//        public IInfrastructureClient CreateRestInfrastructureClient()
//        [
//            var httpClientFactory = new TestHttpClientFactory(Host.Server)
//            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory)
//            return infrastructureClient
//        }

    }
}