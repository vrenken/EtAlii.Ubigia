namespace EtAlii.Servus.Api.Transport
{
    using System;
    using EtAlii.xTechnology.Logging;

    public class LoggingDataConnection : IDataConnection
    {
        private readonly IDataConnection _connection;
        private readonly ILogger _logger;

        internal LoggingDataConnection(
            IDataConnection connection,
            ILogger logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public Storage Storage { get { return _connection.Storage; } }
        public Account Account { get { return _connection.Account; } }
        public Space Space { get { return _connection.Space; } }

        public IRootContext Roots { get { return _connection.Roots; } }
        public IEntryContext Entries { get { return _connection.Entries; } }
        public IContentContext Content { get { return _connection.Content; } }
        public IPropertyContext Properties { get { return _connection.Properties; } }

        public IConnectionStatusProvider Status { get { return _connection.Status; } }

        private string _address;
        private string _accountName;
        private string _spaceName;

        public void Open(string address, string accountName, string spaceName)
        {
            _address = address;
            _accountName = accountName;
            _spaceName = spaceName;

            var message = String.Format("Opening data connection (Address: {0} Account: {1} Space: {2})", _address, _accountName, _spaceName);
            _logger.Info(message);
            var start = Environment.TickCount;
            _connection.Open(address, accountName, spaceName);
            message = String.Format("Opened data connection (Address: {0} Account: {1} Space: {2} Duration: {3}ms)", _address, _accountName, _spaceName, Environment.TickCount - start);
            _logger.Info(message);
        }

        public void Open(string address, string accountName, string password, string spaceName)
        {
            _address = address;
            _accountName = accountName;
            _spaceName = spaceName;

            var message = String.Format("Opening data connection (Address: {0} Account: {1} Space: {2})", _address, _accountName, _spaceName);
            _logger.Info(message);
            var start = Environment.TickCount;
            _connection.Open(address, accountName, password, spaceName);
            message = String.Format("Opened data connection (Address: {0} Account: {1} Space: {2} Duration: {3}ms)", _address, _accountName, _spaceName, Environment.TickCount - start);
            _logger.Info(message);
        }

        public void Close()
        {
            var message = String.Format("Closing data connection (Address: {0} Account: {1} Space: {2})", _address, _accountName, _spaceName);
            _logger.Info(message);
            var start = Environment.TickCount;
            _connection.Close();
            message = String.Format("Closed data connection (Address: {0} Account: {1} Space: {2} Duration: {3}ms)", _address, _accountName, _spaceName, Environment.TickCount - start);
            _logger.Info(message);
        }
    }
}
