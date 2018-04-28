namespace EtAlii.Ubigia.Api.Transport.SignalR.Tests
{
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Tests;
	using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management.SignalR;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using EtAlii.Ubigia.Api.Transport.Tests;
	using EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore.Tests;

	public class SignalRTransportTestContext : TransportTestContextBase<InProcessInfrastructureHostTestContext>, ITransportTestContext<InProcessInfrastructureHostTestContext>
    {
        public override async Task<IDataConnection> CreateDataConnection(Uri address, string accountName, string accountPassword, string spaceName, bool openOnCreation, bool useNewSpace, SpaceTemplate spaceTemplate = null)
        {
            var diagnostics = TestDiagnostics.Create();

			//var signalRHttpClient = new SignalRTestHttpClient(c => ((TestInfrastructure)Context.Host.Infrastructure).Server.Handler); 
			var httpMessageHandlerFactory = new Func<HttpMessageHandler>(() => Context.Host.Server.CreateHandler());

			var connectionConfiguration = new DataConnectionConfiguration()
	            //.Use(SignalRTransportProvider.Create(signalRHttpClient))
	            .Use(SignalRTransportProvider.Create(httpMessageHandlerFactory))
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

        public override async Task<IManagementConnection> CreateManagementConnection(Uri address, string account, string password, bool openOnCreation = true)
        {
            var diagnostics = TestDiagnostics.Create();
	        //var signalRHttpClient = new SignalRTestHttpClient(c => ((TestInfrastructure)Context.Host.Infrastructure).Server.CreateHandler());
	        var httpMessageHandlerFactory = new Func<HttpMessageHandler>(() => Context.Host.Server.CreateHandler());

			var connectionConfiguration = new ManagementConnectionConfiguration()
				//.Use(SignalRStorageTransportProvider.Create(signalRHttpClient))
				//.Use(SignalRStorageTransportProvider.Create())
				.Use(SignalRStorageTransportProvider.Create(httpMessageHandlerFactory))
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
