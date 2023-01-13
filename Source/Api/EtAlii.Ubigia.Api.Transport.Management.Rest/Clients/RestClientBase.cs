// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Rest;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.Rest;

internal abstract class RestClientBase
{
    protected IRestStorageConnection Connection { get; private set; }

    public async Task Connect(IStorageConnection storageConnection)
    {
        await Connect((IStorageConnection<IRestStorageTransport>)storageConnection).ConfigureAwait(false);
    }

    public virtual Task Connect(IStorageConnection<IRestStorageTransport> storageConnection)
    {
        Connection = (IRestStorageConnection)storageConnection;
        return Task.CompletedTask;
    }

    public async Task Disconnect(IStorageConnection storageConnection)
    {
        await Connect((IStorageConnection<IRestStorageTransport>)storageConnection).ConfigureAwait(false);
    }

    public virtual Task Disconnect(IStorageConnection<IRestStorageTransport> storageConnection)
    {
        Connection = null;
        return Task.CompletedTask;
    }
}
