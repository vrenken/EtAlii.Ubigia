namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using System.Threading.Tasks;
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

            var message = String.Format("Opening data connection (Address: {0} Account: {1} Space: {2})", address, accountName, spaceName);
            _logger.Info(message);
            var start = Environment.TickCount;
            await _decoree.Open();
            message = String.Format("Opened data connection (Address: {0} Account: {1} Space: {2} Duration: {3}ms)", address, accountName, spaceName, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            _logger.Info(message);
        }

        public async Task Close()
        {
            var address = _decoree.Configuration.Address;
            var accountName = _decoree.Configuration.AccountName;
            var spaceName = _decoree.Configuration.Space;

            var message = String.Format("Closing data connection (Address: {0} Account: {1} Space: {2})", address, accountName, spaceName);
            _logger.Info(message);
            var start = Environment.TickCount;
            await _decoree.Close();
            message = String.Format("Closed data connection (Address: {0} Account: {1} Space: {2} Duration: {3}ms)", address, accountName, spaceName, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            _logger.Info(message);
        }
    }
}
