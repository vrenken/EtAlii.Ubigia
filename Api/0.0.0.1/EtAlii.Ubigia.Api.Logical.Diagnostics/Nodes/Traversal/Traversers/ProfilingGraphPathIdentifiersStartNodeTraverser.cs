namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Logical;

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
            string identifiers = String.Join(", ", ((GraphIdentifiersStartNode)parameters.Part).Identifiers.Select(i => i.ToTimeString()));

            dynamic profile = _profiler.Begin("Configuring start identifiers traversing: " + identifiers);
            profile.Part = parameters.Part;

            _decoree.Configure(parameters);

            _profiler.End(profile);
        }

        public async Task<IEnumerable<Identifier>> Traverse(GraphPathPart part, Identifier start, ITraversalContext context, ExecutionScope scope)
        {
            string identifiers = String.Join(", ", ((GraphIdentifiersStartNode) part).Identifiers.Select(i => i.ToTimeString()));

            dynamic profile = _profiler.Begin("Traversing start identifiers: " + identifiers);
            profile.Part = part;
            profile.Start = start;

            var result = await _decoree.Traverse(part, start, context, scope);

            _profiler.End(profile);

            return result;
        }
    }
}