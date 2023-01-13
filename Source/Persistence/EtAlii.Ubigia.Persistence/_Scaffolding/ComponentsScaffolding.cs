// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using EtAlii.xTechnology.MicroContainer;

public class ComponentsScaffolding : IScaffolding
{
    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<IComponentStorage, ComponentStorage>();
        container.Register<IComponentStorer, ComponentStorer>();
        container.Register<ICompositeComponentStorer, CompositeComponentStorer>();
        container.Register<IComponentRetriever, ComponentRetriever>();

        container.Register<INextCompositeComponentIdAlgorithm, NextCompositeComponentIdAlgorithm>();
    }
}
