// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System.Threading.Tasks;

public class SpaceClientContextBase<TDataClient> : ISpaceClientContext
    where TDataClient: ISpaceTransportClient
{
    public TDataClient Data { get; }

    protected SpaceClientContextBase(
        TDataClient data)
    {
        Data = data;
    }

    public async Task Open(ISpaceConnection spaceConnection)
    {
        await Data.Connect(spaceConnection).ConfigureAwait(false);
    }

    public async Task Close(ISpaceConnection spaceConnection)
    {
        await Data.Disconnect().ConfigureAwait(false);
    }
}
