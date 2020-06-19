namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using System.Diagnostics;
    using System.Security.Cryptography;
    using EtAlii.xTechnology.Hosting;
    using Grpc.Core;
    using Grpc.Core.Logging;
    using Grpc.Net.Client;

    public class InProcessInfrastructureHostTestContext : HostTestContext, IHostTestContext
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

        public GrpcChannel CreateAdminGrpcInfrastructureChannel() =>  this.CreateChannel(ManagementApiAddress);
        public GrpcChannel CreateUserGrpcInfrastructureChannel() => this.CreateChannel(DataApiAddress);
        public GrpcChannel CreateGrpcInfrastructureChannel(Uri address) => this.CreateChannel(address.ToString());
    }
}