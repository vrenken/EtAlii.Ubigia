﻿namespace EtAlii.Ubigia.Api.Transport.Grpc.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Threading;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.xTechnology.Diagnostics;
    using global::Grpc.Net.Client;

    public class GrpcTransportTestContext : TransportTestContextBase<InProcessInfrastructureHostTestContext>
    {
        protected override async Task<IDataConnection> CreateDataConnectionToNewSpace(
            Uri address, string accountName, string accountPassword,
            bool openOnCreation, IContextCorrelator contextCorrelator, SpaceTemplate spaceTemplate = null)
        {
            var spaceName = Guid.NewGuid().ToString();

            var diagnostics = DiagnosticsConfiguration.Default;

            var grpcChannelFactory = new Func<Uri, GrpcChannel>((channelAddress) => Context.CreateGrpcInfrastructureChannel(channelAddress));

            var connectionConfiguration = new DataConnectionConfiguration()
                .UseTransport(GrpcTransportProvider.Create(grpcChannelFactory, contextCorrelator))
                .Use(address)
                .Use(accountName, spaceName, accountPassword)
                .UseTransportDiagnostics(diagnostics);
            var connection = new DataConnectionFactory().Create(connectionConfiguration);

            using var managementConnection = await CreateManagementConnection().ConfigureAwait(false);
            var account = await managementConnection.Accounts.Get(accountName).ConfigureAwait(false) ??
                          await managementConnection.Accounts.Add(accountName, accountPassword, AccountTemplate.User).ConfigureAwait(false);
            await managementConnection.Spaces.Add(account.Id, spaceName, spaceTemplate ?? SpaceTemplate.Data).ConfigureAwait(false);
            await managementConnection.Close().ConfigureAwait(false);

            if (openOnCreation)
            {
                await connection.Open().ConfigureAwait(false);
            }
            return connection;
        }

        protected override async Task<IDataConnection> CreateDataConnectionToExistingSpace(
            Uri address, string accountName, string accountPassword,
            string spaceName, IContextCorrelator contextCorrelator, bool openOnCreation)
        {
            var diagnostics = DiagnosticsConfiguration.Default;

			var grpcChannelFactory = new Func<Uri, GrpcChannel>((channelAddress) => Context.CreateGrpcInfrastructureChannel(channelAddress));

			var connectionConfiguration = new DataConnectionConfiguration()
	            .UseTransport(GrpcTransportProvider.Create(grpcChannelFactory, contextCorrelator))
                .Use(address)
                .Use(accountName, spaceName, accountPassword)
                .UseTransportDiagnostics(diagnostics);
            var connection = new DataConnectionFactory().Create(connectionConfiguration);

            if (openOnCreation)
            {
                await connection.Open().ConfigureAwait(false);
            }
            return connection;
        }

        protected override async Task<IManagementConnection> CreateManagementConnection(
            Uri address, string account, string password,
            IContextCorrelator contextCorrelator,
            bool openOnCreation = true)
        {
            var diagnostics = DiagnosticsConfiguration.Default;

            var grpcChannelFactory = new Func<Uri, GrpcChannel>((channelAddress) => Context.CreateGrpcInfrastructureChannel(channelAddress));

            var connectionConfiguration = new ManagementConnectionConfiguration()
				.Use(GrpcStorageTransportProvider.Create(grpcChannelFactory, contextCorrelator))
				.Use(address)
                .Use(account, password)
                .UseTransportDiagnostics(diagnostics);
            var connection = new ManagementConnectionFactory().Create(connectionConfiguration);
            if (openOnCreation)
            {
                await connection.Open().ConfigureAwait(false);
            }
            return connection;
        }
    }
}
