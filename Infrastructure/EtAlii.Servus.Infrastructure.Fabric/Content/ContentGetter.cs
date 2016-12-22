namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;

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