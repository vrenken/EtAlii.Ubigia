// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Tests
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management.SignalR;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.AspNetCore;

    public class ProvisioningTestContext : IProvisioningTestContext
    {
        public IHostTestContext<InProcessInfrastructureTestHost> Context { get; private set; }

        private readonly IHostTestContextFactory _testHostFactory;

        public ProvisioningTestContext(IHostTestContextFactory testHostFactory)
        {
            _testHostFactory = testHostFactory;
        }

        public async Task<IGraphSLScriptContext> CreateScriptContext(string accountName, string accountPassword, string spaceName)
        {
            var connection = await CreateDataConnection(accountName, accountPassword, spaceName);
            return new GraphSLScriptContextFactory().Create(connection);
        }

        public async Task<IDataConnection> CreateDataConnection(string accountName, string accountPassword, string spaceName)
        {
            var diagnostics = TestDiagnostics.Create();
			var httpMessageHandlerFactory = new Func<HttpMessageHandler>(() => Context.Host.Server.CreateHandler());

			var connectionConfiguration = new DataConnectionConfiguration()
                .Use(diagnostics)
				.Use(SignalRTransportProvider.Create(httpMessageHandlerFactory))
                .Use(Context.DataServiceAddress)
                .Use(accountName, spaceName, accountPassword);
            var connection = new DataConnectionFactory().Create(connectionConfiguration);

            await connection.Open();

            return connection;
        }

        public async Task<IManagementConnection> OpenManagementConnection()
        {
            var diagnostics = TestDiagnostics.Create();
			var httpMessageHandlerFactory = new Func<HttpMessageHandler>(() => Context.Host.Server.CreateHandler());

			var connectionConfiguration = new ManagementConnectionConfiguration()
				.Use(SignalRStorageTransportProvider.Create(httpMessageHandlerFactory))
                .Use(diagnostics)
                .Use(Context.ManagementServiceAddress)
                .Use(Context.TestAccountName, Context.TestAccountPassword);
            var connection = new ManagementConnectionFactory().Create(connectionConfiguration);
            await connection.Open();
            return connection;
        }

        #region start/stop

        public async Task Start()
        {
            await Task.Run(() =>
            {
                Context = _testHostFactory.Create<InProcessInfrastructureHostTestContext>();
                Context.Start();
            });
        }

        public async Task Stop()
        {
            await Task.Run(() =>
            {
                Context.Stop();
                Context = null;
            });
        }

        #endregion start/stop
    }
}
