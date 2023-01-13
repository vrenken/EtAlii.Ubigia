// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using EtAlii.xTechnology.MicroContainer;

internal class GraphScaffolding : IScaffolding
{
    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<IGraphPathBuilder, GraphPathBuilder>();
        container.Register<IGraphComposerFactory, GraphComposerFactory>();
        container.Register<IGraphAssignerFactory, GraphAssignerFactory>();
        container.Register<IGraphPathTraverser, GraphPathTraverser>();
    }
}
