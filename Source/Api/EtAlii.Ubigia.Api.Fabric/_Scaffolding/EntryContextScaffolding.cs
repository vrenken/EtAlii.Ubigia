// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using EtAlii.xTechnology.MicroContainer;

internal class EntryContextScaffolding : IScaffolding
{
    private readonly bool _enableCaching;

    public EntryContextScaffolding(bool enableCaching)
    {
        _enableCaching = enableCaching;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<IEntryContext, EntryContext>();

        if (_enableCaching)
        {
            // Caching enabled.
            container.RegisterDecorator<IEntryContext, CachingEntryContext>();
        }
    }
}
