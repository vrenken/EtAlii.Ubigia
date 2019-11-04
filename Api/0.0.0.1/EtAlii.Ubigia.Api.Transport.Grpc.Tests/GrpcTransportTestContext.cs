namespace EtAlii.Ubigia.Api.Transport.Grpc.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using global::Grpc.Net.Client;

    public class GrpcTransportTestContext : TransportTestContextBase<InProcessInfrastructureHostTestContext>
    {
        public override async Task<IDataConnection> CreateDataConnection(Uri address, string accountName, string accountPassword, string spaceName, bool openOnCreation, bool useNewSpace, SpaceTemplate spaceTemplate = null)
        {
            var diagnostics = TestDiagnostics.Create();

			var grpcChannelFactory = new Func<Uri, GrpcChannel>((channelAddress) => Context.CreateGrpcInfrastructureChannel(channelAddress));
            
			var connectionConfiguration = new DataConnectionConfiguration()
	            .UseTransport(GrpcTransportProvider.Create(grpcChannelFactory))
                .Use(address)
                .Use(accountName, spaceName, accountPassword)
                .UseTransportDiagnostics(diagnostics);
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

            var grpcChannelFactory = new Func<Uri, GrpcChannel>((channelAddress) => Context.CreateGrpcInfrastructureChannel(channelAddress));

            var connectionConfiguration = new ManagementConnectionConfiguration()
				.Use(GrpcStorageTransportProvider.Create(grpcChannelFactory))
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
