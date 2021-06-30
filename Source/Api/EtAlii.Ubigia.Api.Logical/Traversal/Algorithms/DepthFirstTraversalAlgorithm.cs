// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class DepthFirstTraversalAlgorithm : IDepthFirstTraversalAlgorithm
    {
        private readonly IGraphPathPartTraverserSelector _graphPathPartTraverserSelector;

        public DepthFirstTraversalAlgorithm(IGraphPathPartTraverserSelector graphPathPartTraverserSelector)
        {
            _graphPathPartTraverserSelector = graphPathPartTraverserSelector;
        }
        public async Task Traverse(GraphPath graphPath, Identifier current, IPathTraversalContext context, ExecutionScope scope, IObserver<Identifier> finalOutput)
        {
            if (graphPath.Any())
            {
                var currentGraphPathPart = graphPath.First();
                var traverser = _graphPathPartTraverserSelector.Select(currentGraphPathPart);

                var relatedNodes = traverser
                    .Traverse(currentGraphPathPart, current, context, scope)
                    .ConfigureAwait(false);
                var subPathParts = graphPath.Skip(1).ToArray();
                if (subPathParts.Any())
                {
                    await foreach (var relatedNode in relatedNodes)
                    {
                        var subGraphPath = new GraphPath(subPathParts);
                        await Traverse(subGraphPath, relatedNode, context, scope, finalOutput).ConfigureAwait(false);
                    }
                }
                else
                {
                    await foreach (var relatedNode in relatedNodes)
                    {
                        finalOutput.OnNext(relatedNode);
                    }
                }
            }
        }

    }
}
