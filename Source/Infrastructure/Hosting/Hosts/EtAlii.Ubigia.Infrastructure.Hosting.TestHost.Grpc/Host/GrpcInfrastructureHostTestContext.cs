// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc
{
	using System.Linq;
	using System.Threading.Tasks;
	using EtAlii.xTechnology.Hosting;

	public class GrpcInfrastructureHostTestContext : HostTestContextBase
    {
        /// <inheritdoc />
	    public override async Task Start(PortRange portRange)
	    {
		    await base.Start(portRange).ConfigureAwait(false);

		    ServiceDetails = Infrastructure.Options.ServiceDetails.Single(sd => sd.Name == "Grpc");
	    }
    }
}
