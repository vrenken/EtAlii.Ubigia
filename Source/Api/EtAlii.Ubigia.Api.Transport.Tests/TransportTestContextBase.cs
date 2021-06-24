// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.xTechnology.Hosting;
    using IHostTestContext = EtAlii.Ubigia.Infrastructure.Hosting.TestHost.IHostTestContext;
    using EtAlii.xTechnology.Threading;

    public abstract class TransportTestContextBase<THostTestContext> : ITransportTestContext<THostTestContext>
        where THostTestContext: class, IHostTestContext, new()
    {
        public THostTestContext Context { get; private set; }

        private readonly IHostTestContextFactory _testHostFactory;
        private readonly IContextCorrelator _contextCorrelator;

        protected TransportTestContextBase()//IHostTestContextFactory testHostFactory)
        {
            _testHostFactory = new HostTestContextFactory();
            _contextCorrelator = new ContextCorrelator();
        }

        public Task<IDataConnection> CreateDataConnectionToNewSpace(bool openOnCreation = true)
        {
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
	        return CreateDataConnectionToNewSpace(Context.ServiceDetails.DataAddress, accountName, password, openOnCreation, _contextCorrelator);
		}

        public Task<IDataConnection> CreateDataConnectionToNewSpace(string accountName, string accountPassword, bool openOnCreation, SpaceTemplate spaceTemplate = null)
        {
            return CreateDataConnectionToNewSpace(Context.ServiceDetails.DataAddress, accountName, accountPassword, openOnCreation, _contextCorrelator, spaceTemplate);
        }
        public Task<IDataConnection> CreateDataConnectionToNewSpace(Uri address, string accountName, string accountPassword, bool openOnCreation, SpaceTemplate spaceTemplate = null)
        {
            return CreateDataConnectionToNewSpace(address, accountName, accountPassword, openOnCreation, _contextCorrelator, spaceTemplate);
        }

        protected abstract Task<IDataConnection> CreateDataConnectionToNewSpace(Uri address, string accountName, string accountPassword, bool openOnCreation, IContextCorrelator contextCorrelator, SpaceTemplate spaceTemplate = null);

        public Task<IDataConnection> CreateDataConnectionToExistingSpace(string accountName, string accountPassword, string spaceName, bool openOnCreation)
        {
            return CreateDataConnectionToExistingSpace(Context.ServiceDetails.DataAddress, accountName, accountPassword, spaceName, _contextCorrelator, openOnCreation);
        }

        protected abstract Task<IDataConnection> CreateDataConnectionToExistingSpace(Uri address, string accountName, string accountPassword, string spaceName, IContextCorrelator contextCorrelator, bool openOnCreation);

        public Task<IManagementConnection> CreateManagementConnection(bool openOnCreation = true)
        {
			return CreateManagementConnection(Context.ServiceDetails.ManagementAddress, Context.TestAccountName, Context.TestAccountPassword, _contextCorrelator, openOnCreation);
        }

        public Task<IManagementConnection> CreateManagementConnection(Uri address, string account, string password, bool openOnCreation = true)
        {
            return CreateManagementConnection(address, account, password, _contextCorrelator, openOnCreation);
        }
        protected abstract Task<IManagementConnection> CreateManagementConnection(Uri address, string account, string password, IContextCorrelator contextCorrelator, bool openOnCreation = true);

        public Task<Account> AddUserAccount(IManagementConnection connection)
        {
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            return connection.Accounts.Add(name, password, AccountTemplate.User);
        }

        #region start/stop

        public async Task Start(PortRange portRange)
        {
            Context = _testHostFactory.Create<THostTestContext>();
            await Context.Start(portRange).ConfigureAwait(false);
        }

        public async Task Stop()
        {
            await Context.Stop().ConfigureAwait(false);
            Context = null;
            //SpaceName = null
        }

        #endregion start/stop
    }
}
