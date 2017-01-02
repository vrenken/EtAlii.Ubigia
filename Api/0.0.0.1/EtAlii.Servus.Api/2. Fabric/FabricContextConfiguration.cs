namespace EtAlii.Servus.Api.Fabric
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Transport;

    public class FabricContextConfiguration : IFabricContextConfiguration
    {

        public IDataConnection Connection { get { return _connection; } }
        private IDataConnection _connection;

        public bool TraversalCachingEnabled { get { return _traversalCachingEnabled; } }
        private bool _traversalCachingEnabled;


        public IFabricContextExtension[] Extensions { get { return _extensions; } }
        private IFabricContextExtension[] _extensions;

        public FabricContextConfiguration()
        {
            _traversalCachingEnabled = true;
            _extensions = new IFabricContextExtension[0];
        }

        public IFabricContextConfiguration Use(IDataConnection connection)
        {
            if (!connection.IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.NoConnection);
            }
            _connection = connection;
            return this;
        }

        public IFabricContextConfiguration UseTraversalCaching(bool cachingEnabled)
        {
            _traversalCachingEnabled = cachingEnabled;
            return this;
        }

        public IFabricContextConfiguration Use(IFabricContextExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException("extensions");
            }

            _extensions = extensions
                .Concat(_extensions)
                .Distinct()
                .ToArray();
            return this;
        }
    }
}