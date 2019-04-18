namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BreadthFirstTraversalAlgorithm : IBreadthFirstTraversalAlgorithm
    {
        private readonly IGraphPathPartTraverserSelector _graphPathPartTraverserSelector;

        public BreadthFirstTraversalAlgorithm(IGraphPathPartTraverserSelector graphPathPartTraverserSelector)
        {
            _graphPathPartTraverserSelector = graphPathPartTraverserSelector;
        }
        public async Task Traverse(GraphPath graphPath, Identifier current, ITraversalContext context, ExecutionScope scope, IObserver<Identifier> finalOutput)
        {
            IEnumerable<Identifier> previousResult = new[] { current };

            for (int i = 0; i < graphPath.Length; i++)
            {
                var currentGraphPathPart = graphPath[i];

                var traverser = _graphPathPartTraverserSelector.Select(currentGraphPathPart);

                var iterationResult = new List<Identifier>();

                var isLast = i == graphPath.Length - 1;
                foreach (var identifier in previousResult)
                {
                    var relatedNodes = await traverser.Traverse(currentGraphPathPart, identifier, context, scope);

                    if (isLast)
                    {
                        foreach (var relatedNode in relatedNodes)
                        {
                            finalOutput.OnNext(relatedNode);
                        }
                    }
                    else
                    {
                        iterationResult.AddRange(relatedNodes);
                    }
                }
                if (!isLast)
                {
                    previousResult = iterationResult;
                }
            }
        }

    }
}