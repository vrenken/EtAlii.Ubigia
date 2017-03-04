namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Logging;

    public class LoggingSpaceConnection : ISpaceConnection
    {
        private readonly ISpaceConnection _connection;
        private readonly ILogger _logger;

        private string _address;

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
        public IPropertyContext Properties => _connection.Properties;

        public LoggingSpaceConnection(
            ISpaceConnection connection,
            ILogger logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task Open()
        {
            _address = _connection.Configuration.Address;

            var message = String.Format("Opening space connection (Address: {0}", _address);
            _logger.Info(message);
            var start = Environment.TickCount;

            await _connection.Open();

            message = $"Opened space connection (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public async Task Close()
        {
            var message = String.Format("Closing space connection (Address: {0}", _address);
            _logger.Info(message);
            var start = Environment.TickCount;

            await _connection.Close();
            _address = null;

            message = $"Closed space connection (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        #region Disposable

        private bool _disposed = false;

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
                    if (IsConnected)
                    {
                        var task = Task.Run(async () =>
                        {
                            await Close();
                        });
                        task.Wait();
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
