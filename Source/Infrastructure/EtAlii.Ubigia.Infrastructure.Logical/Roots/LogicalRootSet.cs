// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Fabric;

    public class LogicalRootSet : ILogicalRootSet
    {
        private readonly IFabricContext _fabricContext;
        private readonly IRootInitializer _rootInitializer;

        public LogicalRootSet(IFabricContext fabricContext, IRootInitializer rootInitializer)
        {
            _fabricContext = fabricContext;
            _rootInitializer = rootInitializer;
        }

        public async Task<Root> Add(Guid spaceId, Root root)
        {
            root = await _fabricContext.Roots.Add(spaceId, root).ConfigureAwait(false);
            var isAdded = root != null;
            if (isAdded)
            {
                await _rootInitializer.Initialize(spaceId, root).ConfigureAwait(false);
            }
            return root;
        }

        public IAsyncEnumerable<Root> GetAll(Guid spaceId)
        {
            return _fabricContext.Roots.GetAll(spaceId);
        }

        public Task<Root> Get(Guid spaceId, Guid rootId)
        {
            return _fabricContext.Roots.Get(spaceId, rootId);
        }

        public Task<Root> Get(Guid spaceId, string name)
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

        public Task<Root> Update(Guid spaceId, Guid rootId, Root updatedRoot)
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