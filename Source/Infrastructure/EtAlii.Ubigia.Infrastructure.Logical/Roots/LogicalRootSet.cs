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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IAsyncEnumerable<Root> GetAll(Guid spaceId)
        {
            return _fabricContext.Roots.GetAll(spaceId);
        }

        /// <inheritdoc />
        public Task<Root> Get(Guid spaceId, Guid rootId)
        {
            return _fabricContext.Roots.Get(spaceId, rootId);
        }

        /// <inheritdoc />
        public Task<Root> Get(Guid spaceId, string name)
        {
            return _fabricContext.Roots.Get(spaceId, name);
        }

        /// <inheritdoc />
        public Task Remove(Guid spaceId, Guid rootId)
        {
            return _fabricContext.Roots.Remove(spaceId, rootId);
        }

        /// <inheritdoc />
        public Task Remove(Guid spaceId, Root root)
        {
            return _fabricContext.Roots.Remove(spaceId, root);
        }

        /// <inheritdoc />
        public Task<Root> Update(Guid spaceId, Guid rootId, Root updatedRoot)
        {
            return _fabricContext.Roots.Update(spaceId, rootId, updatedRoot);
        }

        /// <inheritdoc />
        public void Start()
        {
            // In case of emergencies somehow restore the root information from somewhere.
            // More information can be found in the GitHub issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/89
        }

        /// <inheritdoc />
        public void Stop()
        {
            // Somehow backup the root information somewhere.
            // More information can be found in the GitHub issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/89
        }
    }
}
