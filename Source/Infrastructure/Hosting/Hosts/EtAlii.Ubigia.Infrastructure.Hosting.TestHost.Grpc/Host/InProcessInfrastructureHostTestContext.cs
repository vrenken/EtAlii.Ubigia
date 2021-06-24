// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using System;
    using System.Diagnostics;
    using System.Security.Cryptography;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc;
    using global::Grpc.Core;
    using global::Grpc.Core.Logging;
    using global::Grpc.Net.Client;

    public class InProcessInfrastructureHostTestContext : GrpcHostTestContext, IHostTestContext
    {
        public string HostIdentifier { get; }

        public InProcessInfrastructureHostTestContext()
        {
            UseInProcessConnection = true;

            if(Debugger.IsAttached)
            {
                // Server Startup
                GrpcEnvironment.SetLogger(new LogLevelFilterLogger(new ConsoleLogger(), LogLevel.Debug) ); // show inner log
            }

            var bytes = new byte[64];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetNonZeroBytes(bytes);
            HostIdentifier = Convert.ToBase64String(bytes);
        }

        public GrpcChannel CreateAdminGrpcInfrastructureChannel() => this.CreateChannel(ServiceDetails.ManagementAddress);

        public GrpcChannel CreateUserGrpcInfrastructureChannel() => this.CreateChannel(ServiceDetails.DataAddress);

        public GrpcChannel CreateGrpcInfrastructureChannel(Uri address) => this.CreateChannel(address.ToString());
    }
}
