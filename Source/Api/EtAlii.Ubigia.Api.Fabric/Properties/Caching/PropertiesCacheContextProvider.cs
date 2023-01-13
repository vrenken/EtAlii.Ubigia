// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

internal class PropertiesCacheContextProvider : IPropertiesCacheContextProvider
{
    public IPropertiesContext Context { get; }

    public PropertiesCacheContextProvider(IPropertiesContext context)
    {
        Context = context;
    }
}
