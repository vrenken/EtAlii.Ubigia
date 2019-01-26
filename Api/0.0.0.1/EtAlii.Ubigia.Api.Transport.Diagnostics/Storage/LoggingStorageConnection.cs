namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Logging;

    public class LoggingStorageConnection : IStorageConnection
    {
        private readonly IStorageConnection _connection;
        private readonly ILogger _logger;

        private Uri _address;

        public Storage Storage => _connection.Storage;
        public bool IsConnected => _connection.IsConnected;
        public IStorageTransport Transport => ((dynamic)_connection).Transport;
        public IStorageContext Storages => _connection?.Storages;
        public IAccountContext Accounts => _connection?.Accounts;
        public ISpaceContext Spaces => _connection?.Spaces;
        public IStorageConnectionConfiguration Configuration => _connection.Configuration;

        public LoggingStorageConnection(
            IStorageConnection connection,
            ILogger logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task Open(string accountName, string password)
        {
            _address = _connection.Transport.Address;

            var message = $"Opening storage connection (Address: {_address}";
            _logger.Info(message);
            var start = Environment.TickCount;

            await _connection.Open(accountName, password);

            message = $"Opened storage connection (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public async Task Close()
        {
            var message = $"Closing storage connection (Address: {_address}";
            _logger.Info(message);
            var start = Environment.TickCount;

            await _connection.Close();
            _address = null;

            message = $"Closed storage connection (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
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
            if (!_disposed)
            {
                if (disposing && IsConnected)
                {
                    // Free other state (managed objects).
                    var task = Task.Run(async () =>
                    {
                        await Close();
                    });
                    task.Wait();
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                _disposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        ~LoggingStorageConnection()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion Disposable

    }
}
