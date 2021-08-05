// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;
    using Microsoft.Extensions.Configuration;

    public class FabricContextOptions : ConfigurationBase, IFabricContextOptions, IEditableFabricContextOptions
    {
        /// <inheritdoc/>
        public IConfiguration ConfigurationRoot { get; }

        /// <inheritdoc/>
        IDataConnection IEditableFabricContextOptions.Connection { get => Connection; set => Connection = value; }

        /// <inheritdoc/>
        public IDataConnection Connection {get; private set; }

        /// <inheritdoc/>
        bool IEditableFabricContextOptions.TraversalCachingEnabled { get => TraversalCachingEnabled; set => TraversalCachingEnabled = value; }

        /// <inheritdoc/>
        public bool TraversalCachingEnabled {get; private set; }

        public FabricContextOptions(IConfiguration configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            TraversalCachingEnabled = true;
        }
    }
}
