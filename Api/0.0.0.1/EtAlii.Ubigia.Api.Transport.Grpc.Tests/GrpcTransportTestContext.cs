namespace EtAlii.Ubigia.Api.Transport.Grpc.Tests
{
	using System;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
	using EtAlii.Ubigia.Api.Transport.Grpc;
	using EtAlii.Ubigia.Api.Transport.Management.Grpc;
	using EtAlii.Ubigia.Api.Transport.Tests;
	using EtAlii.Ubigia.Infrastructure.Hosting.Tests;

	public class GrpcTransportTestContext : TransportTestContextBase<InProcessInfrastructureHostTestContext>
    {
        public override async Task<IDataConnection> CreateDataConnection(Uri address, string accountName, string accountPassword, string spaceName, bool openOnCreation, bool useNewSpace, SpaceTemplate spaceTemplate = null)
        {
            var diagnostics = TestDiagnostics.Create();

			//var signalRHttpClient = new SignalRTestHttpClient(c => ((TestInfrastructure)Context.Host.Infrastructure).Server.Handler); 
			//var httpMessageHandlerFactory = new Func<HttpMessageHandler>(() => Context.Host.Server.CreateHandler());

	        throw new NotImplementedException("GrpcTransportProvider.Create should be supported");
			var connectionConfiguration = new DataConnectionConfiguration()
	            //.Use(SignalRTransportProvider.Create(signalRHttpClient))
	            .Use(GrpcTransportProvider.Create(null))
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
	        //var httpMessageHandlerFactory = new Func<HttpMessageHandler>(() => Context.Host.Server.CreateHandler());

	        throw new NotImplementedException("GrpcStorageTransportProvider.Create should be supported");
	        
			var connectionConfiguration = new ManagementConnectionConfiguration()
				//.Use(SignalRStorageTransportProvider.Create(signalRHttpClient))
				//.Use(SignalRStorageTransportProvider.Create())
				//.Use(SignalRStorageTransportProvider.Create(httpMessageHandlerFactory))
				.Use(GrpcStorageTransportProvider.Create(null))
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
