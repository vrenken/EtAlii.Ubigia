﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.NetCore
{
	using System;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport;
	using EtAlii.xTechnology.Hosting;

	public class HostTestContext : HostTestContextBase<InfrastructureTestHost>, IHostTestContext<InfrastructureTestHost>
    {
	    protected HostTestContext()
		    : base("Host/settings.json")
	    {
	    }
	    
	    public override async Task Start(PortRange portRange)
	    {
		    await base.Start(portRange);
		    
		    // TODO: Ugly. This needs to change and not be needed at all.
		    // However, for now it works...
		    var codeBase = Assembly.GetExecutingAssembly()!.Location;
		    var isWebApiTestBase = codeBase!.Contains("WebApi.Tests") || 
		                           codeBase.Contains("PowerShell.Tests");
		    var isSignalRTestBase = codeBase.Contains("SignalR.Tests") || 
		                            codeBase.Contains("Google.Tests");
		    var isSystemTestBase = codeBase.Contains("Infrastructure.Hosting.NetCore.Tests");
		    
		    if (isWebApiTestBase && isSignalRTestBase)
		    {
			    throw new NotSupportedException("SignalR and WebApi unit tests cannot live in the same assembly (yet)");
		    }

		    if (isSignalRTestBase)
		    {
			    ServiceDetails = Infrastructure.Configuration.ServiceDetails.Single(sd => sd.Name == "SignalR");
		    }
			else if (isWebApiTestBase)
		    {
			    ServiceDetails = Infrastructure.Configuration.ServiceDetails.Single(sd => sd.Name == "WebApi");
		    }
		    else if (isSystemTestBase)
		    {
			    ServiceDetails = Infrastructure.Configuration.ServiceDetails.Single(sd => sd.IsSystemService);
		    }
		    else
		    {
			    throw new NotSupportedException($"Unable to determine SignalR, WebApi or System unit tests targeting: {codeBase}");
		    }
	    }

	    public Task<ISystemConnection> CreateSystemConnection()
	    {
		    var connectionConfiguration = new SystemConnectionConfiguration()
			    .Use(Infrastructure)
			    .Use(SystemTransportProvider.Create(Infrastructure));
		    var connection = new SystemConnectionFactory().Create(connectionConfiguration);
		    return Task.FromResult(connection);
	    }

	    public async Task AddUserAccountAndSpaces(ISystemConnection connection, string accountName, string password, string[] spaceNames)
	    {
		    var managementConnection = await connection.OpenManagementConnection();
		    var account = await managementConnection.Accounts.Add(accountName, password, AccountTemplate.User);

		    foreach (var spaceName in spaceNames)
		    {
			    await managementConnection.Spaces.Add(account.Id, spaceName, SpaceTemplate.Data);
		    }
		    await managementConnection.Close();
	    }
	}
}
