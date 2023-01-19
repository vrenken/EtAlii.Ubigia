// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using EtAlii.xTechnology.MicroContainer;

internal class FunctionalContextExtension : IExtension
{
    private readonly FunctionalContextOptions _options;

    public FunctionalContextExtension(FunctionalContextOptions options)
    {
        _options = options;
    }

    public void Initialize(IRegisterOnlyContainer container)
    {
        new FunctionalContextScaffolding(_options).Register(container);
    }
}
