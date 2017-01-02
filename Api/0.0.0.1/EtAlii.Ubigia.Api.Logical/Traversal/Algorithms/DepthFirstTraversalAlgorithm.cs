namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Collections;

    public class DepthFirstTraversalAlgorithm : IDepthFirstTraversalAlgorithm
    {
        private readonly IGraphPathPartTraverserSelector _graphPathPartTraverserSelector;

        public DepthFirstTraversalAlgorithm(IGraphPathPartTraverserSelector graphPathPartTraverserSelector)
        {
            _graphPathPartTraverserSelector = graphPathPartTraverserSelector;
        }
        public async Task Traverse(GraphPath graphPath, Identifier current, ITraversalContext context, ExecutionScope scope, IObserver<Identifier> output)
        {
            if (graphPath.Any())
            {
                var currentGraphPathPart = graphPath.First();
                var traverser = _graphPathPartTraverserSelector.Select(currentGraphPathPart);

                var relatedNodes = await traverser.Traverse(currentGraphPathPart, current, context, scope);
                var subPathParts = graphPath.Skip(1).ToArray();
                if (subPathParts.Any())
                {
                    foreach (var relatedNode in relatedNodes)
                    {
                        var subGraphPath = new GraphPath(subPathParts);
                        await Traverse(subGraphPath, relatedNode, context, scope, output);
                    }
                }
                else
                {
                    foreach (var relatedNode in relatedNodes)
                    {
                        output.OnNext(relatedNode);
                    }
                }
            }
        }
        
    }
}
