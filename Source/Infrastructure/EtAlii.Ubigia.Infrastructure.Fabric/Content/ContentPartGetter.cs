﻿namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Persistence;

    internal class ContentPartGetter : IContentPartGetter
    {
        private readonly IStorage _storage;

        public ContentPartGetter(IStorage storage)
        {
            _storage = storage;
        }

        public IReadOnlyContentPart Get(Identifier identifier, ulong contentPartId)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            var contentPart = _storage.Blobs.Retrieve<ContentPart>(containerId, contentPartId);
            return contentPart;
        }
    }
}