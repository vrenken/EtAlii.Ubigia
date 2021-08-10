// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public interface ISystemConnectionOptions : IExtensible
    {
        /// <summary>
        /// The client configuration root that will be used to configure the system connection.
        /// </summary>
        IConfigurationRoot ConfigurationRoot { get; }

        IStorageTransportProvider TransportProvider { get; }

        Func<ISystemConnection> FactoryExtension { get; }

        IInfrastructure Infrastructure { get; }
    }
}
