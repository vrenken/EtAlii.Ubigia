﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management;

using System;
using System.Threading.Tasks;

public abstract class StorageConnection<TTransport> : IStorageConnection<TTransport>
    where TTransport : IStorageTransport
{
    private bool _disposed;

    /// <inheritdoc />
    public Storage Storage { get; private set; }

    /// <inheritdoc />
    public Account Account => throw new NotImplementedException();

    IStorageTransport IStorageConnection.Transport => Transport;

    /// <inheritdoc />
    public TTransport Transport { get; }

    /// <inheritdoc />
    public IStorageContext Storages { get; }

    /// <inheritdoc />
    public IAccountContext Accounts { get; }

    private readonly IAuthenticationManagementContext _authentication;
    private readonly IInformationContext _information;

    /// <inheritdoc />
    public ISpaceContext Spaces { get; }

    public bool IsConnected => Storage != null;

    /// <inheritdoc />
    public IStorageConnectionDetails Details => _details;
    private readonly StorageConnectionDetails _details;

    /// <inheritdoc />
    public IStorageConnectionOptions Options { get; }

    protected StorageConnection(
        IStorageTransport transport,
        IStorageConnectionOptions options,
        IStorageContext storages,
        ISpaceContext spaces,
        IAccountContext accounts,
        IAuthenticationManagementContext authentication,
        IInformationContext information)
    {
        Transport = (TTransport)transport;
        Options = options;
        Storages = storages;
        Spaces = spaces;
        Accounts = accounts;
        _authentication = authentication;
        _information = information;

        _details = new StorageConnectionDetails();
    }

    public async Task Close()
    {
        if (!IsConnected)
        {
            throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyClosed);
        }

        await Accounts.Close(this).ConfigureAwait(false);
        await Storages.Close(this).ConfigureAwait(false);
        await Spaces.Close(this).ConfigureAwait(false);
        await _information.Close(this).ConfigureAwait(false);

        await Transport.Stop().ConfigureAwait(false);
        Storage = null;
    }

    public async Task Open(string accountName, string password)
    {
        if (IsConnected)
        {
            throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
        }

        await  _authentication.Data.Authenticate(this, accountName, password).ConfigureAwait(false);

        Storage = await _information.Data.GetConnectedStorage(this).ConfigureAwait(false);
        var details = await _information.Data.GetConnectivityDetails(this).ConfigureAwait(false);
        _details.Update(Storage, details.ManagementAddress, details.DataAddress);

        await _information.Open(this).ConfigureAwait(false);
        await Accounts.Open(this).ConfigureAwait(false);
        await Storages.Open(this).ConfigureAwait(false);
        await Spaces.Open(this).ConfigureAwait(false);

        await Transport.Start().ConfigureAwait(false);

    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        GC.SuppressFinalize(this);

        if (IsConnected)
        {
            await Close().ConfigureAwait(false);
            Storage = null;
        }

        _disposed = true;
    }
}
