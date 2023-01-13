// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.SignalR;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

public sealed partial class SignalRStorageDataClient : IStorageDataClient<ISignalRStorageTransport>
{
    private HubConnection _connection;
    private readonly IHubProxyMethodInvoker _invoker;

    public SignalRStorageDataClient(IHubProxyMethodInvoker invoker)
    {
        _invoker = invoker;
    }


    public async Task<Storage> Add(string storageName, string storageAddress)
    {
        var storage = new Storage
        {
            Name = storageName,
            Address = storageAddress,
        };
        return await _invoker.Invoke<Storage>(_connection, SignalRHub.Storage, "Post", storage).ConfigureAwait(false);
    }

    public async Task Remove(Guid storageId)
    {
        await _invoker.Invoke(_connection, SignalRHub.Storage, "Delete", storageId).ConfigureAwait(false);
    }

    public async Task<Storage> Change(Guid storageId, string storageName, string storageAddress)
    {
        var storage = new Storage
        {
            Id = storageId,
            Name = storageName,
            Address = storageAddress,
        };
        return await _invoker.Invoke<Storage>(_connection, SignalRHub.Storage, "Put", storageId, storage).ConfigureAwait(false);
    }

    public async Task<Storage> Get(string storageName)
    {
        return await _invoker.Invoke<Storage>(_connection, SignalRHub.Storage, "GetByName", storageName).ConfigureAwait(false);
    }

    public async Task<Storage> Get(Guid storageId)
    {
        return await _invoker.Invoke<Storage>(_connection, SignalRHub.Storage, "Get", storageId).ConfigureAwait(false);
    }

    public async IAsyncEnumerable<Storage> GetAll()
    {
        var items = _invoker
            .Stream<Storage>(_connection, SignalRHub.Storage, "GetAll")
            .ConfigureAwait(false);
        await foreach (var item in items)
        {
            yield return item;
        }
    }

    public async Task Connect(IStorageConnection storageConnection)
    {
        await Connect((IStorageConnection<ISignalRStorageTransport>)storageConnection).ConfigureAwait(false);
    }

    public async Task Disconnect(IStorageConnection storageConnection)
    {
        await Disconnect((IStorageConnection<ISignalRStorageTransport>)storageConnection).ConfigureAwait(false);
    }

    public async Task Connect(IStorageConnection<ISignalRStorageTransport> storageConnection)
    {
        _connection = new HubConnectionFactory().Create(storageConnection.Transport, new Uri(storageConnection.Transport.Address + UriHelper.Delimiter + SignalRHub.Storage, UriKind.Absolute));
        await _connection.StartAsync().ConfigureAwait(false);
    }

    public async Task Disconnect(IStorageConnection<ISignalRStorageTransport> storageConnection)
    {
        await _connection.DisposeAsync().ConfigureAwait(false);
        _connection = null;
    }
}
