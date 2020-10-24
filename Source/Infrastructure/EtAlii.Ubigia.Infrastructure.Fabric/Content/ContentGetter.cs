namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Persistence;

    internal class ContentGetter : IContentGetter
    {
        private readonly IStorage _storage;

        public ContentGetter(IStorage storage)
        {
            _storage = storage;
        }

        public IReadOnlyContent Get(Identifier identifier)
        {
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            var content = _storage.Blobs.Retrieve<Content>(containerId);
            return content;
        }
    }
}