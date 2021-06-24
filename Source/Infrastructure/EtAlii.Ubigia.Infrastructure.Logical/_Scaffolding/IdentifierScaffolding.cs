// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using EtAlii.xTechnology.MicroContainer;

    internal class IdentifierScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<ILogicalIdentifierSet, LogicalIdentifierSet>();

            container.Register<IIdentifierTailGetter, IdentifierTailGetter>();
            container.Register<IIdentifierHeadGetter, IdentifierHeadGetter>();
            container.Register<INextIdentifierGetter, NextIdentifierGetter>();
            container.Register<IIdentifierRootUpdater, IdentifierRootUpdater>();
        }
    }
}