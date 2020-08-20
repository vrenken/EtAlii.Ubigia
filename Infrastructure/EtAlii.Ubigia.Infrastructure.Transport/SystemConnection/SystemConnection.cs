﻿namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Infrastructure.Functional;

    internal class SystemConnection : ISystemConnection
    {
        private readonly ISystemConnectionConfiguration _configuration;

        public SystemConnection(ISystemConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IDataConnection> OpenSpace(string accountName, string spaceName)
        {
            var serviceDetails = _configuration.Infrastructure.Configuration.ServiceDetails.Single(sd => sd.IsSystemService);

	        var address = serviceDetails.DataAddress;

            var connectionConfiguration = new DataConnectionConfiguration()
                .UseTransport(_configuration.TransportProvider)
                .Use(address)
                .Use(accountName, spaceName, null);
            var dataConnection = new DataConnectionFactory().Create(connectionConfiguration);
            await dataConnection.Open();
            return dataConnection;
        }

        public async Task<IManagementConnection> OpenManagementConnection()
        {
            var serviceDetails = _configuration.Infrastructure.Configuration.ServiceDetails.Single(sd => sd.IsSystemService);

	        var address = serviceDetails.ManagementAddress;

	        var connectionConfiguration = new ManagementConnectionConfiguration()
                .Use(_configuration.TransportProvider)
                .Use(address);
            var managementConnection = new ManagementConnectionFactory().Create(connectionConfiguration);
            await managementConnection.Open();
            return managementConnection;
        }

        #region Disposable

        private bool _disposed;

        //Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            
            if (disposing)
            {
                // Free other state (managed objects).
                //if [_status.IsConnected]
                //[
//                        //var task = Task. Run(async () =>
//                        var task = Close()
//                        task.Wait[]; // TODO: HIGH PRIORITY Refactor the dispose into a Disconnect or something similar. 
                //]
            }
            // Free your own state (unmanaged objects).
            // Set large fields to null.
            _disposed = true;
        }

        // Use C# destructor syntax for finalization code.
        ~SystemConnection()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion Disposable

    }
}
    