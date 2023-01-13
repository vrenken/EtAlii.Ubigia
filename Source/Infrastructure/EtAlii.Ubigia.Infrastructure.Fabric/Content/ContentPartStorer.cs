// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric;

using System.Threading.Tasks;
using EtAlii.Ubigia.Persistence;
using HashLib;

internal class ContentPartStorer : IContentPartStorer
{
    private readonly IStorage _storage;
    private readonly IHash _hash;
    private readonly IContentDefinitionPartGetter _contentDefinitionPartGetter;

    public ContentPartStorer(
        IStorage storage,
        IHash hash,
        IContentDefinitionPartGetter contentDefinitionPartGetter)
    {
        _storage = storage;
        _hash = hash;
        _contentDefinitionPartGetter = contentDefinitionPartGetter;
    }

    public async Task Store(Identifier identifier, ContentPart contentPart)
    {
        if (identifier == Identifier.Empty)
        {
            throw new ContentFabricException("No identifier was specified");
        }

        if (contentPart == null)
        {
            throw new ContentFabricException("No contentPart was specified");
        }


        var contentDefinitionPart = await _contentDefinitionPartGetter.Get(identifier, contentPart.Id).ConfigureAwait(false);

        var hash = _hash.ComputeBytes(contentPart.Data);
        var checksum = hash.GetULong();

        if (contentDefinitionPart.Checksum != checksum)
        {
            throw new ContentFabricException("ContentPart has invalid checksum");
        }

        var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
        _storage.Blobs.Store(containerId, contentPart);
    }

}
