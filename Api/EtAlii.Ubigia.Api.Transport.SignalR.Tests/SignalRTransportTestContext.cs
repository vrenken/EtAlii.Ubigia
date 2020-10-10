namespace EtAlii.Ubigia.Api.Transport.SignalR.Tests
{
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Transport.Diagnostics;
	using EtAlii.Ubigia.Api.Transport.Management;
	using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
	using EtAlii.Ubigia.Api.Transport.Management.SignalR;
	using EtAlii.Ubigia.Api.Transport.Tests;
	using EtAlii.Ubigia.Diagnostics;
	using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;

	public class SignalRTransportTestContext : TransportTestContextBase<InProcessInfrastructureHostTestContext>
    {
	    public override async Task<IDataConnection> CreateDataConnectionToNewSpace(Uri address, string accountName, string accountPassword, bool openOnCreation, SpaceTemplate spaceTemplate = null)
	    {
		    var spaceName = Guid.NewGuid().ToString();
		    
		    var diagnostics = UbigiaDiagnostics.DefaultConfiguration;

		    var httpMessageHandlerFactory = new Func<HttpMessageHandler>(Context.CreateHandler);

		    var connectionConfiguration = new DataConnectionConfiguration()
			    //.Use(SignalRTransportProvider.Create(signalRHttpClient))
			    .UseTransport(SignalRTransportProvider.Create(httpMessageHandlerFactory))
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
            var diagnostics = UbigiaDiagnostics.DefaultConfiguration;

			var httpMessageHandlerFactory = new Func<HttpMessageHandler>(Context.CreateHandler);

			var connectionConfiguration = new DataConnectionConfiguration()
	            //.Use(SignalRTransportProvider.Create(signalRHttpClient))
	            .UseTransport(SignalRTransportProvider.Create(httpMessageHandlerFactory))
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
            var diagnostics = UbigiaDiagnostics.DefaultConfiguration;

	        var httpMessageHandlerFactory = new Func<HttpMessageHandler>(Context.CreateHandler);

			var connectionConfiguration = new ManagementConnectionConfiguration()
				//.Use(SignalRStorageTransportProvider.Create(signalRHttpClient))
				//.Use(SignalRStorageTransportProvider.Create())
				.Use(SignalRStorageTransportProvider.Create(httpMessageHandlerFactory))
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
