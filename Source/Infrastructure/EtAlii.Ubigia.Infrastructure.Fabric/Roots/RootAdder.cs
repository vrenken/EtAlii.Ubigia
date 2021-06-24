// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;

    internal class RootAdder : IRootAdder
    {
        private readonly IStorage _storage;
        private readonly IRootGetter _rootGetter;

        public RootAdder(
            IStorage storage,
            IRootGetter rootGetter)
        {
            _storage = storage;
            _rootGetter = rootGetter;
        }


        public async Task<Root> Add(Guid spaceId, Root root)
        {
            var canAdd = await CanAdd(spaceId, root).ConfigureAwait(false);
            if (canAdd)
            {
                root.Id = root.Id != Guid.Empty ? root.Id : Guid.NewGuid();
                var containerId = _storage.ContainerProvider.ForRoots(spaceId);
                _storage.Items.Store(root, root.Id, containerId);
            }
            return canAdd ? root : null;
        }

        private Task<bool> CanAdd(Guid spaceId, Root item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "No item specified");
            }

            return CanAddInternal(spaceId, item);
        }

        private async Task<bool> CanAddInternal(Guid spaceId, Root item)
        {
            var canAdd = !string.IsNullOrWhiteSpace(item.Name);
            if (canAdd)
            {
                canAdd = item.Id == Guid.Empty;
            }
            //if [canAdd]
            //[
            //    var containerId = ContainerIdentifier.ForRoots(spaceId)
            //    canAdd = !ItemStorage.Has(item.Id, containerId)
            //]
            //if [canAdd]
            //[
            //    canAdd = Get(spaceId, item.Id) == null
            //]
            if (canAdd)
            {
                canAdd = await _rootGetter.Get(spaceId, item.Name).ConfigureAwait(false) == null;
            }

            return canAdd;
        }

    }
}
