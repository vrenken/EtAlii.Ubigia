// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    internal class TraversalContextPropertySet : ITraversalContextPropertySet
    {
        private readonly IFabricContext _context;

        private readonly IDictionary<Identifier, PropertyDictionary> _cache; 

        public TraversalContextPropertySet(IFabricContext context)
        {
            _context = context;
            _cache = new Dictionary<Identifier, PropertyDictionary>();
        }

        public async Task<PropertyDictionary> Retrieve(Identifier entryIdentifier, ExecutionScope scope)
        {
            PropertyDictionary result;
            if (!_cache.TryGetValue(entryIdentifier, out result))
            {
                _cache[entryIdentifier] = result = await _context.Properties.Retrieve(entryIdentifier, scope).ConfigureAwait(false);
            }
            return result;
        }
    }
}