namespace EtAlii.Ubigia.Api.Fabric
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;

    public class FabricContextConfiguration : IFabricContextConfiguration
    {

        public IDataConnection Connection { get; private set; }

        public bool TraversalCachingEnabled { get; private set; }


        public IFabricContextExtension[] Extensions { get; private set; }

        public FabricContextConfiguration()
        {
            TraversalCachingEnabled = true;
            Extensions = new IFabricContextExtension[0];
        }

        public IFabricContextConfiguration Use(IDataConnection connection)
        {
            if (!connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            Connection = connection;
            return this;
        }

        public IFabricContextConfiguration UseTraversalCaching(bool cachingEnabled)
        {
            TraversalCachingEnabled = cachingEnabled;
            return this;
        }

        public IFabricContextConfiguration Use(IFabricContextExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException("extensions");
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return this;
        }
    }
}