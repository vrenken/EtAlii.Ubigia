namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
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


        public Root Add(Guid spaceId, Root root)
        {
            var canAdd = CanAdd(spaceId, root);
            if (canAdd)
            {
                root.Id = root.Id != Guid.Empty ? root.Id : Guid.NewGuid();
                var containerId = _storage.ContainerProvider.ForRoots(spaceId);
                _storage.Items.Store(root, root.Id, containerId);
            }
            return canAdd ? root : null;
        }

        protected bool CanAdd(Guid spaceId, Root item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "No item specified");
            }

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
                canAdd = _rootGetter.Get(spaceId, item.Name) == null;
            }

            return canAdd;
        }

    }
}