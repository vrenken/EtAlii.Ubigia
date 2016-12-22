﻿namespace EtAlii.Servus.Provisioning
{
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Transport;

    public class ProviderContext : IProviderContext
    {
        public IDataContext SystemDataContext { get { return _systemDataContext; } }
        private readonly IDataContext _systemDataContext;

        public IManagementConnection ManagementConnection { get { return _managementConnection; } }
        private readonly IManagementConnection _managementConnection;

        private readonly IProviderConfiguration _configuration;

        public ProviderContext(
            IDataContext systemDataContext, 
            IManagementConnection managementConnection,
            IProviderConfiguration configuration)
        {
            _systemDataContext = systemDataContext;
            _managementConnection = managementConnection;
            _configuration = configuration;
        }

        public IDataContext CreateDataContext(Space space)
        {
            IDataConnection dataConnection = null;
            var task = Task.Run(async () =>
            {
                dataConnection = await _managementConnection.OpenSpace(space);
            });
            task.Wait();
            return _configuration.CreateDataContext(dataConnection);
        }

        public IDataContext CreateDataContext(string accountName, string spaceName)
        {
            IDataConnection dataConnection = null;
            var task = Task.Run(async () =>
            {
                dataConnection = await _managementConnection.OpenSpace(accountName, spaceName);
            });
            task.Wait();
            return _configuration.CreateDataContext(dataConnection);
        }
    }
}