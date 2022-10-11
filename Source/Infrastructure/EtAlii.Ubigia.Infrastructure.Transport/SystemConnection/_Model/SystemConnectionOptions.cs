// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class SystemConnectionOptions : IExtensible
    {
        /// <summary>
        /// The client configuration root that will be used to configure the system connection.
        /// </summary>
        public IConfigurationRoot ConfigurationRoot { get; }

        /// <inheritdoc/>
        IExtension[] IExtensible.Extensions { get; set; }

        /// <summary>
        /// The storage transport for which the system connection should be created.
        /// This same transport is also used when data or management connections are requested
        /// through the system connection.
        /// </summary>
        public IStorageTransportProvider TransportProvider { get; private set; }

        /// <summary>
        /// A system connection extension factory with which more complex connection setups
        /// can be configured.
        /// </summary>
        public Func<ISystemConnection> FactoryExtension { get; private set; }

        /// <summary>
        /// The service details for which the system connection should be created.
        /// </summary>/>
        public ServiceDetails[] ServiceDetails { get; private set; }

        public SystemConnectionOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;

            ((IExtensible)this).Extensions = new IExtension[]
            {
                new CommonSystemConnectionExtension(this)
            };
        }

        public SystemConnectionOptions Use(Func<ISystemConnection> factoryExtension)
        {
            FactoryExtension = factoryExtension;
            return this;
        }

        public SystemConnectionOptions Use(IFunctionalContext functionalContext)
        {
            functionalContext = functionalContext ?? throw new ArgumentNullException(nameof(functionalContext));

            if (TransportProvider != null)
            {
                throw new InvalidOperationException("A TransportProvider has already been assigned to this SystemConnectionOptions");
            }

            ServiceDetails = functionalContext.Options.ServiceDetails;

            TransportProvider = new SystemTransportProvider(functionalContext);

            return this;
        }
    }
}
