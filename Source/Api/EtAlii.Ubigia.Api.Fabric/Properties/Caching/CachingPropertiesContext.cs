// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using System.Threading.Tasks;

public class CachingPropertiesContext : IPropertiesContext
{
    private readonly IPropertyCacheRetrieveHandler _retrieveHandler;
    private readonly IPropertyCacheStoreHandler _storeHandler;

    public CachingPropertiesContext(
        IPropertyCacheRetrieveHandler retrieveHandler,
        IPropertyCacheStoreHandler storeHandler)
    {
        _retrieveHandler = retrieveHandler;
        _storeHandler = storeHandler;
    }

    public async Task Store(Identifier identifier, PropertyDictionary properties, ExecutionScope scope)
    {
        await _storeHandler
            .Handle(identifier, properties, scope)
            .ConfigureAwait(false);
    }

    public async Task<PropertyDictionary> Retrieve(Identifier identifier, ExecutionScope scope)
    {
        return await _retrieveHandler
            .Handle(identifier, scope)
            .ConfigureAwait(false);
    }
}
