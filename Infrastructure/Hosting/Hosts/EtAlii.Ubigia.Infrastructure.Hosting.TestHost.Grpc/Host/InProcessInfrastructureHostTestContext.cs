// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using System;
    using System.Diagnostics;
    using System.Security.Cryptography;
    using EtAlii.xTechnology.Hosting;
    using global::Grpc.Core;
    using global::Grpc.Core.Logging;
    using global::Grpc.Net.Client;

    public class InProcessInfrastructureHostTestContext : Grpc.HostTestContext, IHostTestContext
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

        public GrpcChannel CreateAdminGrpcInfrastructureChannel() =>  this.CreateChannel(ServiceDetails.ManagementAddress);
        public GrpcChannel CreateUserGrpcInfrastructureChannel() => this.CreateChannel(ServiceDetails.DataAddress);
        public GrpcChannel CreateGrpcInfrastructureChannel(Uri address) => this.CreateChannel(address.ToString());
    }
}