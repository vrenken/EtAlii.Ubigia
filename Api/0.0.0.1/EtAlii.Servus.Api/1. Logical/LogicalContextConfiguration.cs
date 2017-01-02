namespace EtAlii.Servus.Api.Logical
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Fabric;

    public class LogicalContextConfiguration : ILogicalContextConfiguration
    {
        public IFabricContext Fabric { get { return _fabric; } }
        private IFabricContext _fabric;

        public ILogicalContextExtension[] Extensions { get { return _extensions; } }
        private ILogicalContextExtension[] _extensions;

        public bool CachingEnabled { get { return _cachingEnabled; } }
        private bool _cachingEnabled;


        public LogicalContextConfiguration()
        {
            _cachingEnabled = true;
            _extensions = new ILogicalContextExtension[0];
        }

        public ILogicalContextConfiguration UseCaching(bool cachingEnabled)
        {
            _cachingEnabled = cachingEnabled;
            return this;
        }

        public ILogicalContextConfiguration Use(IFabricContext fabric)
        {
            _fabric = fabric;
            return this;
        }

        public ILogicalContextConfiguration Use(ILogicalContextExtension[] extensions)
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