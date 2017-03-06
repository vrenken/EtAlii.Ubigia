namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

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
            return await _decoree.Add(name);
        }

        public async Task Remove(Guid id)
        {
            await _decoree.Remove(id);
        }

        public async Task<Root> Change(Guid rootId, string rootName)
        {
            return await _decoree.Change(rootId, rootName);
        }

        public async Task<Root> Get(string rootName)
        {
            return await _decoree.Get(rootName);
        }

        public async Task<Root> Get(Guid rootId)
        {
            return await _decoree.Get(rootId);
        }

        public async Task<IEnumerable<Root>> GetAll()
        {
            return await _decoree.GetAll();
        }
    }
}