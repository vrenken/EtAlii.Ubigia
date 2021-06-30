// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Diagnostics.Profiling;

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

        public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope)
        {
            var graphTaggedNode = (GraphTaggedNode)part;

            dynamic profile = _profiler.Begin($"Traversing tagged node: {graphTaggedNode.Name}#{graphTaggedNode.Tag}");
            profile.Part = part;
            profile.Start = start;

            var result = _decoree
                .Traverse(part, start, context, scope)
                .ConfigureAwait(false);
            await foreach (var item in result)
            {
                yield return item;
            }

            _profiler.End(profile);
        }
    }
}
