// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using EtAlii.xTechnology.MicroContainer;

internal class ContentDefinitionScaffolding : IScaffolding
{
    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<IContentDefinitionSet, ContentDefinitionSet>();

        //container.Register<IContentDefinitionRepository, ContentDefinitionRepository>()
        container.Register<IContentDefinitionGetter, ContentDefinitionGetter>();
        container.Register<IContentDefinitionPartGetter, ContentDefinitionPartGetter>();
        container.Register<IContentDefinitionStorer, ContentDefinitionStorer>();
        container.Register<IContentDefinitionPartStorer, ContentDefinitionPartStorer>();
    }
}
