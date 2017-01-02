namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    public class ProfilingGraphPathNextRelationTraverser : IGraphPathNextRelationTraverser
    {
        private readonly IGraphPathNextRelationTraverser _decoree;
        private readonly IProfiler _profiler;

        public ProfilingGraphPathNextRelationTraverser(IGraphPathNextRelationTraverser decoree, IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Logical.Traversers);
        }

        public void Configure(TraversalParameters parameters)
        {
            dynamic profile = _profiler.Begin("Configuring relation traversing: NEXT");
            profile.Part = parameters.Part;

            _decoree.Configure(parameters);

            _profiler.End(profile);
        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Traversing relation: NEXT");
            profile.Part = part;
            profile.Start = start;

            var result = await _decoree.Traverse(part, start, context, scope);

            _profiler.End(profile);

            return result;

        }
    }
}