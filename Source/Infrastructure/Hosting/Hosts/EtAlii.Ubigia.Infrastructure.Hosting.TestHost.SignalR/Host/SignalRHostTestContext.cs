// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.SignalR
{
	using System;
	using System.Linq;
    using System.Reflection;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport;
	using EtAlii.xTechnology.Hosting;

	public class SignalRHostTestContext : EtAlii.Ubigia.Infrastructure.Hosting.TestHost.HostTestContextBase<InfrastructureTestHost>, IHostTestContext<InfrastructureTestHost>
    {
        public override async Task Start(PortRange portRange)
	    {
		    await base.Start(portRange).ConfigureAwait(false);

            // Improve this SignalRHostTestContext: is very ugly and breaks with many standardizations we tried to put in place.
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
			    ServiceDetails = Infrastructure.Configuration.ServiceDetails.Single(sd => sd.Name == "SignalR");
		    }
			else if (isRestTestBase)
		    {
			    ServiceDetails = Infrastructure.Configuration.ServiceDetails.Single(sd => sd.Name == "Rest");
		    }
		    else if (isSystemTestBase)
		    {
			    ServiceDetails = Infrastructure.Configuration.ServiceDetails.Single(sd => sd.IsSystemService);
		    }
		    else
		    {
			    throw new NotSupportedException($"Unable to determine SignalR, Rest or System unit tests targeting: {codeBase}");
		    }
	    }

	    public Task<ISystemConnection> CreateSystemConnection()
	    {
		    var connectionConfiguration = new SystemConnectionConfiguration()
			    .Use(Infrastructure)
			    .Use(new SystemTransportProvider(Infrastructure));
		    var connection = new SystemConnectionFactory().Create(connectionConfiguration);
		    return Task.FromResult(connection);
	    }

	    public async Task AddUserAccountAndSpaces(ISystemConnection connection, string accountName, string password, string[] spaceNames)
	    {
		    var managementConnection = await connection.OpenManagementConnection().ConfigureAwait(false);
		    var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User).ConfigureAwait(false);

		    foreach (var spaceName in spaceNames)
		    {
			    await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Data).ConfigureAwait(false);
		    }
		    await managementConnection.Close().ConfigureAwait(false);
	    }
	}
}
