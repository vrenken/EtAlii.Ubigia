namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Logical;

    public class ProfilingGraphPathTaggedNodeTraverser : IGraphPathTaggedNodeTraverser
    {
        private readonly IGraphPathTaggedNodeTraverser _decoree;
        private readonly IProfiler _profiler;

        public ProfilingGraphPathTaggedNodeTraverser(
            IGraphPathTaggedNodeTraverser decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Logical.Traversers);
        }

        public void Configure(TraversalParameters parameters)
        {
            var graphTaggedNode = (GraphTaggedNode)parameters.Part;

            dynamic profile = _profiler.Begin($"Configuring tagged node traversing: {graphTaggedNode.Name}#{graphTaggedNode.Tag}");
            profile.Part = parameters.Part;

            _decoree.Configure(parameters);

            _profiler.End(profile);
        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            var graphTaggedNode = (GraphTaggedNode)part;

            dynamic profile = _profiler.Begin($"Traversing tagged node: {graphTaggedNode.Name}#{graphTaggedNode.Tag}");
            profile.Part = part;
            profile.Start = start;

            var result = await _decoree.Traverse(part, start, context, scope);

            _profiler.End(profile);

            return result;
        }
    }
}