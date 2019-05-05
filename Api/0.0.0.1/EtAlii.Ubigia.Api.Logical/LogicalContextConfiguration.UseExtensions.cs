namespace EtAlii.Ubigia.Api.Logical
{
//    using EtAlii.Ubigia.Api.Fabric;

    public static class LogicalContextConfigurationUseExtensions
    {

        public static TLogicalContextConfiguration UseCaching<TLogicalContextConfiguration>(this TLogicalContextConfiguration configuration, bool cachingEnabled)
            where TLogicalContextConfiguration: LogicalContextConfiguration
        {
            ((IEditableLogicalContextConfiguration)configuration).CachingEnabled = cachingEnabled;
            return configuration;
        }

//        public static TLogicalContextConfiguration Use<TLogicalContextConfiguration>(this TLogicalContextConfiguration configuration, IFabricContext fabric)
//            where TLogicalContextConfiguration: LogicalContextConfiguration
//        {
//            ((IEditableLogicalContextConfiguration)configuration).Fabric = fabric;
//            return configuration;
//        }
    }
}