﻿namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;

    internal class RootGetter : IRootGetter
    {
        private readonly IStorage _storage;

        public RootGetter(IStorage storage)
        {
            _storage = storage;
        }

        public async IAsyncEnumerable<Root> GetAll(Guid spaceId)
        {
            var containerId = _storage.ContainerProvider.ForRoots(spaceId);

            var itemIds = _storage.Items.Get(containerId);
            foreach (var itemId in itemIds)
            {
                var item = await _storage.Items.Retrieve<Root>(itemId, containerId);
                yield return item;
            }
        }

        public Task<Root> Get(Guid spaceId, Guid rootId)
        {
            var containerId = _storage.ContainerProvider.ForRoots(spaceId);
            return _storage.Items.Retrieve<Root>(rootId, containerId);
        }

        public async Task<Root> Get(Guid spaceId, string name)
        {
            var roots = GetAll(spaceId);
            var root = await roots.SingleOrDefaultAsync(r => r.Name == name);
            return root;
        }
    }
}