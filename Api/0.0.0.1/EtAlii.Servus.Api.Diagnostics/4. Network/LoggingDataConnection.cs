namespace EtAlii.Servus.Api.Transport
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

        public Storage Storage { get { return _decoree.Storage; } }
        public Account Account { get { return _decoree.Account; } }
        public Space Space { get { return _decoree.Space; } }

        public IRootContext Roots { get { return _decoree.Roots; } }
        public IEntryContext Entries { get { return _decoree.Entries; } }
        public IContentContext Content { get { return _decoree.Content; } }
        public IPropertyContext Properties { get { return _decoree.Properties; } }

        public bool IsConnected { get { return _decoree.IsConnected; } }
        public IDataConnectionConfiguration Configuration { get { return _decoree.Configuration; } }

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
