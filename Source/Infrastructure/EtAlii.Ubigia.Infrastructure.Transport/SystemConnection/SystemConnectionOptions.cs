// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.Extensions.Configuration;

    public class SystemConnectionOptions : ConfigurationBase, ISystemConnectionOptions, IEditableSystemConnectionOptions
    {
        /// <inheritdoc />
        public IConfigurationRoot ConfigurationRoot { get; }

        /// <inheritdoc />
        IStorageTransportProvider IEditableSystemConnectionOptions.TransportProvider { get => TransportProvider; set => TransportProvider = value; }

        /// <inheritdoc />
        public IStorageTransportProvider TransportProvider { get; private set; }

        /// <inheritdoc />
        Func<ISystemConnection> IEditableSystemConnectionOptions.FactoryExtension { get => FactoryExtension; set => FactoryExtension = value; }

        /// <inheritdoc />
        public Func<ISystemConnection> FactoryExtension { get; private set; }

        /// <inheritdoc />
        IInfrastructure IEditableSystemConnectionOptions.Infrastructure { get => Infrastructure; set => Infrastructure = value; }

        /// <inheritdoc />
        public IInfrastructure Infrastructure { get; private set; }

        public SystemConnectionOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
        }
    }
}
