// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    internal class NodeToIdentifierAssigner : INodeToIdentifierAssigner
    {
        private readonly IFabricContext _fabric;
        private readonly IUpdateEntryFactory _updateEntryFactory;
        private readonly IGraphPathTraverser _graphPathTraverser;

        public NodeToIdentifierAssigner(
            IUpdateEntryFactory updateEntryFactory,
            IFabricContext fabric,
            IGraphPathTraverser graphPathTraverser)
        {
            _updateEntryFactory = updateEntryFactory;
            _fabric = fabric;
            _graphPathTraverser = graphPathTraverser;
        }

        public async Task<INode> Assign(INode node, Identifier id, ExecutionScope scope)
        {
            var latestEntry = await _graphPathTraverser.TraverseToSingle(id, scope).ConfigureAwait(false);
            id = latestEntry.Id;

            var sourceNode = (IInternalNode)node;
            var newProperties = sourceNode.GetProperties();

            var entry = await _fabric.Entries.Get(id, scope).ConfigureAwait(false);
            var oldProperties = await _fabric.Properties.Retrieve(id, scope).ConfigureAwait(false) ?? new PropertyDictionary();

            if (oldProperties.CompareTo(newProperties) == 0)
            {
                // The two PropertyDictionaries are the same. Let's return the old node.
                return new DynamicNode(entry, oldProperties);
            }
            else
            {
                var newEntry = await _updateEntryFactory.Create(entry, scope).ConfigureAwait(false);

                // We want to do an addition, so lets combine the old with the new properties.
                var properties = new PropertyDictionary(oldProperties);

                // Lets remove all properties that should be set to null.
                foreach (var newProperty in newProperties)
                {
                    if (newProperty.Value == null)
                    {
                        properties.Remove(newProperty.Key);
                    }
                    else
                    {
                        properties[newProperty.Key] = newProperty.Value;
                    }
                }

                await _fabric.Properties.Store(newEntry.Id, properties, scope).ConfigureAwait(false);

                var newNode = (IInternalNode)new DynamicNode((IReadOnlyEntry)newEntry, properties);
                return newNode;
            }
        }
    }
}
