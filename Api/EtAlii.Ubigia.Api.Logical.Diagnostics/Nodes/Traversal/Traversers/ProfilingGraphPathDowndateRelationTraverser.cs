namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.Collections.Generic;
    using EtAlii.Ubigia.Diagnostics.Profiling;

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

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Traversing relation: DOWNDATE");
            profile.Part = part;
            profile.Start = start;

            var result = _decoree.Traverse(part, start, context, scope);
            await foreach (var item in result)
            {
                yield return item;
            }

            _profiler.End(profile);
        }
    }
}