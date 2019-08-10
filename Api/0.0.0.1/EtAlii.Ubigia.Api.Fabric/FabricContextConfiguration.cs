namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

    public class FabricContextConfiguration : Configuration, IFabricContextConfiguration, IEditableFabricContextConfiguration
    { 
        /// <inheritdoc/>
        IDataConnection IEditableFabricContextConfiguration.Connection { get => Connection; set => Connection = value; }

        /// <inheritdoc/>
        public IDataConnection Connection {get; private set; }

        /// <inheritdoc/>
        bool IEditableFabricContextConfiguration.TraversalCachingEnabled { get => TraversalCachingEnabled; set => TraversalCachingEnabled = value; }

        /// <inheritdoc/>
        public bool TraversalCachingEnabled {get; private set; }
        
        public FabricContextConfiguration()
        {
            TraversalCachingEnabled = true;
        }
    }
}