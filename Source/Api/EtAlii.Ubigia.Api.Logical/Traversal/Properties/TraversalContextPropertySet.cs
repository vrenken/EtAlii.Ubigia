// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    internal sealed class TraversalContextPropertySet : ITraversalContextPropertySet
    {
        private readonly IFabricContext _context;

        public TraversalContextPropertySet(IFabricContext context)
        {
            _context = context;
        }

        public Task<PropertyDictionary> Retrieve(Identifier entryIdentifier, ExecutionScope scope)
        {
            return _context.Properties
                .Retrieve(entryIdentifier, scope);
        }
    }
}
