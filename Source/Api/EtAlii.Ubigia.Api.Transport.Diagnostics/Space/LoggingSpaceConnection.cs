// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using Serilog;

    public class LoggingSpaceConnection : ISpaceConnection
    {
        private readonly ISpaceConnection _decoree;
        private readonly ILogger _logger = Log.ForContext<ISpaceConnection>();

        private Uri _address;

        public Account Account => _decoree.Account;
        public Space Space => _decoree.Space;
        public Storage Storage => _decoree.Storage;
        public bool IsConnected => _decoree.IsConnected;
        public ISpaceTransport Transport => ((dynamic)_decoree).Transport;
        public ISpaceConnectionConfiguration Configuration => _decoree.Configuration;

        public IAuthenticationContext Authentication => _decoree.Authentication;
        public IEntryContext Entries => _decoree.Entries;
        public IRootContext Roots => _decoree.Roots;
        public IContentContext Content => _decoree.Content;
        public IPropertiesContext Properties => _decoree.Properties;

        public LoggingSpaceConnection(ISpaceConnection decoree)
        {
            _decoree = decoree;
        }

        public async Task Open(string accountName, string password)
        {
            _address = _decoree.Transport.Address;

            _logger.Debug("Opening space connection (Address: {Address}", _address);
            var start = Environment.TickCount;

            await _decoree.Open(accountName, password).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Opened space connection (Address: {Address} Duration: {Duration}ms)", _address, duration);
        }

        public async Task Close()
        {
            _logger.Debug("Closing space connection (Address: {Address}", _address);
            var start = Environment.TickCount;

            await _decoree.Close().ConfigureAwait(false);
            _address = null;

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Closed space connection (Address: {Address} Duration: {Duration}ms)", _address, duration);
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
        ~LoggingSpaceConnection()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion Disposable

    }
}
