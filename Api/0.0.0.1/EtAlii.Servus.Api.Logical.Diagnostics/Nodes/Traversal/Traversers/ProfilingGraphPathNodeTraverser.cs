namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Logical;

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

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var name = ((GraphNode)part).Name;

            dynamic profile = _profiler.Begin("Traversing path node: " + name);
            profile.Part = part;
            profile.Start = start;

            var result = await _decoree.Traverse(part, start, context, scope);

            _profiler.End(profile);

            return result;
        }
    }
}