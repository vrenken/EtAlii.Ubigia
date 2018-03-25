namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Infrastructure.Hosting;

    public abstract class TransportTestContextBase : ITransportTestContext
    {
        public IHostTestContext Context { get; private set; }

        private readonly IHostTestContextFactory _testHostFactory;

        protected TransportTestContextBase(IHostTestContextFactory testHostFactory)
        {
            _testHostFactory = testHostFactory;
        }

        public async Task<IDataConnection> CreateDataConnection(bool openOnCreation = true)
        {
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            return await CreateDataConnection(Context.Host.Infrastructure.Configuration.Address, accountName, password, spaceName, openOnCreation, true, null);
        }

        public async Task<IDataConnection> CreateDataConnection(string accountName, string accountPassword, string spaceName, bool openOnCreation, bool useNewSpace, SpaceTemplate spaceTemplate = null)
        {
            return await CreateDataConnection(Context.Host.Infrastructure.Configuration.Address, accountName, accountPassword, spaceName, openOnCreation, useNewSpace, spaceTemplate);
        }
        public abstract Task<IDataConnection> CreateDataConnection(string address, string accountName, string accountPassword, string spaceName, bool openOnCreation, bool useNewSpace, SpaceTemplate spaceTemplate = null);

        public async Task<IManagementConnection> CreateManagementConnection(bool openOnCreation = true)
        {
            var configuration = Context.Host.Infrastructure.Configuration;

            return await CreateManagementConnection(configuration.Address, Context.TestAccountName, Context.TestAccountPassword, openOnCreation);
        }

        public abstract Task<IManagementConnection> CreateManagementConnection(string address, string account, string password, bool openOnCreation = true);

        public async Task<Account> AddUserAccount(IManagementConnection connection)
        {
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            return await connection.Accounts.Add(name, password, AccountTemplate.User);
        }

        #region start/stop

        public async Task Start()
        {
            await Task.Run(() =>
            {
                Context = _testHostFactory.Create();
                Context.Start();
            });
        }

        public async Task Stop()
        {
            await Task.Run(() =>
            {
                Context.Stop();
                Context = null;
                //SpaceName = null;
            });
        }

        #endregion start/stop
    }
}
