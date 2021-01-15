namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using Serilog;

    public class LoggingManagementConnection : IManagementConnection
    {
        private readonly IManagementConnection _decoree;
        private readonly ILogger _logger = Log.ForContext<IManagementConnection>();

        public Storage Storage => _decoree.Storage;
        public IStorageContext Storages => _decoree.Storages;
        public IAccountContext Accounts => _decoree.Accounts;
        public ISpaceContext Spaces => _decoree.Spaces;

        public bool IsConnected => _decoree.IsConnected;

        public IStorageConnectionDetails Details => _decoree.Details;
        public IManagementConnectionConfiguration Configuration => _decoree.Configuration;

        public LoggingManagementConnection(IManagementConnection decoree)
        {
            _decoree = decoree;
        }

        public async Task Open()
        {
            var address = _decoree.Configuration.Address;
            var accountName = _decoree.Configuration.AccountName;

            _logger.Information("Opening management connection (Address: {Address} Account: {AccountName})", address, accountName);
            var start = Environment.TickCount;

            await _decoree.Open().ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Opened management connection (Address: {Address} Account: {AccountName} Duration: {Duration}ms)", address, accountName, duration);
        }

        public async Task<IDataConnection> OpenSpace(Space space)
        {
            var address = _decoree.Configuration.Address;

            _logger.Information("Opening data connection from management connection (Address: {Address} Account: {AccountId} Space: {SpaceId})", address, space.AccountId, space.Id);
            var start = Environment.TickCount;

            var connection = await _decoree.OpenSpace(space).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Opened data connection from management connection (Address: {Address} Account: {AccountId} Space: {SpaceId} Duration: {Duration}ms)", address, space.AccountId, space.Id, duration);

            return connection;
        }

        public async Task<IDataConnection> OpenSpace(Guid accountId, Guid spaceId)
        {
            var address = _decoree.Configuration.Address;

            _logger.Information("Opening data connection from management connection (Address: {Address} Account: {AccountId} Space: {SpaceId})", address, accountId, spaceId);
            var start = Environment.TickCount;

            var connection = await _decoree.OpenSpace(accountId, spaceId).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Opened data connection from management connection (Address: {Address} Account: {AccountId} Space: {SpaceId} Duration: {Duration}ms)", address, accountId, spaceId, duration);

            return connection;
        }

        public async Task<IDataConnection> OpenSpace(string accountName, string spaceName)
        {
            var address = _decoree.Configuration.Address;

            _logger.Information("Opening data connection from management connection (Address: {Address} Account: {AccountName} Space: {SpaceName})", address, accountName, spaceName);
            var start = Environment.TickCount;

            var connection = await _decoree.OpenSpace(accountName, spaceName).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Opened data connection from management connection (Address: {Address} Account: {AccountName} Space: {SpaceName} Duration: {Duration}ms)", address, accountName, spaceName, duration);

            return connection;
        }

        public async Task Close()
        {
            var address = _decoree.Configuration.Address;
            var accountName = _decoree.Configuration.AccountName;

            _logger.Information("Closing management connection (Address: {Address} Account: {AccountName})", address, accountName);
            var start = Environment.TickCount;

            await _decoree.Close().ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Closed management connection (Address: {Address} Account: {AccountName} Duration: {Duration}ms)", address, accountName, duration);
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
