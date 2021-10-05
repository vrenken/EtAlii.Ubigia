// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class LogicalContextOptions : IExtensible, ILogicalContextOptions
    {
        /// <summary>
        /// The host configuration root that will be used to configure the logical context.
        /// </summary>
        public IConfigurationRoot ConfigurationRoot { get; }

        /// <summary>
        /// The fabric that should be used by the logical context.
        /// </summary>
        public IFabricContext Fabric { get; private set; }

        /// <inheritdoc/>
        IExtension[] IExtensible.Extensions { get; set; }

        /// <summary>
        /// The name of the Ubigia storage.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The address (schema+host) at which the storage can be found.
        /// </summary>
        public Uri StorageAddress { get; private set; }

        public LogicalContextOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
        }

        /// <inheritdoc />
        public LogicalContextOptions Use(string name, Uri storageAddress)
        {
			Name = name ?? throw new ArgumentNullException(nameof(name));
            StorageAddress = storageAddress ?? throw new ArgumentNullException(nameof(storageAddress));
            ((IExtensible)this).Extensions = Array.Empty<IExtension>();

            return this;
        }

        /// <inheritdoc />
        public LogicalContextOptions Use(IFabricContext fabric)
        {
			Fabric = fabric ?? throw new ArgumentException("No fabric context specified", nameof(fabric));

            return this;
        }
    }
}
