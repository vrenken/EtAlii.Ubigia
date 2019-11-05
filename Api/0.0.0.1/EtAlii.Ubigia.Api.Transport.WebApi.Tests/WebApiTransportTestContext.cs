namespace EtAlii.Ubigia.Api.Transport.WebApi.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management.WebApi;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;

    public class WebApiTransportTestContext : TransportTestContextBase<InProcessInfrastructureHostTestContext>, ITransportTestContext
    {
        public override async Task<IDataConnection> CreateDataConnectionToNewSpace(Uri address, string accountName, string accountPassword, bool openOnCreation, SpaceTemplate spaceTemplate = null)
        {
            var spaceName = Guid.NewGuid().ToString();

            var diagnostics = TestDiagnostics.Create();

            //var httpClientFactory = new TestHttpClientFactory((TestInfrastructure)Context.Host.Infrastructure)
            var client = Context.CreateRestInfrastructureClient();

            var connectionConfiguration = new DataConnectionConfiguration()
                .UseTransport(WebApiTransportProvider.Create(client))
                .Use(address)
                .Use(accountName, spaceName, accountPassword)
                .UseTransportDiagnostics(diagnostics);
            var connection = new DataConnectionFactory().Create(connectionConfiguration);

            using var managementConnection = await CreateManagementConnection();
            var account = await managementConnection.Accounts.Get(accountName) ??
                          await managementConnection.Accounts.Add(accountName, accountPassword, AccountTemplate.User);
            await managementConnection.Spaces.Add(account.Id, spaceName, spaceTemplate ?? SpaceTemplate.Data);
            await managementConnection.Close();

            if (openOnCreation)
            {
                await connection.Open();
            }
            return connection;
        }

        public override async Task<IDataConnection> CreateDataConnectionToExistingSpace(Uri address, string accountName, string accountPassword, string spaceName, bool openOnCreation)
        {
            var diagnostics = TestDiagnostics.Create();

			//var httpClientFactory = new TestHttpClientFactory((TestInfrastructure)Context.Host.Infrastructure)
	        var client = Context.CreateRestInfrastructureClient();

			var connectionConfiguration = new DataConnectionConfiguration()
	            .UseTransport(WebApiTransportProvider.Create(client))
                .Use(address)
                .Use(accountName, spaceName, accountPassword)
                .UseTransportDiagnostics(diagnostics);
            var connection = new DataConnectionFactory().Create(connectionConfiguration);
            
            if (openOnCreation)
            {
                await connection.Open();
            }
            return connection;
        }

        public override async Task<IManagementConnection> CreateManagementConnection(Uri address, string account, string password, bool openOnCreation = true)
        {
            var diagnostics = TestDiagnostics.Create();

	        var client = Context.CreateRestInfrastructureClient();

            var connectionConfiguration = new ManagementConnectionConfiguration()
	            .Use(WebApiStorageTransportProvider.Create(client))
                .Use(address)
                .Use(account, password)
                .UseTransportDiagnostics(diagnostics);
            var connection = new ManagementConnectionFactory().Create(connectionConfiguration);
            if (openOnCreation)
            {
                await connection.Open();
            }
            return connection;
        }
    }
}
