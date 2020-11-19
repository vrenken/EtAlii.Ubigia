namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;

    internal class ContentGetter : IContentGetter
    {
        private readonly IStorage _storage;

        public ContentGetter(IStorage storage)
        {
            _storage = storage;
        }

        public async Task<IReadOnlyContent> Get(Identifier identifier)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            var content = await _storage.Blobs.Retrieve<Content>(containerId);
            return content;
        }
    }
}