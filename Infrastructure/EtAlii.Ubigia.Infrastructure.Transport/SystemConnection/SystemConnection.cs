namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    internal class SystemConnection : ISystemConnection
    {
        private readonly ISystemConnectionConfiguration _configuration;

        internal SystemConnection(ISystemConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IDataConnection> OpenSpace(string accountName, string spaceName)
        {
            var connectionConfiguration = new DataConnectionConfiguration()
                .Use(_configuration.TransportProvider)
                .Use(_configuration.Infrastructure.Configuration.Address)
                .Use(accountName, spaceName, null);
            var dataConnection = new DataConnectionFactory().Create(connectionConfiguration);
            await dataConnection.Open();
            return dataConnection;
        }

        public async Task<IManagementConnection> OpenManagementConnection()
        {
            var connectionConfiguration = new ManagementConnectionConfiguration()
                .Use(_configuration.TransportProvider)
                .Use(_configuration.Infrastructure.Configuration.Address);
            var managementConnection = new ManagementConnectionFactory().Create(connectionConfiguration);
            await managementConnection.Open();
            return managementConnection;
        }

        #region Disposable

        private bool _disposed = false;

        //Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                    //if (_status.IsConnected)
                    //{
                        //var task = Task.Run(async () =>
                        //{
                        //    await Close();
                        //});
                        //task.Wait();
                    //}
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                _disposed = true;
            }
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
    