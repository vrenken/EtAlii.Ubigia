// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;
    using Microsoft.Extensions.Configuration;

    public sealed class FabricContextOptions : ConfigurationBase, IFabricContextOptions
    {
        /// <inheritdoc/>
        public IConfigurationRoot ConfigurationRoot { get; }

        /// <inheritdoc/>
        public IDataConnection Connection {get; private set; }

        /// <inheritdoc/>
        public bool CachingEnabled {get; private set; }

        public FabricContextOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            CachingEnabled = true;
        }

        /// <summary>
        /// Set the connection that should be used when instantiating a FabricContext.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        /// <exception cref="InvalidInfrastructureOperationException"></exception>
        public FabricContextOptions Use(IDataConnection connection)
        {
            if (!connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            Connection = connection;
            return this;
        }

        public FabricContextOptions Use(FabricContextOptions otherOptions)
        {
            this.Use((ConfigurationBase)otherOptions);

            Connection = otherOptions.Connection;
            CachingEnabled = otherOptions.CachingEnabled;

            return this;
        }

        /// <summary>
        /// When cachingEnabled is set to true the instantiated FabricContext is configured to use traversal caching.
        /// </summary>
        /// <param name="cachingEnabled"></param>
        /// <returns></returns>
        public FabricContextOptions UseCaching(bool cachingEnabled)
        {
            CachingEnabled = cachingEnabled;
            return this;
        }
    }
}
