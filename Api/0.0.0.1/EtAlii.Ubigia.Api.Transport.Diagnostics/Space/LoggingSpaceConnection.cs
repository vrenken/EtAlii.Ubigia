namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Logging;

    public class LoggingSpaceConnection : ISpaceConnection
    {
        private readonly ISpaceConnection _connection;
        private readonly ILogger _logger;

        private Uri _address;

        public Account Account => _connection.Account;
        public Space Space => _connection.Space;
        public Storage Storage => _connection.Storage;
        public bool IsConnected => _connection.IsConnected;
        public ISpaceTransport Transport => ((dynamic)_connection).Transport;
        public ISpaceConnectionConfiguration Configuration => _connection.Configuration;

        public IAuthenticationContext Authentication => _connection.Authentication;
        public IEntryContext Entries => _connection.Entries;
        public IRootContext Roots => _connection.Roots;
        public IContentContext Content => _connection.Content;
        public IPropertiesContext Properties => _connection.Properties;

        public LoggingSpaceConnection(
            ISpaceConnection connection,
            ILogger logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task Open(string accountName, string password)
        {
            _address = _connection.Transport.Address;

            var message = $"Opening space connection (Address: {_address}";
            _logger.Info(message);
            var start = Environment.TickCount;

            await _connection.Open(accountName, password);

            message = $"Opened space connection (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public async Task Close()
        {
            var message = $"Closing space connection (Address: {_address}";
            _logger.Info(message);
            var start = Environment.TickCount;

            await _connection.Close();
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
                    if (IsConnected)
                    {
                        var task = Close();
                        task.Wait(); // TODO: HIGH PRIORITY Refactor the dispose into a Disconnect or something similar. 
                    }
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
