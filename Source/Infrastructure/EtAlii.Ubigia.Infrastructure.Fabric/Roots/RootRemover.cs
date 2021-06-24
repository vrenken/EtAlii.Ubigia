// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.Ubigia.Persistence;

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