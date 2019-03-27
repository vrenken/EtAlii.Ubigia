//namespace EtAlii.Ubigia.Api.Logical
//{
//    using System.Reactive.Disposables;
//    using System.Reactive.Linq;
//    using System.Threading.Tasks;
//    using EtAlii.Ubigia.Api.Fabric;

//    public class SingleEntryToLatestTraverser : ISingleEntryToLatestTraverser
//    {
//        private readonly IGraphPathTraverser _graphPathTraverser;

//        public SingleEntryToLatestTraverser(IGraphPathTraverser graphPathTraverser)
//        {
//            _graphPathTraverser = graphPathTraverser;
//        }

//        public async Task<IReadOnlyEntry> Traverse(Identifier identifier, ExecutionScope scope)
//        {
//            var results = Observable.Create<IReadOnlyEntry>(output =>
//            {
//                _graphPathTraverser.Traverse(GraphPath.Create(identifier), Traversal.DepthFirst, scope, output);
//                return Disposable.Empty;
//            }).ToHotObservable();
//            return await results.SingleAsync();
//        }
//    }
//}