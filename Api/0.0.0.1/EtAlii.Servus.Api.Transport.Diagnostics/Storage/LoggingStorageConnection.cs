namespace EtAlii.Servus.Api.Management
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.Logging;

    public class LoggingStorageConnection : IStorageConnection
    {
        private readonly IStorageConnection _connection;
        private readonly ILogger _logger;

        private string _address;

        public Storage Storage { get { return _connection.Storage; } }
        public bool IsConnected { get { return _connection.IsConnected; } }
        public IStorageTransport Transport { get { return ((dynamic)_connection).Transport; } }
        public IStorageContext Storages { get { return _connection?.Storages; } }
        public IAccountContext Accounts { get { return _connection?.Accounts; } }
        public ISpaceContext Spaces { get { return _connection?.Spaces; } }
        public IStorageConnectionConfiguration Configuration { get { return _connection.Configuration; } }

        public LoggingStorageConnection(
            IStorageConnection connection,
            ILogger logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task Open()
        {
            _address = _connection.Configuration.Address;

            var message = String.Format("Opening storage connection (Address: {0}", _address);
            _logger.Info(message);
            var start = Environment.TickCount;

            await _connection.Open();

            message = $"Opened storage connection (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public async Task Close()
        {
            var message = String.Format("Closing storage connection (Address: {0}", _address);
            _logger.Info(message);
            var start = Environment.TickCount;

            await _connection.Close();
            _address = null;

            message = $"Closed storage connection (Address: {_address} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
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
        ~LoggingStorageConnection()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion Disposable

    }
}
