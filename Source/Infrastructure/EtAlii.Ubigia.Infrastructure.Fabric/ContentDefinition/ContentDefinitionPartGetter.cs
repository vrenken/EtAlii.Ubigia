// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;

    internal class ContentDefinitionPartGetter : IContentDefinitionPartGetter
    {
        private readonly IStorage _storage;

        public ContentDefinitionPartGetter(IStorage storage)
        {
            _storage = storage;
        }

        public async Task<ContentDefinitionPart> Get(Identifier identifier, ulong contentDefinitionPartId)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            var contentDefinitionPart = await _storage.Blobs.Retrieve<ContentDefinitionPart>(containerId, contentDefinitionPartId).ConfigureAwait(false);
            return contentDefinitionPart;
        }
    }
}