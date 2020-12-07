namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Persistence;

    public class FabricContextConfiguration : ConfigurationBase
    {
        public IStorage Storage { get; private set; }

        public FabricContextConfiguration Use(IStorage storage)
        {
            Storage = storage ?? throw new ArgumentException("No storage specified", nameof(storage));

            return this;
        }
    }
}
