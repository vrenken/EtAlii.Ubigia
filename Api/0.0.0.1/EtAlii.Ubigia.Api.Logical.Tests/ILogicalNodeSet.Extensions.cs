namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    public static class ILogicalNodeSetExtensions
    {
        public static async Task<IReadOnlyEntry> Select(this ILogicalNodeSet nodeSet, GraphPath path, ExecutionScope scope)
        {
            var logicalNodeSet = (LogicalNodeSet) nodeSet;
            
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                logicalNodeSet.GraphPathTraverser.Traverse(path, Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            return await results.SingleOrDefaultAsync();
        }
    }
}