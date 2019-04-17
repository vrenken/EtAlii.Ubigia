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
            var entries = await _fabric.Entries.GetRelated(location, EntryRelation.Child, scope);
            var entry = entries
                .SingleOrDefault(); // We do not support multiple empty childs yet.

            if (entry != null)
            {

                // We do not support multiple empty childs yet.
                entry = await _graphPathTraverser.TraverseToSingle(entry.Id, scope);
            }
            else
            {
                var newEntry = await _fabric.Entries.Prepare();
                newEntry.Parent = Relation.NewRelation(location);
                entry = await _fabric.Entries.Change(newEntry, scope);
            }
            return entry;
        }

        public Task<IReadOnlyEntry> AddChild(Identifier location, Identifier childId, ExecutionScope scope)
        {
            if (childId == Identifier.Empty) throw new ArgumentException(nameof(childId));
            
            return AddChildInternal(location, childId, scope);
        }

        private async Task<IReadOnlyEntry> AddChildInternal(Identifier location, Identifier childId, ExecutionScope scope)
        {
            var related = await _fabric.Entries.GetRelated(location, EntryRelation.Child, scope);
            var child = related.SingleOrDefault(e => e.Id == childId);
            if (child != null)
            {
                var message = $"Unable to add child '{childId}' to entry: {location}";
                throw new GraphComposeException(message);
            }
            else
            {
                var originalChild = await _fabric.Entries.Get(childId, scope);
                var updatedChild = await _fabric.Entries.Prepare();
                updatedChild.Parent = Relation.NewRelation(location);
                updatedChild.Type = originalChild.Type;
                updatedChild.Tag = originalChild.Tag;
                updatedChild.Downdate = Relation.NewRelation(originalChild.Id);
                child = await _fabric.Entries.Change(updatedChild, scope);
            }
            return child;
        }

        public Task<IReadOnlyEntry> AddChild(Identifier location, string name, ExecutionScope scope)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            return AddChildInternal(location, name, scope);
        }

        private async Task<IReadOnlyEntry> AddChildInternal(Identifier location, string name, ExecutionScope scope)
        {
            var related = await _fabric.Entries.GetRelated(location, EntryRelation.Child, scope);

            var entry = related.SingleOrDefault(e => e.Type == name);

            if (entry != null)
            {
                var message = $"Unable to add child '{name}' to entry: {location}";
                throw new GraphComposeException(message);
            }
            else
            { 
                var newEntry = await _fabric.Entries.Prepare();
                newEntry.Parent = Relation.NewRelation(location);
                newEntry.Type = name;
                entry = await _fabric.Entries.Change(newEntry, scope);
            }
            return entry;

        }
    }
}