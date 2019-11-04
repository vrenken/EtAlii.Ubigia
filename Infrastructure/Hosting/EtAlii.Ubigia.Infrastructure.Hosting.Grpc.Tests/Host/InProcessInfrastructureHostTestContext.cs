namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Diagnostics;
    using System.Security.Cryptography;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc;
    using Grpc.Core;
    using Grpc.Core.Logging;
    using Grpc.Net.Client;

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
            var rnd = RandomNumberGenerator.Create();
            rnd.GetNonZeroBytes(bytes);
            HostIdentifier = Convert.ToBase64String(bytes);
        }

        public GrpcChannel CreateAdminGrpcInfrastructureChannel()
        { 
            var options = new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure };
            return  GrpcChannel.ForAddress($"http://localhost:{Host.AdminModule.Port}", options);
        }
        public GrpcChannel CreateUserGrpcInfrastructureChannel()
        {
            var options = new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure };
            return  GrpcChannel.ForAddress($"http://localhost:{Host.UserModule.Port}", options);
        }
        public GrpcChannel CreateGrpcInfrastructureChannel(Uri address)
        {
            var options = new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure };
            return  GrpcChannel.ForAddress(address, options);
        }
        
//        public IInfrastructureClient CreateRestInfrastructureClient()
//        [
//            var httpClientFactory = new TestHttpClientFactory(Host.Server)
//            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory)
//            return infrastructureClient
//        ]
    }
}