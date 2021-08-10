// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    internal class EntryScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IEntrySet, EntrySet>();

            container.Register<IEntryUpdater, EntryUpdater>();
            container.Register<IEntryGetter, EntryGetter>();
            container.Register<IEntryStorer, EntryStorer>();
        }
    }
}
