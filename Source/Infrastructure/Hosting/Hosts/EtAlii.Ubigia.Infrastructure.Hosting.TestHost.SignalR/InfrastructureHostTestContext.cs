// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.Hosting;

    /// <summary>
    /// We need to make the name of this HostTestContext transport-agnostic in order for it to be used in all
    /// unit tests. Reason is that these are reused using shared projects.
    /// </summary>
    public class InfrastructureHostTestContext : HostTestContextBase, IInfrastructureHostTestContext
    {
        /// <inheritdoc />
        public override async Task Start(PortRange portRange)
        {
            await base
                .Start(portRange)
                .ConfigureAwait(false);
            ServiceDetails = Infrastructure.Options.ServiceDetails.Single(sd => sd.Name == ServiceDetailsName.SignalR);
        }
    }
}
