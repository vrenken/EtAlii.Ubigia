// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    public static class LogicalNodeSetExtensions
    {
        public static async Task<IReadOnlyEntry> Select(this ILogicalNodeSet nodeSet, GraphPath path, ExecutionScope scope)
        {
            var logicalNodeSet = (LogicalNodeSet) nodeSet;

            var configuration = new GraphPathTraverserConfiguration()
                .Use(logicalNodeSet.Fabric);
            var traverser = logicalNodeSet.GraphPathTraverserFactory.Create(configuration);
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverser.Traverse(path, Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            return await results.SingleOrDefaultAsync();
        }
    }
}
