namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingGraphPathNodeTraverser : IGraphPathNodeTraverser
    {
        private readonly IGraphPathNodeTraverser _decoree;
        private readonly IProfiler _profiler;

        public ProfilingGraphPathNodeTraverser(IGraphPathNodeTraverser decoree, IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Logical.Traversers);
        }

        public void Configure(TraversalParameters parameters)
        {
            var name = ((GraphNode)parameters.Part).Name;

            dynamic profile = _profiler.Begin("Configuring path node traversing: " + name);
            profile.Part = parameters.Part;

            _decoree.Configure(parameters);

            _profiler.End(profile);
        }

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var name = ((GraphNode)part).Name;

            dynamic profile = _profiler.Begin("Traversing path node: " + name);
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