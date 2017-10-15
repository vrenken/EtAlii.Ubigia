namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;

    internal class ContentDefinitionPartStorer : IContentDefinitionPartStorer
    {
        private readonly IStorage _storage;

        public ContentDefinitionPartStorer(IStorage storage)
        {
            _storage = storage;
        }

        public void Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            _storage.Blobs.Store(containerId, contentDefinitionPart);
        }
    }
}