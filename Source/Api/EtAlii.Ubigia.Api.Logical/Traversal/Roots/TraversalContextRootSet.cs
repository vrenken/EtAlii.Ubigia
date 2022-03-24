// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    public sealed class TraversalContextRootSet : ITraversalContextRootSet
    {
        private readonly IFabricContext _context;
        private readonly bool _cachingEnabled;

        public TraversalContextRootSet(IFabricContext context)
        {
            _context = context;
            _cachingEnabled = _context.Options.CachingEnabled;
        }

        public async Task<Root> Get(string name, ExecutionScope scope)
        {
            Root result;
            if (_cachingEnabled)
            {
                if (!scope.RootCache.TryGetValue(name, out result))
                {
                    scope.RootCache[name] = result = await _context.Roots.Get(name).ConfigureAwait(false);
                }
            }
            else
            {
                result = await _context.Roots
                    .Get(name)
                    .ConfigureAwait(false);
            }
            return result;
        }
    }
}
