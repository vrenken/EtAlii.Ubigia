namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalContextConfiguration : ILogicalContextConfiguration
    {
        public IFabricContext Fabric { get; private set; }

        public string Name { get; private set; }

        public Uri Address { get; private set; }

        public ILogicalContextConfiguration Use(string name, Uri address)
        {
			Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));

            return this;
        }

        public ILogicalContextConfiguration Use(IFabricContext fabric)
        {
			Fabric = fabric ?? throw new ArgumentException(nameof(fabric));

            return this;
        }
    }
}
