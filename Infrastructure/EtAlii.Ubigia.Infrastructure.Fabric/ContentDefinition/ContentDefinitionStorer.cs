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

        public void Store(Identifier identifier, ContentDefinition contentDefinition)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            _storage.Blobs.Store(containerId, contentDefinition);
        }

        public void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            _storage.Blobs.Store(containerId, contentDefinitionPart);
        }
    }
}