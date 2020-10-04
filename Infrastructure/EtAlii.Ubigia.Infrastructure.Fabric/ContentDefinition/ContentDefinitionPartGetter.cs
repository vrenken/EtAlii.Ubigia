namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Persistence;

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