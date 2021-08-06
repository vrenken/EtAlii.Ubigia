// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Persistence;
    using Microsoft.Extensions.Configuration;

    public class FabricContextOptions : ConfigurationBase
    {
        /// <summary>
        /// The host configuration root that will be used to configure the logical context.
        /// </summary>
        public IConfigurationRoot ConfigurationRoot { get; }

        public IStorage Storage { get; private set; }

        public FabricContextOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
        }

        public FabricContextOptions Use(IStorage storage)
        {
            Storage = storage ?? throw new ArgumentException("No storage specified", nameof(storage));

            return this;
        }
    }
}
