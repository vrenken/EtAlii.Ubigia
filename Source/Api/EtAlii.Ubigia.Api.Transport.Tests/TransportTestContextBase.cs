﻿namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.xTechnology.Hosting;
    using IHostTestContext = EtAlii.Ubigia.Infrastructure.Hosting.TestHost.IHostTestContext;

    public abstract class TransportTestContextBase<THostTestContext> : ITransportTestContext<THostTestContext>
        where THostTestContext: class, IHostTestContext, new()
    {
        public THostTestContext Context { get; private set; }

        private readonly IHostTestContextFactory _testHostFactory;

        protected TransportTestContextBase()//IHostTestContextFactory testHostFactory)
        {
            _testHostFactory = new HostTestContextFactory();
        }

        public Task<IDataConnection> CreateDataConnectionToNewSpace(bool openOnCreation = true)
        {
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
	        return CreateDataConnectionToNewSpace(Context.ServiceDetails.DataAddress, accountName, password, openOnCreation);
		}

        public Task<IDataConnection> CreateDataConnectionToNewSpace(string accountName, string accountPassword, bool openOnCreation, SpaceTemplate spaceTemplate = null)
        {
			return CreateDataConnectionToNewSpace(Context.ServiceDetails.DataAddress, accountName, accountPassword, openOnCreation, spaceTemplate);
        }
        public abstract Task<IDataConnection> CreateDataConnectionToNewSpace(Uri address, string accountName, string accountPassword, bool openOnCreation, SpaceTemplate spaceTemplate = null);

        public Task<IDataConnection> CreateDataConnectionToExistingSpace(string accountName, string accountPassword, string spaceName, bool openOnCreation)
        {
            return CreateDataConnectionToExistingSpace(Context.ServiceDetails.DataAddress, accountName, accountPassword, spaceName, openOnCreation);
        }
        public abstract Task<IDataConnection> CreateDataConnectionToExistingSpace(Uri address, string accountName, string accountPassword, string spaceName, bool openOnCreation);

        public Task<IManagementConnection> CreateManagementConnection(bool openOnCreation = true)
        {
			return CreateManagementConnection(Context.ServiceDetails.ManagementAddress, Context.TestAccountName, Context.TestAccountPassword, openOnCreation);
        }

		public abstract Task<IManagementConnection> CreateManagementConnection(Uri address, string account, string password, bool openOnCreation = true);

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
            await Context.Start(portRange);
        }

        public async Task Stop()  
        {
            await Context.Stop();
            Context = null;
            //SpaceName = null
        }

        #endregion start/stop
    }
}