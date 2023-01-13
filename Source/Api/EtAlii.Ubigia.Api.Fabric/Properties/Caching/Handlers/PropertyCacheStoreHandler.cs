// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using System.Threading.Tasks;

internal class PropertyCacheStoreHandler : IPropertyCacheStoreHandler
{
    private readonly IPropertyCacheProvider _cacheProvider;
    private readonly IPropertiesCacheContextProvider _contextProvider;

    public PropertyCacheStoreHandler(
        IPropertyCacheProvider cacheProvider,
        IPropertiesCacheContextProvider contextProvider)
    {
        _cacheProvider = cacheProvider;
        _contextProvider = contextProvider;
    }

    public Task Handle(Identifier identifier)
    {
        if (_cacheProvider.Cache.TryGetValue(identifier, out _))
        {
            // Yup, we got a cache hit.
            _cacheProvider.Cache.Remove(identifier);
        }

        return Task.CompletedTask;
    }

    public async Task Handle(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
    {
        await _contextProvider.Context.Store(identifier, properties, scope).ConfigureAwait(false);
    }
}
