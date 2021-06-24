// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    public class TraversalContextRootSet : ITraversalContextRootSet
    {
        private readonly IFabricContext _fabricContext;

        private readonly IDictionary<string, Root> _cache;

        public TraversalContextRootSet(IFabricContext fabricContext)
        {
            _fabricContext = fabricContext;
            _cache = new Dictionary<string, Root>();
        }

        public async Task<Root> Get(string name)
        {
            if (!_cache.TryGetValue(name, out var result))
            {
                _cache[name] = result = await _fabricContext.Roots.Get(name).ConfigureAwait(false);
            }
            return result;
        }
    }
}
