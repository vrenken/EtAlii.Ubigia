// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System.Threading.Tasks;
using EtAlii.Ubigia.Persistence;

internal class ContentDefinitionGetter : IContentDefinitionGetter
{
    private readonly IStorage _storage;

    public ContentDefinitionGetter(IStorage storage)
    {
        _storage = storage;
    }

    /// <inheritdoc />
    public async Task<ContentDefinition> Get(Identifier identifier)
    {
        var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
        var contentDefinition = await _storage.Blobs.Retrieve<ContentDefinition>(containerId).ConfigureAwait(false);
        return contentDefinition;
    }
}
