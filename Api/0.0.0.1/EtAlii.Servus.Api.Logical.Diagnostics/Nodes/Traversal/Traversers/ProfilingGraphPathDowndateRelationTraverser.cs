namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;

    public class ProfilingGraphPathDowndateRelationTraverser : IGraphPathDowndateRelationTraverser
    {
        private readonly IGraphPathDowndateRelationTraverser _decoree;
        private readonly IProfiler _profiler;

        public ProfilingGraphPathDowndateRelationTraverser(IGraphPathDowndateRelationTraverser decoree, IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Logical.Traversers);
        }

        public void Configure(TraversalParameters parameters)
        {
            dynamic profile = _profiler.Begin("Configuring relation traversing: DOWNDATE");
            profile.Part = parameters.Part;

            _decoree.Configure(parameters);

            _profiler.End(profile);
        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Traversing relation: DOWNDATE");
            profile.Part = part;
            profile.Start = start;

            var result = await _decoree.Traverse(part, start, context, scope);

            _profiler.End(profile);

            return result;
        }
    }
}