namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Storage;

    internal class RootUpdater : IRootUpdater
    {
        private readonly IStorage _storage;
        private readonly IRootGetter _rootGetter;

        public RootUpdater(IStorage storage, IRootGetter rootGetter)
        {
            _storage = storage;
            _rootGetter = rootGetter;
        }

        public Root Update(Guid spaceId, Guid rootId, Root updatedRoot)
        {
            var rootToUpdate = _rootGetter.Get(spaceId, rootId);

            if (rootToUpdate.Name != updatedRoot.Name || rootToUpdate.Identifier != updatedRoot.Identifier)
            {
                rootToUpdate.Name = updatedRoot.Name;
                rootToUpdate.Identifier = updatedRoot.Identifier == Identifier.Empty ? rootToUpdate.Identifier : updatedRoot.Identifier;

                var containerId = _storage.ContainerProvider.ForRoots(spaceId);
                _storage.Items.Store(updatedRoot, rootId, containerId);
            }

            return updatedRoot;
        }
    }
}