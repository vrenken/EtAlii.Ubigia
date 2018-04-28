namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Transport;

    public partial class HostTestContext<TInfrastructureTestHost> : IHostTestContext<TInfrastructureTestHost>
        where TInfrastructureTestHost : class, IInfrastructureTestHost
    {
        public async Task<ISystemConnection> CreateSystemConnection()
        {
            var connectionConfiguration = new SystemConnectionConfiguration()
                .Use(Infrastructure)
                .Use(SystemTransportProvider.Create(Infrastructure));
            var connection = new SystemConnectionFactory().Create(connectionConfiguration);
            return await Task.FromResult(connection);
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
