﻿namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;

    internal class ContentPartGetter : IContentPartGetter
    {
        private readonly IStorage _storage;

        public ContentPartGetter(IStorage storage)
        {
            _storage = storage;
        }

        public IReadOnlyContentPart Get(Identifier identifier, UInt64 contentPartId)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            var contentPart = _storage.Blobs.Retrieve<ContentPart>(containerId, contentPartId);
            return contentPart;
        }
    }
}