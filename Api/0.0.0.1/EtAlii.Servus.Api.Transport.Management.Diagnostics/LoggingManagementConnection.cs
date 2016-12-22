﻿namespace EtAlii.Servus.Api.Management
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.Logging;

    public class LoggingManagementConnection : IManagementConnection
    {
        private readonly IManagementConnection _decoree;
        private readonly ILogger _logger;

        public Storage Storage { get { return _decoree.Storage; } }
        public IStorageContext Storages { get { return _decoree.Storages; } }
        public IAccountContext Accounts { get { return _decoree.Accounts; } }
        public ISpaceContext Spaces { get { return _decoree.Spaces; } }

        public bool IsConnected { get { return _decoree.IsConnected; } }
        public IManagementConnectionConfiguration Configuration{ get { return _decoree.Configuration; } }

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

            var message = String.Format("Opening management connection (Address: {0} Account: {1})", address, accountName);
            _logger.Info(message);
            var start = Environment.TickCount;

            await _decoree.Open();

            message = $"Opened management connection (Address: {address} Account: {accountName} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public async Task<IDataConnection> OpenSpace(Space space)
        {
            var address = _decoree.Configuration.Address;

            var message = String.Format("Opening management connection (Address: {0} Account: {1} Space: {2})", address, space.AccountId, space.Id);
            _logger.Info(message);
            var start = Environment.TickCount;

            var connection = await _decoree.OpenSpace(space);

            message = $"Opened dataconnection from management connection (Address: {address} Account: {space.AccountId} Space: {space.Id} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return connection;
        }

        public async Task<IDataConnection> OpenSpace(Guid accountId, Guid spaceId)
        {
            var address = _decoree.Configuration.Address;

            var message = String.Format("Opening management connection (Address: {0} Account: {1} Space: {2})", address, accountId, spaceId);
            _logger.Info(message);
            var start = Environment.TickCount;

            var connection = await _decoree.OpenSpace(accountId, spaceId);

            message = $"Opened dataconnection from management connection (Address: {address} Account: {accountId} Space: {spaceId} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return connection;
        }

        public async Task<IDataConnection> OpenSpace(string accountName, string spaceName)
        {
            var address = _decoree.Configuration.Address;

            var message = String.Format("Opening management connection (Address: {0} Account: {1} Space: {2})", address, accountName, spaceName);
            _logger.Info(message);
            var start = Environment.TickCount;

            var connection = await _decoree.OpenSpace(accountName, spaceName);

            message = $"Opened dataconnection from management connection (Address: {address} Account: {accountName} Space: {spaceName} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return connection;
        }

        public async Task Close()
        {
            var address = _decoree.Configuration.Address;
            var accountName = _decoree.Configuration.AccountName;

            var message = String.Format("Closing management connection (Address: {0} Account: {1})", address, accountName);
            _logger.Info(message);
            var start = Environment.TickCount;

            await _decoree.Close();

            message = $"Closed management connection (Address: {address} Account: {accountName} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
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

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                    if (_decoree.IsConnected)
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
        ~LoggingManagementConnection()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion Disposable

    }
}
