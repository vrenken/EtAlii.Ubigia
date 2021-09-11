// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    internal class TraversalContextPropertySet : ITraversalContextPropertySet
    {
        private readonly IFabricContext _context;

        private readonly bool _cachingEnabled;

        public TraversalContextPropertySet(IFabricContext context)
        {
            _context = context;
            //_cachingEnabled = _context.Options.CachingEnabled;
            _cachingEnabled = false;// TODO: CF42 Caching does not work yet.
        }

        public async Task<PropertyDictionary> Retrieve(Identifier entryIdentifier, ExecutionScope scope)
        {
            PropertyDictionary result;
            if (_cachingEnabled)
            {
                if (!scope.PropertyCache.TryGetValue(entryIdentifier, out result))
                {
                    scope.PropertyCache[entryIdentifier] = result = await _context.Properties
                        .Retrieve(entryIdentifier, scope)
                        .ConfigureAwait(false);
                }
            }
            else
            {
                result = await _context.Properties
                    .Retrieve(entryIdentifier, scope)
                    .ConfigureAwait(false);
            }

            return result;
        }
    }
}
