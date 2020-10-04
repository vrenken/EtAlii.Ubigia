namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Persistence;

    public class FabricContextConfiguration : IFabricContextConfiguration
    {
        public IStorage Storage { get; private set; }

        public IFabricContextConfiguration Use(IStorage storage)
        {
            Storage = storage ?? throw new ArgumentException(nameof(storage));

            return this;
        }
    }
}
