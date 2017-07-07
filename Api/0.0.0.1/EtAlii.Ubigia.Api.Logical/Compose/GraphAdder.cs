﻿namespace EtAlii.Ubigia.Api.Logical
{
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal class GraphAdder : IGraphAdder
    {
        private readonly IComposeContext _context;
        private readonly IGraphPathTraverserFactory _graphPathTraverserFactory;
        private readonly IGraphChildAdder _graphChildAdder;
        private readonly IGraphLinkAdder _graphLinkAdder;
        private readonly IGraphUpdater _graphUpdater;

        public GraphAdder(
            IComposeContext context,
            IGraphPathTraverserFactory graphPathTraverserFactory,
            IGraphChildAdder graphChildAdder,
            IGraphLinkAdder graphLinkAdder,
            IGraphUpdater graphUpdater)
        {
            _context = context;
            _graphPathTraverserFactory = graphPathTraverserFactory;
            _graphChildAdder = graphChildAdder;
            _graphLinkAdder = graphLinkAdder;
            _graphUpdater = graphUpdater;
        }

        public async Task<IReadOnlyEntry> Add(Identifier parent, string child, ExecutionScope scope)
        {
            IReadOnlyEntry result = null;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_context.Fabric);
            var graphPathTraverser = _graphPathTraverserFactory.Create(configuration);

            var entries = Observable.Create<IReadOnlyEntry>(output =>
            {
                graphPathTraverser.Traverse(GraphPath.Create(parent), Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var entry = await entries.SingleAsync(); // The GraphComposer cannot handle multiple updates yet.

            // Let's check if a path already exists.
            var linkAddResult = await _graphLinkAdder.GetLink(child, entry, graphPathTraverser, result, scope);
            var originalLinkEntry = linkAddResult.Item1;
            result = linkAddResult.Item2;

            if (result == null)
            {
                var updateEntry = await _graphUpdater.Update(entry, scope);
                var updateLinkEntry = await _graphLinkAdder.AddLink(updateEntry, originalLinkEntry, EntryType.Add, scope);
                result = await _graphChildAdder.AddChild(updateLinkEntry.Id, child, scope);
            }
            return result;
        }

        public async Task<IReadOnlyEntry> Add(Identifier parent, Identifier child, ExecutionScope scope)
        {
            IReadOnlyEntry result = null;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_context.Fabric);
            var graphPathTraverser = _graphPathTraverserFactory.Create(configuration);

            var entries = Observable.Create<IReadOnlyEntry>(output =>
            {
                graphPathTraverser.Traverse(GraphPath.Create(parent), Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var entry = await entries.SingleAsync(); // The GraphComposer cannot handle multiple updates yet.

            // Let's check if a path already exists.
            var linkAddResult = await _graphLinkAdder.GetLink(child, entry, graphPathTraverser, result, scope);
            var originalLinkEntry = linkAddResult.Item1;
            result = linkAddResult.Item2;

            if (result == null)
            {
                var updateEntry = await _graphUpdater.Update(entry, scope);
                var updateLinkEntry = await _graphLinkAdder.AddLink(updateEntry, originalLinkEntry, EntryType.Add, scope);
                result = await _graphChildAdder.AddChild(updateLinkEntry.Id, child, scope);
            }
            return result;
        }
    }
}
