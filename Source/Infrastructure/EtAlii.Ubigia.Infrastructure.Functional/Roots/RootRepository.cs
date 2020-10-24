﻿namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System;
    using System.Collections.Generic;
    using EtAlii.Ubigia.Infrastructure.Logical;

    internal class RootRepository : IRootRepository
    {
        private readonly ILogicalContext _logicalContext;

        public RootRepository(ILogicalContext logicalContext)
        {
            _logicalContext = logicalContext;
        }

        public IEnumerable<Root> GetAll(Guid spaceId)
        {
            return _logicalContext.Roots.GetAll(spaceId);
        }

        public Root Get(Guid spaceId, Guid rootId)
        {
            return _logicalContext.Roots.Get(spaceId, rootId);
        }

        public Root Get(Guid spaceId, string name)
        {
            return _logicalContext.Roots.Get(spaceId, name);
        }

        public Root Add(Guid spaceId, Root root)
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

        public Root Update(Guid spaceId, Guid rootId, Root root)
        {
            return _logicalContext.Roots.Update(spaceId, rootId, root);
        }
    }
}