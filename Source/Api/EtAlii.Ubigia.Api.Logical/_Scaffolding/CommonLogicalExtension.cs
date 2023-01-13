// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using EtAlii.xTechnology.MicroContainer;

internal class CommonLogicalExtension : IExtension
{
    private readonly LogicalOptions _options;

    public CommonLogicalExtension(LogicalOptions options)
    {
        _options = options;
    }

    public void Initialize(IRegisterOnlyContainer container)
    {
        new ContextScaffolding(_options).Register(container);
        new GraphScaffolding().Register(container);
        new TraversalScaffolding().Register(container);
        new TraversalContextScaffolding().Register(container);
    }
}
