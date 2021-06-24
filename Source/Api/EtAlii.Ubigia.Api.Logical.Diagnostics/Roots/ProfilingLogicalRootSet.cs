// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProfilingLogicalRootSet : ILogicalRootSet
    {
        private readonly ILogicalRootSet _decoree;

        public event Action<Guid> Added;
        public event Action<Guid> Changed;
        public event Action<Guid> Removed;

        public ProfilingLogicalRootSet(ILogicalRootSet decoree)
        {
            _decoree = decoree;
            _decoree.Added += id => Added?.Invoke(id);
            _decoree.Removed += id => Removed?.Invoke(id);
            _decoree.Changed += id => Changed?.Invoke(id);
        }

        public async Task<Root> Add(string name)
        {
            return await _decoree.Add(name).ConfigureAwait(false);
        }

        public async Task Remove(Guid id)
        {
            await _decoree.Remove(id).ConfigureAwait(false);
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            return await _decoree.Change(rootId, rootName).ConfigureAwait(false);
        }

        public async Task<Root> Get(string rootName)
        {
            return await _decoree.Get(rootName).ConfigureAwait(false);
        }

        public async Task<Root> Get(Guid rootId)
        {
            return await _decoree.Get(rootId).ConfigureAwait(false);
        }

        public IAsyncEnumerable<Root> GetAll()
        {
            return _decoree.GetAll();
        }
    }
}