namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingGraphPathIdentifiersStartNodeTraverser : IGraphPathIdentifiersStartNodeTraverser
    {
        private readonly IGraphPathIdentifiersStartNodeTraverser _decoree;
        private readonly IProfiler _profiler;

        public ProfilingGraphPathIdentifiersStartNodeTraverser(IGraphPathIdentifiersStartNodeTraverser decoree, IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Logical.Traversers);
        }

        public void Configure(TraversalParameters parameters)
        {
            var identifiers = string.Join(", ", ((GraphIdentifiersStartNode)parameters.Part).Identifiers.Select(i => i.ToTimeString()));

            dynamic profile = _profiler.Begin("Configuring start identifiers traversing: " + identifiers);
            profile.Part = parameters.Part;

            _decoree.Configure(parameters);

            _profiler.End(profile);
        }

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope)
        {
            var identifiers = string.Join(", ", ((GraphIdentifiersStartNode) part).Identifiers.Select(i => i.ToTimeString()));

            dynamic profile = _profiler.Begin("Traversing start identifiers: " + identifiers);
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