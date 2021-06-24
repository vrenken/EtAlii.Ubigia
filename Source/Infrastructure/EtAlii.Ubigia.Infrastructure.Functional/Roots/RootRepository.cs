// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Logical;

    internal class RootRepository : IRootRepository
    {
        private readonly ILogicalContext _logicalContext;

        public RootRepository(ILogicalContext logicalContext)
        {
            _logicalContext = logicalContext;
        }

        public IAsyncEnumerable<Root> GetAll(Guid spaceId)
        {
            return _logicalContext.Roots.GetAll(spaceId);
        }

        public Task<Root> Get(Guid spaceId, Guid rootId)
        {
            return _logicalContext.Roots.Get(spaceId, rootId);
        }

        public Task<Root> Get(Guid spaceId, string name)
        {
            return _logicalContext.Roots.Get(spaceId, name);
        }

        public Task<Root> Add(Guid spaceId, Root root)
        {
            return _logicalContext.Roots.Add(spaceId, root);
        }

        public void Remove(Guid spaceId, Guid rootId)
        {
            _logicalContext.Roots.Remove(spaceId, rootId);
        }

        public void Remove(Guid spaceId, Root root)
        {
            _logicalContext.Roots.Remove(spaceId, root);
        }

        public Task<Root> Update(Guid spaceId, Guid rootId, Root root)
        {
            return _logicalContext.Roots.Update(spaceId, rootId, root);
        }
    }
}