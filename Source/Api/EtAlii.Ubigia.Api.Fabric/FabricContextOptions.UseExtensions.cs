// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

    /// <summary>
    /// The UseExtensions class provides methods with which configuration specific settings can be configured without losing configuration type.
    /// This comes in very handy during the fluent method chaining involved.
    /// </summary>
    public static class FabricContextOptionsUseExtensions
    {
        /// <summary>
        /// Set the connection that should be used when instantiating a FabricContext.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="connection"></param>
        /// <typeparam name="TFabricContextOptions"></typeparam>
        /// <returns></returns>
        /// <exception cref="InvalidInfrastructureOperationException"></exception>
        public static TFabricContextOptions Use<TFabricContextOptions>(this TFabricContextOptions options, IDataConnection connection)
            where TFabricContextOptions: FabricContextOptions
        {
            if (!connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            ((IEditableFabricContextOptions)options).Connection = connection;
            return options;
        }

        public static TFabricContextOptions Use<TFabricContextOptions>(this TFabricContextOptions options, FabricContextOptions otherOptions)
            where TFabricContextOptions: FabricContextOptions
        {
            options.Use((ConfigurationBase)otherOptions);

            var editableOptions = (IEditableFabricContextOptions) options;

            editableOptions.Connection = otherOptions.Connection;
            editableOptions.TraversalCachingEnabled = otherOptions.TraversalCachingEnabled;

            return options;
        }

        /// <summary>
        /// When cachingEnabled is set to true the instantiated FabricContext is configured to use traversal caching.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="cachingEnabled"></param>
        /// <typeparam name="TFabricContextOptions"></typeparam>
        /// <returns></returns>
        public static TFabricContextOptions UseTraversalCaching<TFabricContextOptions>(this TFabricContextOptions options, bool cachingEnabled)
            where TFabricContextOptions: FabricContextOptions
        {
            ((IEditableFabricContextOptions)options).TraversalCachingEnabled = cachingEnabled;
            return options;
        }
    }
}
