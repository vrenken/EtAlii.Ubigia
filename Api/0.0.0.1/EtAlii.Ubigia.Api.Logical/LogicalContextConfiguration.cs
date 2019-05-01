namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public class LogicalContextConfiguration : Configuration<ILogicalContextExtension, LogicalContextConfiguration>, ILogicalContextConfiguration
    {
        public IFabricContext Fabric { get; private set; }

        public bool CachingEnabled { get; private set; }


        public LogicalContextConfiguration()
        {
            CachingEnabled = true;
        }

        public LogicalContextConfiguration UseCaching(bool cachingEnabled)
        {
            CachingEnabled = cachingEnabled;
            return this;
        }

        public LogicalContextConfiguration Use(IFabricContext fabric)
        {
            Fabric = fabric;
            return this;
        }
    }
}