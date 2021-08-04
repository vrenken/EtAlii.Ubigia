// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;
    using Microsoft.Extensions.Configuration;

    public class FabricContextConfiguration : ConfigurationBase, IFabricContextConfiguration, IEditableFabricContextConfiguration
    {
        public IConfiguration ConfigurationRoot { get; }

        /// <inheritdoc/>
        IDataConnection IEditableFabricContextConfiguration.Connection { get => Connection; set => Connection = value; }

        /// <inheritdoc/>
        public IDataConnection Connection {get; private set; }

        /// <inheritdoc/>
        bool IEditableFabricContextConfiguration.TraversalCachingEnabled { get => TraversalCachingEnabled; set => TraversalCachingEnabled = value; }

        /// <inheritdoc/>
        public bool TraversalCachingEnabled {get; private set; }

        public FabricContextConfiguration(IConfiguration configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            TraversalCachingEnabled = true;
        }
    }
}
