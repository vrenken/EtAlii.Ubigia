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
        private readonly ISystemConnectionOptions _options;
        private bool _disposed;

        public SystemConnection(ISystemConnectionOptions options)
        {
            _options = options;
        }

        /// <inheritdoc />
        public async Task<(IDataConnection, DataConnectionOptions)> OpenSpace(string accountName, string spaceName)
        {
            var serviceDetails = _options.Infrastructure.Options.ServiceDetails.First(); // We'll take the first ServiceDetails to build the system connection with.

	        var address = serviceDetails.DataAddress;

            var options = new DataConnectionOptions(_options.ConfigurationRoot)
                .UseTransport(_options.TransportProvider)
                .Use(address)
                .Use(accountName, spaceName, null);
            var dataConnection = Factory.Create<IDataConnection>(options);
            await dataConnection
                .Open()
                .ConfigureAwait(false);
            return (dataConnection, options);
        }

        /// <inheritdoc />
        public async Task<IManagementConnection> OpenManagementConnection()
        {
            var serviceDetails = _options.Infrastructure.Options.ServiceDetails.First(); // We'll take the first ServiceDetails to build the system connection with.

	        var address = serviceDetails.ManagementAddress;

	        var connectionOptions = new ManagementConnectionOptions(_options.ConfigurationRoot)
                .Use(_options.TransportProvider)
                .Use(address);
            var managementConnection = Factory.Create<IManagementConnection>(connectionOptions);
            await managementConnection
                .Open()
                .ConfigureAwait(false);
            return managementConnection;
        }

        /// <inheritdoc />
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
//                        //var task = Task. Run[async [] = >
//                        var task = Close[]
//                // Refactor the dispose in the Connections to a Disconnect or something similar.
//                // More details can be found in the GitHub issue below:
//                // https://github.com/vrenken/EtAlii.Ubigia/issues/90
//                        task.Wait[]
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
    }
}
