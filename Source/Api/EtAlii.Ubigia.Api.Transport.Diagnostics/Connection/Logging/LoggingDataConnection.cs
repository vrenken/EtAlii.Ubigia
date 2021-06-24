// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System;
    using System.Threading.Tasks;
    using Serilog;

    public class LoggingDataConnection : IDataConnection
    {
        private readonly IDataConnection _decoree;
        private readonly ILogger _logger = Log.ForContext<IDataConnection>();

        public LoggingDataConnection(IDataConnection decoree)
        {
            _decoree = decoree;
        }

        public Storage Storage => _decoree.Storage;
        public Account Account => _decoree.Account;
        public Space Space => _decoree.Space;

        public IRootContext Roots => _decoree.Roots;
        public IEntryContext Entries => _decoree.Entries;
        public IContentContext Content => _decoree.Content;
        public IPropertiesContext Properties => _decoree.Properties;

        public bool IsConnected => _decoree.IsConnected;
        public IDataConnectionConfiguration Configuration => _decoree.Configuration;

        public async Task Open()
        {
            var address = _decoree.Configuration.Address;
            var accountName = _decoree.Configuration.AccountName;
            var spaceName = _decoree.Configuration.Space;

            _logger.Information("Opening data connection (Address: {Address} Account: {AccountName} Space: {SpaceName})", address, accountName, spaceName);
            var start = Environment.TickCount;
            await _decoree.Open().ConfigureAwait(false);
            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Opened data connection (Address: {Address} Account: {AccountName} Space: {SpaceName} Duration: {Duration}ms)", address, accountName, spaceName, duration);
        }

        public async Task Close()
        {
            var address = _decoree.Configuration.Address;
            var accountName = _decoree.Configuration.AccountName;
            var spaceName = _decoree.Configuration.Space;

            _logger.Information("Closing data connection (Address: {Address} Account: {AccountName} Space: {SpaceName})", address, accountName, spaceName);
            var start = Environment.TickCount;
            await _decoree.Close().ConfigureAwait(false);
            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Closed data connection (Address: {Address} Account: {AccountName} Space: {SpaceName} Duration: {Duration}ms)", address, accountName, spaceName, duration);
        }
    }
}
