// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using Microsoft.Extensions.Configuration;

    public class LogicalContextOptions : ConfigurationBase, ILogicalContextOptions
    {
        /// <inheritdoc />
        public IConfigurationRoot ConfigurationRoot { get; }

        /// <inheritdoc />
        public IFabricContext Fabric { get; private set; }

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <inheritdoc />
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
