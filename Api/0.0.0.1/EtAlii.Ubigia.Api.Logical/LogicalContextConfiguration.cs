namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Fabric;

    public class LogicalContextConfiguration : ILogicalContextConfiguration
    {
        public IFabricContext Fabric => _fabric;
        private IFabricContext _fabric;

        public ILogicalContextExtension[] Extensions => _extensions;
        private ILogicalContextExtension[] _extensions;

        public bool CachingEnabled => _cachingEnabled;
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