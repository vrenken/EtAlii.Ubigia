// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Provisioning.Tests
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Management.SignalR;
    using EtAlii.Ubigia.Api.Transport.SignalR;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.NetCore;
    using EtAlii.Ubigia.Infrastructure.Hosting.Tests;

    public class ProvisioningTestContext : IProvisioningTestContext
    {
        public IHostTestContext<InfrastructureTestHost> Context { get; private set; }

        private readonly IHostTestContextFactory _testHostFactory;

        public ProvisioningTestContext(IHostTestContextFactory testHostFactory)
        {
            _testHostFactory = testHostFactory;
        }

        public async Task<IGraphSLScriptContext> CreateScriptContext(string accountName, string accountPassword, string spaceName)
        {
            var connection = await CreateDataConnection(accountName, accountPassword, spaceName);
            
            var configuration = new GraphSLScriptContextConfiguration()
                .UseCaching(true)
                .UseTraversalCaching(true)
                .Use(connection);
            return new GraphSLScriptContextFactory().Create(configuration);
        }

        private async Task<IDataConnection> CreateDataConnection(string accountName, string accountPassword, string spaceName)
        {
            var diagnostics = TestDiagnostics.Create();
            var httpMessageHandlerFactory = new Func<HttpMessageHandler>(Context.CreateHandler);

			var connectionConfiguration = new DataConnectionConfiguration()
                .UseTransportDiagnostics(diagnostics)
				.UseTransport(SignalRTransportProvider.Create(httpMessageHandlerFactory))
                .Use(Context.DataServiceAddress)
                .Use(accountName, spaceName, accountPassword);
            var connection = new DataConnectionFactory().Create(connectionConfiguration);

            await connection.Open();

            return connection;
        }

        public async Task<IManagementConnection> OpenManagementConnection()
        {
            var diagnostics = TestDiagnostics.Create();
            var httpMessageHandlerFactory = new Func<HttpMessageHandler>(Context.CreateHandler);

			var connectionConfiguration = new ManagementConnectionConfiguration()
				.Use(SignalRStorageTransportProvider.Create(httpMessageHandlerFactory))
                .UseTransportDiagnostics(diagnostics)
                .Use(Context.ManagementServiceAddress)
                .Use(Context.TestAccountName, Context.TestAccountPassword);
            var connection = new ManagementConnectionFactory().Create(connectionConfiguration);
            await connection.Open();
            return connection;
        }

        #region start/stop

        public async Task Start()
        {
            Context = _testHostFactory.Create<InProcessInfrastructureHostTestContext>();
            await Context.Start();
        }

        public async Task Stop()
        {
            await Context.Stop();
            Context = null;
        }

        #endregion start/stop
    }
}
