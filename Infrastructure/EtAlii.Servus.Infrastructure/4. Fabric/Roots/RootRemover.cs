namespace EtAlii.Servus.Infrastructure.Fabric
{
    using System;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;

    internal class RootRemover : IRootRemover
    {
        private readonly IStorage _storage;

        public RootRemover(IStorage storage)
        {
            _storage = storage;
        }

        public void Remove(Guid spaceId, Guid rootId)
        {
            var containerId = _storage.ContainerProvider.ForRoots(spaceId);

            var hasItem = _storage.Items.Has(rootId, containerId);
            if (!hasItem)
            {
                throw new InvalidOperationException("No item found");
            }

            _storage.Items.Remove(rootId, containerId);
        }

        public void Remove(Guid spaceId, Root root)
        {
            Remove(spaceId, root.Id);
        }
    }
}