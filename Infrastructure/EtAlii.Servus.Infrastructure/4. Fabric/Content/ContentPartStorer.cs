namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;
    using HashLib;

    internal class ContentPartStorer : IContentPartStorer
    {
        private readonly IStorage _storage;
        private readonly IHash _hash;
        private readonly IContentDefinitionPartGetter _contentDefinitionPartGetter;

        public ContentPartStorer(
            IStorage storage,
            IHash hash,
            IContentDefinitionPartGetter contentDefinitionPartGetter)
        {
            _storage = storage;
            _hash = hash;
            _contentDefinitionPartGetter = contentDefinitionPartGetter;
        }

        public void Store(Identifier identifier, ContentPart contentPart)
        {
            if (identifier == Identifier.Empty)
            {
                throw new ContentFabricException("No identifier was specified");
            }

            if (contentPart == null)
            {
                throw new ContentFabricException("No contentPart was specified");
            }


            var contentDefinitionPart = _contentDefinitionPartGetter.Get(identifier, contentPart.Id);

            var hash = _hash.ComputeBytes(contentPart.Data);
            var checksum = hash.GetULong();

            if (contentDefinitionPart.Checksum != checksum)
            {
                throw new ContentFabricException("ContentPart has invalid checksum");
            }

            var containerId = _storage.ContainerProvider.FromIdentifier(identifier);
            _storage.Blobs.Store(containerId, contentPart);
        }

    }
}