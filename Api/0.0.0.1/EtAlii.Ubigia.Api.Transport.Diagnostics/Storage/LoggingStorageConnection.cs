namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Logging;

    public sealed class LoggingStorageConnection : IStorageConnection
    {
        private readonly IStorageConnection _decoree;
        private readonly ILogger _logger;

        private Uri _address;

        /// <inheritdoc />
        public Storage Storage => _decoree.Storage;
        /// <inheritdoc />
        public bool IsConnected => _decoree.IsConnected;
        /// <inheritdoc />
        public IStorageTransport Transport => ((dynamic)_decoree).Transport;
        /// <inheritdoc />
        public IStorageContext Storages => _decoree?.Storages;
        /// <inheritdoc />
        public IAccountContext Accounts => _decoree?.Accounts;
        /// <inheritdoc />
        public ISpaceContext Spaces => _decoree?.Spaces;
        /// <inheritdoc />
        public IStorageConnectionDetails Details => _decoree.Details;
        /// <inheritdoc />
        public IStorageConnectionConfiguration Configuration => _decoree.Configuration;

        public LoggingStorageConnection(
            IStorageConnection decoree,
            ILogger logger)
        {
            _decoree = decoree;
            _logger = logger;
        }

        public async Task Open(string accountName, string password)
        {
            _address = _decoree.Transport.Address;

            var message = $"Opening storage connection (Address: {_address}";
            _logger.Info(message);
            var start = Environment.TickCount;

            await _decoree.Open(accountName, password);

            message = $"Opened storage connection (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public async Task Close()
        {
            var message = $"Closing storage connection (Address: {_address}";
            _logger.Info(message);
            var start = Environment.TickCount;

            await _decoree.Close();
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

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                    _decoree.Dispose();
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
