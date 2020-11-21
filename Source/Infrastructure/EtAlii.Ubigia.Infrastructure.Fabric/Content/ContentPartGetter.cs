﻿namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;

    internal class ContentPartGetter : IContentPartGetter
    {
        private readonly IStorage _storage;

        public ContentPartGetter(IStorage storage)
        {
            _storage = storage;
        }

        public async Task<IReadOnlyContentPart> Get(Identifier identifier, ulong contentPartId)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            var contentPart = await _storage.Blobs.Retrieve<ContentPart>(containerId, contentPartId).ConfigureAwait(false);
            return contentPart;
        }
    }
}