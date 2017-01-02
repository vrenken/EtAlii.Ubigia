namespace EtAlii.Servus.Api.Transport.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Management.SignalR;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.Servus.Api.Transport.SignalR;
    using EtAlii.Servus.Infrastructure.Hosting.Tests;
    using EtAlii.Servus.Infrastructure.Tests;

    public class TransportTestContext : ITransportTestContext
    {
        public IHostTestContext Context
        {
            get { return _context; }
        }
        private IHostTestContext _context;

        private readonly IHostTestContextFactory _testHostFactory;

        public TransportTestContext(IHostTestContextFactory testHostFactory)
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

        public async Task<IDataConnection> CreateDataConnection(string address, string accountName, string accountPassword, string spaceName, bool openOnCreation, bool useNewSpace, SpaceTemplate spaceTemplate = null)
        {
            var diagnostics = TestDiagnostics.Create();

            var signalRHttpClient = new SignalRTestHttpClient(c => ((TestInfrastructure)Context.Host.Infrastructure).Server.Handler); 

            var connectionConfiguration = new DataConnectionConfiguration()
                .Use(SignalRTransportProvider.Create(signalRHttpClient))
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

        public async Task<IManagementConnection> CreateManagementConnection(bool openOnCreation = true)
        {
            var configuration = Context.Host.Infrastructure.Configuration;

            return await CreateManagementConnection(configuration.Address, configuration.Account, configuration.Password, openOnCreation);
        }

        public async Task<IManagementConnection> CreateManagementConnection(string address, string account, string password, bool openOnCreation = true)
        {
            var diagnostics = TestDiagnostics.Create();
            var signalRHttpClient = new SignalRTestHttpClient(c => ((TestInfrastructure)Context.Host.Infrastructure).Server.Handler);

            var connectionConfiguration = new ManagementConnectionConfiguration()
                .Use(SignalRStorageTransportProvider.Create(signalRHttpClient))
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
