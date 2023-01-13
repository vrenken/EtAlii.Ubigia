// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using EtAlii.xTechnology.MicroContainer;

internal sealed class TraversalContextScaffolding : IScaffolding
{
    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<ITraversalContextEntrySet, TraversalContextEntrySet>();
        container.Register<ITraversalContextPropertySet, TraversalContextPropertySet>();
        container.Register<ITraversalContextRootSet, TraversalContextRootSet>();
    }
}
