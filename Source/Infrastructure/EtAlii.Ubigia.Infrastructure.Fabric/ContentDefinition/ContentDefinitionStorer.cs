// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Persistence;

    internal class ContentDefinitionStorer : IContentDefinitionStorer
    {
        private readonly IStorage _storage;

        public ContentDefinitionStorer(IStorage storage)
        {
            _storage = storage;
        }

        public void Store(in Identifier identifier, ContentDefinition contentDefinition)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            _storage.Blobs.Store(containerId, contentDefinition);
        }

        public void Store(in Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            _storage.Blobs.Store(containerId, contentDefinitionPart);
        }
    }
}