namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

    public class ProfilingGraphPathWildcardTraverser : IGraphPathWildcardTraverser
    {
        private readonly IGraphPathWildcardTraverser _decoree;
        private readonly IProfiler _profiler;

        public ProfilingGraphPathWildcardTraverser(
            IGraphPathWildcardTraverser decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Logical.Traversers);
        }

        public void Configure(TraversalParameters parameters)
        {
            var pattern = ((GraphWildcard)parameters.Part).Pattern;

            dynamic profile = _profiler.Begin("Configuring wildcard pattern traversing: " + pattern);
            profile.Part = parameters.Part;

            _decoree.Configure(parameters);

            _profiler.End(profile);
        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var pattern = ((GraphWildcard)part).Pattern;

            dynamic profile = _profiler.Begin("Traversing wildcard pattern: " + pattern);
            profile.Part = part;
            profile.Start = start;

            var result = await _decoree.Traverse(part, start, context, scope);

            _profiler.End(profile);

            return result;
        }
    }
}