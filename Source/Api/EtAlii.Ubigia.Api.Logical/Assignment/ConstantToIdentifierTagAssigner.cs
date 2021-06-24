// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    internal class ConstantToIdentifierTagAssigner : IConstantToIdentifierTagAssigner
    {
        private readonly IFabricContext _fabric;
        private readonly IGraphPathTraverser _graphPathTraverser;
        private readonly IUpdateEntryFactory _updateEntryFactory;

        public ConstantToIdentifierTagAssigner(
            IUpdateEntryFactory updateEntryFactory, 
            IFabricContext fabric,
            IGraphPathTraverser graphPathTraverser)
        {
            _updateEntryFactory = updateEntryFactory;
            _fabric = fabric;
            _graphPathTraverser = graphPathTraverser;
        }

        public async Task<INode> Assign(string constant, Identifier id, ExecutionScope scope)
        {
            var latestEntry = await _graphPathTraverser.TraverseToSingle(id, scope).ConfigureAwait(false);
            id = latestEntry.Id;

            var entry = await _fabric.Entries.Get(id, scope).ConfigureAwait(false);
            var updatedEntry = await _updateEntryFactory.Create(entry, constant, scope).ConfigureAwait(false);

            var newNode = (IInternalNode)new DynamicNode((IReadOnlyEntry)updatedEntry);
            return newNode;
        }
    }
}