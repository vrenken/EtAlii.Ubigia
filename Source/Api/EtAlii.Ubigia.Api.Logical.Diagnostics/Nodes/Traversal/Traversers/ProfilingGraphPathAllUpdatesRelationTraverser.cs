
namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingGraphPathAllUpdatesRelationTraverser : IGraphPathAllUpdatesRelationTraverser
    {
        private readonly IGraphPathAllUpdatesRelationTraverser _decoree;
        private readonly IProfiler _profiler;

        public ProfilingGraphPathAllUpdatesRelationTraverser(
            IGraphPathAllUpdatesRelationTraverser decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Logical.Traversers);
        }

        public void Configure(TraversalParameters parameters)
        {
            dynamic profile = _profiler.Begin("Configuring relation traversing: ALL UPDATES");
            profile.Part = parameters.Part;

            _decoree.Configure(parameters);

            _profiler.End(profile);
        }

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope)
        {
            dynamic profile = _profiler.Begin("Traversing relation: ALL UPDATES");
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