// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

    /// <summary>
    /// The UseExtensions class provides methods with which configuration specific settings can be configured without losing configuration type.
    /// This comes in very handy during the fluent method chaining involved.
    /// </summary>
    public static class FabricContextConfigurationUseExtensions
    {
        /// <summary>
        /// Set the connection that should be used when instantiating a FabricContext.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="connection"></param>
        /// <typeparam name="TFabricContextConfiguration"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidInfrastructureOperationException"></exception>
        public static TFabricContextConfiguration Use<TFabricContextConfiguration>(this TFabricContextConfiguration configuration, IDataConnection connection)
            where TFabricContextConfiguration: FabricContextConfiguration
        {
            if (!connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            ((IEditableFabricContextConfiguration)configuration).Connection = connection;
            return configuration;
        }

        public static TFabricContextConfiguration Use<TFabricContextConfiguration>(this TFabricContextConfiguration configuration, FabricContextConfiguration otherConfiguration)
            where TFabricContextConfiguration: FabricContextConfiguration
        {
            configuration.Use((ConfigurationBase)otherConfiguration);

            var editableConfiguration = (IEditableFabricContextConfiguration) configuration;

            editableConfiguration.Connection = otherConfiguration.Connection;
            editableConfiguration.TraversalCachingEnabled = otherConfiguration.TraversalCachingEnabled;

            return configuration;
        }

        /// <summary>
        /// When cachingEnabled is set to true the instantiated FabricContext is configured to use traversal caching.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="cachingEnabled"></param>
        /// <typeparam name="TFabricContextConfiguration"></typeparam>
        /// <returns></returns>
        public static TFabricContextConfiguration UseTraversalCaching<TFabricContextConfiguration>(this TFabricContextConfiguration configuration, bool cachingEnabled)
            where TFabricContextConfiguration: FabricContextConfiguration
        {
            ((IEditableFabricContextConfiguration)configuration).TraversalCachingEnabled = cachingEnabled;
            return configuration;
        }
    }
}
