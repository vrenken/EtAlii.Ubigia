namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;

    public class LoggingManagementConnection : IManagementConnection
    {
        private readonly IManagementConnection _decoree;
        private readonly ILogger _logger;

        public Storage Storage => _decoree.Storage;
        public IStorageContext Storages => _decoree.Storages;
        public IAccountContext Accounts => _decoree.Accounts;
        public ISpaceContext Spaces => _decoree.Spaces;

        public bool IsConnected => _decoree.IsConnected;

        public IStorageConnectionDetails Details => _decoree.Details;
        public IManagementConnectionConfiguration Configuration => _decoree.Configuration;

        public LoggingManagementConnection(
            IManagementConnection decoree,
            ILogger logger)
        {
            _decoree = decoree;
            _logger = logger;
        }

        public async Task Open()
        {
            var address = _decoree.Configuration.Address;
            var accountName = _decoree.Configuration.AccountName;

            var message = "Opening management connection (Address: {address} Account: {accountName})";
            _logger.Info(message, address, accountName);
            var start = Environment.TickCount;

            await _decoree.Open();

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Opened management connection (Address: {address} Account: {accountName} Duration: {duration}ms)";
            _logger.Info(message, address, accountName, duration);
        }

        public async Task<IDataConnection> OpenSpace(Space space)
        {
            var address = _decoree.Configuration.Address;

            var message = "Opening data connection from management connection (Address: {address} Account: {accountId} Space: {spaceId})";
            _logger.Info(message, address, space.AccountId, space.Id);
            var start = Environment.TickCount;

            var connection = await _decoree.OpenSpace(space);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Opened data connection from management connection (Address: {address} Account: {accountId} Space: {spaceId} Duration: {duration}ms)";
            _logger.Info(message, address, space.AccountId, space.Id, duration);

            return connection;
        }

        public async Task<IDataConnection> OpenSpace(Guid accountId, Guid spaceId)
        {
            var address = _decoree.Configuration.Address;

            var message = "Opening data connection from management connection (Address: {address} Account: {accountId} Space: {spaceId})";
            _logger.Info(message, address, accountId, spaceId);
            var start = Environment.TickCount;

            var connection = await _decoree.OpenSpace(accountId, spaceId);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Opened data connection from management connection (Address: {address} Account: {accountId} Space: {spaceId} Duration: {duration}ms)";
            _logger.Info(message, address, accountId, spaceId, duration);

            return connection;
        }

        public async Task<IDataConnection> OpenSpace(string accountName, string spaceName)
        {
            var address = _decoree.Configuration.Address;

            var message = "Opening data connection from management connection (Address: {address} Account: {accountName} Space: {spaceName})";
            _logger.Info(message, address, accountName, spaceName);
            var start = Environment.TickCount;

            var connection = await _decoree.OpenSpace(accountName, spaceName);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Opened data connection from management connection (Address: {address} Account: {accountName} Space: {spaceName} Duration: {duration}ms)";
            _logger.Info(message, address, accountName, spaceName, duration);

            return connection;
        }

        public async Task Close()
        {
            var address = _decoree.Configuration.Address;
            var accountName = _decoree.Configuration.AccountName;

            var message = "Closing management connection (Address: {address} Account: {accountName})";
            _logger.Info(message, address, accountName);
            var start = Environment.TickCount;

            await _decoree.Close();

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            message = "Closed management connection (Address: {address} Account: {accountName} Duration: {duration}ms)";
            _logger.Info(message, address, accountName, duration);
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
        ~LoggingManagementConnection()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion Disposable

    }
}
