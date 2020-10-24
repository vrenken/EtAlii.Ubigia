namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using System.Collections.Generic;

    public class RootSet : IRootSet
    {
        private readonly IRootAdder _rootAdder;
        private readonly IRootGetter _rootGetter;
        private readonly IRootRemover _rootRemover;
        private readonly IRootUpdater _rootUpdater;

        public RootSet(
            IRootAdder rootAdder, 
            IRootGetter rootGetter, 
            IRootRemover rootRemover, 
            IRootUpdater rootUpdater)
        {
            _rootAdder = rootAdder;
            _rootGetter = rootGetter;
            _rootRemover = rootRemover;
            _rootUpdater = rootUpdater;
        }

        public Root Add(Guid spaceId, Root root)
        {
            return _rootAdder.Add(spaceId, root);
        }

        public IEnumerable<Root> GetAll(Guid spaceId)
        {
            return _rootGetter.GetAll(spaceId);
        }

        public Root Get(Guid spaceId, Guid rootId)
        {
            return _rootGetter.Get(spaceId, rootId);
        }

        public Root Get(Guid spaceId, string name)
        {
            return _rootGetter.Get(spaceId, name);
        }

        public void Remove(Guid spaceId, Guid rootId)
        {
            _rootRemover.Remove(spaceId, rootId);
        }

        public void Remove(Guid spaceId, Root root)
        {
            _rootRemover.Remove(spaceId, root);
        }

        public Root Update(Guid spaceId, Guid rootId, Root updatedRoot)
        {
            return _rootUpdater.Update(spaceId, rootId, updatedRoot);
        }
    }
}