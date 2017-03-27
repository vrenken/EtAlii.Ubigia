namespace EtAlii.Ubigia.Api.Transport.SignalR.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management.SignalR;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting;
    using EtAlii.Ubigia.Infrastructure;

    public class SignalRTransportTestContext : TransportTestContextBase
    {
        public SignalRTransportTestContext(IHostTestContextFactory testHostFactory) : base(testHostFactory)
        {
        }

        public override async Task<IDataConnection> CreateDataConnection(string address, string accountName, string accountPassword, string spaceName, bool openOnCreation, bool useNewSpace, SpaceTemplate spaceTemplate = null)
        {
            var diagnostics = TestDiagnostics.Create();

            var signalRHttpClient = new SignalRTestHttpClient(c => ((TestInfrastructure)Context.Host.Infrastructure).Server.Handler); 

            var connectionConfiguration = new DataConnectionConfiguration()
                .Use(SignalRTransportProvider.Create(signalRHttpClient))
                .Use(address)
                .Use(accountName, spaceName, accountPassword)
                .Use(diagnostics);
            var connection = new DataConnectionFactory().Create(connectionConfiguration);

            if (useNewSpace)
            {
                var managementConnection = await CreateManagementConnection();
                var account = await managementConnection.Accounts.Get(accountName) ??
                              await managementConnection.Accounts.Add(accountName, accountPassword, AccountTemplate.User);
                await managementConnection.Spaces.Add(account.Id, spaceName, spaceTemplate ?? SpaceTemplate.Data);
                await managementConnection.Close();
            }

            if (openOnCreation)
            {
                await connection.Open();
            }
            return connection;
        }

        public override async Task<IManagementConnection> CreateManagementConnection(string address, string account, string password, bool openOnCreation = true)
        {
            var diagnostics = TestDiagnostics.Create();
            var signalRHttpClient = new SignalRTestHttpClient(c => ((TestInfrastructure)Context.Host.Infrastructure).Server.Handler);

            var connectionConfiguration = new ManagementConnectionConfiguration()
                .Use(SignalRStorageTransportProvider.Create(signalRHttpClient))
                .Use(address)
                .Use(account, password)
                .Use(diagnostics);
            var connection = new ManagementConnectionFactory().Create(connectionConfiguration);
            if (openOnCreation)
            {
                await connection.Open();
            }
            return connection;
        }
    }
}
