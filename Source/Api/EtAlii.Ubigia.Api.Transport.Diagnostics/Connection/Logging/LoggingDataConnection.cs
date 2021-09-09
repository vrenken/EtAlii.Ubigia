// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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

        /// <inheritdoc />
        public Storage Storage => _decoree.Storage;

        /// <inheritdoc />
        public Account Account => _decoree.Account;

        /// <inheritdoc />
        public Space Space => _decoree.Space;

        /// <inheritdoc />
        public IRootContext Roots => _decoree.Roots;

        /// <inheritdoc />
        public IEntryContext Entries => _decoree.Entries;

        /// <inheritdoc />
        public IContentContext Content => _decoree.Content;

        /// <inheritdoc />
        public IPropertiesContext Properties => _decoree.Properties;

        /// <inheritdoc />
        public bool IsConnected => _decoree.IsConnected;

        /// <inheritdoc />
        public DataConnectionOptions Options => _decoree.Options;

        /// <inheritdoc />
        public async Task Open()
        {
            var address = _decoree.Options.Address;
            var accountName = _decoree.Options.AccountName;
            var spaceName = _decoree.Options.Space;

            _logger.Debug("Opening data connection (Address: {Address} Account: {AccountName} Space: {SpaceName})", address, accountName, spaceName);
            var start = Environment.TickCount;
            await _decoree.Open().ConfigureAwait(false);
            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Opened data connection (Address: {Address} Account: {AccountName} Space: {SpaceName} Duration: {Duration}ms)", address, accountName, spaceName, duration);
        }

        /// <inheritdoc />
        public async Task Close()
        {
            var address = _decoree.Options.Address;
            var accountName = _decoree.Options.AccountName;
            var spaceName = _decoree.Options.Space;

            _logger.Debug("Closing data connection (Address: {Address} Account: {AccountName} Space: {SpaceName})", address, accountName, spaceName);
            var start = Environment.TickCount;
            await _decoree.Close().ConfigureAwait(false);
            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Closed data connection (Address: {Address} Account: {AccountName} Space: {SpaceName} Duration: {Duration}ms)", address, accountName, spaceName, duration);
        }
    }
}
