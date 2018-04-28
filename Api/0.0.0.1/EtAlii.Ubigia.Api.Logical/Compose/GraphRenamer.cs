namespace EtAlii.Ubigia.Api.Logical
{
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    internal class GraphRenamer : IGraphRenamer
    {
        private readonly IComposeContext _context;
        private readonly IGraphPathTraverserFactory _graphPathTraverserFactory;
        private readonly IGraphUpdater _graphUpdater;

        public GraphRenamer(
            IComposeContext context,
            IGraphPathTraverserFactory graphPathTraverserFactory,
            IGraphUpdater graphUpdater)
        {
            _context = context;
            _graphPathTraverserFactory = graphPathTraverserFactory;
            _graphUpdater = graphUpdater;
        }

        public async Task<IReadOnlyEntry> Rename(Identifier item, string newName, ExecutionScope scope)
        {
            var configuration = new GraphPathTraverserConfiguration()
                .Use(_context.Fabric);
            var graphPathTraverser = _graphPathTraverserFactory.Create(configuration);
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                graphPathTraverser.Traverse(GraphPath.Create(item), Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            var result = await results.SingleAsync();

            if (result.Type != newName)
            {
                result = (IReadOnlyEntry)await _graphUpdater.Update(result, newName, scope);
            }

            return result;
        }
    }
}
