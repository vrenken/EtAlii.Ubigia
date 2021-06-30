// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory
{
    using EtAlii.xTechnology.MicroContainer;

    public class InMemoryStorageExtension : IStorageExtension
    {
        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new InMemoryFactoryScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}
