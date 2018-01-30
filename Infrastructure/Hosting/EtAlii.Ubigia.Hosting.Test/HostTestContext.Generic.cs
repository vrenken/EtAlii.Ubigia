namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport;
    using EtAlii.xTechnology.Hosting;

    public class HostTestContext<THost>
        where THost : class, IHost
    {
        public IInfrastructure Infrastructure { get; private set; }
        public THost Host { get; private set; }
        public string SystemAccountName { get; private set; }
		public string SystemAccountPassword { get; private set; }
		public string TestAccountName { get; private set; }
		public string TestAccountPassword { get; private set; }

		public void Start(IHost host, IInfrastructure infrastructure)
        {
            Host = (THost)host;
            Host.Start();

            Infrastructure = infrastructure;

	        var systemAccount = Infrastructure.Accounts.Get("System");
	        SystemAccountName = systemAccount.Name;
	        SystemAccountPassword = systemAccount.Password;

	        var adminAccount = Infrastructure.Accounts.Get("Administrator");
	        TestAccountName = adminAccount.Name;
	        TestAccountPassword = adminAccount.Password;
        }

		public async Task<ISystemConnection> CreateSystemConnection()
        {
            var connectionConfiguration = new SystemConnectionConfiguration()
                .Use(Infrastructure)
                .Use(SystemTransportProvider.Create(Infrastructure));
            var connection = new SystemConnectionFactory().Create(connectionConfiguration);
            return await Task.FromResult(connection);
        }



        public void Stop()
        {
            Host.Stop();
            Host = null;

            Infrastructure = null;

	        SystemAccountName = null;
	        SystemAccountPassword = null;
	        TestAccountName = null;
	        TestAccountPassword = null;
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
