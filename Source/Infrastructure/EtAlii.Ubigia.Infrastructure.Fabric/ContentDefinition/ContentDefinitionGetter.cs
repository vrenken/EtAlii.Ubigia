namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;

    internal class ContentDefinitionGetter : IContentDefinitionGetter
    {
        private readonly IStorage _storage;

        public ContentDefinitionGetter(IStorage storage)
        {
            _storage = storage;
        }

        public async Task<IReadOnlyContentDefinition> Get(Identifier identifier)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            var contentDefinition = await _storage.Blobs.Retrieve<ContentDefinition>(containerId).ConfigureAwait(false);
            return contentDefinition;
        }
    }
}