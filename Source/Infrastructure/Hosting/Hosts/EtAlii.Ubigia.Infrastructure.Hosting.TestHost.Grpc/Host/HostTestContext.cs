﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc
{
	using System.Linq;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport;
	using EtAlii.xTechnology.Hosting;

	public class HostTestContext : HostTestContextBase<InfrastructureTestHost>
    {
	    protected HostTestContext()
		    : base("Host/settings.json")
	    {
	    }

	    public override async Task Start(PortRange portRange)
	    {
		    await base.Start(portRange);

		    ServiceDetails = Infrastructure.Configuration.ServiceDetails.Single(sd => sd.Name == "Grpc");
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