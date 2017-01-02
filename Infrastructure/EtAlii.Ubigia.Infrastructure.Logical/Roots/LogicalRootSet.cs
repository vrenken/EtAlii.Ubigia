namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalRootSet : ILogicalRootSet
    {
        private readonly IFabricContext _fabricContext;

        public event EventHandler<RootAddedEventArgs> Added { add { _added += value; } remove { var added = _added; if (added != null) added -= value; } }
        private EventHandler<RootAddedEventArgs> _added;

        public LogicalRootSet(IFabricContext fabricContext)
        {
            _fabricContext = fabricContext;
        }

        public Root Add(Guid spaceId, Root root)
        {
            root = _fabricContext.Roots.Add(spaceId, root);
            if (root != null)
            {
                _added?.Invoke(this, new RootAddedEventArgs(root, spaceId));
            }
            return root;
        }

        public IEnumerable<Root> GetAll(Guid spaceId)
        {
            return _fabricContext.Roots.GetAll(spaceId);
        }

        public Root Get(Guid spaceId, Guid rootId)
        {
            return _fabricContext.Roots.Get(spaceId, rootId);
        }

        public Root Get(Guid spaceId, string name)
        {
            return _fabricContext.Roots.Get(spaceId, name);
        }

        public void Remove(Guid spaceId, Guid rootId)
        {
            _fabricContext.Roots.Remove(spaceId, rootId);
        }

        public void Remove(Guid spaceId, Root root)
        {
            _fabricContext.Roots.Remove(spaceId, root);
        }

        public Root Update(Guid spaceId, Guid rootId, Root updatedRoot)
        {
            return _fabricContext.Roots.Update(spaceId, rootId, updatedRoot);
        }

        public void Start()
        {
            // TODO: In case of emergencies somehow restore the root information from somewhere.
        }

        public void Stop()
        {
            // TODO: Somehow backup the root information somewhere.
        }
    }
}