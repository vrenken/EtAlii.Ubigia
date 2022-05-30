// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;

    internal class RootRemover : IRootRemover
    {
        private readonly IStorage _storage;

        public RootRemover(IStorage storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public Task Remove(Guid spaceId, Guid rootId)
        {
            var containerId = _storage.ContainerProvider.ForRoots(spaceId);

            var hasItem = _storage.Items.Has(rootId, containerId);
            if (!hasItem)
            {
                throw new InvalidOperationException("No item found");
            }

            _storage.Items.Remove(rootId, containerId);

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task Remove(Guid spaceId, Root root)
        {
            return Remove(spaceId, root.Id);
        }
    }
}
