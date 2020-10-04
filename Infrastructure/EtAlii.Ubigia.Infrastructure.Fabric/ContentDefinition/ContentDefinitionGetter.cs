namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Persistence;

    internal class ContentDefinitionGetter : IContentDefinitionGetter
    {
        private readonly IStorage _storage;

        public ContentDefinitionGetter(IStorage storage)
        {
            _storage = storage;
        }

        public IReadOnlyContentDefinition Get(Identifier identifier)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            var contentDefinition = _storage.Blobs.Retrieve<ContentDefinition>(containerId);
            return contentDefinition;
        }
    }
}