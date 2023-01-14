// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost;

using System.Linq;
using EtAlii.Ubigia.Infrastructure.Fabric;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.Ubigia.Infrastructure.Transport;
using EtAlii.Ubigia.Persistence;
using EtAlii.xTechnology.Hosting;

/// <summary>
/// This is the base class for all infrastructure test hosts.
/// It provides the host service factory that is able to understand which.
/// different services can be hosted within an infrastructure host.
/// </summary>
public class InfrastructureTestHost : ITestHost
{
    public IFunctionalContext Functional { get; }

    public IStorage Storage { get; }

    public InfrastructureTestHost(IService[] services)
    {
        Functional = services
            .OfType<IInfrastructureService>()
            .Single().Functional;
        Storage = services
            .OfType<IStorageService>()
            .Single().Storage;
    }
}
