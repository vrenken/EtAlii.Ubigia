namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;

    internal class ContentDefinitionPartGetter : IContentDefinitionPartGetter
    {
        private readonly IStorage _storage;

        public ContentDefinitionPartGetter(IStorage storage)
        {
            _storage = storage;
        }

        public IReadOnlyContentDefinitionPart Get(Identifier identifier, UInt64 contentDefinitionPartId)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            var contentDefinitionPart = _storage.Blobs.Retrieve<ContentDefinitionPart>(containerId, contentDefinitionPartId);
            return contentDefinitionPart;
        }
    }
}