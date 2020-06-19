namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalContextConfiguration : ILogicalContextConfiguration
    {
        public IFabricContext Fabric { get; private set; }

        public string Name { get; private set; }

        public Uri DataApiAddress { get; private set; }

        public ILogicalContextConfiguration Use(string name, Uri dataApiAddress)
        {
			Name = name ?? throw new ArgumentNullException(nameof(name));
            DataApiAddress = dataApiAddress ?? throw new ArgumentNullException(nameof(dataApiAddress));

            return this;
        }

        public ILogicalContextConfiguration Use(IFabricContext fabric)
        {
			Fabric = fabric ?? throw new ArgumentException(nameof(fabric));

            return this;
        }
    }
}
