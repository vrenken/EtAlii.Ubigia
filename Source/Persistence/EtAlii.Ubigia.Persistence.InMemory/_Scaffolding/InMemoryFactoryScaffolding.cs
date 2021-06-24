// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory
{
    using EtAlii.xTechnology.MicroContainer;

    public class InMemoryFactoryScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IInMemoryItems, InMemoryItems>();
            container.Register<IInMemoryItemsHelper, InMemoryItemsHelper>();
        }
    }
}
