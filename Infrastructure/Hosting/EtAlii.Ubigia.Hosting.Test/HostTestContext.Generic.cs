namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure;
    using EtAlii.Ubigia.Infrastructure.Transport;

    public class HostTestContext<THost>
        where THost : class, IHost
    {
        public THost Host { get; private set; }
        public string SystemAccountName { get { return Host?.Infrastructure.Configuration.Account; } }
        public string SystemAccountPassword { get { return Host?.Infrastructure.Configuration.Password; } }

        public void Start(IHost host)
        {
            Host = (THost)host;
            Host.Start();
        }

        public async Task<ISystemConnection> CreateSystemConnection()
        {
            var connectionConfiguration = new SystemConnectionConfiguration()
                .Use(Host.Infrastructure)
                .Use(SystemTransportProvider.Create(Host.Infrastructure));
            var connection = new SystemConnectionFactory().Create(connectionConfiguration);
            return await Task.FromResult(connection);
        }



        public void Stop()
        {
            Host.Stop();
            Host = null;
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
