// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    /// <summary>
    /// Facade that hides away complex logical Root operations.
    /// </summary>
    public class LogicalRootSet : ILogicalRootSet
    {
        private readonly IFabricContext _fabric;

        public LogicalRootSet(IFabricContext fabric)
        {
            _fabric = fabric;
        }

        public Task<Root> Add(string name, RootType rootType) => _fabric.Roots.Add(name, rootType);

        public Task Remove(Guid id) => _fabric.Roots.Remove(id);

        public Task<Root> Change(Guid rootId, string rootName) => _fabric.Roots.Change(rootId, rootName);

        public Task<Root> Get(string rootName) => _fabric.Roots.Get(rootName);

        public Task<Root> Get(Guid rootId) => _fabric.Roots.Get(rootId);

        public IAsyncEnumerable<Root> GetAll() => _fabric.Roots.GetAll();
    }
}
