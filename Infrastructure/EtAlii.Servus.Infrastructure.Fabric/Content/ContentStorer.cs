namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;

    internal class ContentStorer : IContentStorer
    {
        private readonly IStorage _storage;

        public ContentStorer(IStorage storage)
        {
            _storage = storage;
        }

        public void Store(Identifier identifier, Content content)
        {
            if (identifier == Identifier.Empty)
            {
                throw new ContentFabricException("No identifier was specified");
            }
            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            _storage.Blobs.Store(containerId, content);
        }
    }
}