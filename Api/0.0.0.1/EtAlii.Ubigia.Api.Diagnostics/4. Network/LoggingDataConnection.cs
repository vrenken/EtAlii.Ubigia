namespace EtAlii.Ubigia.Api.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Logging;

    public class LoggingDataConnection : IDataConnection
    {
        private readonly IDataConnection _decoree;
        private readonly ILogger _logger;

        internal LoggingDataConnection(
            IDataConnection decoree,
            ILogger logger)
        {
            _decoree = decoree;
            _logger = logger;
        }

        public Storage Storage => _decoree.Storage;
        public Account Account => _decoree.Account;
        public Space Space => _decoree.Space;

        public IRootContext Roots => _decoree.Roots;
        public IEntryContext Entries => _decoree.Entries;
        public IContentContext Content => _decoree.Content;
        public IPropertyContext Properties => _decoree.Properties;

        public bool IsConnected => _decoree.IsConnected;
        public IDataConnectionConfiguration Configuration => _decoree.Configuration;

        public async Task Open()
        {
            var address = _decoree.Configuration.Address;
            var accountName = _decoree.Configuration.AccountName;
            var spaceName = _decoree.Configuration.Space;

            var message = $"Opening data connection (Address: {address} Account: {accountName} Space: {spaceName})";
            _logger.Info(message);
            var start = Environment.TickCount;
            await _decoree.Open();
            message =
                $"Opened data connection (Address: {address} Account: {accountName} Space: {spaceName} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }

        public async Task Close()
        {
            var address = _decoree.Configuration.Address;
            var accountName = _decoree.Configuration.AccountName;
            var spaceName = _decoree.Configuration.Space;

            var message = $"Closing data connection (Address: {address} Account: {accountName} Space: {spaceName})";
            _logger.Info(message);
            var start = Environment.TickCount;
            await _decoree.Close();
            message =
                $"Closed data connection (Address: {address} Account: {accountName} Space: {spaceName} Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);
        }
    }
}
