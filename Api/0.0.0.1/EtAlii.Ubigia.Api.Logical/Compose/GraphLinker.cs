namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    internal class GraphLinker : IGraphLinker
    {
        private readonly IComposeContext _context;
        private readonly IGraphPathTraverserFactory _graphPathTraverserFactory;
        private readonly IGraphChildAdder _graphChildAdder;
        private readonly IGraphLinkAdder _graphLinkAdder;
        private readonly IGraphUpdater _graphUpdater;

        public GraphLinker(
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

        public async Task<IReadOnlyEntry> Link(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            IReadOnlyEntry locationResult = null;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(_context.Fabric);
            var graphPathTraverser = _graphPathTraverserFactory.Create(configuration);
            var locationEntries = Observable.Create<IReadOnlyEntry>(output =>
            {
                graphPathTraverser.Traverse(GraphPath.Create(location), Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var locationEntry = await locationEntries.SingleAsync(); // The GraphComposer cannot handle multiple updates yet.

            var itemEntries = Observable.Create<IReadOnlyEntry>(output =>
            {
                graphPathTraverser.Traverse(GraphPath.Create(item), Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var itemEntry = await itemEntries.SingleAsync(); // The GraphComposer cannot handle multiple updates yet.

            // Let's check if a path already exists.
            var linkAddResult = await _graphLinkAdder.GetLink(itemName, locationEntry, graphPathTraverser, locationResult, scope);
            var locationLinkOriginalEntry = linkAddResult.Item1;
            locationResult = linkAddResult.Item2;
            if (locationResult == null)
            {
                var locationUpdateEntry = await _graphUpdater.Update(locationEntry, scope);
                var locationLinkUpdateEntry = await _graphLinkAdder.AddLink(locationUpdateEntry, locationLinkOriginalEntry, EntryType.Add, scope);
                locationResult = await _graphChildAdder.AddChild(locationLinkUpdateEntry.Id, itemName, scope);
            }

            throw new NotImplementedException();

            //return locationResult;
        }
    }
}
