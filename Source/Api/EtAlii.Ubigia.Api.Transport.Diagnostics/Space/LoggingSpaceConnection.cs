// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics;

using System;
using System.Threading.Tasks;
using Serilog;

public sealed class LoggingSpaceConnection : ISpaceConnection
{
    private readonly ISpaceConnection _decoree;
    private readonly ILogger _logger = Log.ForContext<ISpaceConnection>();

    private Uri _address;

    /// <inheritdoc />
    public Account Account => _decoree.Account;

    /// <inheritdoc />
    public Space Space => _decoree.Space;

    /// <inheritdoc />
    public Storage Storage => _decoree.Storage;

    /// <inheritdoc />
    public bool IsConnected => _decoree.IsConnected;

    /// <inheritdoc />
    public ISpaceTransport Transport => ((dynamic)_decoree).Transport;

    /// <inheritdoc />
    public SpaceConnectionOptions Options => _decoree.Options;

    /// <inheritdoc />
    public IAuthenticationContext Authentication => _decoree.Authentication;

    /// <inheritdoc />
    public IEntryContext Entries => _decoree.Entries;

    /// <inheritdoc />
    public IRootContext Roots => _decoree.Roots;

    /// <inheritdoc />
    public IContentContext Content => _decoree.Content;

    /// <inheritdoc />
    public IPropertiesContext Properties => _decoree.Properties;

    public LoggingSpaceConnection(ISpaceConnection decoree)
    {
        _decoree = decoree;
    }

    /// <inheritdoc />
    public async Task Open(string accountName, string password)
    {
        _address = _decoree.Transport.Address;

        _logger.Debug("Opening space connection (Address: {Address}", _address);
        var start = Environment.TickCount;

        await _decoree.Open(accountName, password).ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Debug("Opened space connection (Address: {Address} Duration: {Duration}ms)", _address, duration);
    }

    /// <inheritdoc />
    public async Task Close()
    {
        _logger.Debug("Closing space connection (Address: {Address}", _address);
        var start = Environment.TickCount;

        await _decoree.Close().ConfigureAwait(false);
        _address = null;

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Debug("Closed space connection (Address: {Address} Duration: {Duration}ms)", _address, duration);
    }

    public async ValueTask DisposeAsync()
    {
        await _decoree
            .DisposeAsync()
            .ConfigureAwait(false);
    }
}
