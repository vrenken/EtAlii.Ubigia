// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using EtAlii.Ubigia.Persistence;

internal class PropertiesGetter : IPropertiesGetter
{
    private readonly IStorage _storage;

    public PropertiesGetter(IStorage storage)
    {
        _storage = storage;
    }

    public PropertyDictionary Get(in Identifier identifier)
    {
        var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
        var properties = _storage.Properties.Retrieve(containerId);
        return properties;
    }
}
