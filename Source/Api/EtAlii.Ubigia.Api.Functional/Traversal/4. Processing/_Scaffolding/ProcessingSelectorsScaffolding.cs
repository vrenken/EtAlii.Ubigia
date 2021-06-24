// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class ProcessingSelectorsScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IItemToIdentifierConverter, ItemToIdentifierConverter>();
            container.Register<IItemToPathSubjectConverter, ItemToPathSubjectConverter>();
            container.Register<IEntriesToDynamicNodesConverter, EntriesToDynamicNodesConverter>();
        }
    }
}
