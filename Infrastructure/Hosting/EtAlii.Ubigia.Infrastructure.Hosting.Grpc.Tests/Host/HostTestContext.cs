namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
	using System.Linq;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Transport;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc;
	using EtAlii.Ubigia.Infrastructure.Transport;

	public class HostTestContext : HostTestContextBase<InfrastructureTestHost>
    {
	    protected HostTestContext()
		    : base("Host/settings.json")
	    {
	    }

	    public override async Task Start()
	    {
		    await base.Start();

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
