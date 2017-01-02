namespace EtAlii.Servus.Api.Logical
{
    using System;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Functional;

    internal partial class GraphLinkAdder : IGraphLinkAdder
    {
        private readonly IComposeContext _context;
        private readonly IGraphChildAdder _graphChildAdder;

        public GraphLinkAdder(
            IComposeContext context,
            IGraphChildAdder graphChildAdder)
        {
            _context = context;
            _graphChildAdder = graphChildAdder;
        }

        public async Task<IEditableEntry> AddLink(IEditableEntry updateEntry, IReadOnlyEntry originalLinkEntry, string type, ExecutionScope scope)
        {
            var linkEntry = (IEditableEntry)await _graphChildAdder.AddChild(updateEntry.Id, type, scope);
            if (originalLinkEntry != null)
            {
                linkEntry.Downdate = Relation.NewRelation(originalLinkEntry.Id);
                linkEntry = (IEditableEntry)await _context.Fabric.Entries.Change(linkEntry, scope);
            }
            return linkEntry;
        }

        public async Task<Tuple<IReadOnlyEntry, IReadOnlyEntry>> GetLink(string itemName, IReadOnlyEntry entry, IGraphPathTraverser graphPathTraverser, IReadOnlyEntry result, ExecutionScope scope)
        {
            var entries = await _context.Fabric.Entries.GetRelated(entry.Id, EntryRelation.Child, scope);
            var linkEntry = entries.SingleOrDefault();
            if (linkEntry != null)
            {
                var results = Observable.Create<IReadOnlyEntry>(output =>
                {
                    graphPathTraverser.Traverse(GraphPath.Create(entry.Id, GraphRelation.Child, new GraphNode(itemName)), Traversal.DepthFirst, scope, output);
                    return Disposable.Empty;
                }).ToHotObservable();
                result = await results.SingleOrDefaultAsync();
            }
            return new Tuple<IReadOnlyEntry, IReadOnlyEntry>(linkEntry, result);
        }

        public async Task<Tuple<IReadOnlyEntry, IReadOnlyEntry>> GetLink(Identifier item, IReadOnlyEntry entry, IGraphPathTraverser graphPathTraverser, IReadOnlyEntry result, ExecutionScope scope)
        {
            var entries = await _context.Fabric.Entries.GetRelated(entry.Id, EntryRelation.Child, scope);
            var linkEntry = entries.SingleOrDefault();
            if (linkEntry != null)
            {
                var results = Observable.Create<IReadOnlyEntry>(output =>
                {
                    graphPathTraverser.Traverse(GraphPath.Create(entry.Id, GraphRelation.Child, new GraphWildcard("*")), Traversal.DepthFirst, scope, output);
                    return Disposable.Empty;
                }).ToHotObservable();
                result = await results.SingleOrDefaultAsync(e => e.Id == item);
            }
            return new Tuple<IReadOnlyEntry, IReadOnlyEntry>(linkEntry, result);
        }
    }
}
