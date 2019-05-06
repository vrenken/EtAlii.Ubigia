namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

    /// <summary>
    /// The UseExtensions class provides methods with which configuration specific settings can be configured without losing configuration type.
    /// This comes in very handy during the fluent method chaining involved. 
    /// </summary>
    public static class FabricContextConfigurationUseExtensions
    {
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
        
        public static TFabricContextConfiguration UseTraversalCaching<TFabricContextConfiguration>(this TFabricContextConfiguration configuration, bool cachingEnabled)
            where TFabricContextConfiguration: FabricContextConfiguration
        {
            ((IEditableFabricContextConfiguration)configuration).TraversalCachingEnabled = cachingEnabled;
            return configuration;
        }
        
        public static TFabricContextConfiguration Use<TFabricContextConfiguration>(this TFabricContextConfiguration configuration, FabricContextConfiguration otherConfiguration)
            where TFabricContextConfiguration: FabricContextConfiguration
        {
            configuration.Use((Configuration)otherConfiguration);

            var editableConfiguration = (IEditableFabricContextConfiguration) configuration;

            editableConfiguration.Connection = otherConfiguration.Connection;
            editableConfiguration.TraversalCachingEnabled = otherConfiguration.TraversalCachingEnabled;

            return configuration;
        }

    }
}