﻿namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using Serilog;

    public sealed class LoggingStorageConnection : IStorageConnection
    {
        private readonly IStorageConnection _decoree;
        private readonly ILogger _logger = Log.ForContext<IStorageConnection>();

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

        public LoggingStorageConnection(IStorageConnection decoree)
        {
            _decoree = decoree;
        }

        public async Task Open(string accountName, string password)
        {
            _address = _decoree.Transport.Address;

            var message = "Opening storage connection (Address: {Address}";
            _logger.Information(message, _address);
            var start = Environment.TickCount;

            await _decoree.Open(accountName, password).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Opened storage connection (Address: {Address} Duration: {Duration}ms)";
            _logger.Information(message, _address, duration);
        }

        public async Task Close()
        {
            var message = "Closing storage connection (Address: {Address}";
            _logger.Information(message, _address);
            var start = Environment.TickCount;

            await _decoree.Close().ConfigureAwait(false);
            _address = null;

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Closed storage connection (Address: {Address} Duration: {Duration}ms)";
            _logger.Information(message, _address, duration);
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
