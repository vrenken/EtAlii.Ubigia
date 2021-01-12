namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingGraphPathRootStartNodeTraverser : IGraphPathRootStartNodeTraverser
    {
        private readonly IGraphPathRootStartNodeTraverser _decoree;
        private readonly IProfiler _profiler;

        public ProfilingGraphPathRootStartNodeTraverser(
            IGraphPathRootStartNodeTraverser decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Logical.Traversers);
        }

        public void Configure(TraversalParameters parameters)
        {
            var rootStartNode = (GraphRootStartNode)parameters.Part;
            var rootName = rootStartNode.Root;

            dynamic profile = _profiler.Begin("Configuring start root traversing: " + rootName);
            profile.Part = parameters.Part;

            _decoree.Configure(parameters);

            _profiler.End(profile);
        }

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope)
        {
            var rootStartNode = (GraphRootStartNode)part;
            var rootName = rootStartNode.Root;

            dynamic profile = _profiler.Begin("Traversing start root: " + rootName);
            profile.Part = part;
            profile.Start = start;

            var result = _decoree.Traverse(part, start, context, scope);
            await foreach (var item in result.ConfigureAwait(false))
            {
                yield return item;
            }

            _profiler.End(profile);
        }
    }
}