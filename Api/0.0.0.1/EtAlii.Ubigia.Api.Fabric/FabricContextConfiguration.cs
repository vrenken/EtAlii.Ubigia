namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.Ubigia.Api.Transport;

    public class FabricContextConfiguration : Configuration<IFabricContextExtension, FabricContextConfiguration>, IFabricContextConfiguration
    {

        public IDataConnection Connection { get; private set; }

        public bool TraversalCachingEnabled { get; private set; }

        public FabricContextConfiguration()
        {
            TraversalCachingEnabled = true;
        }

        public FabricContextConfiguration Use(IDataConnection connection)
        {
            if (!connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            Connection = connection;
            return this;
        }

        public FabricContextConfiguration UseTraversalCaching(bool cachingEnabled)
        {
            TraversalCachingEnabled = cachingEnabled;
            return this;
        }
    }
}