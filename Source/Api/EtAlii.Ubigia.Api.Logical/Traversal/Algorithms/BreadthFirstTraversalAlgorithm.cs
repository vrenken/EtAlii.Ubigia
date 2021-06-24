// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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
        public async Task Traverse(GraphPath graphPath, Identifier current, IPathTraversalContext context, ExecutionScope scope, IObserver<Identifier> finalOutput)
        {
            IEnumerable<Identifier> previousResult = new[] { current };

            for (var i = 0; i < graphPath.Length; i++)
            {
                var currentGraphPathPart = graphPath[i];

                var traverser = _graphPathPartTraverserSelector.Select(currentGraphPathPart);

                var iterationResult = new List<Identifier>();

                var isLast = i == graphPath.Length - 1;
                foreach (var identifier in previousResult)
                {
                    var relatedNodes = traverser.Traverse(currentGraphPathPart, identifier, context, scope);

                    if (isLast)
                    {
                        await foreach (var relatedNode in relatedNodes.ConfigureAwait(false))
                        {
                            finalOutput.OnNext(relatedNode);
                        }
                    }
                    else
                    {
                        await foreach (var relatedNode in relatedNodes.ConfigureAwait(false))
                        {
                            iterationResult.Add(relatedNode);
                        }
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
