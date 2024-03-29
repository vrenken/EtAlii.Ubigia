// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System.Threading.Tasks;

internal sealed class PropertiesContextStub : IPropertiesContext
{
    public IPropertiesDataClient Data { get; }

    public PropertiesContextStub()
    {
        Data = new PropertiesDataClientStub();
    }

    public Task Open(ISpaceConnection spaceConnection)
    {
        return Task.CompletedTask;
    }

    public Task Close(ISpaceConnection spaceConnection)
    {
        return Task.CompletedTask;
    }

}
