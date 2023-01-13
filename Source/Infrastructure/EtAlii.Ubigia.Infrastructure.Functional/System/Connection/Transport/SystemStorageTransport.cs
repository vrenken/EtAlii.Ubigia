// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport;

public class SystemStorageTransport : ISystemStorageTransport
{
    public bool IsConnected { get; private set; }

    private readonly IFunctionalContext _functionalContext;

    public Uri Address { get; }

    public SystemStorageTransport(Uri address, IFunctionalContext functionalContext)
    {
        Address = address;
        _functionalContext = functionalContext;
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

    xTechnology.MicroContainer.IScaffolding[] IStorageTransport.CreateScaffolding()
    {
        return new xTechnology.MicroContainer.IScaffolding[]
        {
            new SystemClientsScaffolding(_functionalContext)
        };
    }
}
