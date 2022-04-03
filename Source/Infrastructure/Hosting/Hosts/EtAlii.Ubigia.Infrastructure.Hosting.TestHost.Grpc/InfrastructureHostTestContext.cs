// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;
    using global::Grpc.Core;
    using global::Grpc.Core.Logging;
    using global::Grpc.Net.Client;

    /// <summary>
    /// We need to make the name of this HostTestContext transport-agnostic in order for it to be used in all
    /// unit tests. Reason is that these are reused using shared projects.
    /// </summary>
    public class InfrastructureHostTestContext : HostTestContextBase, IInfrastructureHostTestContext
    {
        public string HostIdentifier { get; }

        public InfrastructureHostTestContext()
        {
            if(Debugger.IsAttached)
            {
                // Server Startup
                GrpcEnvironment.SetLogger(new LogLevelFilterLogger(new ConsoleLogger(), LogLevel.Debug)); // show inner log
            }

            var bytes = new byte[64];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetNonZeroBytes(bytes);
            HostIdentifier = Convert.ToBase64String(bytes);
        }

        /// <inheritdoc />
        public override async Task Start()
        {
            await base
                .Start()
                .ConfigureAwait(false);

            ServiceDetails = Infrastructure.Options.ServiceDetails.Single(sd => sd.Name == ServiceDetailsName.Grpc);
        }

        public GrpcChannel CreateAdminGrpcInfrastructureChannel() => this.CreateChannel(ServiceDetails.ManagementAddress);

        public GrpcChannel CreateUserGrpcInfrastructureChannel() => this.CreateChannel(ServiceDetails.DataAddress);

        public GrpcChannel CreateGrpcInfrastructureChannel(Uri address) => this.CreateChannel(address.ToString());
    }
}
