namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    internal class GraphChildAdder : IGraphChildAdder
    {
        private readonly IComposeContext _context;
        private readonly IGraphPathTraverserFactory _graphPathTraverserFactory;


        public GraphChildAdder(
            IComposeContext context,
            IGraphPathTraverserFactory graphPathTraverserFactory)
        {
            _context = context;
            _graphPathTraverserFactory = graphPathTraverserFactory;
        }

        public async Task<IReadOnlyEntry> TryAddChild(Identifier location, ExecutionScope scope)
        {
            var entries = await _context.Fabric.Entries.GetRelated(location, EntryRelation.Child, scope);
            var entry = entries
                .SingleOrDefault(); // We do not support multiple empty childs yet.

            if (entry != null)
            {
                var traversedEntries = Observable.Create<IReadOnlyEntry>(output =>
                {
                    var configuration = new GraphPathTraverserConfiguration()
                        .Use(_context.Fabric);
                    var traverser = _graphPathTraverserFactory.Create(configuration);
                    traverser.Traverse(GraphPath.Create(entry.Id), Traversal.DepthFirst, scope, output);
                    return Disposable.Empty;
                }).ToHotObservable();

                entry = await traversedEntries.SingleAsync(); // We do not support multiple empty childs yet.
            }
            else
            {
                var newEntry = await _context.Fabric.Entries.Prepare();
                newEntry.Parent = Relation.NewRelation(location);
                entry = await _context.Fabric.Entries.Change(newEntry, scope);
            }
            return entry;
        }

        public async Task<IReadOnlyEntry> AddChild(Identifier location, Identifier childId, ExecutionScope scope)
        {
            if (childId == Identifier.Empty) throw new ArgumentException("childId");

            var related = await _context.Fabric.Entries.GetRelated(location, EntryRelation.Child, scope);
            var child = related.SingleOrDefault(e => e.Id == childId);
            if (child != null)
            {
                var message = String.Format("Unable to add child '{0}' to entry: {1}", childId, location);
                throw new GraphComposeException(message);
            }
            else
            {
                var originalChild = await _context.Fabric.Entries.Get(childId, scope);
                var updatedChild = await _context.Fabric.Entries.Prepare();
                updatedChild.Parent = Relation.NewRelation(location);
                updatedChild.Type = originalChild.Type;
                updatedChild.Downdate = Relation.NewRelation(originalChild.Id);
                child = await _context.Fabric.Entries.Change(updatedChild, scope);
            }
            return child;
        }

        public async Task<IReadOnlyEntry> AddChild(Identifier location, string name, ExecutionScope scope)
        {
            if (name == null) throw new ArgumentNullException("name");

            var related = await _context.Fabric.Entries.GetRelated(location, EntryRelation.Child, scope);

            var entry = related.SingleOrDefault(e => e.Type == name);

            if (entry != null)
            {
                var message = String.Format("Unable to add child '{0}' to entry: {1}", name, location);
                throw new GraphComposeException(message);
            }
            else
            {
                var newEntry = await _context.Fabric.Entries.Prepare();
                newEntry.Parent = Relation.NewRelation(location);
                newEntry.Type = name;
                entry = await _context.Fabric.Entries.Change(newEntry, scope);
            }
            return entry;

        }
    }
}