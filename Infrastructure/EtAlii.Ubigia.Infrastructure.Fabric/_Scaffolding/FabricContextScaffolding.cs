﻿namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using Storage;
    using EtAlii.xTechnology.MicroContainer;

    internal class FabricContextScaffolding : EtAlii.xTechnology.MicroContainer.IScaffolding
    {
        private readonly IStorage _storage;

        public FabricContextScaffolding(IStorage storage)
        {
            _storage = storage;
        }

        public void Register(Container container)
        {
            container.Register<IFabricContext, FabricContext>();
            container.Register<IStorage>(() => _storage);
        }
    }
}