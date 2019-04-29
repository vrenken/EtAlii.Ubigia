namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Logging;

    public class LoggingSpaceConnection : ISpaceConnection
    {
        private readonly ISpaceConnection _decoree;
        private readonly ILogger _logger;

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

        public LoggingSpaceConnection(
            ISpaceConnection decoree,
            ILogger logger)
        {
            _decoree = decoree;
            _logger = logger;
        }

        public async Task Open(string accountName, string password)
        {
            _address = _decoree.Transport.Address;

            var message = $"Opening space connection (Address: {_address}";
            _logger.Info(message);
            var start = Environment.TickCount;

            await _decoree.Open(accountName, password);

            message = $"Opened space connection (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public async Task Close()
        {
            var message = $"Closing space connection (Address: {_address}";
            _logger.Info(message);
            var start = Environment.TickCount;

            await _decoree.Close();
            _address = null;

            message = $"Closed space connection (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
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
