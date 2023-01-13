// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using EtAlii.xTechnology.MicroContainer;

internal class ItemsScaffolding : IScaffolding
{
    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<IItemsSet, ItemsSet>();
        container.Register<IItemAdder, ItemAdder>();
        container.Register<IItemRemover, ItemRemover>();
        container.Register<IItemGetter, ItemGetter>();
        container.Register<IItemUpdater, ItemUpdater>();
    }
}
