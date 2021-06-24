// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalContextConfiguration : ILogicalContextConfiguration
    {
        /// <inheritdoc /> 
        public IFabricContext Fabric { get; private set; }

        /// <inheritdoc /> 
        public string Name { get; private set; }

        /// <inheritdoc /> 
        public Uri StorageAddress { get; private set; }

        public ILogicalContextConfiguration Use(string name, Uri storageAddress)
        {
			Name = name ?? throw new ArgumentNullException(nameof(name));
            StorageAddress = storageAddress ?? throw new ArgumentNullException(nameof(storageAddress));

            return this;
        }

        public ILogicalContextConfiguration Use(IFabricContext fabric)
        {
			Fabric = fabric ?? throw new ArgumentException("No fabric context specified", nameof(fabric));

            return this;
        }
    }
}
