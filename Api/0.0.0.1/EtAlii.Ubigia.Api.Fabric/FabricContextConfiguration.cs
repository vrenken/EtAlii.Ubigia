namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

    public class FabricContextConfiguration : Configuration<FabricContextConfiguration>, IFabricContextConfiguration, IEditableFabricContextConfiguration 
    {
        IDataConnection IEditableFabricContextConfiguration.Connection { get => Connection; set => Connection = value; }
        public IDataConnection Connection {get; private set; }

        bool IEditableFabricContextConfiguration.TraversalCachingEnabled { get => TraversalCachingEnabled; set => TraversalCachingEnabled = value; }
        public bool TraversalCachingEnabled {get; private set; }
        
        public FabricContextConfiguration()
        {
            TraversalCachingEnabled = true;
        }
    }
}