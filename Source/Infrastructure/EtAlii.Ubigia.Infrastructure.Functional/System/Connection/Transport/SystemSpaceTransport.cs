// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport;
using EtAlii.xTechnology.MicroContainer;

public class SystemSpaceTransport : ISystemSpaceTransport
{
    public bool IsConnected { get; private set; }

    private readonly IFunctionalContext _functionalContext;

    public Uri Address { get; }

    public SystemSpaceTransport(Uri address, IFunctionalContext functionalContext)
    {
        Address = address;
        _functionalContext = functionalContext;
    }

    public SystemSpaceTransport(Uri address)
    {
        Address = address;
    }

    public Task Start()
    {
        IsConnected = true;
        return Task.CompletedTask;
    }

    public Task Stop()
    {
        IsConnected = false;
        return Task.CompletedTask;
    }

    IScaffolding[] ISpaceTransport.CreateScaffolding(SpaceConnectionOptions spaceConnectionOptions)
    {
        return new IScaffolding[]
        {
            new SystemClientsScaffolding(_functionalContext, spaceConnectionOptions)
        };
    }
}
