namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

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
    }
}