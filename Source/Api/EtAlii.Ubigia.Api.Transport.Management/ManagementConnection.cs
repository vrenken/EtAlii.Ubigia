// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management;

using System;
using System.Threading.Tasks;
using EtAlii.xTechnology.MicroContainer;

internal sealed class ManagementConnection : IManagementConnection
{
    private bool _disposed;

    /// <inheritdoc />
    public bool IsConnected => _connection?.IsConnected ?? false;

    /// <inheritdoc />
    public Storage Storage => _connection?.Storage;

    /// <inheritdoc />
    public IStorageContext Storages => _connection?.Storages;

    /// <inheritdoc />
    public IAccountContext Accounts => _connection?.Accounts;

    /// <inheritdoc />
    public ISpaceContext Spaces => _connection?.Spaces;

    /// <inheritdoc />
    public IStorageConnectionDetails Details => _connection?.Details;

    /// <inheritdoc />
    public ManagementConnectionOptions Options { get; }

    private IStorageConnection _connection;

    public ManagementConnection(ManagementConnectionOptions options)
    {
        Options = options;
    }

    /// <inheritdoc />
    public async Task<IDataConnection> OpenSpace(Guid accountId, Guid spaceId)
    {
        var account = await Accounts.Get(accountId).ConfigureAwait(false);
        var space = await Spaces.Get(spaceId).ConfigureAwait(false);
        return await OpenSpace(account.Name, space.Name).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IDataConnection> OpenSpace(Space space)
    {
        var account = await Accounts.Get(space.AccountId).ConfigureAwait(false);
        return await OpenSpace(account.Name, space.Name).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<IDataConnection> OpenSpace(string accountName, string spaceName)
    {
        var address = _connection.Details.DataAddress;

        var options = new DataConnectionOptions(Options.ConfigurationRoot)
            .UseTransport(Options.TransportProvider)
            .Use(address)
            .Use(accountName, spaceName, null);
        var dataConnection = Factory.Create<IDataConnection>(options);
        await dataConnection
            .Open()
            .ConfigureAwait(false);
        return dataConnection;
    }

    /// <inheritdoc />
    public async Task Open()
    {
        if (IsConnected)
        {
            throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
        }

        var options = new StorageConnectionOptions(Options.ConfigurationRoot)
            .Use(Options.TransportProvider.GetStorageTransport(Options.Address));
        _connection = new StorageConnectionFactory()
            .Create(options);
        await _connection
            .Open(Options.AccountName, Options.Password)
            .ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task Close()
    {
        if (!IsConnected)
        {
            throw new InvalidInfrastructureOperationException("The connection is already closed");
        }

        await _connection
            .Close()
            .ConfigureAwait(false);
        _connection = null;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        if (IsConnected)
        {
            await Close().ConfigureAwait(false);
        }

        _disposed = true;
    }
}
