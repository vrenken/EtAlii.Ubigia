// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management;

using System.Threading.Tasks;

public abstract class StorageClientContextBase<TDataClient> : IStorageClientContext
    where TDataClient: IStorageTransportClient
{
    public TDataClient Data { get; }

    protected IStorageConnection Connection { get; private set; }

    protected StorageClientContextBase(TDataClient data)
    {
        Data = data;
    }

    public async Task Open(IStorageConnection storageConnection)
    {
        await Data.Connect(storageConnection).ConfigureAwait(false);

        Connection = storageConnection;
    }

    public async Task Close(IStorageConnection storageConnection)
    {
        await Data.Disconnect(storageConnection).ConfigureAwait(false);

        Connection = null;
    }
}
