// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management.Rest;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.xTechnology.Threading;

    public class RestTransportTestContext : TransportTestContextBase<InProcessInfrastructureHostTestContext>
    {
        /// <inheritdoc />
        protected override async Task<IDataConnection> CreateDataConnectionToNewSpace(
            Uri address, string accountName, string accountPassword, bool openOnCreation, IContextCorrelator contextCorrelator, SpaceTemplate spaceTemplate = null)
        {
            var spaceName = Guid.NewGuid().ToString();

            //var httpClientFactory = new TestHttpClientFactory((TestInfrastructure)Context.Host.Infrastructure)
            var client = Host.CreateRestInfrastructureClient();

            var options = new DataConnectionOptions(Host.ClientConfiguration)
                .UseTransport(RestTransportProvider.Create(client))
                .Use(address)
                .Use(accountName, spaceName, accountPassword)
                .UseTransportDiagnostics();
            var connection = new DataConnectionFactory().Create(options);

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

        /// <inheritdoc />
        protected override async Task<IDataConnection> CreateDataConnectionToExistingSpace(Uri address, string accountName, string accountPassword, string spaceName, IContextCorrelator contextCorrelator, bool openOnCreation)
        {
			//var httpClientFactory = new TestHttpClientFactory((TestInfrastructure)Context.Host.Infrastructure)
	        var client = Host.CreateRestInfrastructureClient();

			var options = new DataConnectionOptions(Host.ClientConfiguration)
	            .UseTransport(RestTransportProvider.Create(client))
                .Use(address)
                .Use(accountName, spaceName, accountPassword)
                .UseTransportDiagnostics();
            var connection = new DataConnectionFactory().Create(options);

            if (openOnCreation)
            {
                await connection.Open().ConfigureAwait(false);
            }
            return connection;
        }

        /// <inheritdoc />
        protected override async Task<IManagementConnection> CreateManagementConnection(Uri address, string account, string password, IContextCorrelator contextCorrelator, bool openOnCreation = true)
        {
	        var client = Host.CreateRestInfrastructureClient();

            var connectionOptions = new ManagementConnectionOptions(Host.ClientConfiguration)
	            .Use(RestStorageTransportProvider.Create(client))
                .Use(address)
                .Use(account, password)
                .UseTransportManagementDiagnostics();
            var connection = new ManagementConnectionFactory().Create(connectionOptions);
            if (openOnCreation)
            {
                await connection
                    .Open()
                    .ConfigureAwait(false);
            }
            return connection;
        }
    }
}
