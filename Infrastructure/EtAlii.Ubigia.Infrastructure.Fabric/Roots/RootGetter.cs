namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Ubigia.Persistence;

    internal class RootGetter : IRootGetter
    {
        private readonly IStorage _storage;

        public RootGetter(IStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<Root> GetAll(Guid spaceId)
        {
            var items = new List<Root>();

            var containerId = _storage.ContainerProvider.ForRoots(spaceId);

            var itemIds = _storage.Items.Get(containerId);
            foreach (var itemId in itemIds)
            {
                var item = _storage.Items.Retrieve<Root>(itemId, containerId);
                items.Add(item);
            }
            return items;
        }

        public Root Get(Guid spaceId, Guid rootId)
        {
            var containerId = _storage.ContainerProvider.ForRoots(spaceId);
            return _storage.Items.Retrieve<Root>(rootId, containerId);
        }

        public Root Get(Guid spaceId, string name)
        {
            var roots = GetAll(spaceId);
            var root = roots.SingleOrDefault(r => r.Name == name);
            return root;
        }
    }
}