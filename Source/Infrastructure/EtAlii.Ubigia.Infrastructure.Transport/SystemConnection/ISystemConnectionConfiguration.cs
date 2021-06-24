// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public interface ISystemConnectionConfiguration : IConfiguration
    {
        IStorageTransportProvider TransportProvider { get; }
        Func<ISystemConnection> FactoryExtension { get; }
        IInfrastructure Infrastructure { get; }
    }
}