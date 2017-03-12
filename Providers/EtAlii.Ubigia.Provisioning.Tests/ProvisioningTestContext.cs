// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Diagnostics;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management.SignalR;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using EtAlii.Ubigia.Api.Transport.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using EtAlii.Ubigia.Infrastructure.Tests;

    public class ProvisioningTestContext : IProvisioningTestContext
    {
        public IHostTestContext Context => _context;
        private IHostTestContext _context;

        private readonly IHostTestContextFactory _testHostFactory;

        public ProvisioningTestContext(IHostTestContextFactory testHostFactory)
        {
            _testHostFactory = testHostFactory;
        }

        public async Task<IDataContext> CreateDataContext(string accountName, string accountPassword, string spaceName)
        {
            var connection = await CreateDataConnection(accountName, accountPassword, spaceName);
            return new DataContextFactory().Create(connection);
        }


        public async Task<IDataConnection> CreateDataConnection(string accountName, string accountPassword, string spaceName)
        {
            var diagnostics = TestDiagnostics.Create();
            var signalRHttpClient = new SignalRTestHttpClient(c => ((TestInfrastructure)Context.Host.Infrastructure).Server.Handler);

            var connectionConfiguration = new DataConnectionConfiguration()
                .Use(diagnostics)
                .Use(SignalRTransportProvider.Create(signalRHttpClient))
                .Use(Context.Host.Infrastructure.Configuration.Address)
                .Use(accountName, spaceName, accountPassword);
            var connection = new DataConnectionFactory().Create(connectionConfiguration);

            await connection.Open();

            return connection;
        }

        public async Task<IManagementConnection> OpenManagementConnection()
        {
            var diagnostics = TestDiagnostics.Create();
            var configuration = Context.Host.Infrastructure.Configuration;
            var signalRHttpClient = new SignalRTestHttpClient(c => ((TestInfrastructure)Context.Host.Infrastructure).Server.Handler);

            var connectionConfiguration = new ManagementConnectionConfiguration()
                .Use(SignalRStorageTransportProvider.Create(signalRHttpClient))
                .Use(diagnostics)
                .Use(configuration.Address)
                .Use(configuration.Account, configuration.Password);
            var connection = new ManagementConnectionFactory().Create(connectionConfiguration);
            await connection.Open();
            return connection;
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
            });
        }

        #endregion start/stop
    }
}
