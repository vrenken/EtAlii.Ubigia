// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics;

using System;
using System.Threading.Tasks;
using Serilog;

public sealed class LoggingStorageConnection : IStorageConnection
{
    private readonly IStorageConnection _decoree;
    private readonly ILogger _logger = Log.ForContext<IStorageConnection>();

    private Uri _address;

    /// <inheritdoc />
    public Storage Storage => _decoree.Storage;

    /// <inheritdoc />
    public Account Account => _decoree.Account;

    /// <inheritdoc />
    public bool IsConnected => _decoree.IsConnected;

    /// <inheritdoc />
    public IStorageTransport Transport => ((dynamic)_decoree).Transport;

    /// <inheritdoc />
    public IStorageContext Storages => _decoree?.Storages;

    /// <inheritdoc />
    public IAccountContext Accounts => _decoree?.Accounts;

    /// <inheritdoc />
    public ISpaceContext Spaces => _decoree?.Spaces;

    /// <inheritdoc />
    public IStorageConnectionDetails Details => _decoree.Details;

    /// <inheritdoc />
    public IStorageConnectionOptions Options => _decoree.Options;

    public LoggingStorageConnection(IStorageConnection decoree)
    {
        _decoree = decoree;
    }

    /// <inheritdoc />
    public async Task Open(string accountName, string password)
    {
        _address = _decoree.Transport.Address;

        _logger.Debug("Opening storage connection (Address: {Address}", _address);
        var start = Environment.TickCount;

        await _decoree.Open(accountName, password).ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Debug("Opened storage connection (Address: {Address} Duration: {Duration}ms)", _address, duration);
    }

    /// <inheritdoc />
    public async Task Close()
    {
        _logger.Debug("Closing storage connection (Address: {Address}", _address);
        var start = Environment.TickCount;

        await _decoree.Close().ConfigureAwait(false);
        _address = null;

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Debug("Closed storage connection (Address: {Address} Duration: {Duration}ms)", _address, duration);
    }

    public async ValueTask DisposeAsync()
    {
        await _decoree
            .DisposeAsync()
            .ConfigureAwait(false);
    }
}
