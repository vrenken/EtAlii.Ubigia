namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Logical;

    public class ProfilingGraphPathAllParentsRelationTraverser : IGraphPathAllParentsRelationTraverser
    {
        private readonly IGraphPathAllParentsRelationTraverser _decoree;
        private readonly IProfiler _profiler;

        public ProfilingGraphPathAllParentsRelationTraverser(IGraphPathAllParentsRelationTraverser decoree, IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Logical.Traversers);
        }

        public void Configure(TraversalParameters parameters)
        {
            dynamic profile = _profiler.Begin("Configuring relation traversing: ALL PARENTS");
            profile.Part = parameters.Part;

            _decoree.Configure(parameters);

            _profiler.End(profile);
        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Traversing relation: ALL PARENTS");
            profile.Part = part;
            profile.Start = start;

            var result = await _decoree.Traverse(part, start, context, scope);

            _profiler.End(profile);

            return result;
        }
    }
}