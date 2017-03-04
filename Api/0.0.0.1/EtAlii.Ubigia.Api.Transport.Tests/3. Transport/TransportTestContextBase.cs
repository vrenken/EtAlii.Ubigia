namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Management;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;

    public abstract class TransportTestContextBase : ITransportTestContext
    {
        public IHostTestContext Context => _context;
        private IHostTestContext _context;

        private readonly IHostTestContextFactory _testHostFactory;

        public TransportTestContextBase(IHostTestContextFactory testHostFactory)
        {
            _testHostFactory = testHostFactory;
        }

        public async Task<IDataConnection> CreateDataConnection(bool openOnCreation = true)
        {
            var accountName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var spaceName = Guid.NewGuid().ToString();
            return await CreateDataConnection(_context.Host.Infrastructure.Configuration.Address, accountName, password, spaceName, openOnCreation, true, null);
        }

        public async Task<IDataConnection> CreateDataConnection(string accountName, string accountPassword, string spaceName, bool openOnCreation, bool useNewSpace, SpaceTemplate spaceTemplate = null)
        {
            return await CreateDataConnection(_context.Host.Infrastructure.Configuration.Address, accountName, accountPassword, spaceName, openOnCreation, useNewSpace, spaceTemplate);
        }
        public abstract Task<IDataConnection> CreateDataConnection(string address, string accountName, string accountPassword, string spaceName, bool openOnCreation, bool useNewSpace, SpaceTemplate spaceTemplate = null);

        public async Task<IManagementConnection> CreateManagementConnection(bool openOnCreation = true)
        {
            var configuration = Context.Host.Infrastructure.Configuration;

            return await CreateManagementConnection(configuration.Address, configuration.Account, configuration.Password, openOnCreation);
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
                _context = _testHostFactory.Create();
                _context.Start();
            });
        }

        public async Task Stop()
        {
            await Task.Run(() =>
            {
                _context.Stop();
                _context = null;
                //SpaceName = null;
            });
        }

        #endregion start/stop
    }
}
