// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
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
            await dataConnection.Open().ConfigureAwait(false);
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
            await managementConnection.Open().ConfigureAwait(false);
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
//                // Refactor the dispose in the Connections to a Disconnect or something similar.
//                // More details can be found in the GitHub issue below:
//                // https://github.com/vrenken/EtAlii.Ubigia/issues/90
//                        task.Wait[];
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
