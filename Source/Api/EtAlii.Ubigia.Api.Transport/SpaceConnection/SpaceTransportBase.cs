// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport;

using System;
using System.Threading.Tasks;
using EtAlii.xTechnology.MicroContainer;

public abstract class SpaceTransportBase : ISpaceTransport
{
    public bool IsConnected { get; private set; }

    public Uri Address { get; }

    protected SpaceTransportBase(Uri address)
    {
        Address = address;
    }

    public virtual Task Start()
    {
        IsConnected = true;
        return Task.CompletedTask;
    }

    public virtual Task Stop()
    {
        IsConnected = false;
        return Task.CompletedTask;
    }

    protected abstract IScaffolding[] CreateScaffoldingInternal(SpaceConnectionOptions spaceConnectionOptions);

    IScaffolding[] ISpaceTransport.CreateScaffolding(SpaceConnectionOptions spaceConnectionOptions)
    {
        return CreateScaffoldingInternal(spaceConnectionOptions);
    }
}
