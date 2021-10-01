// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Rest
{
	using System;
    using System.Linq;
    using System.Reflection;
	using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;

    public class RestInfrastructureHostTestContext : Hosting.TestHost.HostTestContextBase<InfrastructureTestHost>, IInfrastructureHostTestContext<InfrastructureTestHost>
    {
        /// <inheritdoc />
        public override async Task Start(PortRange portRange)
	    {
		    await base.Start(portRange).ConfigureAwait(false);

            // Improve this RestHostTestContext: is very ugly and breaks with many standardizations we tried to put in place.
            // However, for now it works...
            // More details can be found in the Github issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/91

            var codeBase = Assembly.GetExecutingAssembly()!.Location;
		    var isRestTestBase = codeBase!.Contains("Rest.Tests") ||
		                           codeBase.Contains("PowerShell.Tests");
		    var isSignalRTestBase = codeBase.Contains("SignalR.Tests") ||
		                            codeBase.Contains("Google.Tests");
		    var isSystemTestBase = codeBase.Contains("Infrastructure.Hosting.NetCore.Tests");

		    if (isRestTestBase && isSignalRTestBase)
		    {
			    throw new NotSupportedException("SignalR and Rest unit tests cannot live in the same assembly (yet)");
		    }

		    if (isSignalRTestBase)
		    {
			    ServiceDetails = Infrastructure.Options.ServiceDetails.Single(sd => sd.Name == "SignalR");
		    }
			else if (isRestTestBase)
		    {
			    ServiceDetails = Infrastructure.Options.ServiceDetails.Single(sd => sd.Name == "Rest");
		    }
		    else if (isSystemTestBase)
		    {
			    ServiceDetails = Infrastructure.Options.ServiceDetails.Single(sd => sd.IsSystemService);
		    }
		    else
		    {
			    throw new NotSupportedException($"Unable to determine SignalR, Rest or System unit tests targeting: {codeBase}");
		    }
	    }
    }
}
