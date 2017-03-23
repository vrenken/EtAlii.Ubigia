namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Fabric;

    public class LogicalContextConfiguration : ILogicalContextConfiguration
    {
        public IFabricContext Fabric { get; private set; }

        public ILogicalContextExtension[] Extensions { get; private set; }

        public bool CachingEnabled { get; private set; }


        public LogicalContextConfiguration()
        {
            CachingEnabled = true;
            Extensions = new ILogicalContextExtension[0];
        }

        public ILogicalContextConfiguration UseCaching(bool cachingEnabled)
        {
            CachingEnabled = cachingEnabled;
            return this;
        }

        public ILogicalContextConfiguration Use(IFabricContext fabric)
        {
            Fabric = fabric;
            return this;
        }

        public ILogicalContextConfiguration Use(ILogicalContextExtension[] extensions)
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