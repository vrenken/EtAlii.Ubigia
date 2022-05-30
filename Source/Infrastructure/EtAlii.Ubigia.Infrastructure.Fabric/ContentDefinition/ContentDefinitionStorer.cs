// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;

    internal class ContentDefinitionStorer : IContentDefinitionStorer
    {
        private readonly IStorage _storage;

        public ContentDefinitionStorer(IStorage storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public Task Store(in Identifier identifier, ContentDefinition contentDefinition)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            _storage.Blobs.Store(containerId, contentDefinition);

            return Task.CompletedTask;
        }

        public Task Store(in Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            _storage.Blobs.Store(containerId, contentDefinitionPart);

            return Task.CompletedTask;
        }
    }
}
