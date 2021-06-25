// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    internal class GraphChildAdder : IGraphChildAdder
    {
        private readonly IFabricContext _fabric;
        private readonly IGraphPathTraverser _graphPathTraverser;

        public GraphChildAdder(IGraphPathTraverser graphPathTraverser, IFabricContext fabric)
        {
            _fabric = fabric;
            _graphPathTraverser = graphPathTraverser;
        }

        public async Task<IReadOnlyEntry> TryAddChild(Identifier location, ExecutionScope scope)
        {
            var entry = await _fabric.Entries
                .GetRelated(location, EntryRelations.Child, scope)
                .SingleOrDefaultAsync()  // We do not support multiple empty childs yet.
                .ConfigureAwait(false);

            if (entry != null)
            {

                // We do not support multiple empty childs yet.
                entry = await _graphPathTraverser.TraverseToSingle(entry.Id, scope).ConfigureAwait(false);
            }
            else
            {
                var newEntry = await _fabric.Entries.Prepare().ConfigureAwait(false);
                newEntry.Parent = Relation.NewRelation(location);
                entry = await _fabric.Entries.Change(newEntry, scope).ConfigureAwait(false);
            }
            return entry;
        }

        public Task<IReadOnlyEntry> AddChild(Identifier location, Identifier childId, ExecutionScope scope)
        {
            if (childId == Identifier.Empty) throw new ArgumentException("No empty identifiers are allowed whilst adding to the graph", nameof(childId));

            return AddChildInternal(location, childId, scope);
        }

        public Task<IReadOnlyEntry> AddChild(Identifier location, string name, ExecutionScope scope)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            return AddChildInternal(location, name, scope);
        }

        private async Task<IReadOnlyEntry> AddChildInternal(Identifier location, Identifier childId, ExecutionScope scope)
        {
            var child = await _fabric.Entries
                .GetRelated(location, EntryRelations.Child, scope)
                .SingleOrDefaultAsync(e => e.Id == childId)
                .ConfigureAwait(false);

            if (child != null)
            {
                var message = $"Unable to add child '{childId}' to entry: {location}";
                throw new GraphComposeException(message);
            }

            var originalChild = await _fabric.Entries.Get(childId, scope).ConfigureAwait(false);
            var updatedChild = await _fabric.Entries.Prepare().ConfigureAwait(false);
            updatedChild.Parent = Relation.NewRelation(location);
            updatedChild.Type = originalChild.Type;
            updatedChild.Tag = originalChild.Tag;
            updatedChild.Downdate = Relation.NewRelation(originalChild.Id);
            child = await _fabric.Entries.Change(updatedChild, scope).ConfigureAwait(false);
            return child;
        }

        private async Task<IReadOnlyEntry> AddChildInternal(Identifier location, string name, ExecutionScope scope)
        {
            var entry = await _fabric.Entries
                .GetRelated(location, EntryRelations.Child, scope)
                .SingleOrDefaultAsync(e => e.Type == name)
                .ConfigureAwait(false);
            if (entry != null)
            {
                var message = $"Unable to add child '{name}' to entry: {location}";
                throw new GraphComposeException(message);
            }

            var newEntry = await _fabric.Entries.Prepare().ConfigureAwait(false);
            newEntry.Parent = Relation.NewRelation(location);
            newEntry.Type = name;
            entry = await _fabric.Entries.Change(newEntry, scope).ConfigureAwait(false);
            return entry;
        }
    }
}
