namespace EtAlii.Ubigia.Api.Transport.WebApi.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management.WebApi;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using EtAlii.Ubigia.Infrastructure.Tests;

    public class WebApiTransportTestContext : TransportTestContextBase
    {
        public WebApiTransportTestContext(IHostTestContextFactory testHostFactory) : base(testHostFactory)
        {
        }

        public override async Task<IDataConnection> CreateDataConnection(string address, string accountName, string accountPassword, string spaceName, bool openOnCreation, bool useNewSpace, SpaceTemplate spaceTemplate = null)
        {
            var diagnostics = TestDiagnostics.Create();

            var httpClientFactory = new TestHttpClientFactory((TestInfrastructure)Context.Host.Infrastructure);
            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory);

            var connectionConfiguration = new DataConnectionConfiguration()
                .Use(WebApiTransportProvider.Create(infrastructureClient))
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

            var httpClientFactory = new TestHttpClientFactory((TestInfrastructure)Context.Host.Infrastructure);
            var infrastructureClient = new DefaultInfrastructureClient(httpClientFactory);

            var connectionConfiguration = new ManagementConnectionConfiguration()
                .Use(WebApiStorageTransportProvider.Create(infrastructureClient))
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
