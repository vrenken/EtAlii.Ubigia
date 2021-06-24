// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Persistence;
    using EtAlii.xTechnology.MicroContainer;

    internal class FabricContextScaffolding : IScaffolding
    {
        private readonly IStorage _storage;

        public FabricContextScaffolding(IStorage storage)
        {
            _storage = storage;
        }

        public void Register(Container container)
        {
            container.Register<IFabricContext, FabricContext>();
            container.Register(() => _storage);
        }
    }
}