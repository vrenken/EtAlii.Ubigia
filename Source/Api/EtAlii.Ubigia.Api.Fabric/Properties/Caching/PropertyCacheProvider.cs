// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using System.Collections.Concurrent;
using System.Collections.Generic;

internal class PropertyCacheProvider : IPropertyCacheProvider
{
    public IDictionary<Identifier, PropertyDictionary> Cache { get; }

    public PropertyCacheProvider()
    {
        Cache = new ConcurrentDictionary<Identifier, PropertyDictionary>();
    }
}
