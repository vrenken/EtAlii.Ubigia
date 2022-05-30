// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
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

        /// <inheritdoc />
        public async IAsyncEnumerable<Root> GetAll(Guid spaceId)
        {
            var containerId = _storage.ContainerProvider.ForRoots(spaceId);

            var itemIds = _storage.Items.Get(containerId);
            foreach (var itemId in itemIds)
            {
                var item = await _storage.Items.Retrieve<Root>(itemId, containerId).ConfigureAwait(false);
                yield return item;
            }
        }

        /// <inheritdoc />
        public Task<Root> Get(Guid spaceId, Guid rootId)
        {
            var containerId = _storage.ContainerProvider.ForRoots(spaceId);
            return _storage.Items.Retrieve<Root>(rootId, containerId);
        }

        /// <inheritdoc />
        public async Task<Root> Get(Guid spaceId, string name)
        {
            var roots = GetAll(spaceId);
            var root = await roots
                .SingleOrDefaultAsync(r => r.Name == name)
                .ConfigureAwait(false);
            return root;
        }
    }
}
