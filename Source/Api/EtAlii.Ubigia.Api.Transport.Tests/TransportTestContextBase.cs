// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.xTechnology.Threading;

    public abstract class TransportTestContextBase<THostTestContext> : ITransportTestContext
        where THostTestContext: class, IInfrastructureHostTestContext, new()
    {
        IInfrastructureHostTestContext ITransportTestContext.Host => Host;
        protected THostTestContext Host { get; private set; }

        private readonly IContextCorrelator _contextCorrelator;

        protected TransportTestContextBase()
        {
            _contextCorrelator = new ContextCorrelator();
        }

        private async Task<(IDataConnection, DataConnectionOptions)> CreateDataConnectionToNewSpace(
            Uri address,
            string accountName,
            string accountPassword,
            bool openOnCreation,
            IContextCorrelator contextCorrelator,
            SpaceTemplate spaceTemplate = null)
        {
            var spaceName = Guid.NewGuid().ToString();
            var transportProvider = CreateTransportProvider(contextCorrelator);

            var dataConnectionOptions = new DataConnectionOptions(Host.ClientConfiguration)
                .UseTransport(transportProvider)
                .Use(address)
                .Use(accountName, spaceName, accountPassword)
                .UseTransportDiagnostics();
            var dataConnection = Factory.Create<IDataConnection>(dataConnectionOptions);

            var (managementConnection, _) = await CreateManagementConnection().ConfigureAwait(false);
            var account = await managementConnection.Accounts
                .Get(accountName)
                .ConfigureAwait(false) ?? await managementConnection.Accounts
                .Add(accountName, accountPassword, AccountTemplate.User)
                .ConfigureAwait(false);
            await managementConnection.Spaces
                .Add(account.Id, spaceName, spaceTemplate ?? SpaceTemplate.Data)
                .ConfigureAwait(false);
            await managementConnection
                .DisposeAsync()
                .ConfigureAwait(false);

            if (openOnCreation)
            {
                await dataConnection
                    .Open()
                    .ConfigureAwait(false);
            }
            return (dataConnection, dataConnectionOptions);
        }

        protected abstract ITransportProvider CreateTransportProvider(IContextCorrelator contextCorrelator);

        protected abstract IStorageTransportProvider CreateStorageTransportProvider(IContextCorrelator contextCorrelator);

        private async Task<(IManagementConnection, ManagementConnectionOptions)> CreateManagementConnection(
            Uri address, string account, string password,
            IContextCorrelator contextCorrelator,
            bool openOnCreation = true)
        {
            var storageTransportProvider = CreateStorageTransportProvider(contextCorrelator);

            var managementConnectionOptions = new ManagementConnectionOptions(Host.ClientConfiguration)
                .Use(storageTransportProvider)
                .Use(address)
                .Use(account, password)
                .UseTransportManagementDiagnostics();
            var managementConnection = Factory.Create<IManagementConnection>(managementConnectionOptions);
            if (openOnCreation)
            {
                await managementConnection.Open().ConfigureAwait(false);
            }
            return (managementConnection, managementConnectionOptions);
        }

        private async Task<(IDataConnection, DataConnectionOptions)> CreateDataConnectionToExistingSpace(Uri address, string accountName, string accountPassword, string spaceName, IContextCorrelator contextCorrelator, bool openOnCreation)
        {
            var transportProvider = CreateTransportProvider(contextCorrelator);

            var dataConnectionOptions = new DataConnectionOptions(Host.ClientConfiguration)
                .UseTransport(transportProvider)
                .Use(address)
                .Use(accountName, spaceName, accountPassword)
                .UseTransportDiagnostics();
            var dataConnection = Factory.Create<IDataConnection>(dataConnectionOptions);

            if (openOnCreation)
            {
                await dataConnection
                    .Open()
                    .ConfigureAwait(false);
            }
            return (dataConnection, dataConnectionOptions);
        }

        public Task<(IDataConnection, DataConnectionOptions)> CreateDataConnectionToNewSpace(bool openOnCreation = true)
        {
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
	        return CreateDataConnectionToNewSpace(Host.ServiceDetails.DataAddress, accountName, password, openOnCreation, _contextCorrelator);
		}

        public Task<(IDataConnection, DataConnectionOptions)> CreateDataConnectionToNewSpace(string accountName, string accountPassword, bool openOnCreation, SpaceTemplate spaceTemplate = null)
        {
            return CreateDataConnectionToNewSpace(Host.ServiceDetails.DataAddress, accountName, accountPassword, openOnCreation, _contextCorrelator, spaceTemplate);
        }

        public Task<(IDataConnection, DataConnectionOptions)> CreateDataConnectionToExistingSpace(string accountName, string accountPassword, string spaceName, bool openOnCreation)
        {
            return CreateDataConnectionToExistingSpace(Host.ServiceDetails.DataAddress, accountName, accountPassword, spaceName, _contextCorrelator, openOnCreation);
        }

        public Task<(IManagementConnection, ManagementConnectionOptions)> CreateManagementConnection(bool openOnCreation = true)
        {
			return CreateManagementConnection(Host.ServiceDetails.ManagementAddress, Host.TestAccountName, Host.TestAccountPassword, _contextCorrelator, openOnCreation);
        }

        public Task<(IManagementConnection, ManagementConnectionOptions)> CreateManagementConnection(Uri address, string account, string password, bool openOnCreation = true)
        {
            return CreateManagementConnection(address, account, password, _contextCorrelator, openOnCreation);
        }

        public Task<Account> AddUserAccount(IManagementConnection connection)
        {
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            return connection.Accounts.Add(name, password, AccountTemplate.User);
        }

        public async Task Start(PortRange portRange)
        {
            Host = new THostTestContext();
            await Host
                .Start(portRange)
                .ConfigureAwait(false);
        }

        public async Task Stop()
        {
            await Host
                .Stop()
                .ConfigureAwait(false);
            Host = null;
        }
    }
}
