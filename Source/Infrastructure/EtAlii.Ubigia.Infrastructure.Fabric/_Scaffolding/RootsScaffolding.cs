// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    internal class RootsScaffolding : IScaffolding
    {
        public void Register(IRegisterOnlyContainer container)
        {
            container.Register<IRootSet, RootSet>();

            container.Register<IRootGetter, RootGetter>();
            container.Register<IRootAdder, RootAdder>();
            container.Register<IRootRemover, RootRemover>();
            container.Register<IRootUpdater, RootUpdater>();
        }
    }
}
